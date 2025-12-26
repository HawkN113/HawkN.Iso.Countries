using HawkN.Iso.Countries.Models;

namespace HawkN.Iso.Countries.Tests.Models;

public class CountryTests
{
    [Theory]
    [InlineData(4, "004")]
    [InlineData(40, "040")]
    [InlineData(840, "840")]
    [InlineData(0, "000")]
    public void NumericCodeString_ReturnsThreeDigitString(int numericCode, string expectedString)
    {
        // Arrange
        var country = new Country(
            "Testland",
            CountryCode.TwoLetterCode.TL,
            CountryCode.ThreeLetterCode.TLS,
            numericCode
        );

        // Act
        var numericCodeString = country.NumericCodeString;

        // Assert
        Assert.Equal(expectedString, numericCodeString);
    }
}