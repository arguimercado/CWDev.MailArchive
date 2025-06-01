using CWDev.MailArchive.Core.Models.Entity;
using CWDev.MailArchive.Core.Models.ValueObjects;

namespace CWDev.MailArchive.Core.Models.Aggregates;

public class MailBox
{
    protected MailBox()
    {
        // Initialize non-nullable properties to default values to satisfy EF Core requirements.  

    }

    public MailBox(Guid employeeId, ArchiveSchedule schedule)
    {
        EmployeeId = employeeId;
        ArchiveSchedule = schedule ?? throw new ArgumentNullException(nameof(schedule));
    }

    public Guid Id { get; set; }
    public Guid EmployeeId { get; private set; }

    public ArchiveSchedule ArchiveSchedule { get; private set; } = default!;

    #region Mails
    private IList<Mail> _mails = new List<Mail>();
    // Navigation property for archived emails, etc.  
    public IEnumerable<Mail> Mails => _mails.AsReadOnly();
    #endregion
    public void UpdateSchedule(ArchiveSchedule newSchedule)
    {
        ArchiveSchedule = newSchedule ?? throw new ArgumentNullException(nameof(newSchedule));
    }
}
