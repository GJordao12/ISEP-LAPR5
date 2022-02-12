using System.Collections.Generic;
using DDDSample1.Domain.Tags;
using DDDSample1.Domain.Utilizador;

namespace DDDSample1.Domain.Ligacoes
{
    public class LigacaoRedeDeConexoesDTO
    {
        public LigacaoID Id { get; set; }
        
        public UserId principal { get; set; }

        public UserDTO secundario { get; set; }
        
        public virtual ICollection<TagDTO> listaTagsSecundario { get; set; }
        
        public int forcaLigacao { get; set; }
        
        public int forçaRelacao { get; set; }
        
        public virtual ICollection<TagDTO> listaTags { get; set; }

        public LigacaoRedeDeConexoesDTO(LigacaoID id, UserId principal, UserDTO secundario, ICollection<TagDTO> listaTagsSecundario, int forcaLigacao,int forcaRelacao,ICollection<TagDTO>listaTags)
        {
            this.Id = id;
            this.principal = principal;
            this.secundario = secundario;
            this.listaTagsSecundario = listaTagsSecundario;
            this.forcaLigacao = forcaLigacao;
            this.forçaRelacao = forcaRelacao;
            this.listaTags = listaTags;
        }
    }
}