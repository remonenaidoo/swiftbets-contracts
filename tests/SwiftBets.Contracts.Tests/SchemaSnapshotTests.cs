using SwiftBets.Contracts.SchemaGen;

namespace SwiftBets.Contracts.Tests;

public sealed class SchemaSnapshotTests
{
    private static readonly string RepositoryRoot = FindRepositoryRoot();

    [Fact]
    public void Checked_in_schemas_match_the_contracts()
    {
        foreach (var (file, json) in SchemaGenerator.GenerateSchemas())
        {
            var path = Path.Combine(RepositoryRoot, "schemas", file);
            File.Exists(path).ShouldBeTrue($"{file} is missing; run the schema generator.");
            File.ReadAllText(path).ShouldBe(json, $"{file} is stale; a contract changed without regenerating. Breaking changes need a new version.");
        }
    }

    [Fact]
    public void Checked_in_typescript_matches_the_contracts() =>
        File.ReadAllText(Path.Combine(RepositoryRoot, "ts", "src", "generated.ts")).ShouldBe(SchemaGenerator.GenerateTypeScript());

    private static string FindRepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null && !File.Exists(Path.Combine(directory.FullName, "global.json")))
        {
            directory = directory.Parent;
        }

        return directory?.FullName ?? throw new InvalidOperationException("Repository root not found.");
    }
}
