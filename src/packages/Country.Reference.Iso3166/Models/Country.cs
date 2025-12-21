namespace Country.Reference.Iso3166.Models;

/// <summary>
/// 
/// </summary>
/// <param name="Name">Country name (English)</param>
/// <param name="TwoLetterCode">ISO 3166-1 Alpha-2 (e.g., "AT")</param>
/// <param name="ThreeLetterCode">ISO 3166-1 Alpha-3 (e.g., "AUT")</param>
/// <param name="NumericCode">ISO 3166-1 Numeric (e.g., "840")</param>
/// <param name="OfficialName">ISO 3166-1 Official name (e.g., "Republic of Austria")</param>
public sealed record Country(
    string Name,
    CountryCode.TwoLetterCode TwoLetterCode,
    CountryCode.ThreeLetterCode ThreeLetterCode,
    string NumericCode,
    string? OfficialName);
