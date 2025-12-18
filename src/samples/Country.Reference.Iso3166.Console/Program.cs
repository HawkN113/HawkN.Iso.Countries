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
    var countryCodeService = scope.ServiceProvider.GetRequiredService<ICountryCodeService>();

    Console.WriteLine(separator);
    Console.WriteLine("1. Data Retrieval (GetAll)");
    Console.WriteLine(separator);

    var allCountries = countryCodeService.GetAll();
    Console.WriteLine("\t| Alpha-2 | Alpha-3 | Numeric | Country Name |");
    Console.WriteLine("\t|:-------:|:-------:|:-------:|:-------------|");

    foreach (var country in allCountries)
    {
        Console.WriteLine($"\t| {country.TwoLetterCode,-7} | {country.ThreeLetterCode,-7} | {country.NumericCode,-7} | {country.Name,-12} |");
    }
    Console.WriteLine($"\tTotal countries found: {allCountries.Count}");

    Console.WriteLine(separator);
    Console.WriteLine("2. Lookup Operations (Find & Get)");
    Console.WriteLine(separator);

    // Exact name lookup (now using FindByName)
    var byName = countryCodeService.FindByName("Germany");
    Console.WriteLine($"\t[FindByName 'Germany'] -> Code: {byName?.TwoLetterCode}, Numeric: {byName?.NumericCode}");

    // Lookup by any String Code (Alpha-2, Alpha-3, or Numeric M49 string)
    var byCode = countryCodeService.FindByCode("at");
    Console.WriteLine($"\t[FindByCode 'at']      -> Name: {byCode?.Name}");

    // Lookup by Numeric Integer (New in interface)
    var byInt = countryCodeService.Get(840);
    Console.WriteLine($"\t[Get by Int 840]       -> Name: {byInt?.Name}");

    // Strongly Typed Enum lookups
    var byEnum = countryCodeService.Get(CountryCode.TwoLetterCode.FR);
    Console.WriteLine($"\t[Get by Enum FR]       -> Name: {byEnum.Name}");

    Console.WriteLine(separator);
    Console.WriteLine("3. Smart Search (SearchByName)");
    Console.WriteLine(separator);

    // Partial match search (Autocomplete simulation)
    var searchResults = countryCodeService.SearchByName("uni");
    Console.WriteLine($"\tSearch for 'uni' found {searchResults.Count()} matches:");
    foreach (var match in searchResults)
    {
        Console.WriteLine($"\t  - {match.Name} ({match.TwoLetterCode})");
    }

    Console.WriteLine(separator);
    Console.WriteLine("4. Validation with Result Object");
    Console.WriteLine(separator);

    // Validating Alpha-2 code
    var resOk = countryCodeService.ValidateByCode("US", out var usa);
    Console.WriteLine($"\t[Validate 'US']        -> IsValid: {resOk.IsValid}, Found: {usa?.Name}");

    // Validating a non-existent code
    var resFail = countryCodeService.ValidateByCode("XYZ", out _);
    if (!resFail.IsValid)
    {
        Console.WriteLine($"\t[Validate 'XYZ']       -> IsValid: {resFail.IsValid}, Reason: '{resFail.Reason}'");
    }

    Console.WriteLine(separator);
    Console.WriteLine("5. Fluent Extensions & Safety");
    Console.WriteLine(separator);

    const string inputCode = "GBR";

    // Using TryGet for safe retrieval
    if (countryCodeService.TryGet(inputCode, out var gbr))
    {
        Console.WriteLine($"\t[TryGet '{inputCode}']   -> Success: {gbr.Name}");
    }

    // Direct conversion using extension method
    var countryExt = inputCode.ToCountry(countryCodeService);
    Console.WriteLine($"\t[Extension method]     -> Resolved to: {countryExt?.Name}");

    Console.WriteLine(separator);
    Console.WriteLine("Console Example Finished.");
    Console.WriteLine(separator);
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}