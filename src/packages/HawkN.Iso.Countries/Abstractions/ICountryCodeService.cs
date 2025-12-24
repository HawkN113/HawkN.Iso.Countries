using System.Diagnostics.CodeAnalysis;
using HawkN.Iso.Countries.Models;

namespace HawkN.Iso.Countries.Abstractions;
/// <summary>
/// Provides a unified service for validating, retrieving, and searching ISO 3166-1 country data.
/// Supports Alpha-2, Alpha-3 codes, common names, and official names.
/// </summary>
public interface ICountryCodeService
{
    /// <summary>
    /// Retrieves a country by any valid ISO 3166-1 code (Alpha-2, Alpha-3 string).
    /// </summary>
    /// <param name="code">The code string (e.g., "US", "USA", or "840"). Case-insensitive.</param>
    /// <returns>A <see cref="Country"/> instance if found; otherwise, <see langword="null"/>.</returns>
    HawkN.Iso.Countries.Models.Country? FindByCode(string code);

    /// <summary>
    /// Retrieves a country by its common name or official ISO 3166-1 name.
    /// </summary>
    /// <param name="name">The common or official name (e.g., "South Korea" or "Republic of Korea"). Case-insensitive.</param>
    /// <returns>A <see cref="Country"/> instance if found; otherwise, <see langword="null"/>.</returns>
    HawkN.Iso.Countries.Models.Country? FindByName(string name);

    /// <summary>
    /// Performs a partial match search across common and official country names.
    /// Useful for autocomplete or filtering UI components.
    /// </summary>
    /// <param name="query">The search term (e.g., "United" or "Republic").</param>
    /// <returns>A collection of countries sorted by relevance (starts-with matches first).</returns>
    IEnumerable<HawkN.Iso.Countries.Models.Country> SearchByName(string query);

    /// <summary>
    /// Gets a country using its strictly typed Alpha-2 enum.
    /// </summary>
    /// <param name="code">The <see cref="CountryCode.TwoLetterCode"/> enum value.</param>
    /// <returns>The corresponding <see cref="Country"/> instance.</returns>
    HawkN.Iso.Countries.Models.Country Get(CountryCode.TwoLetterCode code);

    /// <summary>
    /// Gets a country using its strictly typed Alpha-3 enum.
    /// </summary>
    /// <param name="code">The <see cref="CountryCode.ThreeLetterCode"/> enum value.</param>
    /// <returns>The corresponding <see cref="Country"/> instance.</returns>
    HawkN.Iso.Countries.Models.Country Get(CountryCode.ThreeLetterCode code);

    /// <summary>
    /// Gets a country using its Numeric integer code.
    /// </summary>
    /// <param name="numericCode">The numeric code (e.g., 840 for USA).</param>
    /// <returns>A <see cref="Country"/> instance if found; otherwise, <see langword="null"/>.</returns>
    HawkN.Iso.Countries.Models.Country? Get(int numericCode);

    /// <summary>
    /// Safely attempts to retrieve a country by any ISO code.
    /// </summary>
    /// <param name="code">The Alpha-2, Alpha-3, or Numeric code.</param>
    /// <param name="country">When this method returns, contains the country data if the code exists; otherwise, null.</param>
    /// <returns><see langword="true"/> if the country was found; otherwise, <see langword="false"/>.</returns>
    bool TryGet(string code, [NotNullWhen(true)] out HawkN.Iso.Countries.Models.Country? country);

    /// <summary>
    /// Validates a country code and returns a detailed result with the associated country data.
    /// </summary>
    /// <param name="code">The code to validate.</param>
    /// <param name="country">Contains the <see cref="Country"/> if validation succeeds.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating success or failure reasons.</returns>
    ValidationResult ValidateByCode(string code, [NotNullWhen(true)] out HawkN.Iso.Countries.Models.Country? country);

    /// <summary>
    /// Validates a country name (common or official) and returns a detailed result.
    /// </summary>
    /// <param name="name">The name to validate.</param>
    /// <param name="country">Contains the <see cref="Country"/> if validation succeeds.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating success or failure reasons.</returns>
    ValidationResult ValidateByName(string name, [NotNullWhen(true)] out HawkN.Iso.Countries.Models.Country? country);

    /// <summary>
    /// Returns all officially assigned ISO 3166-1 countries.
    /// </summary>
    /// <returns>A read-only list of all active countries.</returns>
    IReadOnlyList<HawkN.Iso.Countries.Models.Country> GetAll();
}