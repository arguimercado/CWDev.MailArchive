using MailProcessing.Commons.Results;
using MailProcessing.Commons.Settings;
using MailProcessing.Contracts;
using MediatR;

namespace CWDev.MailArchive.Application.Features.MailJobs;

public record FetchMailReccuringCommand() : IRequest<Unit>;

internal class FetchMailReccuringCommandHandler(ISmtpService smtpService) : IRequestHandler<FetchMailReccuringCommand, Unit>
{   
    public async Task<Unit> Handle(FetchMailReccuringCommand request, CancellationToken cancellationToken)
    {
        var gmailImapSetting = new ImapSetting("arnold.mercado", "imap.gmail.com", 993, "arguimercado@gmail.com", "jrpe unmt njlb fwif");
        //var iphostImapSetting = new ImapSetting("devtest", "mail.crossworldmarine.com", 993, "devtest@crossworldmarine.com", "");
        
        await foreach (MailDataResult result in smtpService.ReadDataAsync(gmailImapSetting, "devtest", 10))
        {
            // Process each email file path as needed
            Console.WriteLine($"Email Id: {result.EmlFilePath}");
            Console.WriteLine($"Email Header: {result.Headers}");
        }

        
        return Unit.Value;
    }
}
