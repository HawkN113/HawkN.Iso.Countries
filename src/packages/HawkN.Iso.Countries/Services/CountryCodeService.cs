using System.Diagnostics.CodeAnalysis;
using HawkN.Iso.Countries.Abstractions;
using HawkN.Iso.Countries.Models;
namespace HawkN.Iso.Countries.Services;

internal sealed class CountryCodeService : ICountryCodeService
{
    private readonly Dictionary<string, Country> _byCode = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<CountryCode.TwoLetterCode, Country> _byAlpha2 = [];
    private readonly Dictionary<CountryCode.ThreeLetterCode, Country> _byAlpha3 = [];
    private readonly Dictionary<int, Country> _byNumericInt = [];

    private readonly Dictionary<string, Country> _byName = new(StringComparer.OrdinalIgnoreCase);

    private readonly IReadOnlyList<Country> _allCountries;

    public CountryCodeService()
    {
        _allCountries = LocalCountryDatabase.ActualCountries
            .OrderBy(c => c.Name)
            .ToList();

        foreach (var country in _allCountries)
        {
            _byCode[country.TwoLetterCode.ToString()] = country;
            _byCode[country.ThreeLetterCode.ToString()] = country;
            _byCode[country.NumericCodeString] = country;

            _byNumericInt[country.NumericCode] = country;

            _byAlpha2[country.TwoLetterCode] = country;
            _byAlpha3[country.ThreeLetterCode] = country;

            IndexName(country.Name, country);
        }
    }

    private void IndexName(string? name, Country country)
    {
        if (string.IsNullOrWhiteSpace(name)) return;
        _byName.TryAdd(name, country);
    }

    public Country? FindByCode(string code) =>
        _byCode.GetValueOrDefault(code);

    public Country? FindByName(string name) =>
        _byName.GetValueOrDefault(name);

    public IEnumerable<Country> SearchByName(string query)
    {
        if (string.IsNullOrWhiteSpace(query)) return [];

        var term = query.Trim();
        const StringComparison comparison = StringComparison.OrdinalIgnoreCase;

        return _allCountries
            .Where(c => c.Name?.Contains(term, comparison) ?? false)
            .OrderBy(c => c.Name.StartsWith(term, comparison) ? 0 : 1)
            .ThenBy(c => c.Name);
    }

    public Country Get(CountryCode.TwoLetterCode code) => _byAlpha2[code];

    public Country Get(CountryCode.ThreeLetterCode code) => _byAlpha3[code];

    public Country? Get(int numericCode) =>
        _byNumericInt.GetValueOrDefault(numericCode);

    public bool TryGet(string code, [NotNullWhen(true)] out Country? country) =>
        _byCode.TryGetValue(code, out country);

    public ValidationResult ValidateByCode(string code, [NotNullWhen(true)] out Country? country)
    {
        return TryGet(code, out country) ? ValidationResult.Success() : ValidationResult.Failure($"Country code '{code}' is not a valid ISO 3166-1 code.");
    }

    public ValidationResult ValidateByName(string name, [NotNullWhen(true)] out Country? country)
    {
        country = FindByName(name);
        return country is not null ? ValidationResult.Success() : ValidationResult.Failure($"Country name '{name}' was not found in the ISO 3166-1 database.");
    }

    public IReadOnlyList<Country> GetAll() => _allCountries;
}