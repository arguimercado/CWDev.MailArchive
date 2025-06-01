using CWDev.MailArchive.Core.Models.ValueObjects;

namespace CWDev.MailArchive.Core.Extensions
{
    public static class DateExtension
    {
        public static DateTime GetNextStartDate(DateTime today, ArchiveFrequency frequency, DayOfWeek startDay)
        {
            // If today is already the startDay, we can start today or next week based on your business rule.
            int daysUntilStart = ((int)startDay - (int)today.DayOfWeek + 7) % 7;

            // For daily, always use the next occurrence (can be today)
            if (frequency == ArchiveFrequency.Daily)
            {
                return today.AddDays(daysUntilStart);
            }
            // For weekly, if today is the startDay, schedule for next week (or today based on business rule)
            else // ArchiveFrequency.Weekly
            {
                // If daysUntilStart == 0, today is the start day, so schedule for next week
                return today.AddDays(daysUntilStart == 0 ? 7 : daysUntilStart);
            }
        }
    }
}
