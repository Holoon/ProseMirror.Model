
namespace ProseMirror.Serializer.SystemTextJson.Json;

public class ProseMirrorJsonSerializer
{
    public static string Serialize<T>(T value, bool indent = false)
        => JsonSerializer.Serialize(value, new JsonSerializerOptions().UseProseMirror(indent, true));

    public static T? Deserialize<T>(string json, params CustomNodeSelector[] customNodeSelectors)
        => JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions().UseProseMirror(false, false, customNodeSelectors));
    

}