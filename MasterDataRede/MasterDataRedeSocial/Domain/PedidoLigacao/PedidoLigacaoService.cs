using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Castle.Core.Internal;
using DDDSample1.Domain.Ligacoes;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Tags;
using DDDSample1.Domain.Utilizador;
using Microsoft.AspNetCore.Mvc;

namespace DDDSample1.Domain.PedidoLigacao
{
    public class PedidoLigacaoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPedidoLigacaoRepository _repo;
        private readonly ILigacaoRepository _repoLigacao;

        public PedidoLigacaoService(IUnitOfWork unitOfWork, IPedidoLigacaoRepository repo, ILigacaoRepository repoLigacao)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._repoLigacao = repoLigacao;
        }

        public async Task<List<PedidoLigacaoDTO>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();

            List<PedidoLigacaoDTO> listDto = list.ConvertAll<PedidoLigacaoDTO>(pedido => PedidoLigacaoDTOParser.ParaDTO(pedido));

            return listDto;
        }
        
        public async Task<List<PedidoLigacaoDTO>> GetByEstadoPendente(string userId)
        {
            
            var listPedidos = await this._repo.GetByEstadoPendenteAsync(userId);

            if (listPedidos.IsNullOrEmpty())
            {
                return null;
            }
            
            List<PedidoLigacaoDTO> listDTO = listPedidos.ConvertAll(pedido => PedidoLigacaoDTOParser.ParaDTO(pedido));
            
            return listDTO;
        }

        public async Task<object> UpdatePedidoLigacaoAsync(Guid id, Estado decisao)
        {
            var pedido = await this._repo.GetByIdAsync(new PedidoLigacaoId(id));

            if (pedido == null)
                return null;

            pedido.estado = decisao;


            if (pedido.estado.estado.ToLower().Equals("aceite"))
            {
                Ligacao novaLigacao1 = new Ligacao(pedido.remetente,pedido.destinatario,new ForçaLigacao(0),new List<Tag>());
                Ligacao novaLigacao2 = new Ligacao(pedido.destinatario,pedido.remetente,new ForçaLigacao(0),new List<Tag>());
                await this._repoLigacao.AddAsync(novaLigacao1);
                await this._repoLigacao.AddAsync(novaLigacao2);
            }
            
            await this._unitOfWork.CommitAsync();

            return PedidoLigacaoDTOParser.ParaDTO(pedido);
        }

        public async Task<PedidoLigacaoDTO> AddAsync(CreatingPedidoLigacaoDTO dto)
        {
            List<PedidoLigacao>list1=_repo.GetPedidoLigacaoByUsers(new UserId(dto.remetente), new UserId(dto.destinatario)).Result;
            List<PedidoLigacao>list2=_repo.GetPedidoLigacaoByUsers(new UserId(dto.destinatario), new UserId(dto.remetente)).Result;
            
            if (list1.IsNullOrEmpty() &&
               list2.IsNullOrEmpty())
            {
                var pedido = PedidoLigacaoDTOParser.DeDTOSemID(dto);

                await this._repo.AddAsync(pedido);

                await this._unitOfWork.CommitAsync();

                return PedidoLigacaoDTOParser.ParaDTOSemPassUser(pedido);
            }
            return null;
        }
        public async Task<ActionResult<PedidoLigacaoDTO>> GetById(PedidoLigacaoId pedidoLigacaoId)
        {
            var pedido = await this._repo.GetByIdAsync(pedidoLigacaoId);

            if (pedido == null)
            {
                return null;
            }
            
            return PedidoLigacaoDTOParser.ParaDTO(pedido);
        }
    }
}