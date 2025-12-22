using HawkN.Iso.Countries.Generators.Tests.Helpers;
using Microsoft.CodeAnalysis;
namespace HawkN.Iso.Countries.Generators.Tests;

public class LocalCountryDatabaseGeneratorTests
{
    [Fact]
    public void Generator_Should_Generate_LocalCountryDatabase_With_ActualCountries()
    {
        // Arrange
        var inputSource = "";

        // Act
        var (diagnostics, output) = GeneratorTestHelper.GetGeneratedOutput<LocalCountryDatabaseGenerator>(inputSource);

        // Assert
        Assert.Empty(diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error));
        Assert.Contains("internal static class LocalCountryDatabase", output);
        Assert.Contains("public static IReadOnlyList<Models.Country> ActualCountries", output);
        Assert.Contains("new(", output);
        Assert.Contains("CountryCode.TwoLetterCode.", output);
        Assert.Contains("CountryCode.ThreeLetterCode.", output);
    }

    [Fact]
    public void Generator_Should_Handle_OfficialNames_Correctly()
    {
        // Act
        var (_, output) = GeneratorTestHelper.GetGeneratedOutput<LocalCountryDatabaseGenerator>("");

        // Assert
        Assert.Contains("Federal Republic of Germany", output);
    }

    [Fact]
    public void Generator_Should_Produce_Stub_On_Error()
    {
        // Simulate an exception 
        var (diagnostics, output) = GeneratorTestHelper.GetGeneratedOutput<LocalCountryDatabaseGenerator>("invalid");

        Assert.NotNull(output);
        Assert.Contains("LocalCountryDatabase", output);
    }
}