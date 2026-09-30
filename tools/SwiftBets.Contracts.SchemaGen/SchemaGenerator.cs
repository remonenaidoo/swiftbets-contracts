using NJsonSchema;
using NJsonSchema.CodeGeneration.TypeScript;
using NJsonSchema.Generation;
using SwiftBets.Contracts.Serialization;

namespace SwiftBets.Contracts.SchemaGen;

public static class SchemaGenerator
{
    public static IReadOnlyDictionary<string, string> GenerateSchemas() =>
        ContractCatalog.Contracts.ToDictionary(c => $"{c.Name}.schema.json", c => Schema(c.Type).ToJson().ReplaceLineEndings("\n") + "\n");

    public static string GenerateTypeScript()
    {
        var root = new JsonSchema { Type = JsonObjectType.Object };
        var settings = Settings();
        var resolver = new JsonSchemaResolver(root, settings);
        var generator = new JsonSchemaGenerator(settings);
        foreach (var (_, type) in ContractCatalog.Contracts)
        {
            var schema = MarkRequired(generator.Generate(type, resolver));
            root.Definitions[type.Name] = schema;
            root.Properties[type.Name] = new JsonSchemaProperty { Reference = schema };
        }

        var typeScriptSettings = new TypeScriptGeneratorSettings
        {
            TypeStyle = TypeScriptTypeStyle.Interface,
            TypeScriptVersion = 5.0m,
            EnumStyle = TypeScriptEnumStyle.StringLiteral,
            MarkOptionalProperties = false,
            DateTimeType = TypeScriptDateTimeType.String,
        };
        var code = new TypeScriptGenerator(root, typeScriptSettings).GenerateFile("SwiftBetsContractsRoot");
        var header = "// Generated from SwiftBets.Contracts. Do not edit; run `dotnet run --project tools/SwiftBets.Contracts.SchemaGen`.\n";
        var body = code.ReplaceLineEndings("\n");
        body = body[body.IndexOf("export ", StringComparison.Ordinal)..body.IndexOf("export interface SwiftBetsContractsRoot", StringComparison.Ordinal)];
        return header + "\n" + body.TrimEnd() + "\n";
    }

    private static SystemTextJsonSchemaGeneratorSettings Settings() => new()
    {
        SerializerOptions = ContractJson.Options,
        SchemaType = SchemaType.JsonSchema,
        DefaultReferenceTypeNullHandling = ReferenceTypeNullHandling.NotNull,
        GenerateEnumMappingDescription = false,
        FlattenInheritanceHierarchy = true,
    };

    private static JsonSchema Schema(Type type) => MarkRequired(JsonSchema.FromType(type, Settings()));

    private static JsonSchema MarkRequired(JsonSchema schema)
    {
        foreach (var candidate in schema.Definitions.Values.Append(schema))
        {
            foreach (var property in candidate.ActualSchema.Properties.Values)
            {
                property.IsRequired = !property.IsNullable(SchemaType.JsonSchema);
            }
        }

        return schema;
    }
}
