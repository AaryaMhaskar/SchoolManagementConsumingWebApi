namespace SchoolManagementConsumingWebApi.Models
{
    public class Class
    {
        public int AssignmentResponseId { get; set; }

        public int AssignmentId { get; set; }

        public int StudentId { get; set; }

        public string ResponseFilePath { get; set; } = null!;

        public DateTime SubmittedOn { get; set; }

        public virtual Assignment Assignment { get; set; } = null!;

        public virtual Student Student { get; set; } = null!;
    }
}
