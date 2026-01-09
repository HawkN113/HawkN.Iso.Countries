namespace HawkN.Iso.Countries.Currencies.Generators.Models;

public class GeneratedCountryCurrencyRow(
    string countryCode,
    string primaryCurrency,
    IReadOnlyList<string> secondaryCurrencies)
{
    public string CountryCode { get; } = countryCode;
    public string PrimaryCurrency { get; } = primaryCurrency;
    public IReadOnlyList<string> SecondaryCurrencies { get; } = secondaryCurrencies;
}