namespace CWDev.MailArchive.WorkerAPI.Commons.Contracts
{
    public interface IJobCommand
    {
        Task Excecute(CancellationToken cancellationToken = default);
    }
}
