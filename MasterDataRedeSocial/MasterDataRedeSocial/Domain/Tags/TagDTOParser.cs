
namespace DDDSample1.Domain.Tags
{
    public class TagDTOParser
    {
        public static TagDTO ParaDTO(Tag tag)
        {
            return new TagDTO(tag.Id.AsGuid(),tag.nome.nome);
        }
        
        public static Tag DeDTO(TagDTO dto)
        {
            return new Tag(new TagID(dto.Id), 
                new Nome(dto.nome));
        }
        
    }
}