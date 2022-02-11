using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.EstadosDeHumor
{
    public class EstadoDeHumor : Entity<EstadoDeHumorId>, IAggregateRoot
    {
        public EstadoDeHumorId Id { get; }
        public string Description { get; private set; }
        

        public EstadoDeHumor()
        {
        }

        public EstadoDeHumor(string description)
        {
            Description = description;
        }

        public EstadoDeHumor(EstadoDeHumorId id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}