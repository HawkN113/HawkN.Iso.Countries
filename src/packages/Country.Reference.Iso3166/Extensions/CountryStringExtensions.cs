using Country.Reference.Iso3166.Abstractions;
using Country.Reference.Iso3166.Models;
namespace Country.Reference.Iso3166.Extensions;

/// <summary>
/// 
/// </summary>
public static class CountryStringExtensions
{
    /// <summary>
    /// Converts a string code (Alpha-2, Alpha-3, or Numeric) to a Country model using the provided service.
    /// </summary>
    /// <returns>A <see cref="Models.Country"/> or null if the code is invalid.</returns>
    public static Models.Country? ToCountry(this string? code, ICountryCodeService service)
    {
        return string.IsNullOrWhiteSpace(code) ? null : service.FindByCode(code);
    }

    /// <summary>
    /// Validates a string as a country code and returns the result.
    /// </summary>
    public static ValidationResult ValidateAsCountryCode(this string? code, ICountryCodeService service, out Models.Country? country)
    {
        if (!string.IsNullOrWhiteSpace(code)) return service.ValidateByCode(code, out country);
        country = null;
        return ValidationResult.Failure("Code is required.", ValidationType.Code);
    }

    /// <summary>
    /// Checks if the string is a valid ISO 3166-1 country code.
    /// </summary>
    public static bool IsCountryCode(this string? code, ICountryCodeService service)
    {
        return service.TryGet(code ?? string.Empty, out _);
    }
}