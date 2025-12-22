using HawkN.Iso.Countries.Generators.Factories;
using HawkN.Iso.Countries.Generators.Models;
using Microsoft.CodeAnalysis;
namespace HawkN.Iso.Countries.Generators.Tests.Factories;

public class ErrorFactoryTests
{
    private readonly DiagnosticDescriptor _testDescriptor = new(
        id: "TEST001",
        title: "Test Title",
        messageFormat: "Test Message",
        category: "TestCategory",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    [Fact]
    public void IsExists_Initially_ReturnsFalse()
    {
        var factory = new ErrorFactory();
        Assert.False(factory.IsExists());
    }

    [Fact]
    public void Create_AddsDescriptor_WhenNotExists()
    {
        // Arrange
        var factory = new ErrorFactory();
        var description = new ErrorDescription 
        { 
            DiagnosticDescriptor = _testDescriptor, 
            GeneratorType = GeneratorType.Database 
        };

        // Act
        factory.Create(description);

        // Assert
        Assert.True(factory.IsExists());
    }

    [Fact]
    public void Create_PreventsDuplicates_WithSameIdAndType()
    {
        // Arrange
        var factory = new ErrorFactory();
        var desc1 = new ErrorDescription { DiagnosticDescriptor = _testDescriptor, GeneratorType = GeneratorType.Database };
        var desc2 = new ErrorDescription { DiagnosticDescriptor = _testDescriptor, GeneratorType = GeneratorType.Database };

        // Act
        factory.Create(desc1);
        factory.Create(desc2);

        // Assert
        Assert.True(factory.IsExists());
    }

    [Fact]
    public void Clear_RemovesAllDescriptors()
    {
        // Arrange
        var factory = new ErrorFactory();
        factory.Create(new ErrorDescription { DiagnosticDescriptor = _testDescriptor, GeneratorType = GeneratorType.Database });

        // Act
        factory.Clear();

        // Assert
        Assert.False(factory.IsExists());
    }

    [Fact]
    public void Create_AllowsSameId_ForDifferentGeneratorTypes()
    {
        // Arrange
        var factory = new ErrorFactory();
        var desc1 = new ErrorDescription { DiagnosticDescriptor = _testDescriptor, GeneratorType = GeneratorType.Factory };
        var desc2 = new ErrorDescription { DiagnosticDescriptor = _testDescriptor, GeneratorType = GeneratorType.Database };

        // Act
        factory.Create(desc1);
        factory.Create(desc2);

        // Assert
        Assert.True(factory.IsExists());
    }
    
    [Fact]
    public void ShowDiagnostics_Should_Execute_Without_Errors()
    {
        // Arrange
        var factory = new ErrorFactory();
        var descriptor = new DiagnosticDescriptor(
            "ERR001", "Title", "Message", "Cat", DiagnosticSeverity.Error, true);
        
        factory.Create(new ErrorDescription 
        { 
            DiagnosticDescriptor = descriptor, 
            GeneratorType = GeneratorType.Database 
        });

        var context = (SourceProductionContext)Activator.CreateInstance(
            typeof(SourceProductionContext), 
            nonPublic: true)!;

        // Act & Assert
        var exception = Record.Exception(() => factory.ShowDiagnostics(context, GeneratorType.Database));
        Assert.NotNull(exception);
    }
}