using System;
using System.Collections.Generic;
using System.Linq;
using DDDSample1.Domain.Ligacoes;
using DDDSample1.Domain.Perfis;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Tags
{
    public class Tag : Entity<TagID>, IAggregateRoot
    {
        public TagID Id { get; set; }
        public Nome nome { get; set; }

        public virtual ICollection<Ligacao> ligacoes { get; set; }

        public virtual ICollection<Perfil> perfis { get; set; }

        public Tag()
        {

        }

        public Tag(Nome nome)
        {
            this.Id = new TagID(Guid.NewGuid());
            this.nome = nome;
        }

        public Tag(TagID id, Nome nome)
        {
            this.Id = id;
            this.nome = nome;
        }
    }
}
