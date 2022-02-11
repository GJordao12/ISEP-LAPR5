using System;
using DDDSample1.Domain.Shared;
using Newtonsoft.Json;

namespace DDDSample1.Domain.Ligacoes
{
    public class LigacaoID : EntityId
    {
        [JsonConstructor]
        
        public LigacaoID(Guid value) : base(value)
        {
            
        }

        public LigacaoID(String value) : base(value)
        {
            
        }

        override protected Object createFromString(String id)
        {
            return new Guid(id);
        }
        public override string AsString()
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