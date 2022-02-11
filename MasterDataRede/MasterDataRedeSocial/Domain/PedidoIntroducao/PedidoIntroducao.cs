using System;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Utilizador;
using Microsoft.EntityFrameworkCore;

namespace DDDSample1.Domain.PedidoIntroducao
{
    public class PedidoIntroducao : Entity<PedidoIntroducaoId> , IAggregateRoot
    {   
        public virtual PedidoIntroducaoId Id { get; set; }
        public virtual EstadoPedidoIntroducao estado { get; set; }
        public virtual User remetente { get; set; }
        public virtual User intermediario { get; set; }
        public virtual User destinatario { get; set; }
        public virtual Apresentacao apresentacao { get; set; }
        
        public virtual Apresentacao apresentacaoLigacao { get; set; }
        
        public PedidoIntroducao()
        {
            this.Id = new PedidoIntroducaoId(Guid.NewGuid());
            this.estado = new EstadoPedidoIntroducao().Pendente();
            this.apresentacao = new Apresentacao("");
        }
        
        public PedidoIntroducao( User userRem, User userInt, User userDest,
            Apresentacao apresentacao,Apresentacao apresentacaoLigacao)
        {
            this.Id = new PedidoIntroducaoId(Guid.NewGuid());
            this.remetente = userRem;
            this.intermediario = userInt;
            this.destinatario = userDest;
            this.apresentacao = apresentacao;
            this.apresentacaoLigacao = apresentacaoLigacao;
            this.estado = new EstadoPedidoIntroducao().Pendente();
        }
        
        public PedidoIntroducao(PedidoIntroducaoId id, User userRem, User userInt, User userDest,
            Apresentacao apresentacao,Apresentacao apresentacaoLigacao)
        {
            this.Id = id;
            this.remetente = userRem;
            this.intermediario = userInt;
            this.destinatario = userDest;
            this.apresentacao = apresentacao;
            this.apresentacaoLigacao = apresentacaoLigacao;
            this.estado = new EstadoPedidoIntroducao().Pendente();
        }
        
        public PedidoIntroducao(PedidoIntroducaoId id,EstadoPedidoIntroducao estado, User userRem, User userInt, User userDest,
            Apresentacao apresentacao,Apresentacao apresentacaoLigacao)
        {
            this.Id = id;
            this.remetente = userRem;
            this.intermediario = userInt;
            this.destinatario = userDest;
            this.apresentacao = apresentacao;
            this.apresentacaoLigacao = apresentacaoLigacao;
            this.estado = estado;
        }
    }
}