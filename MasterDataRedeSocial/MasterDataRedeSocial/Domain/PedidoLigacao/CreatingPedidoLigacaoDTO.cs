namespace DDDSample1.Domain.PedidoLigacao
{
    public class CreatingPedidoLigacaoDTO
    {
        public string remetente { get; set; }
        public string destinatario { get; set; }
        public string texto { get; set; }

        public CreatingPedidoLigacaoDTO( string remetente, string destinatario, string texto)
        {
            this.remetente = remetente;
            this.destinatario = destinatario;
            this.texto = texto;
        }
    }
}