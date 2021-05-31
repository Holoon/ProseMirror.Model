# ProseMirror

![logo](https://avatars.githubusercontent.com/u/13659461)

## ProseMirror.Model
C# definitions of ProseMirror's content model, the data structures used to represent and work with documents.

### Installation 

```
Install-Package ProseMirror.Model
```

Nuget package: https://www.nuget.org/packages/ProseMirror.Model/

### Usage
This library contains only definition files, with no other dependencies. See *ProseMirror.Serializer* below for an example of deserialization. 

## ProseMirror.Serializer 
Tools for serialize or deserialize ProseMirror's content model in C#.

### Installation 

```
Install-Package ProseMirror.Serializer
```

Nuget package: https://www.nuget.org/packages/ProseMirror.Serializer/

### Usage

#### Simple use case: 

```c#
var json = "{ \"type\": \"doc\", \"content\": [ { \"type\": \"paragraph\", \"content\": " +
    "[ { \"type\": \"text\", \"text\": \"Hello my first test \" }, ] } ] }";
var proseMirrorNode = ProseMirrorSerializer.Deserialize<Node>(json);
var result = ProseMirrorSerializer.Serialize(proseMirrorNode);
```

#### Use case with custom nodes:

*ProseMirror* allow to expand his model with custom nodes. For example, the `reference` node bellow is not native to *ProseMirror*:
```json
{
    "type": "doc",
    "content": [
    {
        "type": "paragraph",
        "content": [
        {
            "type": "text",
            "text": "Hello my first test "
        },
        {
            "type": "reference",
            "attrs": {
                "decorationChar": "@",
                "referenceDocUid": "m71yUUjgsGKSaob90rp-2",
                "text": "@Human.ArthurDent",
                "assignmentId": 1
            }
        }]
    }]
}
```

This kind of nodes can be deserialized by adding one (or more) `CustomNodeSelector`:

```c#
public class ReferenceAttributes 
{
    public string DecorationChar { get; set; }
    public string ReferenceDocUid { get; set; }
    public string Text { get; set; }
    public int? AssignmentId { get; set; }
}
public class ReferenceNode : CustomNode
{
    public ReferenceAttributes Attrs { get; set; }
}

// ...
var proseMirrorNode = ProseMirrorSerializer.Deserialize<Node>(json, 
    new CustomNodeSelector<ReferenceNode>("reference"));
```

If your custom node implementation need a constructor or need an initialization, you can use the `CustomNodeSelector` overload:

```c#
public class ReferenceNode 
{
    public ReferenceNode(int myParam)
    {
        // ...
    }
}

// ...
var proseMirrorNode = ProseMirrorSerializer.Deserialize<Node>(json, 
    new CustomNodeSelector("reference", () => new ReferenceNode(42)));
```

#### MVC Core Integration or SignalR Integration: 

```c#
// MVC Core:
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers(opt =>
    {
        // ...
    })
    .AddNewtonsoftJson(o =>
    {
        o.SerializerSettings.UseProseMirror();
    })
    .AddControllersAsServices();
}

// SignalR:
public void ConfigureServices(IServiceCollection services)
{
    services.AddSignalR(options =>
    {
        // ...
    })
    .AddHubOptions<MyHub>(options =>
    {
        // ...
    })
    .AddNewtonsoftJsonProtocol(opt =>
    {
        opt.PayloadSerializerSettings.UseProseMirror();
    });
}
```

## Quick Links
Website: https://prosemirror.net/

## Contributing
Pull requests are welcome. If you'd like to contribute, please fork the repository and use a feature branch. Please respect existing style in code.

**Important notes for pull requests** : This project use *GitFlow*, please branch from `develop`, not `master`.
