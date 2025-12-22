using System.Text.Json;
using HawkN.Iso.Countries.Generators.Models;
namespace HawkN.Iso.Countries.Generators.Handlers;

internal sealed class JsonCountryHandler(string jsonContent)
{
    public List<Models.Country> LoadActualCountries()
    {
        var result = new List<Models.Country>();

        if (string.IsNullOrWhiteSpace(jsonContent))
            return result;

        try
        {
            var data = JsonSerializer.Deserialize<DebianIsoJson>(jsonContent);

            if (data?.Countries == null)
                return result;

            foreach (var entry in data.Countries)
            {
                if (!IsValidEntry(entry)) continue;
                var country = new Models.Country(
                    entry.Name.Trim(),
                    entry.Alpha2.Trim().ToUpperInvariant(),
                    entry.Alpha3.Trim().ToUpperInvariant(),
                    entry.Numeric.Trim(),
                    entry.OfficialName?.Trim()
                );
                result.Add(country);
            }
        }
        catch (JsonException ex)
        {
            throw new InvalidDataException("Failed to parse ISO-3166 JSON data.", ex);
        }

        return result;
    }

    private static bool IsValidEntry(DebianCountryEntry entry)
    {
        return !string.IsNullOrWhiteSpace(entry.Name) &&
               !string.IsNullOrWhiteSpace(entry.Alpha2) && entry.Alpha2.Length == 2 &&
               !string.IsNullOrWhiteSpace(entry.Alpha3) && entry.Alpha3.Length == 3 &&
               !string.IsNullOrWhiteSpace(entry.Numeric);
    }
}