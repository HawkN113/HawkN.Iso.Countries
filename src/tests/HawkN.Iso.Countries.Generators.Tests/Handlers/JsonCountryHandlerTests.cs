using HawkN.Iso.Countries.Generators.Handlers;
namespace HawkN.Iso.Countries.Generators.Tests.Handlers;

public class JsonCountryHandlerTests
{
    [Fact]
    public void LoadActualCountries_ValidJson_ReturnsParsedData()
    {
        // Arrange
        var json = @"{
            ""3166-1"": [
                {
                    ""name"": ""Afghanistan"",
                    ""alpha_2"": ""af"",
                    ""alpha_3"": ""afg"",
                    ""numeric"": ""004"",
                    ""official_name"": ""Islamic Republic of Afghanistan""
                }
            ]
        }";
        var handler = new JsonCountryHandler(json);

        // Act
        var results = handler.LoadActualCountries();

        // Assert
        Assert.Single(results);
        Assert.Equal("Afghanistan", results[0].Name);
        Assert.Equal("AF", results[0].CodeAlpha2);
        Assert.Equal("AFG", results[0].CodeAlpha3);
    }

    [Fact]
    public void LoadActualCountries_InvalidEntry_SkipsIt()
    {
        // Arrange
        // Пропускаем alpha_2 (валидация требует длину 2)
        var json = @"{
            ""3166-1"": [
                { ""name"": ""Bad"", ""alpha_2"": ""A"", ""alpha_3"": ""AAA"", ""numeric"": ""001"" }
            ]
        }";
        var handler = new JsonCountryHandler(json);

        // Act
        var results = handler.LoadActualCountries();

        // Assert
        Assert.Empty(results);
    }

    [Fact]
    public void LoadActualCountries_MalformedJson_ThrowsException()
    {
        // Arrange
        var handler = new JsonCountryHandler("invalid json");

        // Act & Assert
        Assert.Throws<InvalidDataException>(() => handler.LoadActualCountries());
    }
}