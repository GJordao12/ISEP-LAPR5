
using DDDSample1.Domain.Utilizador;

namespace DDDSample1.Domain.PedidoIntroducao
{
    public class PedidoIntroducaoDTOParser
    {
        public static PedidoIntroducaoDTO ParaDTO(PedidoIntroducao pedido)
        {
            return new PedidoIntroducaoDTO(pedido.Id.AsGuid(),
                pedido.estado.ToString(),
                UserDTOParser.ParaDTO(pedido.remetente),
                UserDTOParser.ParaDTO(pedido.intermediario),
                UserDTOParser.ParaDTO(pedido.destinatario),pedido.apresentacao.apresentacaoPedido,pedido.apresentacaoLigacao.apresentacaoPedido);
        }
        
        public static PedidoIntroducao DeDTO(PedidoIntroducaoDTO dto)
        {
            return new PedidoIntroducao(new PedidoIntroducaoId(dto.Id), 
                UserDTOParser.DeDTO(dto.remetente), 
                UserDTOParser.DeDTO(dto.intermediario), 
                UserDTOParser.DeDTO(dto.destinatario),new Apresentacao(dto.apresentacao),new Apresentacao(dto.apresentacaoLigacao));
        }

        public static PedidoIntroducao DeDTOSemID(CreatingPedidoIntroducaoDTO dto)
        {
            return new PedidoIntroducao(UserDTOParser.DeDTOComID(dto.remetente), 
                UserDTOParser.DeDTOComID(dto.intermediario), 
                UserDTOParser.DeDTOComID(dto.destinatario),
                new Apresentacao(dto.apresentacao),new Apresentacao(dto.apresentacaoLigacao));
        }
    }
}