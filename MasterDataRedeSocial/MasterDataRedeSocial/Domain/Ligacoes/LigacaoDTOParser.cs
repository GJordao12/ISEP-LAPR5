
using System;
using System.Collections.Generic;
using System.Linq;
using DDDSample1.Domain.Tags;


namespace DDDSample1.Domain.Ligacoes
{
    public class LigacaoDTOParser
    {
        public static LigacaoDTO ParaDTO(Ligacao ligacao)
        {
            ICollection<TagDTO> listaTagDTO = new List<TagDTO>();
            
            foreach (var tag in ligacao.ListaTags)
            {
                TagDTO tagDto = TagDTOParser.ParaDTO(tag);
                listaTagDTO.Add(tagDto);
            }
            
            return new LigacaoDTO(ligacao.Id.AsGuid(),
                ligacao.Principal,
                ligacao.Secundario,
                ligacao.ForcaLigacao.forcaLigacao,
                ligacao.ForçaRelacao.forcaRelacao,listaTagDTO
            );
        }
        public static Ligacao DeDTO(LigacaoDTO dto)
        {
            ICollection<Tag> listaTag = new List<Tag>();
            
            foreach (var tags in dto.listaTags)
            {
                Tag tag = TagDTOParser.DeDTO(tags);
                listaTag.Add(tag);
            }
            
            return new Ligacao(new LigacaoID(dto.Id), 
                dto.principal,
                dto.secundario,new ForçaLigacao(dto.forcaLigacao),
                new ForçaRelacao(dto.forçaRelacao),listaTag);
        }
        public static Ligacao DeDTOSemID(CreatingLigacaoDTO dto)
        {
            ICollection<Tag> listaTag = new List<Tag>();
            
            foreach (var tags in dto.listaTags)
            {
                Tag tag = TagDTOParser.DeDTO(tags);
                listaTag.Add(tag);
            }
            
            return new Ligacao(new LigacaoID(Guid.NewGuid()), dto.principal,
            dto.secundario,new ForçaLigacao(dto.forcaLigacao),
            new ForçaRelacao(dto.forçaRelacao),listaTag);
        }
        
    }
}