
using System;
using System.Text.Json.Serialization;

namespace DDDSample1.Domain.Tags
{
    public class TagDTO
    {
        public Guid Id { get; set; }
        
        public string nome { get; set; }

        public TagDTO(Guid id, string nome)
        {
            this.Id = id;
            this.nome = nome;
        }
        
        [JsonConstructor]
        public TagDTO(string nome)
        {
            this.nome = nome;
        }

    }
}