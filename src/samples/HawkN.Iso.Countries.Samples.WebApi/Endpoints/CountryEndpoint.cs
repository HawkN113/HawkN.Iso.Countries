using HawkN.Iso.Countries.Samples.WebApi.Handlers;
namespace HawkN.Iso.Countries.Samples.WebApi.Endpoints;

public static class CountryEndpoint
{
    public static IEndpointRouteBuilder MapCountryEndpoints(this IEndpointRouteBuilder app)
    {
        var countriesGroup = app.MapGroup("/api/countries");

        countriesGroup.MapGet("/", CountryHandler.GetAllCountries)
            .WithName("GetAllCountries")
            .WithOpenApi(o =>
            {
                o.Summary = "Get all active countries (Ordered by Name)";
                o.Description = "Returns the country list";
                return o;
            });

        countriesGroup.MapGet("/find/code/{code}", CountryHandler.FindCountryByCode)
            .WithName("FindCountryByCode")
            .WithOpenApi(o =>
            {
                o.Summary = "Use this for flexible search (works for 'AT', 'AUT', or '040')";
                o.Description = "Returns the country";
                return o;
            });

        countriesGroup.MapGet("/find/name/{name}", CountryHandler.FindCountryByName)
            .WithName("FindCountryByName")
            .WithOpenApi(o =>
            {
                o.Summary = "Get country by official name";
                o.Description = "Returns the country";
                return o;
            });

        countriesGroup.MapGet("/search", CountryHandler.SearchCountries)
            .WithName("SearchCountries")
            .WithOpenApi(o =>
            {
                o.Summary = "Search countries by name";
                o.Description = "Returns the list of country";
                return o;
            });

        countriesGroup.MapGet("/validate/code/{code}", CountryHandler.ValidateCountryByCode)
            .WithName("ValidateCountryByCode")
            .WithOpenApi(o =>
            {
                o.Summary = "Validate by country code";
                o.Description = "Returns the ValidationResult";
                return o;
            });

        countriesGroup.MapGet("/validate/name/{name}", CountryHandler.ValidateCountryByName)
            .WithName("ValidateCountryByName")
            .WithOpenApi(o =>
            {
                o.Summary = "Validate by country name";
                o.Description = "Returns the ValidationResult";
                return o;
            });

        return app;
    }
}