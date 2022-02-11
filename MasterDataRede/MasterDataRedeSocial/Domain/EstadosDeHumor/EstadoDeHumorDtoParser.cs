namespace DDDSample1.Domain.EstadosDeHumor
{
    public class EstadoDeHumorDtoParser
    {
        public static EstadoDeHumorDto ParaDTO(EstadoDeHumor estadoDeHumor)
        {
            return new EstadoDeHumorDto(estadoDeHumor.Id.AsGuid(), estadoDeHumor.Description);
        }
    }
}