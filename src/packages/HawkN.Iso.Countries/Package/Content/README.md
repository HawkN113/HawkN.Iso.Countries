# HawkN.Iso.Countries

**HawkN.Iso.Countries** provides ISO 3166-1 country codes (Alpha-2, Alpha-3), official names, and validation services.

## Features
- **Comprehensive Country List** – Provides an up-to-date set of country data according to the `ISO 3166-1` standard.
- **Strongly Typed Codes** – `TwoLetterCode` and `ThreeLetterCode` enums are generated at compile-time.
- **Multiple Search Methods** – Lookup by Alpha-2, Alpha-3, Numeric code, or Official Name.
- **Advanced Validation** – Built-in `ValidationResult` providing detailed feedback for code and name verification.
- **Ultra-Fast Performance** – O(1) lookups via pre-indexed static dictionaries.
- **Lightweight & Dependency-Free** – Compatible with .NET 8 and above.

---

## Getting Started

### Install via NuGet

```bash
dotnet add package HawkN.Iso.Countries
````

### Required Namespaces
```csharp
using HawkN.Iso.Countries;
using HawkN.Iso.Countries.Abstractions;
using HawkN.Iso.Countries.Models;
using HawkN.Iso.Countries.Extensions;
```
---

### Usage Example

#### Registration
Register the service in your DI container:
```csharp
using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddCountryCodeService();
    })
    .Build();
```
#### Retrieval & Search
The service provides O(1) lookups via pre-indexed dictionaries and efficient partial searching.
```csharp
var service = scope.ServiceProvider.GetRequiredService<ICountryCodeService>();

// Get all countries sorted by name
var countries = service.GetAll();

// Lookup by string (Supports Alpha-2, Alpha-3, or Numeric)
var germany = service.FindByCode("DE");
var austria = service.FindByCode("040");

// Lookup by Name
var france = service.FindByName("France");

// Strongly typed lookup
var uk = service.Get(CountryCode.TwoLetterCode.GB);

// Strongly typed lookup
var uk = service.Get(CountryCode.TwoLetterCode.GB);

// Scenario: User types "Republic" in a search box
var searchResults = service.SearchByName("Republic");

foreach (var country in searchResults)
{
    // Will return:
    // 1. Republic of Korea
    // 2. Czech Republic
    // 3. Lao People's Democratic Republic...
    Console.WriteLine($"{country.Name} ({country.OfficialName})");
}

// Pro Tip: Use for suggestion lists
var suggestions = service.SearchByName("United")
    .Select(c => c.Name)
    .Take(5); 
// Returns: ["United Arab Emirates", "United Kingdom", "United States", ...]
```

#### Validation
Check if a code or name is valid and retrieve the model simultaneously:
```csharp
// Validate by Code
var result = service.ValidateByCode("US", out var country);
if (result.IsValid)
{
    Console.WriteLine($"Found: {country.Name}");
}

// Validate by Name
var nameResult = service.ValidateByName("Unknown Land", out _);
if (!nameResult.IsValid)
{
    Console.WriteLine($"Error: {nameResult.Reason}"); 
}

```
#### Fluent String Extensions
```csharp
string input = "FRA";

// Direct conversion
var country = input.ToCountry(service);

// Quick check
if ("US".IsCountryCode(service)) 
{
    // ...
}

// Quick validation
var validationResult = "US".ValidateAsCountryCode(countryCodeService, out var _);
if (validationResult.IsValid)
{
   // ...
}
```

#### Emoji Flags Support
The library provides an easy way to display country flags using standard Unicode Emoji. This works without any external image assets and is perfect for lightweight UI components.

```csharp
var country = service.Get(CountryCode.TwoLetterCode.FI);

// Get the emoji flag using the extension method
string flag = country.GetEmojiFlag(); 

Console.WriteLine($"{flag} {country.Name}"); 
// Output: 🇫🇮 Finland
```

---

### Supported countries
Supported 249 countries. See the country list with the [link](https://github.com/HawkN113/HawkN.Iso.Countries?tab=readme-ov-file#supported-countries)
Last updated at `01.12.2025`.

---

### Generated Types
- `CountryCode.TwoLetterCode` – Enum for Alpha-2 codes (e.g., `US`, `GB`).
- `CountryCode.ThreeLetterCode` – Enum for Alpha-3 codes (e.g., `USA`, `GBR`).
- `Country` – Model containing `Name`, enums, string codes, and `NumericCode`.

---

### License & Data Sources

- **Code**: Licensed under the [MIT License](./LICENSE).
- **Data**: Sourced from [Debian iso-codes](https://salsa.debian.org/iso-codes-team/iso-codes) and distributed under [LGPL v2.1](./DATA-LICENSE.txt).

*This project is an independent implementation and is not officially affiliated with ISO or the Debian iso-codes team.*

---

### Troubleshooting: Emoji Display
If you see `??` instead of flags in your console:
1. Ensure your console output encoding is set to UTF-8:
   `Console.OutputEncoding = System.Text.Encoding.UTF8;`
2. Use a modern terminal like **Windows Terminal** or **VS Code Terminal**.
3. Use a font that supports Emojis (e.g., *Segoe UI Emoji* or *Cascadia Code*).

---

### References
- [ISO 3166 Standard](https://www.iso.org/iso-3166-country-codes.html)
- [Debian Iso-Codes Team](https://salsa.debian.org/iso-codes-team/iso-codes/-/tree/main)
- [GitHub Repository](https://github.com/HawkN113/HawkN.Iso.Countries)
