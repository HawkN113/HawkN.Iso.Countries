using System.Diagnostics.CodeAnalysis;
using Country.Reference.Iso3166.Abstractions;
using Country.Reference.Iso3166.Models;
namespace Country.Reference.Iso3166.Services;

internal sealed class CountryCodeService : ICountryCodeService
{
    private readonly Dictionary<string, Models.Country> _byCode = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<CountryCode.TwoLetterCode, Models.Country> _byAlpha2 = [];
    private readonly Dictionary<CountryCode.ThreeLetterCode, Models.Country> _byAlpha3 = [];
    private static readonly Dictionary<string, Models.Country> _byName = new(StringComparer.OrdinalIgnoreCase);
    private readonly IReadOnlyList<Models.Country> _allCountries;

    public CountryCodeService()
    {
        _allCountries = LocalCountryDatabase.ActualCountries
            .OrderBy(c => c.Name)
            .ToList()
            .AsReadOnly();

        foreach (var country in _allCountries)
        {
            _byCode[country.TwoLetterCode.ToString()] = country;
            _byCode[country.ThreeLetterCode.ToString()] = country;
            _byCode[country.NumericCode] = country;
            _byName[country.Name] = country;

            _byAlpha2[country.TwoLetterCode] = country;
            _byAlpha3[country.ThreeLetterCode] = country;
        }
    }

    public Models.Country? FindByCode(string code)
    {
        return string.IsNullOrWhiteSpace(code) ? null : _byCode.GetValueOrDefault(code.Trim());
    }

    public Models.Country Get(CountryCode.TwoLetterCode code)
    {
        return _byAlpha2[code];
    }

    public Models.Country Get(CountryCode.ThreeLetterCode code)
    {
        return _byAlpha3[code];
    }

    public Models.Country? Get(string name)
    {
        return string.IsNullOrWhiteSpace(name) ? null : _byName.GetValueOrDefault(name.Trim());
    }

    public bool TryGet(string code, [NotNullWhen(true)] out Models.Country? country)
    {
        country = FindByCode(code);
        return country is not null;
    }

    public ValidationResult ValidateByCode(string code, [NotNullWhen(true)] out Models.Country? country)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            country = null;
            return ValidationResult.Invalid("Code is required..", ValidationType.Code);
        }

        country = FindByCode(code);

        return country is not null
            ? ValidationResult.Success()
            : ValidationResult.Invalid($"The country code '{code}' does not exist.", ValidationType.Code);
    }

    public ValidationResult ValidateByName(string name, [NotNullWhen(true)] out Models.Country? country)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            country = null;
            return ValidationResult.Invalid("Name is required.", ValidationType.Code);
        }

        country = Get(name);
        return country is not null
            ? ValidationResult.Success()
            : ValidationResult.Invalid($"Country with name '{name}' was not found.", ValidationType.Value);
    }

    public IReadOnlyList<Models.Country> GetAll() => _allCountries;
}