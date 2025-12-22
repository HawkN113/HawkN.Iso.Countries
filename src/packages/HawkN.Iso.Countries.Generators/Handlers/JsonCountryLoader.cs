namespace HawkN.Iso.Countries.Generators.Handlers;

internal sealed class JsonCountryLoader
{
    private readonly List<HawkN.Iso.Countries.Generators.Models.Country> _actualCountries = [];

    public List<HawkN.Iso.Countries.Generators.Models.Country> ActualCountries => _actualCountries;

    public JsonCountryLoader(string actualJson)
    {
        var actual = new JsonCountryHandler(actualJson)
            .LoadActualCountries()
            .OrderBy(c => c.Name)
            .ToList();

        _actualCountries.AddRange(actual);
    }
}