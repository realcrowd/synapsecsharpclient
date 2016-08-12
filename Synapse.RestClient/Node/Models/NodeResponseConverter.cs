using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Synapse.RestClient.Node
{
    public class NodeResponseConverter : CustomCreationConverter<NodeResponse>
    {
        public override NodeResponse Create(Type objectType)
        {
            throw new NotImplementedException();
        }

        public NodeResponse Create(Type objectType, JObject jObject)
        {
            var type = (string)jObject.Property("type");
            switch (type)
            {
                case "ACH-US":
                    return new ACHNodeResponse();
                case "WIRE-US":
                    return new WireUSNodeResponse();
            }

            throw new NotSupportedException(String.Format("The given node type {0} is not supported!", type));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Load JObject from stream
            var jObject = JObject.Load(reader);

            // Create target object based on JObject
            var target = Create(objectType, jObject);

            // Populate the object properties
            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }
    }
}
