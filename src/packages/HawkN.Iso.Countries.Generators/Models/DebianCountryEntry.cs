namespace HawkN.Iso.Countries.Generators.Models;

internal class DebianCountryEntry
{
    [System.Text.Json.Serialization.JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [System.Text.Json.Serialization.JsonPropertyName("official_name")]
    public string? OfficialName { get; set; }

    [System.Text.Json.Serialization.JsonPropertyName("alpha_2")]
    public string Alpha2 { get; set; } = string.Empty;

    [System.Text.Json.Serialization.JsonPropertyName("alpha_3")]
    public string Alpha3 { get; set; } = string.Empty;

    [System.Text.Json.Serialization.JsonPropertyName("numeric")]
    public string Numeric { get; set; } = string.Empty;
}