using System.Collections.Generic;
using DDDSample1.Domain.Tags;
using DDDSample1.Domain.Utilizador;

namespace DDDSample1.Domain.Ligacoes
{
    public class CreatingLigacaoDTO
    {
        public UserId principal { get; set; }
        
        public UserId secundario { get; set; }
        
        public int forcaLigacao { get; set; }
        
        public int forçaRelacao { get; set; }
        
        public ICollection<TagDTO> listaTags { get; set; }

        public CreatingLigacaoDTO( UserId principal, UserId secundario, int forcaLigacao,int forcaRelacao,ICollection<TagDTO>listaTags)
        {
            this.principal = principal;
            this.secundario = secundario;
            this.forcaLigacao = forcaLigacao;
            this.forçaRelacao = forcaRelacao;
            this.listaTags = listaTags;
        }

    }
    }