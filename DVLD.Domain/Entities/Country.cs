namespace DVLD.Domain.Entities;

public class Country
{
    public int CountryId { get; private set; }

    public string CountryName { get; private set; }


    // Create
    public Country(string countryName)
    {
        ValidateCountryName(countryName);

        CountryName = countryName.Trim();
    }


    // Load existing Country from database
    public Country(
        int countryId,
        string countryName)
    {
        CountryId = countryId;
        CountryName = countryName;
    }


    public void UpdateInformation(string countryName)
    {
        ValidateCountryName(countryName);

        CountryName = countryName.Trim();
    }


    private static void ValidateCountryName(string countryName)
    {
        if (string.IsNullOrWhiteSpace(countryName))
            throw new ArgumentException(
                "Country name is required.");
    }
}