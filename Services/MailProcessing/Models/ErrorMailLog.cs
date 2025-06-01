namespace MailProcessing.Models
{
    public class ErrorMailLog
    {
        public Guid Id { get; set; }
        public string Mailbox { get; set; } = default!;
        public DateTime ErrorDate { get; set; }
        public long MailId { get; set; } = default!;
        public string ErrorDetails { get; set; } = default!;

    }
}
