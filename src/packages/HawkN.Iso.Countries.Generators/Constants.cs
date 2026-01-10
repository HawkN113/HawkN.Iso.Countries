namespace HawkN.Iso.Countries.Generators;

internal static class Constants
{
    public const string DefaultNamespace = "HawkN.Iso.Countries";
    public const string DiagnosticsTitle = "Generator error";
    public const string ErrorMark = "#ERROR:";
    public const string GeneratorName = "HawkN.Iso.Countries.Generators source generator";
    public static readonly string[] ExtendedSourceData =
    [
        "Source URL: https://unstats.un.org/unsd/methodology/m49/overview"
    ];
    public static readonly string[] SystemNamespaces = [
        "System.Collections.Generic",
        "System.Collections.Immutable"
    ];
    public static readonly string[] ReferencesNamespaces =
    [
        "HawkN.Iso.Countries.Models"
    ];
}