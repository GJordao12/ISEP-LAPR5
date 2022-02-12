using System;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Utilizador;

namespace DDDSample1.Domain.PedidoLigacao
{
    public class PedidoLigacao : Entity<PedidoLigacaoId> , IAggregateRoot
    {
        public PedidoLigacaoId Id { get; set; }
        public UserId remetente { get;   set; }
        public UserId destinatario { get;  set; }
        public Estado estado { get;  set; }
        public Texto texto { get;  set; }
        
        public PedidoLigacao(UserId remetente, UserId destinatario, Estado estado, Texto texto)
        {
            this.Id = new PedidoLigacaoId(Guid.NewGuid());
            this.remetente = remetente;
            this.destinatario = destinatario;
            this.estado = estado;
            this.texto = texto;
        }
        
        public PedidoLigacao(UserId remetente, UserId destinatario, Texto texto)
        {
            this.Id = new PedidoLigacaoId(Guid.NewGuid());
            this.remetente = remetente;
            this.destinatario = destinatario;
            this.estado = new Estado("Pendente");
            this.texto = texto;
        }
        public PedidoLigacao()
        {
            this.Id = new PedidoLigacaoId(Guid.NewGuid());
            this.estado = new Estado().Pendente();
            this.texto = new Texto("");
        }

        public PedidoLigacao(PedidoLigacaoId id, UserId userRem, UserId userDest,
            Texto texto)
        {
            this.Id = id;
            this.remetente = userRem;
            this.destinatario = userDest;
            this.texto = texto;
            this.estado = new Estado().Pendente();
        }
    }
}