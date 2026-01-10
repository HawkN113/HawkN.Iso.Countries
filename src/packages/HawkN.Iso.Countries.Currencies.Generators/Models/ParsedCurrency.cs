namespace HawkN.Iso.Countries.Currencies.Generators.Models;

internal sealed class ParsedCurrencyRow(string countryCode, string currencyCode, bool isCurrent, int order)
{
    public string CountryCode { get; } = countryCode;
    public string CurrencyCode { get; } = currencyCode;
    public bool IsCurrent { get; } = isCurrent;
    public int Order { get; } = order;
}
