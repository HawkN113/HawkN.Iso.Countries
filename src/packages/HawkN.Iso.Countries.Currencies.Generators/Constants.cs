namespace HawkN.Iso.Countries.Currencies.Generators;

internal static class Constants
{
    public const string DefaultNamespace = "HawkN.Iso.Countries.Currencies";
    public const string DiagnosticsTitle = "Generator error";
    public const string ErrorMark = "#ERROR:";
    public const string GeneratorName = "HawkN.Iso.Countries.Currencies.Generators source generator";
    public const string ErrorPrefixName = "COUNTRY_CURRENCY_";
    public static readonly string[] ExtendedSourceData =
    [
        "Release: release-48",
        "CLDR URL: https://github.com/unicode-org/cldr",
    ];
    public static readonly string[] SystemNamespaces = [
        "System.Collections.Generic",
        "System.Collections.Immutable"
    ];
    public static readonly string[] ReferencesNamespaces =
    [
        "HawkN.Iso.Countries",
        "HawkN.Iso.Currencies",
        "HawkN.Iso.Countries.Currencies.Models"
    ];
}