using HawkN.Iso.Countries.Currencies.Models;
using HawkN.Iso.Currencies;
namespace HawkN.Iso.Countries.Currencies.Tests;

public sealed class CountryCurrencyMapTests
{
    [Fact]
    public void TryGet_Should_Return_False_For_Unknown_Country()
    {
        // Arrange
        var result = CountryCurrencyMap.TryGet(
            (CountryCode.TwoLetterCode)999,
            out var info);

        // Assert
        Assert.False(result);
        Assert.Null(info);
    }

    [Theory]
    [InlineData(CountryCode.TwoLetterCode.DE, CurrencyCode.EUR)]
    [InlineData(CountryCode.TwoLetterCode.US, CurrencyCode.USD)]
    [InlineData(CountryCode.TwoLetterCode.GB, CurrencyCode.GBP)]
    [InlineData(CountryCode.TwoLetterCode.JP, CurrencyCode.JPY)]
    public void TryGet_Should_Return_Correct_Primary_Currency(
        CountryCode.TwoLetterCode country,
        CurrencyCode expectedCurrency)
    {
        // Act
        var result = CountryCurrencyMap.TryGet(country, out var info);

        // Assert
        Assert.True(result);
        Assert.NotNull(info);
        Assert.Equal(expectedCurrency, info!.PrimaryCurrency);
    }

    [Fact]
    public void Countries_With_No_Secondary_Currencies_Should_Return_Empty_List()
    {
        // Act
        var result = CountryCurrencyMap.TryGet(
            CountryCode.TwoLetterCode.DE,
            out var info);

        // Assert
        Assert.True(result);
        Assert.NotNull(info);
        Assert.NotNull(info!.SecondaryCurrencies);
        Assert.Empty(info.SecondaryCurrencies);
    }

    [Fact]
    public void Countries_With_Secondary_Currencies_Should_Return_Them()
    {
        // Act
        var result = CountryCurrencyMap.TryGet(
            CountryCode.TwoLetterCode.CH,
            out var info);

        // Assert
        Assert.True(result);
        Assert.NotNull(info);
        Assert.Equal(CurrencyCode.CHF, info!.PrimaryCurrency);
        Assert.Contains(CurrencyCode.CHE, info.SecondaryCurrencies);
        Assert.Contains(CurrencyCode.CHW, info.SecondaryCurrencies);
    }

    [Fact]
    public void Secondary_Currencies_Should_Not_Contain_Primary()
    {
        foreach (var country in Enum
                     .GetValues<CountryCode.TwoLetterCode>()
                     .Where(c => CountryCurrencyMap.TryGet(c, out _)))
        {
            // Act
            CountryCurrencyMap.TryGet(country, out var info);

            // Assert
            foreach (var secondary in info!.SecondaryCurrencies)
            {
                Assert.NotEqual(info.PrimaryCurrency, secondary);
            }
        }
    }

    [Fact]
    public void All_Currency_Codes_Must_Be_Valid_ISO_4217()
    {
        foreach (var country in Enum
                     .GetValues<CountryCode.TwoLetterCode>()
                     .Where(c => CountryCurrencyMap.TryGet(c, out _)))
        {
            // Act
            CountryCurrencyMap.TryGet(country, out var info);

            // Assert
            Assert.True(Enum.IsDefined(info!.PrimaryCurrency));
            foreach (var secondary in info.SecondaryCurrencies)
            {
                Assert.True(Enum.IsDefined(secondary));
            }
        }
    }

    [Fact]
    public void Mapping_Should_Be_Deterministic()
    {
        // Act
        var firstResult = CountryCurrencyMap.TryGet(
            CountryCode.TwoLetterCode.US,
            out var first);

        var secondResult = CountryCurrencyMap.TryGet(
            CountryCode.TwoLetterCode.US,
            out var second);

        // Assert
        Assert.True(firstResult);
        Assert.True(secondResult);
        Assert.Same(first, second);
    }

    [Fact]
    public void All_Entries_Should_Have_Primary_Currency()
    {
        CountryCurrencyInfo? info = null;
        foreach (var country in Enum
                     .GetValues<CountryCode.TwoLetterCode>()
                     .Where(country => CountryCurrencyMap.TryGet(country, out info)))
        {
            // Assert
            Assert.NotEqual(default, info!.PrimaryCurrency);
        }
    }

    [Fact]
    public void Special_Test_Palestine_Currency_Rule()
    {
        // Act
        var result = CountryCurrencyMap.TryGet(
            CountryCode.TwoLetterCode.PS,
            out var info);

        // Assert
        Assert.True(result);
        Assert.NotNull(info);
        Assert.Equal(CurrencyCode.ILS, info!.PrimaryCurrency);
        Assert.Contains(CurrencyCode.JOD, info.SecondaryCurrencies);
    }
}