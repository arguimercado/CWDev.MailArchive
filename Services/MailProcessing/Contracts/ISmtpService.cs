using MailProcessing.Commons.Results;
using MailProcessing.Commons.Settings;

namespace MailProcessing.Contracts;

public interface ISmtpService
{
    Task SendEmailAsync(string from, string to, string subject, string body);

   

    IAsyncEnumerable<MailDataResult> ReadDataAsync(
        ImapSetting imapSetting, string directory, int maxCount = 10);
}