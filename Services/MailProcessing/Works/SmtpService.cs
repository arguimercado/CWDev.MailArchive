using BlobProcessing.Contracts;
using MailKit.Search;
using MailProcessing.Commons.Results;
using MailProcessing.Commons.Settings;
using MailProcessing.Contracts;
using MailProcessing.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Text;


namespace MailProcessing.Works;

public class SmtpService : ISmtpService
{

    private readonly IServiceScopeFactory _scopeFactory;
  

    private readonly SmtpSettings smtpSettings;


    public SmtpService(IOptions<SmtpSettings> options, IServiceScopeFactory scopeFactory)
    {
        smtpSettings = options.Value;
        _scopeFactory = scopeFactory;

    }

    public async Task SendEmailAsync(string from, string to, string subject, string body)
    {

        MimeMessage message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(from));
        message.To.Add(MailboxAddress.Parse(to));
        message.Subject = subject;
        message.Body = new TextPart("plain") { Text = body };

        using SmtpClient client = new SmtpClient();

        await client.ConnectAsync(smtpSettings.Server, smtpSettings.Port, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(smtpSettings.User, smtpSettings.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }


    public async IAsyncEnumerable<MailDataResult> ReadDataAsync(
        ImapSetting imapSetting,
        string directory,
        int maxCount = 10)
    {

        using var scope = _scopeFactory.CreateScope();
        var mailRepository = scope.ServiceProvider.GetRequiredService<IMailRepository>();
        var blobService = scope.ServiceProvider.GetRequiredService<IBlobService>();


        // Get the latest processed mail UID to prevent deduplication
        var latestMail = await mailRepository.GetLatestMailAsync(imapSetting.Mailbox);
        var latestId = latestMail?.MailId ?? 0;

        using ImapClient client = new ImapClient();
        await client.ConnectAsync(imapSetting.Server, imapSetting.Port, SecureSocketOptions.SslOnConnect);
        await client.AuthenticateAsync(imapSetting.UserName, imapSetting.Password);

        IMailFolder inbox = client.Inbox;
        await inbox.OpenAsync(FolderAccess.ReadOnly);

        // Fetch all UIDs in the inbox
        var uids = await inbox.SearchAsync(SearchQuery.All);

        // Filter UIDs to only those greater than latestId (unprocessed)
        var newUids = uids.Where(uid => uid.Id >= latestId)
                          .OrderBy(uid => uid.Id)
                          .Take(maxCount)
                          .ToList();

        var currentId = latestId;
        foreach (var uid in newUids)
        {
            MailDataResult? result = null;
            try
            {
                MimeMessage message = await inbox.GetMessageAsync(uid);
                await using FileStream stream = blobService.CreateStreamAsync($"email_{uid.Id}.eml", directory);
                await message.WriteToAsync(stream);


                // Extract headers
                var headers = new StringBuilder();
                foreach (Header header in message.Headers)
                {
                    headers.AppendFormat("{0}: {1}\n", header.Field, header.Value);
                }

                // Extract summary (first 100 chars of plain text body)
                var bodyText = message.TextBody ?? string.Empty;
                var summary = bodyText.Length > 100 ? bodyText.Substring(0, 100) : bodyText;

                currentId = uid.Id;
                result = new MailDataResult(directory, headers.ToString(), summary);
            }
            catch (Exception ex)
            {
                // Log the failed mail retrieval
                await mailRepository.LogFailedMailAsync(new ErrorMailLog
                {
                    Mailbox = imapSetting.Mailbox,
                    MailId = uid.Id,
                    ErrorDetails = ex.Message,
                    ErrorDate = DateTime.UtcNow
                });
            }

            if (result != null) {
                yield return result;
            }
        }
        
        if(currentId != latestId)
            await mailRepository.AddLatestMailIdAsync(imapSetting.Mailbox, "Processed emails", currentId);
        
        await client.DisconnectAsync(true);
    }

    
}