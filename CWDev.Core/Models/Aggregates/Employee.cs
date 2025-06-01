namespace CWDev.MailArchive.Core.Models.Aggregates;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public DateTime HireDate { get; set; }
    public string Position { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
    // Navigation properties can be added if needed
}
