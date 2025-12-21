using Country.Reference.Iso3166.Abstractions;
using Country.Reference.Iso3166.Extensions;
using Country.Reference.Iso3166.Models;
using Moq;

namespace Country.Reference.Iso3166.Tests.Extensions;

public class CountryStringExtensionsTests
{
    private readonly Mock<ICountryCodeService> _serviceMock = new();

    [Theory]
    [InlineData("US", "United States")]
    [InlineData("USA", "United States")]
    [InlineData("840", "United States")]
    
    //"United States", 
    public void ToCountry_ValidCode_ReturnsCountry(string input, string expectedName)
    {
        // Arrange
        var country = new Models.Country(expectedName, CountryCode.TwoLetterCode.US, CountryCode.ThreeLetterCode.USA,"840","United States of America");
        _serviceMock.Setup(s => s.FindByCode(input)).Returns(country);

        // Act
        var result = input.ToCountry(_serviceMock.Object);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedName, result.Name);
    }

    [Fact]
    public void ToCountry_NullOrEmpty_ReturnsNull()
    {
        // Act & Assert
        Assert.Null(((string?)null).ToCountry(_serviceMock.Object));
        Assert.Null("".ToCountry(_serviceMock.Object));
    }

    [Fact]
    public void IsCountryCode_ValidCode_ReturnsTrue()
    {
        // Arrange
        Models.Country? country;
        _serviceMock.Setup(s => s.TryGet("DE", out country)).Returns(true);

        // Act
        var result = "DE".IsCountryCode(_serviceMock.Object);

        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData("FR", "FRA", "🇫🇷")]
    [InlineData("JP", "JPN", "🇯🇵")]
    [InlineData("GB", "GBR", "🇬🇧")]
    public void GetEmojiFlag_ValidAlpha2_ReturnsCorrectEmoji(string alpha2, string alpha3, string expectedEmoji)
    {
        // Arrange
        var country = new Models.Country(
            alpha2, 
            Enum.Parse<CountryCode.TwoLetterCode>(alpha2),
            Enum.Parse<CountryCode.ThreeLetterCode>(alpha3), 
            "001", 
            alpha2);

        // Act
        var result = country.GetEmojiFlag();

        // Assert
        Assert.Equal(expectedEmoji, result);
    }
    
    [Fact]
    public void ValidateAsCountryCode_ValidCode_ReturnsSuccessAndCountry()
    {
        // Arrange
        var input = "US";
        var expectedCountry = new Models.Country(
            "United States", 
            CountryCode.TwoLetterCode.US,
            CountryCode.ThreeLetterCode.USA, 
            "840", 
            "United States of America");
        var successResult = ValidationResult.Success();

        // Настройка Mock для метода с out параметром
        _serviceMock.Setup(s => s.ValidateByCode(input, out expectedCountry))
                    .Returns(successResult);

        // Act
        var result = input.ValidateAsCountryCode(_serviceMock.Object, out var actualCountry);

        // Assert
        Assert.True(result.IsValid);
        Assert.NotNull(actualCountry);
        Assert.Equal("United States", actualCountry.Name);
    }

    [Fact]
    public void ValidateAsCountryCode_InvalidCode_ReturnsFailure()
    {
        // Arrange
        var input = "XX";
        Models.Country? nullCountry = null;
        var failureResult = ValidationResult.Failure("Invalid code", ValidationType.Code);

        _serviceMock.Setup(s => s.ValidateByCode(input, out nullCountry))
                    .Returns(failureResult);

        // Act
        var result = input.ValidateAsCountryCode(_serviceMock.Object, out var actualCountry);

        // Assert
        Assert.False(result.IsValid);
        Assert.Null(actualCountry);
        Assert.Equal("Invalid code", result.Reason);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void ValidateAsCountryCode_NullOrWhitespace_ReturnsRequiredFailure(string? input)
    {
        // Act
        var result = input.ValidateAsCountryCode(_serviceMock.Object, out var actualCountry);

        // Assert
        Assert.False(result.IsValid);
        Assert.Null(actualCountry);
        Assert.Equal("Code is required.", result.Reason);
        _serviceMock.Verify(s => s.ValidateByCode(It.IsAny<string>(), out It.Ref<Models.Country?>.IsAny), Times.Never);
    }
}