using System.Text.Json;
using HawkN.Iso.Countries;
using HawkN.Iso.Countries.Abstractions;
using HawkN.Iso.Countries.Extensions;
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
    Console.OutputEncoding = System.Text.Encoding.UTF8;

    var container = host.Services;
    using var scope = container.CreateScope();
    var countryCodeService = scope.ServiceProvider.GetRequiredService<ICountryCodeService>();

    Console.WriteLine(separator);
    Console.WriteLine("1. Data Retrieval (GetAll)");
    Console.WriteLine(separator);

    var allCountries = countryCodeService.GetAll();
    Console.WriteLine("| Alpha-2 | Alpha-3 | Numeric | Country Name | Official Name |");
    Console.WriteLine("|:-------:|:-------:|:-------:|:-------------|:-------------|");

    foreach (var country in allCountries)
    {
        Console.WriteLine($"| {country.TwoLetterCode,-7} | {country.ThreeLetterCode,-7} | {country.NumericCode,-7} | {country.Name,-12} | {country.OfficialName,-12} |");
    }
    Console.WriteLine($"Total countries found: {allCountries.Count}");

    Console.WriteLine(separator);
    Console.WriteLine("2. Lookup Operations (Find & Get)");
    Console.WriteLine(separator);

    // Exact name lookup (now using FindByName)
    var byName = countryCodeService.FindByName("Germany");
    Console.WriteLine($"[FindByName 'Germany'] -> Code: {byName?.TwoLetterCode}, Numeric: {byName?.NumericCode}");

    // Lookup by any String Code (Alpha-2, Alpha-3, or Numeric M49 string)
    var byCode = countryCodeService.FindByCode("at");
    Console.WriteLine($"[FindByCode 'at']      -> Name: {byCode?.Name}");

    // Lookup by Numeric Integer (New in interface)
    var byInt = countryCodeService.Get(840);
    Console.WriteLine($"[Get by Int 840]       -> Name: {byInt?.Name}");

    // Strongly Typed Enum lookups
    var byEnum = countryCodeService.Get(CountryCode.TwoLetterCode.FR);
    Console.WriteLine($"[Get by Enum FR]       -> Name: {byEnum.Name}");

    Console.WriteLine(separator);
    Console.WriteLine("3. Smart Search (SearchByName)");
    Console.WriteLine(separator);

    // Partial match search (Autocomplete simulation)
    var searchResults = countryCodeService.SearchByName("uni");
    var enumerable = searchResults as HawkN.Iso.Countries.Models.Country[] ?? searchResults.ToArray();
    Console.WriteLine($"Search for 'uni' found {enumerable.Length} matches:");
    foreach (var match in enumerable)
    {
        Console.WriteLine($" - {match.Name} ({match.TwoLetterCode})");
    }

    Console.WriteLine(separator);
    Console.WriteLine("4. Validation with Result Object");
    Console.WriteLine(separator);

    // Validating Alpha-2 code
    var resOk = countryCodeService.ValidateByCode("US", out var usa);
    Console.WriteLine($"[Validate 'US']        -> IsValid: {resOk.IsValid}, Found: {usa?.Name}");

    // Validating a non-existent code
    var resFail = countryCodeService.ValidateByCode("XYZ", out _);
    if (!resFail.IsValid)
    {
        Console.WriteLine($"[Validate 'XYZ']       -> IsValid: {resFail.IsValid}, Reason: '{resFail.Reason}'");
    }

    Console.WriteLine(separator);
    Console.WriteLine("5. Fluent Extensions & Safety");
    Console.WriteLine(separator);

    const string inputCode = "GBR";

    // Using TryGet for safe retrieval
    if (countryCodeService.TryGet(inputCode, out var gbr))
    {
        Console.WriteLine($"[TryGet '{inputCode}']   -> Success: {gbr.Name}");
    }

    // Direct conversion using extension method
    var countryExt = inputCode.ToCountry(countryCodeService);
    Console.WriteLine($"[Extension method]     -> Resolved to: {countryExt?.Name}");

    // Scenario: Quick search with visual feedback
    var searchCountryResults = countryCodeService.SearchByName("United");
    foreach (var c in searchCountryResults)
    {
        Console.WriteLine($"[Emoji flag method]     -> {c.GetEmojiFlag()} {c.Name}");
    }
}
catch (JsonException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}