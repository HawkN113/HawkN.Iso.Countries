using System.Collections.Immutable;
using HawkN.Iso.Currencies;
namespace HawkN.Iso.Countries.Currencies.Extensions;

/// <summary>
/// Provides extension methods for working with country currencies via <see cref="CountryCurrencyMap"/>.
/// Allows retrieving the primary, secondary, or all currencies for a given country.
/// </summary>
public static class CountryCurrencyExtensions
{
    /// <summary>
    /// Gets the primary currency of the specified country.
    /// </summary>
    /// <param name="country">The two-letter country code.</param>
    /// <returns>The primary currency of the country, or <c>null</c> if the country is not found.</returns>
    public static CurrencyCode? GetPrimaryCurrency(this CountryCode.TwoLetterCode country)
    {
        return CountryCurrencyMap.TryGet(country, out var info) ? info?.PrimaryCurrency : null;
    }

    /// <summary>
    /// Gets the secondary currencies of the specified country.
    /// </summary>
    /// <param name="country">The two-letter country code.</param>
    /// <returns>A read-only list of secondary currencies, or an empty array if the country is not found.</returns>
    public static IReadOnlyList<CurrencyCode> GetSecondaryCurrencies(this CountryCode.TwoLetterCode country)
    {
        return CountryCurrencyMap.TryGet(country, out var info) ? info!.SecondaryCurrencies : ImmutableArray<CurrencyCode>.Empty;
    }

    /// <summary>
    /// Gets all currencies (primary and secondary) of the specified country.
    /// </summary>
    /// <param name="country">The two-letter country code.</param>
    /// <returns>An enumeration of all currencies of the country. Empty if the country is not found.</returns>
    public static IEnumerable<CurrencyCode> GetAllCurrencies(this CountryCode.TwoLetterCode country)
    {
        if (!CountryCurrencyMap.TryGet(country, out var info))
            yield break;

        yield return info!.PrimaryCurrency;

        foreach (var c in info.SecondaryCurrencies)
            yield return c;
    }

    /// <summary>
    /// Checks if the given country uses the specified currency
    /// (either as primary or secondary currency).
    /// Optimized with HashSet for fast lookups in large secondary currency lists.
    /// </summary>
    /// <param name="country">The two-letter country code.</param>
    /// <param name="currency">The currency code to check.</param>
    /// <returns><c>true</c> if the country uses the currency; otherwise, <c>false</c>.</returns>
    public static bool IsCurrencyUsedByCountry(this CountryCode.TwoLetterCode country, CurrencyCode currency)
    {
        if (!CountryCurrencyMap.TryGet(country, out var info))
            return false;

        if (info?.PrimaryCurrency == currency)
            return true;

        if (info?.SecondaryCurrencies.Count <= 0) return false;
        var set = new HashSet<CurrencyCode>(info!.SecondaryCurrencies);
        return set.Contains(currency);
    }
}