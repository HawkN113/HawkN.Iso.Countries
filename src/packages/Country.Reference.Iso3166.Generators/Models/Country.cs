namespace Country.Reference.Iso3166.Generators.Models;

public class Country
{
    /// <summary>
    /// Country name (English)
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// ISO 3166-1 Alpha-2
    /// </summary>
    public string CodeAlpha2 { get; set; }

    /// <summary>
    /// 3166-1 Alpha-3
    /// </summary>
    public string CodeAlpha3 { get; set; }

    /// <summary>
    /// ISO 3166-1 Numeric
    /// </summary>
    public string NumericCode { get; set; }
}