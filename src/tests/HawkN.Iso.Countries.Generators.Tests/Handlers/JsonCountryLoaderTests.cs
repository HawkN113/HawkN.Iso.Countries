using HawkN.Iso.Countries.Generators.Handlers;
namespace HawkN.Iso.Countries.Generators.Tests.Handlers;

public class JsonCountryLoaderTests
{
    private const string ValidJson = @"{
        ""3166-1"": [
            { ""name"": ""Germany"", ""alpha_2"": ""de"", ""alpha_3"": ""deu"", ""numeric"": ""276"" },
            { ""name"": ""Austria"", ""alpha_2"": ""at"", ""alpha_3"": ""aut"", ""numeric"": ""040"" },
            { ""name"": ""France"", ""alpha_2"": ""fr"", ""alpha_3"": ""fra"", ""numeric"": ""250"" }
        ]
    }";

    [Fact]
    public void Constructor_ValidJson_PopulatesAndSortsCountriesByName()
    {
        // Act
        var loader = new JsonCountryLoader(ValidJson);

        // Assert
        Assert.Equal(3, loader.ActualCountries.Count);
        
        Assert.Equal("Austria", loader.ActualCountries[0].Name);
        Assert.Equal("France", loader.ActualCountries[1].Name);
        Assert.Equal("Germany", loader.ActualCountries[2].Name);
    }

    [Fact]
    public void Constructor_EmptyJson_ReturnsEmptyList()
    {
        // Arrange
        var emptyJson = @"{ ""3166-1"": [] }";

        // Act
        var loader = new JsonCountryLoader(emptyJson);

        // Assert
        Assert.Empty(loader.ActualCountries);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Constructor_InvalidInput_ReturnsEmptyList(string? input)
    {
        // Act
        var loader = new JsonCountryLoader(input!);

        // Assert
        Assert.Empty(loader.ActualCountries);
    }

    [Fact]
    public void Constructor_MalformedJson_ThrowsInvalidDataException()
    {
        // Arrange
        var malformedJson = "{ \"3166-1\": [ { \"name\": \"Missing Brackets\" ";

        // Act & Assert
        Assert.Throws<InvalidDataException>(() => new JsonCountryLoader(malformedJson));
    }
}