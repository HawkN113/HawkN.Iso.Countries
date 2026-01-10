namespace HawkN.Iso.Countries.Currencies.Models;

internal sealed class CountryCurrencyInfoRow
{
    public required string CountryCode { get; init; }
    public required string PrimaryCurrency { get; init; }
    public IReadOnlyList<string> SecondaryCurrencies { get; init; } = [];
}