# Country.Reference.Iso3166

[![NuGet](https://img.shields.io/nuget/v/HawkN.Country.Reference.Iso3166?label=HawkN.Country.Reference.Iso3166)](https://www.nuget.org/packages/HawkN.Country.Reference.Iso3166/)
![Downloads](https://img.shields.io/nuget/dt/HawkN.Country.Reference.Iso3166)
[![GitHub license](https://img.shields.io/github/license/HawkN113/Country.Reference.Iso3166)](https://github.com/HawkN113/Country.Reference.Iso3166/blob/main/LICENSE)
[![License: LGPL v2.1](https://img.shields.io/badge/Data%20License-LGPL%20v2.1-blue.svg)](https://www.gnu.org/licenses/old-licenses/lgpl-2.1.en.html)
[![CodeQL Security](https://img.shields.io/github/actions/workflow/status/HawkN113/Country.Reference.Iso3166/codeql-analysis.yml?label=CodeQL%20Security&style=flat-square)](https://github.com/HawkN113/Country.Reference.Iso3166/actions/workflows/codeql-analysis.yml)

| ![HawkN.Country.Reference.Iso3166](docs/img/Country.Reference.Iso3166.png) | **Country.Reference.Iso3166** provides ISO 3166-1 country codes (Alpha-2, Alpha-3), official names, and validation utilities. |
|--------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
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
dotnet add package HawkN.Country.Reference.Iso3166
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
<details>
<summary>Click to view the full list of supported countries</summary>

| Alpha-2 | Alpha-3 | Numeric |                 Country Name                 |                     Official Name                     |
|:-------:|:-------:|:-------:|:--------------------------------------------:|:-----------------------------------------------------:|
| AF      | AFG     | 004     |                 Afghanistan                  |            Islamic Republic of Afghanistan            |
| AL      | ALB     | 008     |                   Albania                    |                  Republic of Albania                  |
| DZ      | DZA     | 012     |                   Algeria                    |        People's Democratic Republic of Algeria        |
| AS      | ASM     | 016     |                American Samoa                |                                                       |
| AD      | AND     | 020     |                   Andorra                    |                Principality of Andorra                |
| AO      | AGO     | 024     |                    Angola                    |                  Republic of Angola                   |
| AI      | AIA     | 660     |                   Anguilla                   |                                                       |
| AQ      | ATA     | 010     |                  Antarctica                  |                                                       |
| AG      | ATG     | 028     |             Antigua and Barbuda              |                                                       |
| AR      | ARG     | 032     |                  Argentina                   |                  Argentine Republic                   |
| AM      | ARM     | 051     |                   Armenia                    |                  Republic of Armenia                  |
| AW      | ABW     | 533     |                    Aruba                     |                                                       |
| AU      | AUS     | 036     |                  Australia                   |                                                       |
| AT      | AUT     | 040     |                   Austria                    |                  Republic of Austria                  |
| AZ      | AZE     | 031     |                  Azerbaijan                  |                Republic of Azerbaijan                 |
| BS      | BHS     | 044     |                   Bahamas                    |              Commonwealth of the Bahamas              |
| BH      | BHR     | 048     |                   Bahrain                    |                  Kingdom of Bahrain                   |
| BD      | BGD     | 050     |                  Bangladesh                  |            People's Republic of Bangladesh            |
| BB      | BRB     | 052     |                   Barbados                   |                                                       |
| BY      | BLR     | 112     |                   Belarus                    |                  Republic of Belarus                  |
| BE      | BEL     | 056     |                   Belgium                    |                  Kingdom of Belgium                   |
| BZ      | BLZ     | 084     |                    Belize                    |                                                       |
| BJ      | BEN     | 204     |                    Benin                     |                   Republic of Benin                   |
| BM      | BMU     | 060     |                   Bermuda                    |                                                       |
| BT      | BTN     | 064     |                    Bhutan                    |                   Kingdom of Bhutan                   |
| BO      | BOL     | 068     |       Bolivia, Plurinational State of        |            Plurinational State of Bolivia             |
| BQ      | BES     | 535     |       Bonaire, Sint Eustatius and Saba       |           Bonaire, Sint Eustatius and Saba            |
| BA      | BIH     | 070     |            Bosnia and Herzegovina            |          Republic of Bosnia and Herzegovina           |
| BW      | BWA     | 072     |                   Botswana                   |                 Republic of Botswana                  |
| BV      | BVT     | 074     |                Bouvet Island                 |                                                       |
| BR      | BRA     | 076     |                    Brazil                    |             Federative Republic of Brazil             |
| IO      | IOT     | 086     |        British Indian Ocean Territory        |                                                       |
| BN      | BRN     | 096     |              Brunei Darussalam               |                                                       |
| BG      | BGR     | 100     |                   Bulgaria                   |                 Republic of Bulgaria                  |
| BF      | BFA     | 854     |                 Burkina Faso                 |                                                       |
| BI      | BDI     | 108     |                   Burundi                    |                  Republic of Burundi                  |
| CV      | CPV     | 132     |                  Cabo Verde                  |                Republic of Cabo Verde                 |
| KH      | KHM     | 116     |                   Cambodia                   |                  Kingdom of Cambodia                  |
| CM      | CMR     | 120     |                   Cameroon                   |                 Republic of Cameroon                  |
| CA      | CAN     | 124     |                    Canada                    |                                                       |
| KY      | CYM     | 136     |                Cayman Islands                |                                                       |
| CF      | CAF     | 140     |           Central African Republic           |                                                       |
| TD      | TCD     | 148     |                     Chad                     |                   Republic of Chad                    |
| CL      | CHL     | 152     |                    Chile                     |                   Republic of Chile                   |
| CN      | CHN     | 156     |                    China                     |              People's Republic of China               |
| CX      | CXR     | 162     |               Christmas Island               |                                                       |
| CC      | CCK     | 166     |           Cocos (Keeling) Islands            |                                                       |
| CO      | COL     | 170     |                   Colombia                   |                 Republic of Colombia                  |
| KM      | COM     | 174     |                   Comoros                    |                 Union of the Comoros                  |
| CG      | COG     | 178     |                    Congo                     |                 Republic of the Congo                 |
| CD      | COD     | 180     |    Congo, The Democratic Republic of the     |                                                       |
| CK      | COK     | 184     |                 Cook Islands                 |                                                       |
| CR      | CRI     | 188     |                  Costa Rica                  |                Republic of Costa Rica                 |
| HR      | HRV     | 191     |                   Croatia                    |                  Republic of Croatia                  |
| CU      | CUB     | 192     |                     Cuba                     |                   Republic of Cuba                    |
| CW      | CUW     | 531     |                   Curaçao                    |                        Curaçao                        |
| CY      | CYP     | 196     |                    Cyprus                    |                  Republic of Cyprus                   |
| CZ      | CZE     | 203     |                   Czechia                    |                    Czech Republic                     |
| CI      | CIV     | 384     |                Côte d'Ivoire                 |               Republic of Côte d'Ivoire               |
| DK      | DNK     | 208     |                   Denmark                    |                  Kingdom of Denmark                   |
| DJ      | DJI     | 262     |                   Djibouti                   |                 Republic of Djibouti                  |
| DM      | DMA     | 212     |                   Dominica                   |               Commonwealth of Dominica                |
| DO      | DOM     | 214     |              Dominican Republic              |                                                       |
| EC      | ECU     | 218     |                   Ecuador                    |                  Republic of Ecuador                  |
| EG      | EGY     | 818     |                    Egypt                     |                Arab Republic of Egypt                 |
| SV      | SLV     | 222     |                 El Salvador                  |                Republic of El Salvador                |
| GQ      | GNQ     | 226     |              Equatorial Guinea               |             Republic of Equatorial Guinea             |
| ER      | ERI     | 232     |                   Eritrea                    |                 the State of Eritrea                  |
| EE      | EST     | 233     |                   Estonia                    |                  Republic of Estonia                  |
| SZ      | SWZ     | 748     |                   Eswatini                   |                  Kingdom of Eswatini                  |
| ET      | ETH     | 231     |                   Ethiopia                   |        Federal Democratic Republic of Ethiopia        |
| FK      | FLK     | 238     |         Falkland Islands (Malvinas)          |                                                       |
| FO      | FRO     | 234     |                Faroe Islands                 |                                                       |
| FJ      | FJI     | 242     |                     Fiji                     |                   Republic of Fiji                    |
| FI      | FIN     | 246     |                   Finland                    |                  Republic of Finland                  |
| FR      | FRA     | 250     |                    France                    |                    French Republic                    |
| GF      | GUF     | 254     |                French Guiana                 |                                                       |
| PF      | PYF     | 258     |               French Polynesia               |                                                       |
| TF      | ATF     | 260     |         French Southern Territories          |                                                       |
| GA      | GAB     | 266     |                    Gabon                     |                   Gabonese Republic                   |
| GM      | GMB     | 270     |                    Gambia                    |                Republic of the Gambia                 |
| GE      | GEO     | 268     |                   Georgia                    |                                                       |
| DE      | DEU     | 276     |                   Germany                    |              Federal Republic of Germany              |
| GH      | GHA     | 288     |                    Ghana                     |                   Republic of Ghana                   |
| GI      | GIB     | 292     |                  Gibraltar                   |                                                       |
| GR      | GRC     | 300     |                    Greece                    |                   Hellenic Republic                   |
| GL      | GRL     | 304     |                  Greenland                   |                                                       |
| GD      | GRD     | 308     |                   Grenada                    |                                                       |
| GP      | GLP     | 312     |                  Guadeloupe                  |                                                       |
| GU      | GUM     | 316     |                     Guam                     |                                                       |
| GT      | GTM     | 320     |                  Guatemala                   |                 Republic of Guatemala                 |
| GG      | GGY     | 831     |                   Guernsey                   |                                                       |
| GN      | GIN     | 324     |                    Guinea                    |                  Republic of Guinea                   |
| GW      | GNB     | 624     |                Guinea-Bissau                 |               Republic of Guinea-Bissau               |
| GY      | GUY     | 328     |                    Guyana                    |                  Republic of Guyana                   |
| HT      | HTI     | 332     |                    Haiti                     |                   Republic of Haiti                   |
| HM      | HMD     | 334     |      Heard Island and McDonald Islands       |                                                       |
| VA      | VAT     | 336     |        Holy See (Vatican City State)         |                                                       |
| HN      | HND     | 340     |                   Honduras                   |                 Republic of Honduras                  |
| HK      | HKG     | 344     |                  Hong Kong                   |   Hong Kong Special Administrative Region of China    |
| HU      | HUN     | 348     |                   Hungary                    |                        Hungary                        |
| IS      | ISL     | 352     |                   Iceland                    |                  Republic of Iceland                  |
| IN      | IND     | 356     |                    India                     |                   Republic of India                   |
| ID      | IDN     | 360     |                  Indonesia                   |                 Republic of Indonesia                 |
| IR      | IRN     | 364     |          Iran, Islamic Republic of           |               Islamic Republic of Iran                |
| IQ      | IRQ     | 368     |                     Iraq                     |                   Republic of Iraq                    |
| IE      | IRL     | 372     |                   Ireland                    |                                                       |
| IM      | IMN     | 833     |                 Isle of Man                  |                                                       |
| IL      | ISR     | 376     |                    Israel                    |                    State of Israel                    |
| IT      | ITA     | 380     |                    Italy                     |                   Italian Republic                    |
| JM      | JAM     | 388     |                   Jamaica                    |                                                       |
| JP      | JPN     | 392     |                    Japan                     |                                                       |
| JE      | JEY     | 832     |                    Jersey                    |                                                       |
| JO      | JOR     | 400     |                    Jordan                    |              Hashemite Kingdom of Jordan              |
| KZ      | KAZ     | 398     |                  Kazakhstan                  |                Republic of Kazakhstan                 |
| KE      | KEN     | 404     |                    Kenya                     |                   Republic of Kenya                   |
| KI      | KIR     | 296     |                   Kiribati                   |                 Republic of Kiribati                  |
| KP      | PRK     | 408     |    Korea, Democratic People's Republic of    |         Democratic People's Republic of Korea         |
| KR      | KOR     | 410     |              Korea, Republic of              |                                                       |
| KW      | KWT     | 414     |                    Kuwait                    |                    State of Kuwait                    |
| KG      | KGZ     | 417     |                  Kyrgyzstan                  |                    Kyrgyz Republic                    |
| LA      | LAO     | 418     |       Lao People's Democratic Republic       |                                                       |
| LV      | LVA     | 428     |                    Latvia                    |                  Republic of Latvia                   |
| LB      | LBN     | 422     |                   Lebanon                    |                   Lebanese Republic                   |
| LS      | LSO     | 426     |                   Lesotho                    |                  Kingdom of Lesotho                   |
| LR      | LBR     | 430     |                   Liberia                    |                  Republic of Liberia                  |
| LY      | LBY     | 434     |                    Libya                     |                         Libya                         |
| LI      | LIE     | 438     |                Liechtenstein                 |             Principality of Liechtenstein             |
| LT      | LTU     | 440     |                  Lithuania                   |                 Republic of Lithuania                 |
| LU      | LUX     | 442     |                  Luxembourg                  |               Grand Duchy of Luxembourg               |
| MO      | MAC     | 446     |                    Macao                     |     Macao Special Administrative Region of China      |
| MG      | MDG     | 450     |                  Madagascar                  |                Republic of Madagascar                 |
| MW      | MWI     | 454     |                    Malawi                    |                  Republic of Malawi                   |
| MY      | MYS     | 458     |                   Malaysia                   |                                                       |
| MV      | MDV     | 462     |                   Maldives                   |                 Republic of Maldives                  |
| ML      | MLI     | 466     |                     Mali                     |                   Republic of Mali                    |
| MT      | MLT     | 470     |                    Malta                     |                   Republic of Malta                   |
| MH      | MHL     | 584     |               Marshall Islands               |           Republic of the Marshall Islands            |
| MQ      | MTQ     | 474     |                  Martinique                  |                                                       |
| MR      | MRT     | 478     |                  Mauritania                  |            Islamic Republic of Mauritania             |
| MU      | MUS     | 480     |                  Mauritius                   |                 Republic of Mauritius                 |
| YT      | MYT     | 175     |                   Mayotte                    |                                                       |
| MX      | MEX     | 484     |                    Mexico                    |                 United Mexican States                 |
| FM      | FSM     | 583     |       Micronesia, Federated States of        |            Federated States of Micronesia             |
| MD      | MDA     | 498     |             Moldova, Republic of             |                  Republic of Moldova                  |
| MC      | MCO     | 492     |                    Monaco                    |                Principality of Monaco                 |
| MN      | MNG     | 496     |                   Mongolia                   |                                                       |
| ME      | MNE     | 499     |                  Montenegro                  |                      Montenegro                       |
| MS      | MSR     | 500     |                  Montserrat                  |                                                       |
| MA      | MAR     | 504     |                   Morocco                    |                  Kingdom of Morocco                   |
| MZ      | MOZ     | 508     |                  Mozambique                  |                Republic of Mozambique                 |
| MM      | MMR     | 104     |                   Myanmar                    |                  Republic of Myanmar                  |
| NA      | NAM     | 516     |                   Namibia                    |                  Republic of Namibia                  |
| NR      | NRU     | 520     |                    Nauru                     |                   Republic of Nauru                   |
| NP      | NPL     | 524     |                    Nepal                     |         Federal Democratic Republic of Nepal          |
| NL      | NLD     | 528     |                 Netherlands                  |              Kingdom of the Netherlands               |
| NC      | NCL     | 540     |                New Caledonia                 |                                                       |
| NZ      | NZL     | 554     |                 New Zealand                  |                                                       |
| NI      | NIC     | 558     |                  Nicaragua                   |                 Republic of Nicaragua                 |
| NE      | NER     | 562     |                    Niger                     |                 Republic of the Niger                 |
| NG      | NGA     | 566     |                   Nigeria                    |              Federal Republic of Nigeria              |
| NU      | NIU     | 570     |                     Niue                     |                         Niue                          |
| NF      | NFK     | 574     |                Norfolk Island                |                                                       |
| MK      | MKD     | 807     |               North Macedonia                |              Republic of North Macedonia              |
| MP      | MNP     | 580     |           Northern Mariana Islands           |     Commonwealth of the Northern Mariana Islands      |
| NO      | NOR     | 578     |                    Norway                    |                   Kingdom of Norway                   |
| OM      | OMN     | 512     |                     Oman                     |                   Sultanate of Oman                   |
| PK      | PAK     | 586     |                   Pakistan                   |             Islamic Republic of Pakistan              |
| PW      | PLW     | 585     |                    Palau                     |                   Republic of Palau                   |
| PS      | PSE     | 275     |             Palestine, State of              |                the State of Palestine                 |
| PA      | PAN     | 591     |                    Panama                    |                  Republic of Panama                   |
| PG      | PNG     | 598     |               Papua New Guinea               |         Independent State of Papua New Guinea         |
| PY      | PRY     | 600     |                   Paraguay                   |                 Republic of Paraguay                  |
| PE      | PER     | 604     |                     Peru                     |                   Republic of Peru                    |
| PH      | PHL     | 608     |                 Philippines                  |              Republic of the Philippines              |
| PN      | PCN     | 612     |                   Pitcairn                   |                                                       |
| PL      | POL     | 616     |                    Poland                    |                  Republic of Poland                   |
| PT      | PRT     | 620     |                   Portugal                   |                  Portuguese Republic                  |
| PR      | PRI     | 630     |                 Puerto Rico                  |                                                       |
| QA      | QAT     | 634     |                    Qatar                     |                    State of Qatar                     |
| RO      | ROU     | 642     |                   Romania                    |                                                       |
| RU      | RUS     | 643     |              Russian Federation              |                                                       |
| RW      | RWA     | 646     |                    Rwanda                    |                   Rwandese Republic                   |
| RE      | REU     | 638     |                   Réunion                    |                                                       |
| BL      | BLM     | 652     |               Saint Barthélemy               |                                                       |
| SH      | SHN     | 654     | Saint Helena, Ascension and Tristan da Cunha |                                                       |
| KN      | KNA     | 659     |            Saint Kitts and Nevis             |                                                       |
| LC      | LCA     | 662     |                 Saint Lucia                  |                                                       |
| MF      | MAF     | 663     |          Saint Martin (French part)          |                                                       |
| PM      | SPM     | 666     |          Saint Pierre and Miquelon           |                                                       |
| VC      | VCT     | 670     |       Saint Vincent and the Grenadines       |                                                       |
| WS      | WSM     | 882     |                    Samoa                     |              Independent State of Samoa               |
| SM      | SMR     | 674     |                  San Marino                  |                Republic of San Marino                 |
| ST      | STP     | 678     |            Sao Tome and Principe             |     Democratic Republic of Sao Tome and Principe      |
| SA      | SAU     | 682     |                 Saudi Arabia                 |                Kingdom of Saudi Arabia                |
| SN      | SEN     | 686     |                   Senegal                    |                  Republic of Senegal                  |
| RS      | SRB     | 688     |                    Serbia                    |                  Republic of Serbia                   |
| SC      | SYC     | 690     |                  Seychelles                  |                Republic of Seychelles                 |
| SL      | SLE     | 694     |                 Sierra Leone                 |               Republic of Sierra Leone                |
| SG      | SGP     | 702     |                  Singapore                   |                 Republic of Singapore                 |
| SX      | SXM     | 534     |          Sint Maarten (Dutch part)           |               Sint Maarten (Dutch part)               |
| SK      | SVK     | 703     |                   Slovakia                   |                    Slovak Republic                    |
| SI      | SVN     | 705     |                   Slovenia                   |                 Republic of Slovenia                  |
| SB      | SLB     | 090     |               Solomon Islands                |                                                       |
| SO      | SOM     | 706     |                   Somalia                    |              Federal Republic of Somalia              |
| ZA      | ZAF     | 710     |                 South Africa                 |               Republic of South Africa                |
| GS      | SGS     | 239     | South Georgia and the South Sandwich Islands |                                                       |
| SS      | SSD     | 728     |                 South Sudan                  |                Republic of South Sudan                |
| ES      | ESP     | 724     |                    Spain                     |                   Kingdom of Spain                    |
| LK      | LKA     | 144     |                  Sri Lanka                   |      Democratic Socialist Republic of Sri Lanka       |
| SD      | SDN     | 729     |                    Sudan                     |                 Republic of the Sudan                 |
| SR      | SUR     | 740     |                   Suriname                   |                 Republic of Suriname                  |
| SJ      | SJM     | 744     |            Svalbard and Jan Mayen            |                                                       |
| SE      | SWE     | 752     |                    Sweden                    |                   Kingdom of Sweden                   |
| CH      | CHE     | 756     |                 Switzerland                  |                  Swiss Confederation                  |
| SY      | SYR     | 760     |             Syrian Arab Republic             |                                                       |
| TW      | TWN     | 158     |          Taiwan, Province of China           |               Taiwan, Province of China               |
| TJ      | TJK     | 762     |                  Tajikistan                  |                Republic of Tajikistan                 |
| TZ      | TZA     | 834     |         Tanzania, United Republic of         |              United Republic of Tanzania              |
| TH      | THA     | 764     |                   Thailand                   |                  Kingdom of Thailand                  |
| TL      | TLS     | 626     |                 Timor-Leste                  |          Democratic Republic of Timor-Leste           |
| TG      | TGO     | 768     |                     Togo                     |                   Togolese Republic                   |
| TK      | TKL     | 772     |                   Tokelau                    |                                                       |
| TO      | TON     | 776     |                    Tonga                     |                   Kingdom of Tonga                    |
| TT      | TTO     | 780     |             Trinidad and Tobago              |            Republic of Trinidad and Tobago            |
| TN      | TUN     | 788     |                   Tunisia                    |                  Republic of Tunisia                  |
| TM      | TKM     | 795     |                 Turkmenistan                 |                                                       |
| TC      | TCA     | 796     |           Turks and Caicos Islands           |                                                       |
| TV      | TUV     | 798     |                    Tuvalu                    |                                                       |
| TR      | TUR     | 792     |                   Türkiye                    |                  Republic of Türkiye                  |
| UG      | UGA     | 800     |                    Uganda                    |                  Republic of Uganda                   |
| UA      | UKR     | 804     |                   Ukraine                    |                                                       |
| AE      | ARE     | 784     |             United Arab Emirates             |                                                       |
| GB      | GBR     | 826     |                United Kingdom                | United Kingdom of Great Britain and Northern Ireland  |
| US      | USA     | 840     |                United States                 |               United States of America                |
| UM      | UMI     | 581     |     United States Minor Outlying Islands     |                                                       |
| UY      | URY     | 858     |                   Uruguay                    |              Eastern Republic of Uruguay              |
| UZ      | UZB     | 860     |                  Uzbekistan                  |                Republic of Uzbekistan                 |
| VU      | VUT     | 548     |                   Vanuatu                    |                  Republic of Vanuatu                  |
| VE      | VEN     | 862     |      Venezuela, Bolivarian Republic of       |           Bolivarian Republic of Venezuela            |
| VN      | VNM     | 704     |                   Viet Nam                   |            Socialist Republic of Viet Nam             |
| VG      | VGB     | 092     |           Virgin Islands, British            |                British Virgin Islands                 |
| VI      | VIR     | 850     |             Virgin Islands, U.S.             |          Virgin Islands of the United States          |
| WF      | WLF     | 876     |              Wallis and Futuna               |                                                       |
| EH      | ESH     | 732     |                Western Sahara                |                                                       |
| YE      | YEM     | 887     |                    Yemen                     |                   Republic of Yemen                   |
| ZM      | ZMB     | 894     |                    Zambia                    |                  Republic of Zambia                   |
| ZW      | ZWE     | 716     |                   Zimbabwe                   |                 Republic of Zimbabwe                  |
| AX      | ALA     | 248     |                Åland Islands                 |                                                       |

Last updated at `01.12.2025`.

</details>

---

### Generated Types
- `CountryCode.TwoLetterCode` – Enum for Alpha-2 codes (e.g., `US`, `GB`).
- `CountryCode.ThreeLetterCode` – Enum for Alpha-3 codes (e.g., `USA`, `GBR`).
- `Country` – Model containing `Name`, enums, string codes, and `NumericCode`.

---

### Licensing & Data Sources

This project is an independent implementation and is not officially affiliated with or endorsed by the ISO or the Debian iso-codes team.

- **Library Code:** Licensed under the [MIT License](./LICENSE.txt).
- **ISO Data:** Sourced from the [Debian iso-codes project](https://salsa.debian.org/iso-codes-team/iso-codes). Data is distributed under the [LGPL v2.1](./DATA-LICENSE.txt).

By using this library, you agree to comply with the terms of both licenses.

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
- [GitHub Repository](https://github.com/HawkN113/Country.Reference.Iso3166)
