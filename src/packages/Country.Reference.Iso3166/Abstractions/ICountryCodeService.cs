using Country.Reference.Iso3166.Models;

namespace Country.Reference.Iso3166.Abstractions;

/// <summary>
/// Service for working with country codes: validation, retrieval, and querying.
/// </summary>
public interface ICountryCodeService
{
    /// <summary>
    /// Tries to validate the specified string against any known country code (Alpha-2, Alpha-3, M49).
    /// </summary>
    bool TryValidateByCode(string code, out ValidationResult result);
    
    /// <summary>
    /// Tries to validate the specified string against any known country name.
    /// </summary>
    bool TryValidateByName(string name, out ValidationResult result);

    /// <summary>
    /// Gets current (active) country information by code (Alpha-2, Alpha-3, or M49 numeric string).
    /// Case-insensitive.
    /// </summary>
    Models.Country? GetByCode(string code);
    
    /// <summary>
    /// Gets current (active) country information by name.
    /// Case-insensitive.
    /// </summary>
    Models.Country? GetByName(string name);
    
    /// <summary>
    /// Gets current (active) country information by its strictly typed Alpha-2 code.
    /// </summary>
    Models.Country Get(CountryCode.TwoLetterCode code);
    
    /// <summary>
    /// Gets current (active) country information by its strictly typed Alpha-3 code.
    /// </summary>
    Models.Country Get(CountryCode.ThreeLetterCode code);
    
    /// <summary>
    /// Get all actual countries defined in the reference.
    /// </summary>
    /// <returns></returns>
    IReadOnlyList<Models.Country> GetAll();
}