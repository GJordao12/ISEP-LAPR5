using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Castle.Core.Internal;
using DDDSample1.Domain.Ligacoes;
using DDDSample1.Domain.PedidoLigacao;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Utilizador;
using Microsoft.AspNetCore.Mvc;

namespace DDDSample1.Domain.PedidoIntroducao
{
    public class PedidoIntroducaoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPedidoIntroducaoRepository _repo;
        private readonly IPedidoLigacaoRepository _repoPedidoLigacao;
        private readonly IUserRepository _repoUser;
        private readonly ILigacaoRepository _repoLigacao;
        
        public PedidoIntroducaoService(IUnitOfWork unitOfWork, IPedidoIntroducaoRepository repo,IUserRepository repoUser, ILigacaoRepository repoLigacao,IPedidoLigacaoRepository repoPedidoLigacao)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._repoUser = repoUser;
            this._repoLigacao = repoLigacao;
            this._repoPedidoLigacao = repoPedidoLigacao;
        }

        public async Task<List<PedidoIntroducaoDTO>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();

            List<PedidoIntroducaoDTO> listDto = list.ConvertAll<PedidoIntroducaoDTO>(pedido => PedidoIntroducaoDTOParser.ParaDTO(pedido));
            
            return listDto;
        }

        public async Task<ActionResult<PedidoIntroducaoDTO>> GetById(PedidoIntroducaoId pedidoIntroducaoId)
        {
            var pedido = await this._repo.GetByIdAsync(pedidoIntroducaoId);

            if (pedido == null)
            {
                return null;
            }
            
            return PedidoIntroducaoDTOParser.ParaDTO(pedido);
        }
        
        public async Task<List<UserDTO>> GetPossibleIntermediaryUsers(UserId x, UserId z)
        {
            
            var conexoesX = await this._repoLigacao.GetLigacoesbyUser(x);
            var conexoesZ = await this._repoLigacao.GetLigacoesbyUser(z);
            var result = new List<UserId>();

            foreach (Ligacao l1 in conexoesZ)
            {
                foreach (Ligacao l2 in conexoesX)
                {
                    if ((l2.Secundario.Value.Equals(l1.Secundario.Value) || l2.Secundario.Value.Equals(l1.Principal.Value) || l2.Principal.Value.Equals(l1.Secundario.Value) || l2.Principal.Value.Equals(l1.Principal.Value)) 
                        && (!result.Contains(l1.Secundario) || !result.Contains(l1.Principal)))
                    {
                        if (!l2.Principal.Value.Equals(x.Value))
                        {
                            result.Add(l2.Principal);
                        }
                        else
                        {
                            result.Add(l2.Secundario);
                        }
                    }
                }
            }
            
            List<UserDTO> listDTO = result.ConvertAll(user => UserDTOParser.ParaDTO(this._repoUser.GetByIdAsync(user).Result));
            
            return listDTO;
        }
        public async Task<List<PedidoIntroducaoDTO>> GetByEstadoPendente(String userId)
        {
            
            var listPedidos = await this._repo.GetByEstadoPendenteAsync(userId);

            if (listPedidos.IsNullOrEmpty())
            {
                return null;
            }
            
            List<PedidoIntroducaoDTO> listDTO = listPedidos.ConvertAll(pedido => PedidoIntroducaoDTOParser.ParaDTO(pedido));
            
            return listDTO;
        }

        public async Task<PedidoIntroducaoDTO> AddAsync(PedidoIntroducaoPutDTO dto)
        {
            //verificar se os utilizadores se encontram na base de dados pelo username.
            if (_repoUser.GetUserByUsername(dto.userNameRem).Result == null ||
                _repoUser.GetUserByUsername(dto.userNameInt).Result == null ||
                _repoUser.GetUserByUsername(dto.userNameDest).Result == null)
            {
                return null;
            }
            
            //Criados usersDTOS
            UserDTO userDTORem = UserDTOParser.ParaDTO(_repoUser.GetUserByUsername(dto.userNameRem).Result);
            UserDTO userDTOInt = UserDTOParser.ParaDTO(_repoUser.GetUserByUsername(dto.userNameInt).Result);
            UserDTO userDTODest = UserDTOParser.ParaDTO(_repoUser.GetUserByUsername(dto.userNameDest).Result);
            
            //verificar se o User Remetente tem ligacao com o Intermediario e não com o Destinatario
            //verificar se o intermediario tem ligacao com o destinatario

            List<Ligacao> list = _repoLigacao.GetLigacaoByUsers(new UserId(userDTORem.Id), new UserId(userDTOInt.Id))
                .Result;
            List<Ligacao> list2 = _repoLigacao.GetLigacaoByUsers(new UserId(userDTOInt.Id), new UserId(userDTODest.Id))
                .Result;
            List<Ligacao> list3 = _repoLigacao.GetLigacaoByUsers(new UserId(userDTORem.Id), new UserId(userDTODest.Id))
                .Result;

            if (!list.IsNullOrEmpty()  &&
                !list2.IsNullOrEmpty()&&
                list3.IsNullOrEmpty())
            {
                string estado = "PENDENTE";
                
                var pedido = new PedidoIntroducao(_repoUser.GetUserByUsername(dto.userNameRem).Result,_repoUser.GetUserByUsername(dto.userNameInt).Result,_repoUser.GetUserByUsername(dto.userNameDest).Result,new Apresentacao(dto.apresentacao),new Apresentacao(dto.apresentacaoLigacao));

                await this._repo.AddAsync(pedido);

                await this._unitOfWork.CommitAsync();

                return PedidoIntroducaoDTOParser.ParaDTO(pedido);
            }

            return null;
        }

        public async Task<object> UpdateAsync(Guid id,EstadoPedidoIntroducao estado)
        {
            var pedido = await this._repo.GetByIdAsync(new PedidoIntroducaoId(id));

            if (pedido == null)
                return null;

            pedido.estado = estado;
            
            
            if (pedido.estado.ToString().Equals("APROVADO"))
            {
                PedidoLigacao.PedidoLigacao pedidoLigacao = new PedidoLigacao.PedidoLigacao(pedido.remetente.Id,pedido.destinatario.Id, new Estado("Pendente"),new Texto(pedido.apresentacaoLigacao.apresentacaoPedido));
                await this._repoPedidoLigacao.AddAsync(pedidoLigacao);
                
            }
            await this._unitOfWork.CommitAsync();
            return PedidoIntroducaoDTOParser.ParaDTO(pedido);
        }
    }
}