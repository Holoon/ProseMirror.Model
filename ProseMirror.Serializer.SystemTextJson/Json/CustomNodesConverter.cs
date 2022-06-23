
using System.Reflection;

namespace ProseMirror.Serializer.SystemTextJson.Json;

public class CustomNodesConverter : JsonConverter<Node>
{
    public readonly Dictionary<string, CustomNodeSelector> SelectorsMap;

    private readonly CustomNodeSelector[] _selectors;
    
    public CustomNodesConverter(CustomNodeSelector[] selectors)
    {
        _selectors = selectors;
        SelectorsMap = selectors.ToDictionary(x => x.NodeType, y => y);

    }
    
    public override bool CanConvert(Type objectType) => typeof(Node).IsAssignableFrom(objectType) && objectType != typeof(StandardNode);
    
    public override Node? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (JsonDocument.TryParseValue(ref reader, out var doc))
        {
            if (doc.RootElement.TryGetProperty("type", out var type))
            {
                var typeValue = type.GetString();
                var rootElement = doc.RootElement.GetRawText();

                if (Node.DefaultNodeType.Contains(typeValue) || !SelectorsMap.ContainsKey(typeValue))
                {
                    return JsonSerializer.Deserialize<StandardNode>(rootElement, options);
                }

                var selector = SelectorsMap[typeValue];
                var result = JsonSerializer.Deserialize(rootElement, selector.NodeClassType, new JsonSerializerOptions().UseProseMirror(false, false));
                return result as CustomNode;
            }

            throw new JsonException("Failed to extract type property, it might be missing?");
        }

        throw new JsonException("Failed to parse JsonDocument");
    }

    public override void Write(Utf8JsonWriter writer, Node value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}