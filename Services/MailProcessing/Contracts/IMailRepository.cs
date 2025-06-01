using MailProcessing.Models;

namespace MailProcessing.Contracts;

public interface IMailRepository
{
    Task LogFailedMailAsync(ErrorMailLog mailData);
    IEnumerable<MailLog> GetLogByMailbox(string mailbox);
    Task<MailLog?> GetLatestMailAsync(string mailbox);
    Task AddLatestMailIdAsync(string mailbox, string description, long mailId);
}
