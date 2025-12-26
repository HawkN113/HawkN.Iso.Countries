using HawkN.Iso.Countries.Generators.Extensions;
namespace HawkN.Iso.Countries.Generators.Handlers;

internal sealed class CsvCountryHandler(string csvContent)
{
    private const char Delimiter = ';';
    private const string HeaderCountryOrAreaName = "Country or Area";
    private const string HeaderM49CodeName = "M49 Code";
    private const string HeaderAlpha2CodeName = "ISO-alpha2 Code";
    private const string HeaderAlpha3CodeName = "ISO-alpha3 Code";

    public IReadOnlyList<Models.CountryRow> LoadActualCountries()
    {
        var countries = new List<Models.CountryRow>();
        if (string.IsNullOrWhiteSpace(csvContent)) return countries;

        var span = csvContent.AsSpan();
        var headerEnd = span.IndexOf('\n');
        if (headerEnd < 0) return countries;

        var headerLine = span.Slice(0, headerEnd).TrimEnd('\r');
        var lineStart = headerEnd + 1;

        var indices = ParseHeader(headerLine, out int maxIndex);

        while (lineStart < span.Length)
        {
            var lineEnd = span.Slice(lineStart).IndexOf('\n');
            var lineSpan = lineEnd < 0
                ? span.Slice(lineStart).TrimEnd('\r')
                : span.Slice(lineStart, lineEnd).TrimEnd('\r');

            lineStart += lineEnd < 0 ? span.Length - lineStart : lineEnd + 1;

            if (TryParseCountryRow(lineSpan, indices, maxIndex, out var row))
                countries.Add(row);
        }

        return countries;
    }

    private static Dictionary<string, int> ParseHeader(ReadOnlySpan<char> headerLine, out int maxIndex)
    {
        var fields = ParseFields(headerLine);
        var indices = new Dictionary<string, int>
        {
            [HeaderCountryOrAreaName] = -1,
            [HeaderM49CodeName] = -1,
            [HeaderAlpha2CodeName] = -1,
            [HeaderAlpha3CodeName] = -1
        };

        for (var i = 0; i < fields.Length; i++)
        {
            var f = fields[i].TrimQuotes().ToString();
            switch (f.ToLowerInvariant())
            {
                case var _ when f.Equals(HeaderCountryOrAreaName, StringComparison.OrdinalIgnoreCase):
                    indices[HeaderCountryOrAreaName] = i; break;
                case var _ when f.Equals(HeaderM49CodeName, StringComparison.OrdinalIgnoreCase):
                    indices[HeaderM49CodeName] = i; break;
                case var _ when f.Equals(HeaderAlpha2CodeName, StringComparison.OrdinalIgnoreCase):
                    indices[HeaderAlpha2CodeName] = i; break;
                case var _ when f.Equals(HeaderAlpha3CodeName, StringComparison.OrdinalIgnoreCase):
                    indices[HeaderAlpha3CodeName] = i; break;
            }
        }

        if (indices.Values.Any(v => v < 0))
            throw new InvalidDataException("CSV missing required columns.");

        maxIndex = indices.Values.Max();
        return indices;
    }

    private static bool TryParseCountryRow(ReadOnlySpan<char> lineSpan, Dictionary<string, int> indices, int maxIndex, out Models.CountryRow country)
    {
        country = null!;
        var fields = ParseFields(lineSpan);
        if (fields.Length <= maxIndex) return false;

        var name = fields[indices[HeaderCountryOrAreaName]].TrimQuotes().ToString().AsMemory();
        var alpha2 = fields[indices[HeaderAlpha2CodeName]].TrimQuotes().ToString().AsMemory();
        var alpha3 = fields[indices[HeaderAlpha3CodeName]].TrimQuotes().ToString().AsMemory();
        var numericSpan = fields[indices[HeaderM49CodeName]].TrimQuotes();

        if (name.Length == 0 || alpha2.Length != 2 || alpha3.Length != 3 || !int.TryParse(numericSpan.ToString(), out var m49))
            return false;

        country = new Models.CountryRow(name, alpha2, alpha3, m49);
        return true;
    }

    private static string[] ParseFields(ReadOnlySpan<char> line)
    {
        var result = new List<string>();
        var start = 0;
        var inQuotes = false;

        for (var i = 0; i <= line.Length; i++)
        {
            var end = i == line.Length || (line[i] == Delimiter && !inQuotes);
            if (i < line.Length && line[i] == '"') inQuotes = !inQuotes;

            if (!end) continue;

            var field = line.Slice(start, i - start).TrimQuotes().ToString();
            result.Add(field);

            start = i + 1;
        }
        return result.ToArray();
    }
}


