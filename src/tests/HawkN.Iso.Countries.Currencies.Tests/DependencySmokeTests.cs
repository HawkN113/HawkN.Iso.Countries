using System.Reflection;
using HawkN.Iso.Countries.Currencies.Extensions;
using HawkN.Iso.Currencies;
namespace HawkN.Iso.Countries.Currencies.Tests;

public sealed class DependencySmokeTests
{
    [Fact]
    public void Countries_Package_Version_Should_Be_In_Expected_Range()
    {
        // Arrange & Act 
        var assembly = typeof(CountryCode).Assembly;

        var version = GetInformationalVersion(assembly);

        // Assert
        Assert.True(
            version >= new Version(8, 0, 0) && version < new Version(9, 0, 0),
            $"Unexpected HawkN.Iso.Countries version: {version}"
        );
    }

    [Fact]
    public void Currencies_Package_Version_Should_Be_In_Expected_Range()
    {
        // Arrange & Act
        var assembly = typeof(CurrencyCode).Assembly;
        var version = GetInformationalVersion(assembly);

        // Assert
        Assert.True(
            version >= new Version(8, 0, 0) && version < new Version(9, 0, 0),
            $"Unexpected HawkN.Iso.Currencies version: {version}"
        );
    }

    [Fact]
    public void All_Required_Dependencies_Should_Be_Loaded()
    {
        // Arrange & Act
        var loadedAssemblies = AppDomain.CurrentDomain
            .GetAssemblies()
            .Select(a => a.GetName().Name)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        // Assert
        Assert.Contains("HawkN.Iso.Countries", loadedAssemblies);
        Assert.Contains("HawkN.Iso.Currencies", loadedAssemblies);
    }

    [Fact]
    public void CountryCurrencyMap_Should_Initialize_Without_Errors()
    {
        // Arrange
        var country = CountryCode.TwoLetterCode.DE;

        // Act
        var currency = country.GetPrimaryCurrency();

        // Assert
        Assert.NotNull(currency);
    }
        
    private static Version GetInformationalVersion(Assembly assembly)
    {
        var attr = assembly
            .GetCustomAttribute<AssemblyFileVersionAttribute>();
        Assert.NotNull(attr);
        var versionText = attr!.Version.Split('+')[0];
        return Version.Parse(versionText);
    }
}