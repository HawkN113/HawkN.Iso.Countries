using Country.Reference.Iso3166.WebApi.Handlers;

namespace Country.Reference.Iso3166.WebApi.Endpoints;

public static class CountryEndpoint
{
    public static IEndpointRouteBuilder MapCountryEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/countries", CountryHandler.GetAllCountries)
            .WithName("GetAllCountries")
            .WithOpenApi(o =>
            {
                o.Summary = "Get all active countries (Ordered by Name)";
                o.Description = "Returns the country list";
                return o;
            });

        app.MapGet("/api/countries/find/code/{code}", CountryHandler.FindCountryByCode)
            .WithName("FindCountryByCode")
            .WithOpenApi(o =>
            {
                o.Summary = "Use this for flexible search (works for 'AT', 'AUT', or '040')";
                o.Description = "Returns the country";
                return o;
            });

        app.MapGet("/api/countries/find/name/{name}", CountryHandler.FindCountryByName)
            .WithName("FindCountryByName")
            .WithOpenApi(o =>
            {
                o.Summary = "Get country by official name";
                o.Description = "Returns the country";
                return o;
            });

        app.MapGet("/api/countries/validate/code/{code}", CountryHandler.ValidateCountryByCode)
            .WithName("ValidateCountryByCode")
            .WithOpenApi(o =>
            {
                o.Summary = "Validate by country code";
                o.Description = "Returns the ValidationResult";
                return o;
            });

        app.MapGet("/api/countries/validate/name/{name}", CountryHandler.ValidateCountryByName)
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