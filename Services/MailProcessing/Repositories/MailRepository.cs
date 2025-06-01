using MailProcessing.Commons.Persistence;
using MailProcessing.Contracts;
using MailProcessing.Models;
using Microsoft.EntityFrameworkCore;

namespace MailProcessing.Repositories
{
    public class MailRepository(MailProcessingContext context) : IMailRepository
    {
        public async Task<MailLog?> GetLatestMailAsync(string mailbox)
        {
            return await context.MailLogs
                .Where(log => log.Mailbox == mailbox)
                .OrderByDescending(log => log.MailId)
                .FirstOrDefaultAsync();
        }

        public IEnumerable<MailLog> GetLogByMailbox(string mailbox)
        {
            throw new NotImplementedException();
        }

        public async Task LogFailedMailAsync(ErrorMailLog mailData)
        {
            context.ErrorMailLogs.Add(mailData);
            await context.SaveChangesAsync();
        }

        public async Task AddLatestMailIdAsync(string mailbox, string description, long mailId)
        {
            var mailLog = new MailLog
            {
                Id = Guid.NewGuid(),
                Mailbox = mailbox,
                Description = description,
                LatestMailDate = DateTime.UtcNow,
                MailId = mailId
            };

            context.MailLogs.Add(mailLog);
            await context.SaveChangesAsync();
        }
    }
}
