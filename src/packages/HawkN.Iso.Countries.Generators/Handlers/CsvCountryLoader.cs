namespace HawkN.Iso.Countries.Generators.Handlers;

internal sealed class CsvCountryLoader
{
    private readonly List<Models.CountryRow> _actualCountries = [];

    public List<Models.CountryRow> ActualCountries => _actualCountries;

    public CsvCountryLoader(string actualCsv)
    {
        var actual = new CsvCountryHandler(actualCsv)
            .LoadActualCountries()
            .OrderBy(c => c.Name)
            .ToList();

        _actualCountries.AddRange(actual);
    }
}