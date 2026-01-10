using HawkN.Iso.Countries.Currencies.Generators.Handlers;
using HawkN.Iso.Countries.Currencies.Generators.Models;
namespace HawkN.Iso.Countries.Currencies.Generators;

internal sealed class CurrencyDataLoader
{
    public List<ParsedCurrencyRow> ActualCurrencyData { get; }

    public CurrencyDataLoader(string supplementalXml, string enXml)
    {
        var actual = new CldrCurrencyHandler(supplementalXml).LoadCurrencies();
        ActualCurrencyData = actual;
    }
}