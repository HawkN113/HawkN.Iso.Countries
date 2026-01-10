using System.Xml.Linq;
using HawkN.Iso.Countries.Currencies.Generators.Models;
namespace HawkN.Iso.Countries.Currencies.Generators.Handlers;

internal sealed class CldrCurrencyHandler(string xmlContent)
{
    private readonly XDocument _doc = XDocument.Parse(xmlContent);

    public List<ParsedCurrencyRow> LoadCurrencies()
    {
        var currencies = new List<ParsedCurrencyRow>();
        ParseRegionCurrencies(currencies);
        return currencies;
    }

    private void ParseRegionCurrencies(List<ParsedCurrencyRow> currencies)
    {
        foreach (var region in _doc.Descendants("currencyData").Descendants("region"))
        {
            var countryCode = region.Attribute("iso3166")?.Value;
            if (string.IsNullOrEmpty(countryCode))
                continue;

            var order = 0;
            currencies.AddRange(region.Elements("currency")
                .Select(
                    currency => new ParsedCurrencyRow(countryCode!, currency.Attribute("iso4217")!.Value, currency.Attribute("to") is null, order++))
            );
        }
    }
}