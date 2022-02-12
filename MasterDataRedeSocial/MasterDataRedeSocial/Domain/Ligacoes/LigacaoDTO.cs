using System;
using System.Collections.Generic;
using DDDSample1.Domain.Tags;
using DDDSample1.Domain.Utilizador;

namespace DDDSample1.Domain.Ligacoes
{
    public class LigacaoDTO
    {
        public Guid Id { get; set; }
        
        public UserId principal { get; set; }
        
        public UserId secundario { get; set; }
        
        public int forcaLigacao { get; set; }
        
        public int forçaRelacao { get; set; }
        
        public virtual ICollection<TagDTO> listaTags { get; set; }

        public LigacaoDTO(Guid id, UserId principal, UserId secundario, int forcaLigacao,int forcaRelacao,ICollection<TagDTO>listaTags)
        {
            this.Id = id;
            this.principal = principal;
            this.secundario = secundario;
            this.forcaLigacao = forcaLigacao;
            this.forçaRelacao = forcaRelacao;
            this.listaTags = listaTags;
        }
        

    }
}