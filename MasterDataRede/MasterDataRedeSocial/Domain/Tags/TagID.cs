using System;
using DDDSample1.Domain.Shared;
using Newtonsoft.Json;

namespace DDDSample1.Domain.Tags
{
    public class TagID : EntityId
    {
        [JsonConstructor]
        
        public TagID(Guid value) : base(value)
        {
            
        }

        public TagID(String value) : base(value)
        {
            
        }

        protected override object createFromString(string id)
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