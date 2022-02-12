
using DDDSample1.Domain.Utilizador;

namespace DDDSample1.Domain.PedidoLigacao
{
    public class PedidoLigacaoDTOParser
    {
        public static PedidoLigacaoDTO ParaDTO(PedidoLigacao pedido)
        {
            return new PedidoLigacaoDTO(pedido.Id.AsGuid(),
                pedido.estado.estado,
                pedido.remetente.Value,
                pedido.destinatario.Value,
                pedido.texto.texto);
        }
        
        public static PedidoLigacao DeDTO(PedidoLigacaoDTO dto)
        {
            return new PedidoLigacao(new PedidoLigacaoId(dto.Id), 
                new UserId(dto.remetente),
                new UserId(dto.destinatario),new Texto(dto.texto));
        }
        
        public static PedidoLigacao DeDTOSemID(CreatingPedidoLigacaoDTO dto)
        {
            return new PedidoLigacao(new UserId(dto.remetente),
                new UserId(dto.destinatario),
                new Texto(dto.texto));
        }
        
        public static PedidoLigacaoDTO ParaDTOSemPassUser(PedidoLigacao pedido)
        {
            return new PedidoLigacaoDTO(pedido.Id.AsGuid(),
                pedido.estado.estado,
                pedido.remetente.Value,
                pedido.destinatario.Value,
                pedido.texto.texto);
        }
        
    }
}