namespace SchoolManagementConsumingWebApi.Models
{
    public class TimeTable
    {
        public int TimetableId { get; set; }

        public int ClassId { get; set; }

        public string FilePath { get; set; } = null!;

        public virtual Class Class { get; set; } = null!;
    }
}
