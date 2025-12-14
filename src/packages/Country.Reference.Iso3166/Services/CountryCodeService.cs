using Country.Reference.Iso3166.Abstractions;
using Country.Reference.Iso3166.Models;

namespace Country.Reference.Iso3166.Services;

internal sealed class CountryCodeService : ICountryCodeService
{
    private readonly IReadOnlyList<Models.Country> _countries = LocalCountryDatabase.ActualCountries;

    public bool TryValidateByCode(string code, out ValidationResult result)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            result = ValidationResult.Invalid("Code is null or empty.", ValidationType.Value);
            return false;
        }

        var existingCountry = GetByCode(code);
        result = existingCountry is not null
            ? ValidationResult.Success()
            : ValidationResult.Invalid(
                $"The country with code (TwoLetterCode or ThreeLetterCode) '{code}' does not exist");
        return existingCountry is not null && !string.IsNullOrWhiteSpace(existingCountry.Name);
    }

    public bool TryValidateByName(string name, out ValidationResult result)
    {
        throw new NotImplementedException();
    }

    public Models.Country? GetByCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return null;
        }

        return _countries.FirstOrDefault(c =>
            code.Contains(c.TwoLetterCode.ToString(), StringComparison.OrdinalIgnoreCase) ||
            code.Contains(c.ThreeLetterCode.ToString(), StringComparison.OrdinalIgnoreCase));
    }

    public Models.Country? GetByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return null;
        }

        return _countries.FirstOrDefault(c =>
            name.Contains(c.Name.ToString(), StringComparison.OrdinalIgnoreCase));
    }

    public Models.Country Get(CountryCode.TwoLetterCode code)
    {
        return _countries.First(c => c.TwoLetterCode == code);
    }

    public Models.Country Get(CountryCode.ThreeLetterCode code)
    {
        return _countries.First(c => c.ThreeLetterCode == code);
    }

    public IReadOnlyList<Models.Country> GetAll() => _countries.OrderBy(c => c.Name).ToList().AsReadOnly();
}