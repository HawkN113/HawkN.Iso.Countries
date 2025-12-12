### [How to use source generator](#how-use-sourceGenerator)

- In the project __Country.Reference.Iso3166__ use the parameter setting ``GenerateCountryFiles`` in the project. Set `true` value:
```json lines
  <GenerateCountryFiles>true</GenerateCountryFiles>
```
- Save changes;
- Rebuild the solution;
- Review changes in ``CurrencyCode.cs`` and ``LocalDatabase.cs``files;
- The parameter setting ``GenerateCurrencyFiles`` set `false` value;
- Save changes again;
- Rebuild the solution again.

### How to update countries
- Open https://unstats.un.org/unsd/methodology/m49/overview/
- Download a CSV file
- Save data in the file ``Content\UNSD—Methodology.csv``
- Use command from section <a id="how-use-sourceGenerato">How to use source generator</a>