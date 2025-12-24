using HawkN.Iso.Countries.Abstractions;
using HawkN.Iso.Countries.Extensions;
using HawkN.Iso.Countries.Services;
using Microsoft.Extensions.DependencyInjection;
namespace HawkN.Iso.Countries.Tests.Extensions;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddCountryCodeService_RegistersServiceAsSingleton()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddCountryCodeService();
        var serviceDescriptor = services.FirstOrDefault(d => d.ServiceType == typeof(ICountryCodeService));

        // Assert
        Assert.NotNull(serviceDescriptor);
        Assert.Equal(ServiceLifetime.Singleton, serviceDescriptor.Lifetime);
        Assert.Equal(typeof(CountryCodeService), serviceDescriptor.ImplementationType);
    }

    [Fact]
    public void AddCountryCodeService_CanResolveServiceFromProvider()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddCountryCodeService();
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var service = serviceProvider.GetService<ICountryCodeService>();

        // Assert
        Assert.NotNull(service);
        Assert.IsType<CountryCodeService>(service);
    }

    [Fact]
    public void AddCountryCodeService_CalledMultipleTimes_RegistersOnlyOnce()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddCountryCodeService();
        services.AddCountryCodeService(); // Повторный вызов

        // Assert
        var registrations = services.Where(d => d.ServiceType == typeof(ICountryCodeService)).ToList();

        // TryAddSingleton гарантирует, что регистрация будет только одна
        Assert.Single(registrations);
    }
}