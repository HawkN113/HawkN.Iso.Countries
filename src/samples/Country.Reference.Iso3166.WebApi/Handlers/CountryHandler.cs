using Country.Reference.Iso3166.Abstractions;
using Microsoft.AspNetCore.Mvc;
namespace Country.Reference.Iso3166.WebApi.Handlers;

/// <summary>
/// Handles HTTP requests for ISO 3166-1 country data.
/// </summary>
public static class CountryHandler
{
    /// <summary>
    /// Retrieves a complete list of all active ISO 3166-1 countries.
    /// </summary>
    /// <param name="service">The country code service instance.</param>
    /// <returns>A collection of all available countries.</returns>
    internal static IResult GetAllCountries(ICountryCodeService service) =>
        Results.Ok(service.GetAll());

    /// <summary>
    /// Retrieves a specific country by any valid ISO code (Alpha-2, Alpha-3, or Numeric).
    /// </summary>
    /// <param name="service">The country code service instance.</param>
    /// <param name="code">The code string (e.g., "US", "USA", or "840").</param>
    /// <returns>A country object if found; otherwise, a 404 NotFound response.</returns>
    internal static IResult FindCountryByCode(ICountryCodeService service, [FromRoute] string code)
    {
        return service.TryGet(code, out var country) 
            ? Results.Ok(country) 
            : Results.NotFound(new { Message = $"Country with code '{code}' not found." });
    }

    /// <summary>
    /// Retrieves a country by its exact official or short name.
    /// </summary>
    /// <param name="service">The country code service instance.</param>
    /// <param name="name">The exact name of the country (case-insensitive).</param>
    /// <returns>A country object if found; otherwise, a 404 NotFound response.</returns>
    internal static IResult FindCountryByName(ICountryCodeService service, [FromRoute] string name)
    {
        var country = service.FindByName(name);
        return country is not null 
            ? Results.Ok(country) 
            : Results.NotFound(new { Message = $"Country with name '{name}' not found." });
    }

    /// <summary>
    /// Performs a partial search across country names for autocomplete and suggestions.
    /// </summary>
    /// <param name="service">The country code service instance.</param>
    /// <param name="query">The search term (e.g., "uni").</param>
    /// <returns>A list of countries matching the search criteria.</returns>
    internal static IResult SearchCountries(ICountryCodeService service, [FromQuery] string query)
    {
        var results = service.SearchByName(query);
        return Results.Ok(results);
    }

    /// <summary>
    /// Validates a country code and returns detailed feedback along with the country data.
    /// </summary>
    /// <param name="service">The country code service instance.</param>
    /// <param name="code">The Alpha-2, Alpha-3, or Numeric code to validate.</param>
    /// <returns>A validation result indicating success or failure reasons.</returns>
    internal static IResult ValidateCountryByCode(ICountryCodeService service, [FromRoute] string code)
    {
        var result = service.ValidateByCode(code, out var country);
        return result.IsValid 
            ? Results.Ok(new { Valid = true, Country = country }) 
            : Results.BadRequest(new { Valid = false, Message = result.Reason });
    }

    /// <summary>
    /// Validates a country name and returns detailed feedback along with the country data.
    /// </summary>
    /// <param name="service">The country code service instance.</param>
    /// <param name="name">The official country name to validate.</param>
    /// <returns>A validation result indicating success or failure reasons.</returns>
    internal static IResult ValidateCountryByName(ICountryCodeService service, [FromRoute] string name)
    {
        var result = service.ValidateByName(name, out var country);
        return result.IsValid 
            ? Results.Ok(new { Valid = true, Country = country }) 
            : Results.BadRequest(new { Valid = false, Message = result.Reason });
    }
}