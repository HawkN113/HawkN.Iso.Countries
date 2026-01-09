using HawkN.Iso.Countries.Currencies.Extensions;
using HawkN.Iso.Currencies;
namespace HawkN.Iso.Countries.Currencies.Tests.Extensions;

public sealed class CountryCurrencyExtensionsTests
{
    [Fact]
    public void GetPrimaryCurrency_Should_Return_Null_For_Unknown_Country()
    {
        // Arrange
        var country = (CountryCode.TwoLetterCode)999;

        // Act
        var currency = country.GetPrimaryCurrency();

        // Assert
        Assert.Null(currency);
    }

    [Theory]
    [InlineData(CountryCode.TwoLetterCode.US, CurrencyCode.USD)]
    [InlineData(CountryCode.TwoLetterCode.DE, CurrencyCode.EUR)]
    [InlineData(CountryCode.TwoLetterCode.GB, CurrencyCode.GBP)]
    public void GetPrimaryCurrency_Should_Return_Correct_Value(
        CountryCode.TwoLetterCode country,
        CurrencyCode expected)
    {
        // Act
        var currency = country.GetPrimaryCurrency();

        // Assert
        Assert.NotNull(currency);
        Assert.Equal(expected, currency);
    }

    [Fact]
    public void GetSecondaryCurrencies_Should_Return_Empty_For_Unknown_Country()
    {
        // Arrange
        var country = (CountryCode.TwoLetterCode)999;

        // Act
        var result = country.GetSecondaryCurrencies();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void GetSecondaryCurrencies_Should_Not_Contain_Primary_Currency()
    {
        // Arrange
        var country = CountryCode.TwoLetterCode.CH;

        // Act
        var primary = country.GetPrimaryCurrency();
        var secondary = country.GetSecondaryCurrencies();

        // Assert
        Assert.NotNull(primary);
        foreach (var currency in secondary)
        {
            Assert.NotEqual(primary, currency);
        }
    }

    [Fact]
    public void GetAllCurrencies_Should_Return_Primary_First()
    {
        // Arrange
        var country = CountryCode.TwoLetterCode.US;

        // Act
        var currencies = country.GetAllCurrencies().ToArray();

        // Assert
        Assert.NotEmpty(currencies);
        Assert.Equal(country.GetPrimaryCurrency(), currencies[0]);
    }

    [Fact]
    public void GetAllCurrencies_Should_Return_Empty_For_Unknown_Country()
    {
        // Arrange
        var country = (CountryCode.TwoLetterCode)999;

        // Act
        var currencies = country.GetAllCurrencies();

        // Assert
        Assert.Empty(currencies);
    }

    [Theory]
    [InlineData(CountryCode.TwoLetterCode.US, CurrencyCode.USD)]
    [InlineData(CountryCode.TwoLetterCode.DE, CurrencyCode.EUR)]
    public void IsCurrencyUsedByCountry_Should_Return_True_For_Primary_Currency(
        CountryCode.TwoLetterCode country,
        CurrencyCode currency)
    {
        // Act
        var result = country.IsCurrencyUsedByCountry(currency);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsCurrencyUsedByCountry_Should_Return_True_For_Secondary_Currency()
    {
        // Arrange
        var country = CountryCode.TwoLetterCode.CH;

        // Act
        var result = country.IsCurrencyUsedByCountry(CurrencyCode.CHE);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsCurrencyUsedByCountry_Should_Return_False_For_Unused_Currency()
    {
        // Arrange
        var country = CountryCode.TwoLetterCode.US;

        // Act
        var result = country.IsCurrencyUsedByCountry(CurrencyCode.EUR);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsCurrencyUsedByCountry_Should_Return_False_For_Unknown_Country()
    {
        // Arrange
        var country = (CountryCode.TwoLetterCode)999;

        // Act
        var result = country.IsCurrencyUsedByCountry(CurrencyCode.USD);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsCurrencyUsedByCountry_Should_Work_For_All_Returned_Currencies()
    {
        // Arrange
        var country = CountryCode.TwoLetterCode.CH;

        // Act
        foreach (var currency in country.GetAllCurrencies())
        {
            // Assert
            Assert.True(country.IsCurrencyUsedByCountry(currency));
        }
    }
}