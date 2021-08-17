using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;

namespace ProseMirror.Serializer.JSon
{
    public class JSonSerializer
    {
        public static string Serialize<T>(T value, bool indent = false) 
            => JsonConvert.SerializeObject(value, new JsonSerializerSettings().UseProseMirror(indent, true));
        public static T Deserialize<T>(string jSon, params CustomNodeSelector[] customNodeSelectors) 
            => JsonConvert.DeserializeObject<T>(jSon, new JsonSerializerSettings().UseProseMirror(false, false, customNodeSelectors));
    }
}
