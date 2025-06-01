using BlobProcessing.Contracts;
using BlobProcessing.Works;
using MailProcessing.Commons.Persistence;
using MailProcessing.Contracts;
using MailProcessing.Repositories;
using MailProcessing.Works;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MailProcessing;

public static class DependencyInjection
{
    public static IServiceCollection AddMailProcessingServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MailProcessingContext>(opt =>
        {
            var connectionString = configuration.GetConnectionString("MailProcessingDb");
            opt.UseSqlServer(connectionString);
            //add no tracking by default
            opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
        });

        services.AddScoped<IMailRepository, MailRepository>();
        services.AddSingleton<ISmtpService, SmtpService>();
        services.AddSingleton<IBlobService, BlobService>();
        return services;
    }


}


