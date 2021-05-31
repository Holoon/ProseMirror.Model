using System.Collections.Generic;
using Newtonsoft.Json;
using ProseMirror.Serializer;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ProseMirrorSerializerExtensions
    {
        public static JsonSerializerSettings UseProseMirror(this JsonSerializerSettings settings,
            bool indent = false, bool ignoreNullValue = false, params CustomNodeSelector[] customNodeSelectors)
        {
            if (settings == null)
                return null;

            settings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            settings.Formatting = indent ? Formatting.Indented : Formatting.None;
            settings.NullValueHandling = ignoreNullValue ? NullValueHandling.Ignore : NullValueHandling.Include;

            var customNodesConverter = new CustomNodesConverter();
            foreach (var customNodeSelector in customNodeSelectors)
                customNodesConverter.CustomNodeSelectors.Add(customNodeSelector?.NodeType, customNodeSelector?.NodeActivator);
            settings.Converters = new List<JsonConverter>
            {
                customNodesConverter
            };

            return settings;
        }
    }
}