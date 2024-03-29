﻿using System.Collections.Generic;
using Newtonsoft.Json;
using ProseMirror.Serializer;
using ProseMirror.Serializer.JSon;

namespace Microsoft.Extensions.DependencyInjection;

public static class JSonSerializerExtensions
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
        settings.Converters ??= new List<JsonConverter>();
        if (settings.Converters.IsReadOnly)
            settings.Converters = new List<JsonConverter>(settings.Converters);
        settings.Converters.Add(customNodesConverter);

        return settings;
    }
}