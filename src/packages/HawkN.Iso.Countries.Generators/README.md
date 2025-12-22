### [How to use source generator](#how-use-sourceGenerator)

- In the project __HawkN.Iso.Countries__ use the parameter setting ``GenerateCountryFiles`` in the project. Set `true` value:
```json lines
  <GenerateCountryFiles>true</GenerateCountryFiles>
```
- Save changes;
- Rebuild the solution;
- Review changes in ``CountryCode.cs`` and ``LocalCountryDatabase.cs``files;
- The parameter setting ``GenerateCountryFiles`` set `false` value;
- Save changes again;
- Rebuild the solution again.

### How to update countries
- Open https://salsa.debian.org/iso-codes-team/iso-codes/-/blob/main/data/iso_3166-1.json
- Download a JSON file
- Save data in the file ``Content\iso_3166-1.json``
- Use command from section <a id="how-use-sourceGenerator">How to use source generator</a>