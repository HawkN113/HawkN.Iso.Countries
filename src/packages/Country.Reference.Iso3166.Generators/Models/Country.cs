namespace Country.Reference.Iso3166.Generators.Models;

public class Country(string name, string codeAlpha2, string codeAlpha3, string numericCode)
{
    /// <summary>
    /// Country name (English)
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// ISO 3166-1 Alpha-2
    /// </summary>
    public string CodeAlpha2 { get; } = codeAlpha2;

    /// <summary>
    /// 3166-1 Alpha-3
    /// </summary>
    public string CodeAlpha3 { get; } = codeAlpha3;

    /// <summary>
    /// ISO 3166-1 Numeric
    /// </summary>
    public string NumericCode { get; } = numericCode;
}