using HawkN.Iso.Countries.Generators.Handlers;

namespace HawkN.Iso.Countries.Generators.Tests.Handlers;

public class CsvCountryLoaderTests
{
    [Fact]
    public void CsvCountryLoader_ValidCsv_LoadsAndSortsCountries()
    {
        // Arrange
        var csv =
            @"Country or Area;M49 Code;ISO-alpha2 Code;ISO-alpha3 Code
Germany;276;DE;DEU
Austria;040;AT;AUT
United States;840;US;USA";

        // Act
        var loader = new CsvCountryLoader(csv);
        var countries = loader.ActualCountries;

        // Assert
        Assert.Equal(3, countries.Count);

        // Проверка сортировки по имени
        Assert.Equal("Austria", countries[0].Name);
        Assert.Equal("Germany", countries[1].Name);
        Assert.Equal("United States", countries[2].Name);

        // Проверка кодов и numeric
        Assert.Equal("AT", countries[0].CodeAlpha2);
        Assert.Equal("AUT", countries[0].CodeAlpha3);
        Assert.Equal(40, countries[0].NumericCode); // 040 -> int 40
    }

    [Fact]
    public void CsvCountryLoader_EmptyCsv_ReturnsEmptyList()
    {
        // Arrange
        var loader = new CsvCountryLoader(string.Empty);

        // Act
        var countries = loader.ActualCountries;

        // Assert
        Assert.Empty(countries);
    }

    [Fact]
    public void CsvCountryLoader_InvalidRows_AreSkipped()
    {
        // Arrange
        var csv =
            @"Country or Area;M49 Code;ISO-alpha2 Code;ISO-alpha3 Code
Austria;040;AT;AUT
InvalidCountry;XXX;XX;XXXX"; // invalid numeric + alpha codes

        // Act
        var loader = new CsvCountryLoader(csv);
        var countries = loader.ActualCountries;

        // Assert
        Assert.Single(countries);
        Assert.Equal("Austria", countries[0].Name);
    }

    [Fact]
    public void CsvCountryLoader_CsvWithQuotes_ParsesCorrectly()
    {
        // Arrange
        var csv =
            @"Country or Area;M49 Code;ISO-alpha2 Code;ISO-alpha3 Code
""United Kingdom"";826;GB;GBR
""France"";250;FR;FRA";

        // Act
        var loader = new CsvCountryLoader(csv);
        var countries = loader.ActualCountries;

        // Assert
        Assert.Equal(2, countries.Count);

        Assert.Equal("France", countries[0].Name);
        Assert.Equal("United Kingdom", countries[1].Name);
    }

    [Fact]
    public void CsvCountryLoader_AlreadySortedCsv_MaintainsOrder()
    {
        // Arrange
        var csv =
            @"Country or Area;M49 Code;ISO-alpha2 Code;ISO-alpha3 Code
Austria;040;AT;AUT
Germany;276;DE;DEU
United States;840;US;USA";

        // Act
        var loader = new CsvCountryLoader(csv);
        var countries = loader.ActualCountries;

        // Assert
        Assert.Equal("Austria", countries[0].Name);
        Assert.Equal("Germany", countries[1].Name);
        Assert.Equal("United States", countries[2].Name);
    }
}