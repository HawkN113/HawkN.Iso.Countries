using HawkN.Iso.Currencies;
namespace HawkN.Iso.Countries.Currencies.Models;

internal sealed class CountryCurrencyInfo
{
    public required CountryCode.TwoLetterCode CountryCode { get; init; }
    public required CurrencyCode PrimaryCurrency { get; init; }
    public required IReadOnlyList<CurrencyCode> SecondaryCurrencies { get; init; } = [];
}