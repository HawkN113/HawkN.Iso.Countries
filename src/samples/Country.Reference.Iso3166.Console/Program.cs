using Country.Reference.Iso3166;
using Country.Reference.Iso3166.Abstractions;
using Country.Reference.Iso3166.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// ---------------------------------------------------------
// Initialize Host and Dependency Injection
// ---------------------------------------------------------
const string separator = "---------------------------------------------------------";

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        // Register Country Code Service via Extension Method
        services.AddCountryCodeService();
    })
    .Build();
try
{
    var container = host.Services;
    using var scope = container.CreateScope();
    // Retrieve the service instance from the DI container
    var countryCodeService = scope.ServiceProvider.GetRequiredService<ICountryCodeService>();

    Console.WriteLine(separator);
    Console.WriteLine("Data Retrieval (GetAll)");
    Console.WriteLine(separator);

    // Get all existing country codes
    var allCountries = countryCodeService!.GetAll();
    Console.WriteLine("| Alpha-2 | Alpha-3 | Numeric | Country Name |");
    Console.WriteLine("|:-------:|:-------:|:-------:|:-------------|");
    
    foreach (var country in allCountries)
    {
        Console.WriteLine(
                $"| {country.TwoLetterCode}      | {country.ThreeLetterCode}     | {country.NumericCode}     | {country.Name}      |");
    }
    Console.WriteLine(
        $"\tTotal countries found: {allCountries.Count}");

    Console.WriteLine(separator);
    Console.WriteLine("Lookup Operations (By Name, Code, and Enum)");
    Console.WriteLine(separator);
    var byName = countryCodeService.Get("Germany");
    Console.WriteLine($"\t[By Name 'Germany']   -> Code: {byName?.TwoLetterCode}, Numeric: {byName?.NumericCode}");

    // Lookup by any String Code (Alpha-2, Alpha-3, or Numeric M49)
    var byCode = countryCodeService.FindByCode("at"); // Austria
    Console.WriteLine($"\t[By Code 'at']        -> Name: {byCode?.Name}");

    // Lookup by Strongly Typed Enum (Generated at compile time)
    var byEnum = countryCodeService.Get(CountryCode.TwoLetterCode.FR);
    Console.WriteLine($"\t[By Enum 'FR']        -> Name: {byEnum.Name}");

    Console.WriteLine(separator);
    Console.WriteLine("Validation with Result Object");
    Console.WriteLine(separator);
    
    // Validating an existing Alpha-2 code
    var resOk = countryCodeService.ValidateByCode("US", out var usa);
    Console.WriteLine($"\t[Validate 'US']       -> IsValid: {resOk.IsValid}, Found: {usa?.Name}");

    // Validating a non-existent code
    var resFail = countryCodeService.ValidateByCode("XYZ", out _);
    if (!resFail.IsValid)
    {
        Console.WriteLine($"\t[Validate 'XYZ']      -> IsValid: {resFail.IsValid}, Reason: '{resFail.Reason}'");
    }

    // Validating by Name
    var resName = countryCodeService.ValidateByName("France", out _);
    Console.WriteLine($"\t[Validate 'France']   -> IsValid: {resName.IsValid}");
    
    Console.WriteLine(separator);
    Console.WriteLine("Fluent String Extensions (.ToCountry)");
    Console.WriteLine(separator);
    
    var inputCode = "GBR"; // United Kingdom
    
    // Check existence using extension method
    if (inputCode.IsCountryCode(countryCodeService))
    {
        // Direct conversion from string to Model
        var country = inputCode.ToCountry(countryCodeService);
        Console.WriteLine($"\t[Extension method]    -> Input '{inputCode}' resolved to: {country?.Name}");
    }

    await host.RunAsync();
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}