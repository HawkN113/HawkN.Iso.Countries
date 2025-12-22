using HawkN.Iso.Countries.Abstractions;
using HawkN.Iso.Countries.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
namespace HawkN.Iso.Countries.Extensions;

/// <summary>
/// Extension methods for registering country-code-related services into an <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers <see cref="ICountryCodeService"/> with its implementation <see cref="CountryCodeService"/>
    /// as a singleton in the dependency injection container.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddCountryCodeService(this IServiceCollection services)
    {
        services.TryAddSingleton<ICountryCodeService, CountryCodeService>();
        return services;
    }
}