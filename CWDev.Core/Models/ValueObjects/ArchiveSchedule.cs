using CWDev.MailArchive.Core.Extensions;

namespace CWDev.MailArchive.Core.Models.ValueObjects
{
    public enum ArchiveFrequency
    {
        Daily,
        Weekly
    }
    public class ArchiveSchedule
    {

        public ArchiveFrequency Frequency { get; private set; }
        public DayOfWeek StartDay { get; private set; }
        public DateTime NextDate { get; set; }

        public ArchiveSchedule(ArchiveFrequency frequency, DayOfWeek startDay, DateTime startDate)
        {
            Frequency = frequency;
            StartDay = startDay;
            NextDate = DateExtension.GetNextStartDate(startDate, frequency, startDay);
        }


        public ArchiveSchedule UpdateNextSchedule()
        {
            return new ArchiveSchedule(Frequency, StartDay, NextDate);
        }



    }
}
