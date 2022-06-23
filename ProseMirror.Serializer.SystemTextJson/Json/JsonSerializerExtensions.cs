namespace ProseMirror.Serializer.SystemTextJson.Json;

public static class JsonSerializerExtensions
{
    public static JsonSerializerOptions UseProseMirror(this JsonSerializerOptions? options, bool indent = false,
        bool ignoreNullValue = false)
    {
        options ??= new JsonSerializerOptions();
        options.ReadCommentHandling = JsonCommentHandling.Skip;
        options.AllowTrailingCommas = true;
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.WriteIndented = indent;
        options.DefaultIgnoreCondition  = ignoreNullValue ? JsonIgnoreCondition.WhenWritingNull : JsonIgnoreCondition.Never;

        return options;
    }
    public static JsonSerializerOptions? UseProseMirror(this JsonSerializerOptions? options, bool indent = false,
        bool ignoreNullValue = false, params CustomNodeSelector[] customNodeSelectors)
    {
        options = options.UseProseMirror(indent, ignoreNullValue);

        var nodeConverter = new CustomNodesConverter(customNodeSelectors);
        options.Converters.Add(nodeConverter);

        return options;
    }
}