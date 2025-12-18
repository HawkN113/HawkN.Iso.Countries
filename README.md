# Country.Reference.Iso3166

[![NuGet](https://img.shields.io/nuget/v/HawkN.Country.Reference.Iso3166?label=HawkN.Country.Reference.Iso3166)](https://www.nuget.org/packages/HawkN.Country.Reference.Iso3166/)
![Downloads](https://img.shields.io/nuget/dt/HawkN.Country.Reference.Iso3166)
[![GitHub license](https://img.shields.io/github/license/HawkN113/Country.Reference.Iso3166)](https://github.com/HawkN113/Country.Reference.Iso3166/blob/main/LICENSE)
[![CodeQL Security](https://img.shields.io/github/actions/workflow/status/HawkN113/Country.Reference.Iso3166/codeql-analysis.yml?label=CodeQL%20Security&style=flat-square)](https://github.com/HawkN113/Country.Reference.Iso3166/actions/workflows/codeql-analysis.yml)

| ![HawkN.Country.Reference.Iso3166](docs/img/Country.Reference.Iso3166.png) | **Country.Reference.Iso3166** provides ISO 3166-1 country codes (Alpha-2, Alpha-3, Numeric M49), official names, and validation utilities. |
|--------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
---

## Features
- **Comprehensive Country List** – Provides an up-to-date set of country data according to the `ISO 3166-1` standard.
- **Strongly Typed Codes** – `TwoLetterCode` and `ThreeLetterCode` enums are generated at compile-time.
- **Multiple Search Methods** – Lookup by Alpha-2, Alpha-3, Numeric code, or Official Name.
- **Advanced Validation** – Built-in `ValidationResult` providing detailed feedback for code and name verification.
- **Ultra-Fast Performance** – O(1) lookups via pre-indexed static dictionaries.
- **Lightweight & Dependency-Free** – Compatible with .NET 8 and above.

---

## Packages

| Package | Description |
|---------------------------------------------------------------------------------------------------|-------------|
| [HawkN.Country.Reference.Iso3166](https://www.nuget.org/packages/HawkN.Country.Reference.Iso3166) | Main library with country models, validation services, and generated ISO enums. |

---

## Getting Started

### Install via NuGet

```bash
dotnet add package HawkN.Country.Reference.Iso3166 --version <last version>
````

### Required Namespaces
```csharp
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
| Alpha-2 | Alpha-3 | Numeric | Country Name |
|:-------:|:-------:|:-------:|:-------------|
| AF      | AFG     | 004     | Afghanistan      |
| AL      | ALB     | 008     | Albania      |
| DZ      | DZA     | 012     | Algeria      |
| AS      | ASM     | 016     | American Samoa      |
| AD      | AND     | 020     | Andorra      |
| AO      | AGO     | 024     | Angola      |
| AI      | AIA     | 660     | Anguilla      |
| AQ      | ATA     | 010     | Antarctica      |
| AG      | ATG     | 028     | Antigua and Barbuda      |
| AR      | ARG     | 032     | Argentina      |
| AM      | ARM     | 051     | Armenia      |
| AW      | ABW     | 533     | Aruba      |
| AU      | AUS     | 036     | Australia      |
| AT      | AUT     | 040     | Austria      |
| AZ      | AZE     | 031     | Azerbaijan      |
| BS      | BHS     | 044     | Bahamas      |
| BH      | BHR     | 048     | Bahrain      |
| BD      | BGD     | 050     | Bangladesh      |
| BB      | BRB     | 052     | Barbados      |
| BY      | BLR     | 112     | Belarus      |
| BE      | BEL     | 056     | Belgium      |
| BZ      | BLZ     | 084     | Belize      |
| BJ      | BEN     | 204     | Benin      |
| BM      | BMU     | 060     | Bermuda      |
| BT      | BTN     | 064     | Bhutan      |
| BO      | BOL     | 068     | Bolivia (Plurinational State of)      |
| BQ      | BES     | 535     | Bonaire, Sint Eustatius and Saba      |
| BA      | BIH     | 070     | Bosnia and Herzegovina      |
| BW      | BWA     | 072     | Botswana      |
| BV      | BVT     | 074     | Bouvet Island      |
| BR      | BRA     | 076     | Brazil      |
| IO      | IOT     | 086     | British Indian Ocean Territory      |
| VG      | VGB     | 092     | British Virgin Islands      |
| BN      | BRN     | 096     | Brunei Darussalam      |
| BG      | BGR     | 100     | Bulgaria      |
| BF      | BFA     | 854     | Burkina Faso      |
| BI      | BDI     | 108     | Burundi      |
| CV      | CPV     | 132     | Cabo Verde      |
| KH      | KHM     | 116     | Cambodia      |
| CM      | CMR     | 120     | Cameroon      |
| CA      | CAN     | 124     | Canada      |
| KY      | CYM     | 136     | Cayman Islands      |
| CF      | CAF     | 140     | Central African Republic      |
| TD      | TCD     | 148     | Chad      |
| CL      | CHL     | 152     | Chile      |
| CN      | CHN     | 156     | China      |
| HK      | HKG     | 344     | China, Hong Kong Special Administrative Region      |
| MO      | MAC     | 446     | China, Macao Special Administrative Region      |
| CX      | CXR     | 162     | Christmas Island      |
| CC      | CCK     | 166     | Cocos (Keeling) Islands      |
| CO      | COL     | 170     | Colombia      |
| KM      | COM     | 174     | Comoros      |
| CG      | COG     | 178     | Congo      |
| CK      | COK     | 184     | Cook Islands      |
| CR      | CRI     | 188     | Costa Rica      |
| HR      | HRV     | 191     | Croatia      |
| CU      | CUB     | 192     | Cuba      |
| CW      | CUW     | 531     | Curaçao      |
| CY      | CYP     | 196     | Cyprus      |
| CZ      | CZE     | 203     | Czechia      |
| CI      | CIV     | 384     | Côte d'Ivoire      |
| KP      | PRK     | 408     | Democratic People's Republic of Korea      |
| CD      | COD     | 180     | Democratic Republic of the Congo      |
| DK      | DNK     | 208     | Denmark      |
| DJ      | DJI     | 262     | Djibouti      |
| DM      | DMA     | 212     | Dominica      |
| DO      | DOM     | 214     | Dominican Republic      |
| EC      | ECU     | 218     | Ecuador      |
| EG      | EGY     | 818     | Egypt      |
| SV      | SLV     | 222     | El Salvador      |
| GQ      | GNQ     | 226     | Equatorial Guinea      |
| ER      | ERI     | 232     | Eritrea      |
| EE      | EST     | 233     | Estonia      |
| SZ      | SWZ     | 748     | Eswatini      |
| ET      | ETH     | 231     | Ethiopia      |
| FK      | FLK     | 238     | Falkland Islands (Malvinas)      |
| FO      | FRO     | 234     | Faroe Islands      |
| FJ      | FJI     | 242     | Fiji      |
| FI      | FIN     | 246     | Finland      |
| FR      | FRA     | 250     | France      |
| GF      | GUF     | 254     | French Guiana      |
| PF      | PYF     | 258     | French Polynesia      |
| TF      | ATF     | 260     | French Southern Territories      |
| GA      | GAB     | 266     | Gabon      |
| GM      | GMB     | 270     | Gambia      |
| GE      | GEO     | 268     | Georgia      |
| DE      | DEU     | 276     | Germany      |
| GH      | GHA     | 288     | Ghana      |
| GI      | GIB     | 292     | Gibraltar      |
| GR      | GRC     | 300     | Greece      |
| GL      | GRL     | 304     | Greenland      |
| GD      | GRD     | 308     | Grenada      |
| GP      | GLP     | 312     | Guadeloupe      |
| GU      | GUM     | 316     | Guam      |
| GT      | GTM     | 320     | Guatemala      |
| GG      | GGY     | 831     | Guernsey      |
| GN      | GIN     | 324     | Guinea      |
| GW      | GNB     | 624     | Guinea-Bissau      |
| GY      | GUY     | 328     | Guyana      |
| HT      | HTI     | 332     | Haiti      |
| HM      | HMD     | 334     | Heard Island and McDonald Islands      |
| VA      | VAT     | 336     | Holy See      |
| HN      | HND     | 340     | Honduras      |
| HU      | HUN     | 348     | Hungary      |
| IS      | ISL     | 352     | Iceland      |
| IN      | IND     | 356     | India      |
| ID      | IDN     | 360     | Indonesia      |
| IR      | IRN     | 364     | Iran (Islamic Republic of)      |
| IQ      | IRQ     | 368     | Iraq      |
| IE      | IRL     | 372     | Ireland      |
| IM      | IMN     | 833     | Isle of Man      |
| IL      | ISR     | 376     | Israel      |
| IT      | ITA     | 380     | Italy      |
| JM      | JAM     | 388     | Jamaica      |
| JP      | JPN     | 392     | Japan      |
| JE      | JEY     | 832     | Jersey      |
| JO      | JOR     | 400     | Jordan      |
| KZ      | KAZ     | 398     | Kazakhstan      |
| KE      | KEN     | 404     | Kenya      |
| KI      | KIR     | 296     | Kiribati      |
| KW      | KWT     | 414     | Kuwait      |
| KG      | KGZ     | 417     | Kyrgyzstan      |
| LA      | LAO     | 418     | Lao People's Democratic Republic      |
| LV      | LVA     | 428     | Latvia      |
| LB      | LBN     | 422     | Lebanon      |
| LS      | LSO     | 426     | Lesotho      |
| LR      | LBR     | 430     | Liberia      |
| LY      | LBY     | 434     | Libya      |
| LI      | LIE     | 438     | Liechtenstein      |
| LT      | LTU     | 440     | Lithuania      |
| LU      | LUX     | 442     | Luxembourg      |
| MG      | MDG     | 450     | Madagascar      |
| MW      | MWI     | 454     | Malawi      |
| MY      | MYS     | 458     | Malaysia      |
| MV      | MDV     | 462     | Maldives      |
| ML      | MLI     | 466     | Mali      |
| MT      | MLT     | 470     | Malta      |
| MH      | MHL     | 584     | Marshall Islands      |
| MQ      | MTQ     | 474     | Martinique      |
| MR      | MRT     | 478     | Mauritania      |
| MU      | MUS     | 480     | Mauritius      |
| YT      | MYT     | 175     | Mayotte      |
| MX      | MEX     | 484     | Mexico      |
| FM      | FSM     | 583     | Micronesia (Federated States of)      |
| MC      | MCO     | 492     | Monaco      |
| MN      | MNG     | 496     | Mongolia      |
| ME      | MNE     | 499     | Montenegro      |
| MS      | MSR     | 500     | Montserrat      |
| MA      | MAR     | 504     | Morocco      |
| MZ      | MOZ     | 508     | Mozambique      |
| MM      | MMR     | 104     | Myanmar      |
| NA      | NAM     | 516     | Namibia      |
| NR      | NRU     | 520     | Nauru      |
| NP      | NPL     | 524     | Nepal      |
| NL      | NLD     | 528     | Netherlands (Kingdom of the)      |
| NC      | NCL     | 540     | New Caledonia      |
| NZ      | NZL     | 554     | New Zealand      |
| NI      | NIC     | 558     | Nicaragua      |
| NE      | NER     | 562     | Niger      |
| NG      | NGA     | 566     | Nigeria      |
| NU      | NIU     | 570     | Niue      |
| NF      | NFK     | 574     | Norfolk Island      |
| MK      | MKD     | 807     | North Macedonia      |
| MP      | MNP     | 580     | Northern Mariana Islands      |
| NO      | NOR     | 578     | Norway      |
| OM      | OMN     | 512     | Oman      |
| PK      | PAK     | 586     | Pakistan      |
| PW      | PLW     | 585     | Palau      |
| PA      | PAN     | 591     | Panama      |
| PG      | PNG     | 598     | Papua New Guinea      |
| PY      | PRY     | 600     | Paraguay      |
| PE      | PER     | 604     | Peru      |
| PH      | PHL     | 608     | Philippines      |
| PN      | PCN     | 612     | Pitcairn      |
| PL      | POL     | 616     | Poland      |
| PT      | PRT     | 620     | Portugal      |
| PR      | PRI     | 630     | Puerto Rico      |
| QA      | QAT     | 634     | Qatar      |
| KR      | KOR     | 410     | Republic of Korea      |
| MD      | MDA     | 498     | Republic of Moldova      |
| RO      | ROU     | 642     | Romania      |
| RU      | RUS     | 643     | Russian Federation      |
| RW      | RWA     | 646     | Rwanda      |
| RE      | REU     | 638     | Réunion      |
| BL      | BLM     | 652     | Saint Barthélemy      |
| SH      | SHN     | 654     | Saint Helena      |
| KN      | KNA     | 659     | Saint Kitts and Nevis      |
| LC      | LCA     | 662     | Saint Lucia      |
| MF      | MAF     | 663     | Saint Martin (French Part)      |
| PM      | SPM     | 666     | Saint Pierre and Miquelon      |
| VC      | VCT     | 670     | Saint Vincent and the Grenadines      |
| WS      | WSM     | 882     | Samoa      |
| SM      | SMR     | 674     | San Marino      |
| ST      | STP     | 678     | Sao Tome and Principe      |
| SA      | SAU     | 682     | Saudi Arabia      |
| SN      | SEN     | 686     | Senegal      |
| RS      | SRB     | 688     | Serbia      |
| SC      | SYC     | 690     | Seychelles      |
| SL      | SLE     | 694     | Sierra Leone      |
| SG      | SGP     | 702     | Singapore      |
| SX      | SXM     | 534     | Sint Maarten (Dutch part)      |
| SK      | SVK     | 703     | Slovakia      |
| SI      | SVN     | 705     | Slovenia      |
| SB      | SLB     | 090     | Solomon Islands      |
| SO      | SOM     | 706     | Somalia      |
| ZA      | ZAF     | 710     | South Africa      |
| GS      | SGS     | 239     | South Georgia and the South Sandwich Islands      |
| SS      | SSD     | 728     | South Sudan      |
| ES      | ESP     | 724     | Spain      |
| LK      | LKA     | 144     | Sri Lanka      |
| PS      | PSE     | 275     | State of Palestine      |
| SD      | SDN     | 729     | Sudan      |
| SR      | SUR     | 740     | Suriname      |
| SJ      | SJM     | 744     | Svalbard and Jan Mayen Islands      |
| SE      | SWE     | 752     | Sweden      |
| CH      | CHE     | 756     | Switzerland      |
| SY      | SYR     | 760     | Syrian Arab Republic      |
| TJ      | TJK     | 762     | Tajikistan      |
| TH      | THA     | 764     | Thailand      |
| TL      | TLS     | 626     | Timor-Leste      |
| TG      | TGO     | 768     | Togo      |
| TK      | TKL     | 772     | Tokelau      |
| TO      | TON     | 776     | Tonga      |
| TT      | TTO     | 780     | Trinidad and Tobago      |
| TN      | TUN     | 788     | Tunisia      |
| TM      | TKM     | 795     | Turkmenistan      |
| TC      | TCA     | 796     | Turks and Caicos Islands      |
| TV      | TUV     | 798     | Tuvalu      |
| TR      | TUR     | 792     | Türkiye      |
| UG      | UGA     | 800     | Uganda      |
| UA      | UKR     | 804     | Ukraine      |
| AE      | ARE     | 784     | United Arab Emirates      |
| GB      | GBR     | 826     | United Kingdom of Great Britain and Northern Ireland      |
| TZ      | TZA     | 834     | United Republic of Tanzania      |
| UM      | UMI     | 581     | United States Minor Outlying Islands      |
| VI      | VIR     | 850     | United States Virgin Islands      |
| US      | USA     | 840     | United States of America      |
| UY      | URY     | 858     | Uruguay      |
| UZ      | UZB     | 860     | Uzbekistan      |
| VU      | VUT     | 548     | Vanuatu      |
| VE      | VEN     | 862     | Venezuela (Bolivarian Republic of)      |
| VN      | VNM     | 704     | Viet Nam      |
| WF      | WLF     | 876     | Wallis and Futuna Islands      |
| EH      | ESH     | 732     | Western Sahara      |
| YE      | YEM     | 887     | Yemen      |
| ZM      | ZMB     | 894     | Zambia      |
| ZW      | ZWE     | 716     | Zimbabwe      |
| AX      | ALA     | 248     | Åland Islands      |

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
