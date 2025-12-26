using HawkN.Iso.Countries.Generators.Handlers;

namespace HawkN.Iso.Countries.Generators.Tests.Handlers;

public class CsvCountryHandlerTests
{
    [Fact]
    public void LoadActualCountries_ValidCsv_ReturnsAllRows()
    {
        // Arrange
        var csv =
            @"Country or Area;M49 Code;ISO-alpha2 Code;ISO-alpha3 Code
Austria;040;AT;AUT
Germany;276;DE;DEU
United States;840;US;USA";

        var handler = new CsvCountryHandler(csv);

        // Act
        var countries = handler.LoadActualCountries();

        // Assert
        Assert.Equal(3, countries.Count);

        Assert.Equal("Austria", countries[0].Name.ToString());
        Assert.Equal("AT", countries[0].CodeAlpha2.ToString());
        Assert.Equal("AUT", countries[0].CodeAlpha3.ToString());
        Assert.Equal(40, countries[0].NumericCode); // CSV numeric 040 -> int 40
    }

    [Fact]
    public void LoadActualCountries_EmptyCsv_ReturnsEmptyList()
    {
        // Arrange
        var handler = new CsvCountryHandler(string.Empty);

        // Act
        var countries = handler.LoadActualCountries();

        // Assert
        Assert.Empty(countries);
    }

    [Fact]
    public void LoadActualCountries_CsvWithQuotes_ParsesCorrectly()
    {
        // Arrange
        var csv =
            @"Country or Area;M49 Code;ISO-alpha2 Code;ISO-alpha3 Code
""United Kingdom"";826;GB;GBR
""France"";250;FR;FRA";

        var handler = new CsvCountryHandler(csv);

        // Act
        var countries = handler.LoadActualCountries();

        // Assert
        Assert.Equal(2, countries.Count);

        Assert.Equal("United Kingdom", countries[0].Name.ToString());
        Assert.Equal("GB", countries[0].CodeAlpha2.ToString());
        Assert.Equal("GBR", countries[0].CodeAlpha3.ToString());
        Assert.Equal(826, countries[0].NumericCode);
    }

    [Fact]
    public void LoadActualCountries_InvalidRow_IsSkipped()
    {
        // Arrange
        var csv =
            @"Country or Area;M49 Code;ISO-alpha2 Code;ISO-alpha3 Code
Austria;040;AT;AUT
InvalidCountry;XXX;XX;XXXX";

        var handler = new CsvCountryHandler(csv);

        // Act
        var countries = handler.LoadActualCountries();

        // Assert
        Assert.Single(countries);
        Assert.Equal("Austria", countries[0].Name.ToString());
    }

    [Fact]
    public void LoadActualCountries_MissingColumn_Throws()
    {
        // Arrange
        var csv = "Country or Area;ISO-alpha2 Code;ISO-alpha3 Code\nAustria;AT;AUT";

        var handler = new CsvCountryHandler(csv);

        // Act & Assert
        Assert.Throws<InvalidDataException>(() => handler.LoadActualCountries());
    }
}