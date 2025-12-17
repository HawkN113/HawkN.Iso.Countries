using Country.Reference.Iso3166.Abstractions;
using Microsoft.AspNetCore.Mvc;
namespace Country.Reference.Iso3166.WebApi.Handlers;

public static class CountryHandler
{
    internal static IResult GetAllCountries([FromServices] ICountryCodeService service) =>
        Results.Ok(service.GetAll());

    internal static IResult FindCountryByCode([FromServices] ICountryCodeService service, [FromRoute(Name = "code")] string code)
    {
        service.TryGet(code, out var country);
        return country is null ? Results.NotFound() : Results.Json(country);
    }

    internal static IResult FindCountryByName([FromServices] ICountryCodeService service,
        [FromRoute(Name = "name")] string countryName)
    {
        service.ValidateByName(countryName, out var country);
        return country is null ? Results.NotFound() : Results.Json(country);
    }
    
    internal static IResult ValidateCountryByCode([FromServices] ICountryCodeService service,
        [FromRoute(Name = "code")] string countryCode)
    {
        var validationResult = service.ValidateByCode(countryCode, out _);
        return !validationResult.IsValid ? Results.BadRequest($"{validationResult.Reason}. Validation result: {validationResult.IsValid}") : Results.Ok($"Validation result: {validationResult.IsValid}");
    }
    
    internal static IResult ValidateCountryByName([FromServices] ICountryCodeService service,
        [FromRoute(Name = "name")] string countryName)
    {
        var validationResult = service.ValidateByName(countryName, out _);
        return !validationResult.IsValid ? Results.BadRequest($"{validationResult.Reason}. Validation result: {validationResult.IsValid}") : Results.Ok($"Validation result: {validationResult.IsValid}");
    }
}