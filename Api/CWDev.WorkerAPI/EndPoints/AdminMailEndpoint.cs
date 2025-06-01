using Carter;
using CWDev.MailArchive.WorkerAPI.Commons.Jobs;
using Hangfire;

namespace CWDev.MailArchive.WorkerAPI.EndPoints;

public class AdminMailEndpoint : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var appGroup = app.MapGroup("admin/mail");

        appGroup.MapPost("enqueue", () => {
            string jobId = BackgroundJob.Enqueue<FetchRecurringMailJob>((m) => m.Excecute(CancellationToken.None));
            return Results.Ok(new { JobId = jobId });
        });


        appGroup.MapPost("recurring", () => {
            
            RecurringJob.RemoveIfExists("recurring");
            RecurringJob.AddOrUpdate<FetchRecurringMailJob>("recurring",
                (m) => m.Excecute(CancellationToken.None), Cron.Minutely);

            return Results.Ok();
        });

    }
}
