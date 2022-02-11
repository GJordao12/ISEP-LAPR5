using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain.Ligacoes;
using DDDSample1.Domain.Utilizador;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace DDDSample1.Infrastructure.Ligacao
{
    public class LigacaoRepository : BaseRepository<Domain.Ligacoes.Ligacao, LigacaoID>, ILigacaoRepository

    {
        public LigacaoRepository(DDDSample1DbContext context) : base(context.Ligacao)
        {
        }

        public async Task<List<Domain.Ligacoes.Ligacao>> GetLigacaoByUsers(UserId remetente, UserId destinatario)
        {
            return await _objs.Where(r => r.Principal.Equals(remetente) && r.Secundario.Equals(destinatario))
                .ToListAsync();
        }

        public async Task<List<Domain.Ligacoes.Ligacao>> GetLigacoesbyUser(UserId userId)
        {
            return await _objs.Where(r => r.Principal.Equals(userId)).ToListAsync();
        }

        public async Task<Dictionary<int, List<Domain.Ligacoes.Ligacao>>> GetByIdAndLevelAsync(UserId id, int niveis)
        {
            //dictionary onde vai ser armazenada a rede de conexões: key -> nível de ligação | value -> lista com as ligações do nível que está na "key"
            Dictionary<int, List<Domain.Ligacoes.Ligacao>> redeLigacoes = new Dictionary<int, List<Domain.Ligacoes.Ligacao>>();

            //incrementador dos niveis 
            int contadorNiveis = 1;

            //para funcionar como ponte de partida vai se buscar todos as ligações de nivel 1
            List<Domain.Ligacoes.Ligacao> primeiroNivel = await _objs.Where(r => r.Principal.Equals(id)).ToListAsync();
            
            //adiciona-se à rede de conexões o primeiro nível e a sua lista de ligações
            redeLigacoes.Add(contadorNiveis, primeiroNivel);

            //caso seja pedido só as ligações de nível 1 pode se retornar já a rede de conexões pois como ponto de partida essa informação já foi adicionada
            if (niveis != 1)
            {
                //neste for vai se buscar as ligações dos utilizadores dos níveis seguintes ao nível 1
                for (int i = 0; i < redeLigacoes.Keys.ToList().Count; i++)
                {
                    //lista auxiliar para se poder mexer no tamanho da lista ao mesmo tempo que se acrescenta informação
                    List<int> aux = redeLigacoes.Keys.ToList();
                    
                    //nivel atual
                    int nivel = aux[i];
                    
                    //caso não exista uma lista vazia de ligações no nível atual continuamos para o próximo nível, se não como esta se encontra vazia já não vai existir mais ligações no próximo nível
                    if (redeLigacoes[nivel].Count != 0)
                    {
                        //lista onde se vão guardar as ligações dos utilizadores do próximo nível
                        List<Domain.Ligacoes.Ligacao> proximoNivel = new List<Domain.Ligacoes.Ligacao>();

                        //para cada utilizador do nível anterior vai se buscar as ligações dele para guardar no próximo nivel
                        foreach (var ligacao in redeLigacoes[nivel])
                        {
                            //ligações do utilizadr em questão
                            List<Domain.Ligacoes.Ligacao> ligacoesDeUmUser = await _objs.Where(r =>
                                    r.Principal.Equals(ligacao.Secundario) && (!r.Secundario.Equals(ligacao.Principal)))
                                .ToListAsync();

                            //Verifica se exista a mesma ligação em dois sentidos 
                            List<Domain.Ligacoes.Ligacao> ligacoesDeUmUserVerificada = verficaLigacoesNosDoisSentidos(redeLigacoes, ligacoesDeUmUser);
                            
                            //adiciona à lista das ligações do próximo nível as ligações que encontrou do utilizador em questão
                            proximoNivel = proximoNivel.Union(ligacoesDeUmUserVerificada).ToList();
                        }

                        //incrementa o nivel de ligação
                        contadorNiveis++;

                        //Verifica se exista a mesma ligação em dois sentidos no mesmo nível
                        proximoNivel = verificaSeHaLigacoesNosDoisSentidosNoMesmoNivel(proximoNivel);
                        
                        //guarda as ligações do próximo nivel
                        redeLigacoes.Add(contadorNiveis, proximoNivel);
                        
                        //caso não sejam pedidos todos os níveis possíveis (niveis != 0) verifica se já chegou ao nível limite pretendido para parar o ciclo for principal
                        if (niveis != 0)
                        {
                            if (niveis == contadorNiveis)
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            /*
            Se for pedido todos os niveis (niveis == 0) ele vai tentar ir ao máximo possível de niveis por isso o último nível da rede vai ter um lista vazia que é preciso remover;
            Se pedir a rede até um nível que não existe (contadorNiveis < niveis) ele vai fazer apenas até ao limite, que é inferior ao nível pedido, por isso o último nível da rede vai ter uma lista vazia que é preciso remover;
            Se pedir até a um nivel que existe é preciso verificar se nesse nível existem ligações (redeLigacoes[niveis].Count == 0) e se não existir é preciso remover a lista vazia;
            */
            if (niveis == 0 || contadorNiveis < niveis || redeLigacoes[niveis].Count == 0)
            {
                redeLigacoes.Remove(contadorNiveis);
            }

            return redeLigacoes;
        }
        
        /* Verifica se exista a mesma ligação em dois sentidos
           Exemplo:
           A -> B
           B -> A
           Fica só A -> B ou B -> A
         */
        private List<Domain.Ligacoes.Ligacao> verficaLigacoesNosDoisSentidos(
            Dictionary<int, List<Domain.Ligacoes.Ligacao>> redeLigacoes, List<Domain.Ligacoes.Ligacao> ligacoesDeUmUser)
        {
            //flag que vai ser ativada caso encontre uma ligação no sentido contrário
            bool flag = false;

            //lista de ligações verificada, ou seja, que vai ter apenas a ligação num sentido
            List<Domain.Ligacoes.Ligacao> ligacoesDeUmUserVerificadas = new List<Domain.Ligacoes.Ligacao>();

            //Este for vai verificar se já existe uma ligação no sentido contrário e se existir vai ativar a flag para essa ligação não ser guardada na lista verificada
            foreach (var ligacao in ligacoesDeUmUser)
            {
                foreach (var key in redeLigacoes.Keys)
                {
                    foreach (var lig in redeLigacoes[key])
                    {
                        if (lig.Principal.Equals(ligacao.Secundario) && lig.Secundario.Equals(ligacao.Principal))
                        {
                            flag = true;
                            break;
                        }
                    }

                    if (flag)
                    {
                        break;
                    }
                }

                if (flag)
                {
                    flag = false;
                }
                else
                {
                    ligacoesDeUmUserVerificadas.Add(ligacao);
                }
            }
            return ligacoesDeUmUserVerificadas;
        }

        public List<Domain.Ligacoes.Ligacao> verificaSeHaLigacoesNosDoisSentidosNoMesmoNivel(List<Domain.Ligacoes.Ligacao> ligacoesDeUmNivel)
        {
            //verifica se há ligações nos dois sentidos no mesmo nivel
            for (int i = 0; i < ligacoesDeUmNivel.Count; i ++)
            {
                for (int j = i +1; j < ligacoesDeUmNivel.Count; j++)
                {
                    if (ligacoesDeUmNivel[i].Principal.Equals(ligacoesDeUmNivel[j].Secundario) && ligacoesDeUmNivel[i].Secundario.Equals(ligacoesDeUmNivel[j].Principal))
                    {
                        ligacoesDeUmNivel.Remove(ligacoesDeUmNivel[i]);
                        i--;
                    }
                }
            }

            return ligacoesDeUmNivel;
        }
    }
}