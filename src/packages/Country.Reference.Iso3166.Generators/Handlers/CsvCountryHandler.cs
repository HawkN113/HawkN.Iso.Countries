namespace Country.Reference.Iso3166.Generators.Handlers;

internal sealed class CsvCountryHandler(string csvContent)
{
    private const char Delimiter = ';';
    private const string HeaderCountryOrAreaName = "Country or Area";
    private const string HeaderM49CodeName = "M49 Code";
    private const string HeaderAlpha2CodeName = "ISO-alpha2 Code";
    private const string HeaderAlpha3CodeName = "ISO-alpha3 Code";

    public List<Models.Country> LoadActualCountries()
    {
        var countries = new List<Models.Country>();
        var lines = csvContent.Split(['\n', '\r'], StringSplitOptions.RemoveEmptyEntries);

        if (lines.Length <= 1) return countries;
        var headerLine = lines[0];
        var headerFields = headerLine.Split(Delimiter);

        var indices = GetColumnIndices(headerFields);

        if (indices.Any(i => i.Value == -1))
        {
            throw new InvalidDataException(
                "The CSV file does not contain all required columns (Country, Alpha-2, Alpha-3, M49 Code)");
        }

        foreach (var line in lines.Skip(1))
        {
            var fields = line.Split(Delimiter);
            if (fields.Length < indices.Max(i => i.Value) + 1) continue;

            if (!TryGetField(fields, indices[HeaderAlpha2CodeName], 2, out var alpha2Code) ||
                !TryGetField(fields, indices[HeaderAlpha3CodeName], 3, out var alpha3Code))
            {
                continue;
            }
            var m49String = fields[indices[HeaderM49CodeName]].Trim();

            var country = new Models.Country(
                fields[indices[HeaderCountryOrAreaName]].Trim(),
                alpha2Code!.Trim(),
                alpha3Code!.Trim(),
                m49String);
            countries.Add(country);
        }

        return countries;
    }

    private static Dictionary<string, int> GetColumnIndices(string[] headerFields)
    {
        var requiredHeaders = new List<string>
        {
            HeaderCountryOrAreaName,
            HeaderM49CodeName,
            HeaderAlpha2CodeName,
            HeaderAlpha3CodeName
        };

        var indices = new Dictionary<string, int>();
        foreach (var header in requiredHeaders)
        {
            var index = Array.FindIndex(headerFields, h =>
                h.Trim().Equals(header, StringComparison.OrdinalIgnoreCase));
            indices.Add(header, index);
        }

        return indices;
    }

    private static bool TryGetField(string[] fields, int index, int expectedLength, out string? code)
    {
        var value = fields[index].Trim();

        if (string.IsNullOrEmpty(value) || value.Length != expectedLength)
        {
            code = null;
            return false;
        }

        code = value.Trim();
        return true;
    }
}