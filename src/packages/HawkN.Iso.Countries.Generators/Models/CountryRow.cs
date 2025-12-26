namespace HawkN.Iso.Countries.Generators.Models;

/// <summary>
/// Represents a single row from UN M49 CSV file.
/// Generator-only DTO.
/// </summary>
internal sealed class CountryRow(
    ReadOnlyMemory<char> name,
    ReadOnlyMemory<char> codeAlpha2,
    ReadOnlyMemory<char> codeAlpha3,
    int numericCode)
{
    /// <summary>
    /// Country name (English)
    /// </summary>
    public string Name { get; } = name.ToString();

    /// <summary>
    /// ISO 3166-1 Alpha-2
    /// </summary>
    public string CodeAlpha2 { get; } = codeAlpha2.ToString();

    /// <summary>
    /// 3166-1 Alpha-3
    /// </summary>
    public string CodeAlpha3 { get; } = codeAlpha3.ToString();

    /// <summary>
    /// ISO 3166-1 Numeric
    /// </summary>
    public int NumericCode { get; } = numericCode;
}