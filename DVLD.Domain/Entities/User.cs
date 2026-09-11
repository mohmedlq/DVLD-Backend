namespace DVLD.Domain.Entities;

public class User
{
    public int UserId { get; private set; }

    public int PersonId { get; private set; }

    public string UserName { get; private set; } = null!;

    public string Password { get; private set; } = null!;

    public bool IsActive { get; private set; }

    public Person Person { get; private set; } = null!;


    // Create
    public User(
        int personId,
        string userName,
        string password,
        bool isActive,
        Person person)
    {
        ValidateUserName(userName);
        ValidatePassword(password);
        PersonId = personId;
        UserName = userName;
        Password = password;
        IsActive = isActive;
        Person = person;
    }


    // Load existing User from database
    public User(
        int userId,
        int personId,
        string userName,
        string password,
        bool isActive,
        Person person)
    {
        UserId = userId;
        PersonId = personId;
        UserName = userName;
        Password = password;
        IsActive = isActive;
        Person = person;
    }


    public void UpdateInformation(
        string userName,
        bool isActive)
    {
        ValidateUserName(userName);

        UserName = userName;
        IsActive = isActive;
    }


    public void ChangePassword(string newPassword)
    {
        ValidatePassword(newPassword);

        Password = newPassword;
    }


    public void Activate()
    {
        IsActive = true;
    }


    public void Deactivate()
    {
        IsActive = false;
    }


    private static void ValidateUserName(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new ArgumentException("Username is required.");

        if (userName.Length < 3)
            throw new ArgumentException(
                "Username must be at least 3 characters.");
    }


    private static void ValidatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password is required.");

        if (password.Length < 6)
            throw new ArgumentException(
                "Password must be at least 6 characters.");
    }
}