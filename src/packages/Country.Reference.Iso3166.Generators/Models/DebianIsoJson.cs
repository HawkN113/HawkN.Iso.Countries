namespace Country.Reference.Iso3166.Generators.Models;

internal class DebianIsoJson
{
    [System.Text.Json.Serialization.JsonPropertyName("3166-1")]
    public List<DebianCountryEntry> Countries { get; set; } = [];
}