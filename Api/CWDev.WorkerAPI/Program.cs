using BlobProcessing.Settings;
using Carter;
using CWDev.MailArchive.Application.Extensions;
using CWDev.MailArchive.WorkerAPI.Extensions;
using Hangfire;
using MailProcessing.Commons.Settings;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


        //configurat options
        builder.Services.Configure<BlobSettings>(builder.Configuration.GetSection("Blob"));
        builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("Smtp"));

        builder.Services.AddApplicationServices(builder.Configuration);


        builder.Services.AddHangfireService(builder.Configuration);

        builder.Services.AddOpenApi();
        builder.Services.AddCarter();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwaggerUI(opt =>
            {
                //mapopenapi
                opt.SwaggerEndpoint("/openapi/v1.json", "WorkerAPI");
            });
        }

        app.UseHttpsRedirection();
        app.MapCarter();
        app.UseHangfireDashboard();
        app.MapHangfireDashboard("/hangfire");

        app.Run();


    }
}
