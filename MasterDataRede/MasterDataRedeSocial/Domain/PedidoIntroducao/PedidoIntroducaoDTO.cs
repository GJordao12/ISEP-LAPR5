using System;
using DDDSample1.Domain.Utilizador;

namespace DDDSample1.Domain.PedidoIntroducao
{
    public class PedidoIntroducaoDTO
    {
        public Guid Id { get; set; }
        public string estado { get; set; }
        public UserDTO remetente { get; set; }
        public UserDTO intermediario { get; set; }
        public UserDTO destinatario { get; set; }
        public string apresentacao { get; set; }
        
        public string apresentacaoLigacao { get; set; }

        public PedidoIntroducaoDTO(Guid id, string estado, UserDTO remetente, UserDTO intermediario, UserDTO destinatario, string apresentacao,string apresentacaoLigacao)
        {
            this.Id = id;
            this.estado = estado;
            this.remetente = remetente;
            this.intermediario = intermediario;
            this.destinatario = destinatario;
            this.apresentacao = apresentacao;
            this.apresentacaoLigacao = apresentacaoLigacao;
        }

        public override string ToString()
        {
            return (" estado:" + estado + " remetente:" + remetente +" intermediario:"+intermediario+ " destinatario:" + destinatario +
                    " texto:" + apresentacao);
        }

    }
}