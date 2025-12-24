namespace HawkN.Iso.Countries.Generators.Handlers;

internal sealed class JsonCountryLoader
{
    private readonly List<Models.Country> _actualCountries = [];

    public List<Models.Country> ActualCountries => _actualCountries;

    public JsonCountryLoader(string actualJson)
    {
        var actual = new JsonCountryHandler(actualJson)
            .LoadActualCountries()
            .OrderBy(c => c.Name)
            .ToList();

        _actualCountries.AddRange(actual);
    }
}