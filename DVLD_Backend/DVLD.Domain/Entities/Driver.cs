namespace DVLD.Domain.Entities;

public class Driver
{
    public int DriverId { get; private set; }

    public int PersonId { get; private set; }

    public int CreatedByUserId { get; private set; }

    public DateTime CreatedDate { get; private set; }

    public Person Person { get; private set; } = null!;

    public User CreatedByUser { get; private set; } = null!;


    // Create
    public Driver(
        int personId,
        int createdByUserId,
        Person person,
        User createdByUser)
    {
        ValidatePersonId(personId);
        PersonId = personId;
        CreatedByUserId = createdByUserId;
        CreatedDate = DateTime.Now;
        Person = person;
        CreatedByUser = createdByUser;
    }


    // Load existing Driver from database
    public Driver(
        int driverId,
        int personId,
        int createdByUserId,
        DateTime createdDate,
        Person person,
        User createdByUser)
    {
        DriverId = driverId;
        PersonId = personId;
        CreatedByUserId = createdByUserId;
        CreatedDate = createdDate;
        Person = person;
        CreatedByUser = createdByUser;
    }


    private static void ValidatePersonId(int personId)
    {
        if (personId <= 0)
            throw new ArgumentException(
                "Person ID must be greater than 0.");
    }


  
}