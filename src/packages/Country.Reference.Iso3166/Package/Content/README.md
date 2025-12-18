# HawkN.Country.Reference.Iso3166

**Country.Reference.Iso3166** provides ISO 3166-1 country codes (Alpha-2, Alpha-3, Numeric M49), official names, and validation utilities.

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
dotnet add package HawkN.Country.Reference.Iso3166 --version <last version>
````

### Required Namespaces
```csharp
using Country.Reference.Iso3166;
using Country.Reference.Iso3166.Abstractions;
using Country.Reference.Iso3166.Models;
using Country.Reference.Iso3166.Extensions;
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

### Supported countries
Supported 248 countries. See the country list with the [link](https://github.com/HawkN113/Country.Reference.Iso3166?tab=readme-ov-file#supported-countries)
Last updated at 01.12.2025.

---

### Generated Types
- `CountryCode.TwoLetterCode` – Enum for Alpha-2 codes (e.g., `US`, `GB`).
- `CountryCode.ThreeLetterCode` – Enum for Alpha-3 codes (e.g., `USA`, `GBR`).
- `Country` – Model containing `Name`, enums, string codes, and `NumericCode`.

---

### License
This project is licensed under the MIT License.

---

### References
- [ISO 3166 Standard](https://www.iso.org/iso-3166-country-codes.html)
- [GitHub Repository](https://github.com/HawkN113/Country.Reference.Iso4217)
