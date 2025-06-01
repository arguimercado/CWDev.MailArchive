using CWDev.MailArchive.Application.Features.MailJobs;
using CWDev.MailArchive.WorkerAPI.Commons.Contracts;
using MediatR;

namespace CWDev.MailArchive.MailArchive.WorkerAPI.Commons.Jobs
{
    public class FetchRecurringMailJob : IJobCommand
    {
        private readonly IMediator mediator;

        public FetchRecurringMailJob(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task Excecute(CancellationToken cancellationToken = default) {
            await mediator.Send(new FetchMailReccuringCommand(), cancellationToken);
        }
    }
}
