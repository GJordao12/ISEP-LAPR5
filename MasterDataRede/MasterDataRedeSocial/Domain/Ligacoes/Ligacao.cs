using System;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Tags;
using DDDSample1.Domain.Utilizador;

namespace DDDSample1.Domain.Ligacoes
{
    public class Ligacao : Entity<LigacaoID>, IAggregateRoot
    {
        public virtual UserId Principal { get; set; }
        
        public virtual UserId Secundario { get; set; }
        
        public ForçaLigacao ForcaLigacao { get; set; }
        
        public ForçaRelacao ForçaRelacao { get; set; }
        
        public virtual ICollection<Tag> ListaTags { get; set; }
        
        public Ligacao()
        {
            
        }

        public Ligacao(UserId principal, UserId secundario, ForçaLigacao fl, ICollection<Tag> lista)
        {
            this.Id = new LigacaoID(Guid.NewGuid());
            this.Principal = principal;
            this.Secundario = secundario;
            this.ForcaLigacao = fl;
            this.ForçaRelacao = new ForçaRelacao();
            this.ListaTags = lista;
        }

        public Ligacao(LigacaoID id, UserId principal, UserId secundario, ForçaLigacao fl, ForçaRelacao fr,
            ICollection<Tag> lista)
        {
            this.Id = id;
            this.Principal = principal;
            this.Secundario = secundario;
            this.ForcaLigacao = fl;
            this.ForçaRelacao = fr;
            this.ListaTags = lista;
        }
        public Ligacao( UserId principal, UserId secundario, ForçaLigacao fl, ForçaRelacao fr,
            ICollection<Tag> lista)
        {
            this.Principal = principal;
            this.Secundario = secundario;
            this.ForcaLigacao = fl;
            this.ForçaRelacao = fr;
            this.ListaTags = lista;
        }

        public void UpdateForcaLigacao(ForçaLigacao forca)
        {
            this.ForcaLigacao = forca;
        }
        public void UpdateForcaRelacao(ForçaRelacao forca)
        {
            this.ForçaRelacao = forca;
        }

        public void UpdateListaTags(ICollection<Tag> listaTags)
        { 
            foreach (var tag in listaTags)
            {
                if (!this.ListaTags.Contains(tag))
                { 
                    this.ListaTags.Add(tag);    
                }
            }
            
            List<Tag> tagList = new List<Tag>(ListaTags);
            
            for (int i=0;i<tagList.Count;i++)
            {
                if (!listaTags.Contains(tagList[i]))
                {
                    tagList.Remove(tagList[i]);
                    i--;
                }
            }
            
            ICollection<Tag> tagsList = tagList;
            ListaTags = tagsList;
        }
        
    }
}