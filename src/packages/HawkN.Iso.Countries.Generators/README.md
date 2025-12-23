### Summary of Data Acquisition

The library automates the retrieval of standardized country data to ensure accuracy and compliance with the ISO 3166-1 standard.

* **Source URL**: Official Debian Salsa repository
* **Target Path**: Local project directory `Content\iso_3166-1.json`
* **Mechanism**: Integrated MSBuild task that executes before the build process to synchronize data

---

### How to use source generator

The project uses `Incremental Source Generators` to maintain country codes and the local database. The process is now fully automated during the build.

- **Automatic Generation**: The files `CountryCode.g.cs` and `LocalCountryDatabase.g.cs` are generated automatically during the project build.
- **Viewing Generated Files**:
    1. Open the solution in your IDE (Visual Studio / Rider).
    2. Navigate to `HawkN.Iso.Countries` -> `Dependencies` -> `Analyzers` -> `HawkN.Iso.Countries.Generators`.
    3. Here you can review the generated source code.

### How to update countries

You no longer need to download the JSON file manually. The build system is configured to sync with the official source.

- **Data Source**: The library uses the official ISO 3166-1 data from the Debian iso-codes repository.
- **Update Process**:
    1. During the build process, a pre-build task checks for the latest `iso_3166-1.json` from [Debian Salsa](https://salsa.debian.org/iso-codes-team/iso-codes/-/raw/main/data/iso_3166-1.json).
    2. If the file is missing or updated, it is automatically downloaded to the `Content/` folder in the Generators project.
- **Manual Trigger**:
    - If you want to force a fresh update, delete the file `src/HawkN.Iso.Countries.Generators/Content/iso_3166-1.json` and rebuild the solution.
    - The build script will detect the missing file and download the latest version from the repository.