### Summary of Data Acquisition

The library uses standardized country data from the UN M49 standard.

* **Source URL**: [UN M49 overview CSV](https://unstats.un.org/unsd/methodology/m49/overview/)
* **Target Path**: Local project directory `Content/un_m49.csv`
* **Mechanism**: Manual download:
  1. Open the UN M49 overview page.
  2. Download the CSV file.
  3. Save it in the project folder `Content/` as `un_m49.csv`.

---

### How to use source generator

- **Automatic Generation**: Files `CountryCode.g.cs` and `LocalCountryDatabase.g.cs` are generated automatically during build.
- **Viewing Generated Files**:
  1. Open the solution in Visual Studio / Rider.
  2. Navigate to `HawkN.Iso.Countries.Generators` -> `Content/` to see `un_m49.csv`.
  3. Generated files appear under `Analyzers / Generated Source`.

---

### How to update countries

- **Manual Update**:
  1. Download the latest UN M49 CSV from [UN M49 overview](https://unstats.un.org/unsd/methodology/m49/overview/).
  2. Replace the existing `Content/un_m49.csv` file.
  3. Rebuild the solution.
  4. Source Generator will parse the new CSV and regenerate the country classes.