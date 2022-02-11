using System;
using System.Collections.Generic;
using DDDSample1.Domain.EstadosDeHumor;
using DDDSample1.Domain.PedidoIntroducao;
using DDDSample1.Domain.Tags;
using DDDSample1.Domain.Utilizador;

namespace DDDSample1.Domain.Perfis
{
    public class PerfilDtoParser
    {
        public static PerfilDto ParaDTO(Perfil perfil)
        {
            ICollection<TagDTO> listaTagDTO = new List<TagDTO>();
            
            foreach (var tag in perfil.ListaTags)
            {
                TagDTO tagDto = TagDTOParser.ParaDTO(tag);
                listaTagDTO.Add(tagDto);
            }
            
            return new PerfilDto(perfil.Id.AsGuid(),
                perfil.UserId,
                perfil.estadoDeHumorId,
                perfil.perfilDataDeNascimento,
                perfil.perfilFacebook,
                perfil.perfilLinkedin,
                perfil.perfilNome,
                perfil.perfilNTelefone,
                listaTagDTO);
        }

         public static Perfil DeDTO(PerfilDto dto)
         {
             
             ICollection<Tag> listaTag = new List<Tag>();
            
             foreach (var tags in dto.ListaTags)
             {
                 Tag tag = TagDTOParser.DeDTO(tags);
                 listaTag.Add(tag);
             }
             
             return new Perfil(new PerfilId(dto.Id),
                 new UserId(dto.userId.AsGuid()),
                 new EstadoDeHumorId(dto.estadoDeHumorId.AsString()),
                 dto.perfilDataDeNascimento,
                 dto.perfilFacebook,
                 dto.perfilLinkedin,
                 dto.perfilNome,
                 dto.perfilNTelefone,
                 listaTag
             );
        
        
         }
        
         public static Perfil DeDTOSemID(CreatingPerfilDTO dto)
         {
             return new Perfil(new PerfilId(Guid.NewGuid()),
                 dto.userId,
                 dto.estadoDeHumorId, 
                 new PerfilDataDeNascimento(dto.perfilDataDeNascimento),
                 new PerfilFacebook(dto.perfilFacebook),
                 new PerfilLinkedin(dto.perfilLinkedin),
                 new PerfilNome(dto.perfilNome),
                 new PerfilNTelefone(dto.perfilNTelefone),
                 dto.ListaTags
                 
                 
                 );
         }
    }
}