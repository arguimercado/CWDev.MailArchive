using MailProcessing.Models;
using Microsoft.EntityFrameworkCore;

namespace MailProcessing.Commons.Persistence;

public class MailProcessingContext : DbContext
{
    public DbSet<MailLog> MailLogs { get; set; }
    public DbSet<ErrorMailLog> ErrorMailLogs { get; set; }

    public MailProcessingContext(DbContextOptions<MailProcessingContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MailLog>()
            .HasKey(m => m.Id);

        modelBuilder.Entity<MailLog>()
            .Property(m => m.Mailbox)
            .IsRequired()
            .HasMaxLength(256);

        modelBuilder.Entity<MailLog>()
            .Property(m => m.Description)
            .IsRequired()
            .HasMaxLength(512);

        modelBuilder.Entity<MailLog>()
            .Property(m => m.LatestMailDate)
            .IsRequired();

        modelBuilder.Entity<MailLog>()
            .Property(m => m.MailId)
            .IsRequired()
            .HasMaxLength(256);

        modelBuilder.Entity<ErrorMailLog>()
            .HasKey(e => e.Id);
        modelBuilder.Entity<ErrorMailLog>()
            .Property(e => e.Mailbox)
            .IsRequired()
            .HasMaxLength(256);

        modelBuilder.Entity<ErrorMailLog>()
            .Property(e => e.ErrorDetails)
            .IsRequired()
            .HasMaxLength(1024);

        modelBuilder.Entity<ErrorMailLog>()
            .Property(e => e.ErrorDate)
            .IsRequired();



        base.OnModelCreating(modelBuilder);
    }


}
