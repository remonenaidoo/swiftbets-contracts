using SwiftBets.Contracts.SchemaGen;

var root = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();
var schemaDirectory = Path.Combine(root, "schemas");
Directory.CreateDirectory(schemaDirectory);
foreach (var (file, json) in SchemaGenerator.GenerateSchemas())
{
    await File.WriteAllTextAsync(Path.Combine(schemaDirectory, file), json);
}

await File.WriteAllTextAsync(Path.Combine(root, "ts", "src", "generated.ts"), SchemaGenerator.GenerateTypeScript());
Console.WriteLine($"Wrote {SchemaGenerator.GenerateSchemas().Count} schemas and ts/src/generated.ts");
