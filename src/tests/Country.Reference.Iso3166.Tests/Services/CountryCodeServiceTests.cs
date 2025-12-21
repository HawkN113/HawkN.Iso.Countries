using Country.Reference.Iso3166.Services;

namespace Country.Reference.Iso3166.Tests.Services;

public class CountryCodeServiceTests
{
    private readonly CountryCodeService _service = new();

    [Fact]
    public void GetAll_ReturnsSortedList()
    {
        // Act
        var countries = _service.GetAll();

        // Assert
        Assert.NotEmpty(countries);
        Assert.True(string.Compare(countries[0].Name, countries[1].Name, StringComparison.Ordinal) <= 0);
    }

    [Theory]
    [InlineData("US")]
    [InlineData("USA")]
    [InlineData("840")]
    public void FindByCode_VariousFormats_ReturnsSameCountry(string code)
    {
        // Act
        var country = _service.FindByCode(code);

        // Assert
        Assert.NotNull(country);
        Assert.Equal("United States", country.Name);
    }

    [Fact]
    public void FindByName_ExactMatch_ReturnsCountry()
    {
        // Act
        var country = _service.FindByName("France");

        // Assert
        Assert.NotNull(country);
        Assert.Equal(CountryCode.TwoLetterCode.FR, country!.TwoLetterCode);
    }

    [Theory]
    [InlineData("United", 2)] // Как минимум United States и United Kingdom
    [InlineData("Republic", 5)] 
    public void SearchByName_Query_ReturnsMultipleResults(string query, int minimumExpected)
    {
        // Act
        var results = _service.SearchByName(query).ToList();

        // Assert
        Assert.True(results.Count >= minimumExpected);
        if (results[0].Name.StartsWith(query, StringComparison.OrdinalIgnoreCase))
        {
            // Check the first
        }
    }

    [Fact]
    public void Get_ByEnum_ReturnsCorrectCountry()
    {
        // Act
        var country2 = _service.Get(CountryCode.TwoLetterCode.DE);
        var country3 = _service.Get(CountryCode.ThreeLetterCode.DEU);

        // Assert
        Assert.Equal(country2, country3);
        Assert.Equal("Germany", country2.Name);
    }

    [Fact]
    public void Get_ByNumericInt_ReturnsCorrectCountry()
    {
        // Act
        var country = _service.Get(276); // Germany

        // Assert
        Assert.NotNull(country);
        Assert.Equal("Germany", country!.Name);
    }

    [Fact]
    public void ValidateByCode_InvalidCode_ReturnsFailure()
    {
        // Act
        var result = _service.ValidateByCode("ZZZ", out var country);

        // Assert
        Assert.False(result.IsValid);
        Assert.Null(country);
        Assert.Contains("not a valid ISO 3166-1 code", result.Reason);
    }

    [Fact]
    public void ValidateByName_ValidOfficialName_ReturnsSuccess()
    {
        // Act
        var result = _service.ValidateByName("French Republic", out var country);

        // Assert
        Assert.True(result.IsValid);
        Assert.NotNull(country);
        Assert.Equal("France", country!.Name);
    }
}