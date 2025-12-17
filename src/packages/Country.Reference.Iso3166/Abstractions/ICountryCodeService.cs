using System.Diagnostics.CodeAnalysis;
using Country.Reference.Iso3166.Models;
namespace Country.Reference.Iso3166.Abstractions;

/// <summary>
/// Service for working with ISO 3166-1 country codes: validation, retrieval, and querying.
/// </summary>
public interface ICountryCodeService
{
    /// <summary>
    /// Gets a country by any valid ISO code (Alpha-2, Alpha-3, or Numeric M49).
    /// Case-insensitive.
    /// </summary>
    /// <param name="code">The Alpha-2, Alpha-3, or Numeric M49 code.</param>
    /// <returns>A <see cref="Country"/> instance or null if not found.</returns>
    Models.Country? FindByCode(string code);

    /// <summary>
    /// Gets a country using its strictly typed Alpha-2 code.
    /// </summary>
    Models.Country Get(CountryCode.TwoLetterCode code);

    /// <summary>
    /// Gets a country using its strictly typed Alpha-3 code.
    /// </summary>
    Models.Country Get(CountryCode.ThreeLetterCode code);

    /// <summary>
    /// Finds a country by its official name. Case-insensitive.
    /// </summary>
    /// <param name="name">The country name.</param>
    /// <returns>A <see cref="Country"/> instance or null if not found.</returns>
    Models.Country? Get(string name);

    /// <summary>
    /// Attempts to retrieve a country by its code. 
    /// Returns true if the country exists; otherwise, false.
    /// </summary>
    bool TryGet(string code, [NotNullWhen(true)] out Models.Country? country);

    /// <summary>
    /// Validates if a country code exists and provides detailed feedback.
    /// </summary>
    /// <param name="code">The Alpha-2, Alpha-3, or Numeric M49 code to validate.</param>
    /// <param name="country">When this method returns, contains the country data if valid; otherwise, null.</param>
    ValidationResult ValidateByCode(string code, [NotNullWhen(true)] out Models.Country? country);

    /// <summary>
    /// Validates if a country exists by its name and provides detailed feedback.
    /// </summary>
    /// <param name="name">The official country name to validate.</param>
    /// <param name="country">When this method returns, contains the country data if valid; otherwise, null.</param>
    ValidationResult ValidateByName(string name, [NotNullWhen(true)] out Models.Country? country);

    /// <summary>
    /// Retrieves all active countries defined in the ISO 3166-1 standard.
    /// Includes only officially assigned ISO 3166-1 entries.
    /// </summary>
    IReadOnlyList<Models.Country> GetAll();
}