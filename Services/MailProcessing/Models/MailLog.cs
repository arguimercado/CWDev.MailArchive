namespace MailProcessing.Models;

public class MailLog
{
    public Guid Id { get; set; }
    public string Mailbox { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime LatestMailDate { get; set; }
    public long MailId { get; set; } = default!;



}
