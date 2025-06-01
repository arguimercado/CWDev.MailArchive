using CWDev.MailArchive.Core.Models.Aggregates;

namespace CWDev.MailArchive.Core.Models.Entity;

public class Mail
{
    public Guid Id { get; set; }
    public Guid MailboxId { get; private set; } = default!;

    public long MailId { get; set; }

    public string? Subject { get; private set; }
    public string? TextBody { get; set; }
    public string? Body { get; private set; }
    public bool HasAttachment { get; private set; }
    public DateTime ReceivedAt { get; private set; }
    public string FileEMLPath { get; private set; } = string.Empty; // Initialize to avoid null  

    protected Mail() { } // For EF Core  

    public Mail(long mailId,
        string subject, 
        string body, 
        DateTime receivedAt, 
        string fileEMLPath, 
        MailBox mailBox)
    {
        Subject = subject;
        Body = body;
        ReceivedAt = receivedAt;
        FileEMLPath = fileEMLPath; // Ensure non-null value is set  
        MailBox = mailBox; // Ensure non-null value is set  
        MailId = mailId;
    }

    public virtual MailBox MailBox { get; private set; } = default!; // Initialize to avoid null  
}
