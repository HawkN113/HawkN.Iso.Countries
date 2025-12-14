using Country.Reference.Iso3166.Abstractions;
using Country.Reference.Iso3166.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        // ---- Register Country code service ----
        services.AddCountryCodeService();
    })
    .Build();
try
{
    var container = host.Services;
    using var scope = container.CreateScope();
    // ---- Retrieve Country code service instance ----
    var countryCodeService = scope.ServiceProvider.GetRequiredService<ICountryCodeService>();

    // -----------------
    // ---- Queries ----
    // -----------------

    Console.WriteLine(" ---- Queries ---- ");

    // ---- Get all existing country codes ----
    Console.WriteLine(" ---- All existing country codes ---- ");
    var actual = countryCodeService!.GetAll();
    foreach (var currency in actual)
    {
        Console.WriteLine(
            $"\t{currency.Name} - {currency.TwoLetterCode} - {currency.ThreeLetterCode}");
    }
    Console.WriteLine(
        $"\tFound {actual.Count} countries");

    // -------------------------
    // ---- Lookup countries ----
    // -------------------------

    Console.WriteLine(" ---- Lookup country code ---- ");
    Console.WriteLine(
        $"\tLookup by name 'Germany': {countryCodeService.GetByName("Germany")!.TwoLetterCode}");
    Console.WriteLine(
        $"\tLookup by country code 'DE': {countryCodeService.GetByCode("de")!.Name}");

    // --------------------
    // ---- Validation ----
    // --------------------

    Console.WriteLine(" ---- Validation ----");
    countryCodeService.TryValidateByCode("AT", out var invalidateResult);
    Console.WriteLine(
        $"\tValidation code 'AT': {invalidateResult.Reason}, validation status: {invalidateResult.IsValid}");
    countryCodeService.TryValidateByCode("DDD", out var validateResult);
    Console.WriteLine(
        $"\tValidation code 'DDD': {validateResult.Reason}, validation status: {validateResult.IsValid}");

    await host.RunAsync();
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}