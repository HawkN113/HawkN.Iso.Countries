namespace Country.Reference.Iso3166.Generators.Handlers;

internal sealed class CsvCountryLoader
{
    private readonly List<Models.Country> _actualCountries = [];

    public List<Models.Country> ActualCountries => _actualCountries;

    public CsvCountryLoader(string actualCsv)
    {
        var actual = new CsvCountryHandler(actualCsv)
            .LoadActualCountries()
            .OrderBy(c => c.Name)
            .ToList();
        
        _actualCountries.AddRange(actual);
    }
}