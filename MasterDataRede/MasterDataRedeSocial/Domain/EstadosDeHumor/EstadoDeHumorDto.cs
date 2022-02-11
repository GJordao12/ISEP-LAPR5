using System;

namespace DDDSample1.Domain.EstadosDeHumor
{
    public class EstadoDeHumorDto
    {
        public Guid Id { get; }

        public string Description { get; }
        

        public EstadoDeHumorDto(Guid id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}