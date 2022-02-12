using DDDSample1.Domain.Utilizador;

namespace DDDSample1.Domain.PedidoIntroducao
{
    public class CreatingPedidoIntroducaoDTO
    {
        public string estado { get; set; }
        public UserDTO remetente { get; set; }
        public UserDTO intermediario { get; set; }
        public UserDTO destinatario { get; set; }
        public string apresentacao { get; set; }
        public string apresentacaoLigacao { get; set; }

        public CreatingPedidoIntroducaoDTO(string estado, UserDTO remetente, UserDTO intermediario, UserDTO destinatario, string apresentacao,string apresentacaoLigacao)
        {
            this.estado = estado;
            this.remetente = remetente;
            this.intermediario = intermediario;
            this.destinatario = destinatario;
            this.apresentacao = apresentacao;
            this.apresentacaoLigacao = apresentacaoLigacao;
        }
    }
}