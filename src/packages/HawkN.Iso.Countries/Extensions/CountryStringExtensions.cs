using HawkN.Iso.Countries.Abstractions;
using HawkN.Iso.Countries.Models;
namespace HawkN.Iso.Countries.Extensions;

/// <summary>
/// Provides fluent extension methods for string objects and Country models to facilitate ISO 3166-1 lookups and formatting.
/// </summary>
public static class CountryStringExtensions
{
    /// <summary>
    /// Converts a string code (Alpha-2, Alpha-3, or Numeric) to a Country model using the provided service.
    /// </summary>
    /// <returns>A <see cref="Country"/> or null if the code is invalid.</returns>
    public static Country? ToCountry(this string? code, ICountryCodeService service)
    {
        return string.IsNullOrWhiteSpace(code) ? null : service.FindByCode(code);
    }

    /// <summary>
    /// Validates a string as a country code and returns the result.
    /// </summary>
    public static ValidationResult ValidateAsCountryCode(this string? code, ICountryCodeService service, out Country? country)
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

    /// <summary>
    /// Converts the ISO 3166-1 Alpha-2 country code into a corresponding Unicode Emoji flag.
    /// </summary>
    /// <param name="country">The country model containing the Alpha-2 code.</param>
    /// <returns>
    /// A string representing the Emoji flag (e.g., "🇺🇸" for US). 
    /// Returns an empty string if the code is invalid or undefined.
    /// </returns>
    public static string GetEmojiFlag(this Country country)
    {
        var code = country.TwoLetterCode.ToString();

        if (string.IsNullOrEmpty(code) || code.Length != 2)
            return string.Empty;

        return string.Concat(code.ToUpperInvariant().Select(c =>
            char.ConvertFromUtf32(c + 0x1F1A5)));
    }
}