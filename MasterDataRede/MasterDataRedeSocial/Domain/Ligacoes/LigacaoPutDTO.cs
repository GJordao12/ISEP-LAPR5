using System.Collections.Generic;
using DDDSample1.Domain.Tags;

namespace DDDSample1.Domain.Ligacoes
{
    public class LigacaoPutDTO
    {
        public ForçaLigacao forcaLigacao { get; set; }
        public List<TagDTO> listaTags { get; set; }
        
        public LigacaoPutDTO(ForçaLigacao fl,List<TagDTO> listaDtOs)
        {
            this.forcaLigacao = fl;
            listaTags = listaDtOs;
        }

        public LigacaoPutDTO()
        {
            
        }
    }
}