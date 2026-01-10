# HawkN.Iso.Countries.Currencies

**HawkN.Iso.Countries.Currencies** provides ISO-based mapping of primary and secondary currencies for countries.
It allows you to quickly retrieve the main currency, secondary currencies, all currencies, or check if a currency is used by a specific country.

## Features
- **Primary and Secondary Currencies** – Get the main currency or all secondary currencies of a country.
- **Fast Lookups** – O(1) lookups using pre-indexed dictionaries and HashSet for secondary currencies.
- **Currency Validation** – Check if a country uses a given currency.
- **Seamless Integration** – Built to work with `HawkN.Iso.Countries` types (`CountryCode.TwoLetterCode`).
- **Lightweight & Dependency-Free** – Compatible with .NET 8 and above.

---

## Getting Started

### Install via NuGet

```bash
dotnet add package HawkN.Iso.Countries.Currencies
````
#### References
- [HawkN.Iso.Countries](https://www.nuget.org/packages/HawkN.Iso.Countries) – Provides country codes and models.
- [HawkN.Iso.Currencies](https://www.nuget.org/packages/HawkN.Iso.Currencies) – Provides currency codes and enums.

### Required Namespaces
```csharp
using HawkN.Iso.Countries;
using HawkN.Iso.Currencies;
using HawkN.Iso.Countries.Currencies.Extensions;
```
---

### Usage Example

#### Get Main Currency
```csharp
CurrencyCode.TwoLetterCode? mainCurrency = CountryCode.TwoLetterCode.US.GetMainCurrency();
Console.WriteLine($"Primary currency of US: {mainCurrency}"); // USD
```

#### Get Secondary Currencies
```csharp
var secondaryCurrencies = CountryCode.TwoLetterCode.BO.GetSecondaryCurrencies();
Console.WriteLine($"Secondary currencies of BO: {string.Join(", ", secondaryCurrencies)}"); // BOV
```

### Get all Currencies for specific country
```csharp
var allCurrencies = CountryCode.BO.GetAllCurrencies();
Console.WriteLine($"All currencies of BO: {string.Join(", ", allCurrencies)}"); // BOB, BOV
```

#### Validation

Check if Currency is Used by Country
```csharp
bool isUsed = CountryCode.BO.IsCurrencyUsedByCountry(CurrencyCode.BOB); // True
bool isUsdUsed = CountryCode.BO.IsCurrencyUsedByCountry(CurrencyCode.USD); // False
````

---

## License

### Code
This project’s source code is licensed under the [MIT License](LICENSE).

### Data
This project uses data derived from the following sources:

- **Unicode Common Locale Data Repository (CLDR)**  
  Licensed under the [Unicode License Agreement](https://unicode.org/license.html).

- Country data (`ISO 3166-1` and `UN M49` numeric codes) is sourced from the [UN Statistics Division – M49 standard](https://unstats.un.org/unsd/methodology/m49/overview)


The above data licenses are **permissive and compatible with MIT-licensed code**  
when used for reference and code generation.

See [DATA-LICENSE](DATA-LICENSE) for details.

---

### References
- [ISO 3166 Standard](https://www.iso.org/iso-3166-country-codes.html)
- [ISO 4217 Standard](https://www.iso.org/iso-4217-currency-codes.html)
- [UN Statistics Division – M49 standard](https://unstats.un.org/unsd/methodology/m49/overview)
- [GitHub Repository](https://github.com/HawkN113/HawkN.Iso.Countries)
