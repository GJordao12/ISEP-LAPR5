using System;
using DDDSample1.Domain.Shared;
using Newtonsoft.Json;

namespace DDDSample1.Domain.EstadosDeHumor
{
    public class EstadoDeHumorId : EntityId
    {
        [JsonConstructor]
        public EstadoDeHumorId(Guid value) : base(value)
        {
        }

        public EstadoDeHumorId(string value) : base(value)
        {
        }

        override
            protected Object createFromString(String text)
        {
            return new Guid(text);
        }

        override
            public String AsString()
        {
            Guid obj = (Guid) base.ObjValue;
            return obj.ToString();
        }

        public Guid AsGuid()
        {
            return (Guid) base.ObjValue;
        }
    }
}