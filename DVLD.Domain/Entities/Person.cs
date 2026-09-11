namespace DVLD.Domain.Entities;
public class Person
{

    public int PersonId { get; private set; }

    public string NationalNo { get; private set; }

    public string FirstName { get; private set; }

    public string SecondName { get; private set; }

    public string? ThirdName { get; private set; }

    public string LastName { get; private set; }

    public DateTime? DateOfBirth { get; private set; }

    public byte? Gender { get; private set; }

    public string Address { get; private set; }

    public string Phone { get; private set; }

    public string? Email { get; private set; }

    public int? CountryId { get; private set; }

    public string? ImagePath { get; private set; }

    public string UserName { get; private set; }

    public string Password { get; private set; }


    public Person(
       int personId,
       string? nationalNo,
       string? firstName,
       string? secondName,
       string? thirdName,
       string? lastName,
       DateTime? dateOfBirth,
       byte? gender,
       string? address,
       string? phone,
       string? email,
       int? countryId,
       string? imagePath,
       string userName,
       string password)
    {
        PersonId = personId;
        NationalNo = nationalNo;
        FirstName = firstName;
        SecondName = secondName;
        ThirdName = thirdName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Gender = gender;
        Address = address;
        Phone = phone;
        Email = email;
        CountryId = countryId;
        ImagePath = imagePath;
        UserName = userName;
        Password = password;
    }

    public Person(
        string? nationalNo,
        string? firstName,
        string? secondName,
        string? thirdName,
        string? lastName,
        DateTime? dateOfBirth,
        byte? gender,
        string? address,
        string? phone,
        string? email,
        int? countryId,
        string? imagePath,
        string userName,
        string password)
    {
        NationalNo = nationalNo;
        FirstName = firstName;
        SecondName = secondName;
        ThirdName = thirdName;
        LastName = lastName;
        this.DateOfBirth = dateOfBirth;
        this.Gender = gender;
        Address = address;
        Phone = phone;
        Email = email;
        this.CountryId = countryId;
        ImagePath = imagePath;
        UserName = userName;
        Password = password;
    }

    public void UpdateInformation(
        string firstName,
        string secondName,
        string? thirdName,
        string lastName,
        DateTime dateOfBirth,
        byte gender,
        string address,
        string phone,
        string? email,
        int countryId,
        string? imagePath)
    {
        SetName(
            firstName,
            secondName,
            thirdName,
            lastName);

        SetDateOfBirth(dateOfBirth);
        SetGender(gender);
        SetAddress(address);
        SetPhone(phone);
        SetEmail(email);
        SetCountry(countryId);
        SetImagePath(imagePath);
    }


    public void ChangePassword(string newPassword)
    {
        if (string.IsNullOrWhiteSpace(newPassword))
            throw new ArgumentException(
                "Password cannot be empty.");

        if (newPassword.Length < 6)
            throw new ArgumentException(
                "Password must contain at least 6 characters.");

        newPassword = newPassword.Trim();

        EnsureDifferent(Password, newPassword);

        Password = newPassword;
    }


    private void EnsureDifferent<T>(T oldValue, T newValue)
    {
        if (EqualityComparer<T>.Default.Equals(oldValue, newValue))
        {
            throw new ArgumentException(
                "New value cannot be the same as the old value.");
        }
    }


    public void ChangeEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            Email = null;
            return;
        }

        if (!email.Contains('@'))
        {
            throw new ArgumentException(
                "Invalid email address.");
        }

        email = email.Trim();

        EnsureDifferent(Email, email);

        Email = email;
    }


    public void ChangePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException(
                "Phone is required.");

        phone = phone.Trim();

        EnsureDifferent(Phone, phone);

        SetPhone(phone);
    }


    private void SetNationalNo(string nationalNo)
    {
        if (string.IsNullOrWhiteSpace(nationalNo))
            throw new ArgumentException(
                "National number is required.");

        nationalNo = nationalNo.Trim();

        EnsureDifferent(NationalNo, nationalNo);

        NationalNo = nationalNo;
    }


    private void SetName(
        string firstName,
        string secondName,
        string? thirdName,
        string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException(
                "First name is required.");

        if (string.IsNullOrWhiteSpace(secondName))
            throw new ArgumentException(
                "Second name is required.");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException(
                "Last name is required.");

        firstName = firstName.Trim();
        secondName = secondName.Trim();
        thirdName = thirdName?.Trim();
        lastName = lastName.Trim();

        EnsureDifferent(FirstName, firstName);
        EnsureDifferent(SecondName, secondName);
        EnsureDifferent(ThirdName, thirdName);
        EnsureDifferent(LastName, lastName);

        FirstName = firstName;
        SecondName = secondName;
        ThirdName = thirdName;
        LastName = lastName;
    }


    private void SetDateOfBirth(DateTime dateOfBirth)
    {
        if (dateOfBirth.Date >= DateTime.Today)
            throw new ArgumentException(
                "Date of birth must be in the past.");

        EnsureDifferent(DateOfBirth, dateOfBirth);

        DateOfBirth = dateOfBirth;
    }


    private void SetGender(byte gender)
    {
        if (gender != 0 && gender != 1)
            throw new ArgumentException(
                "Gender must be 0 for Male or 1 for Female.");

        EnsureDifferent(Gender, gender);

        Gender = gender;
    }


    private void SetAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException(
                "Address is required.");

        address = address.Trim();

        EnsureDifferent(Address, address);

        Address = address;
    }


    private void SetPhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException(
                "Phone is required.");

        phone = phone.Trim();

        EnsureDifferent(Phone, phone);

        Phone = phone;
    }


    private void SetEmail(string? email)
    {
        if (!string.IsNullOrWhiteSpace(email) &&
            !email.Contains('@'))
        {
            throw new ArgumentException(
                "Invalid email address.");
        }

        Email = email?.Trim();
    }


    private void SetCountry(int countryId)
    {
        if (countryId <= 0)
            throw new ArgumentException(
                "Country is required.");

        CountryId = countryId;
    }


    private void SetImagePath(string? imagePath)
    {
        ImagePath = imagePath?.Trim();
    }


    private void SetUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException(
                "Username is required.");

        UserName = username.Trim();
    }


    private string getFullName()
    {
        return $"{FirstName} {SecondName} {ThirdName} {LastName}";
    }


    private void SetPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException(
                "Password is required.");

        if (password.Length < 6)
            throw new ArgumentException(
                "Password must contain at least 6 characters.");

        Password = password;
    }
}
