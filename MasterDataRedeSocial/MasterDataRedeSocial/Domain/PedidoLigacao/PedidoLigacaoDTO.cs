using System;

namespace DDDSample1.Domain.PedidoLigacao
{
    public class PedidoLigacaoDTO
    {
        public Guid Id { get; set; }

        public string estado { get; set; }

        public string remetente { get; set; }
        public string destinatario { get; set; }

        public string texto { get; set; }

        public PedidoLigacaoDTO(Guid id, string estado, string rem, string dest, string texto)
        {
            this.Id = id;
            this.estado = estado;
            this.remetente = rem;
            this.destinatario = dest;
            this.texto = texto;
        }

        public override string ToString()
        {
            return (" estado:" + estado + " remetente:" + remetente + " destinatario:" + destinatario +
                    " texto:" + texto);
        }
    }
}