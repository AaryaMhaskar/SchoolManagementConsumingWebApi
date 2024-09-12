using NuGet.DependencyResolver;
using System.Xml;

namespace SchoolManagementConsumingWebApi.Models
{
    public class Student
    {
       
    public int StudentId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public int ClassId { get; set; }

        public string? Std { get; set; }

        public string? Password { get; set; }

        public string? ParentEmail { get; set; }

        public string? PhoneNumber { get; set; }

        public string? FeesStatus { get; set; }

        public int? UsersId { get; set; }

        //public virtual ICollection<AssignmentResponse> AssignmentResponses { get; } = new List<AssignmentResponse>();

        //public virtual ICollection<Attendance> Attendances { get; } = new List<Attendance>();

        //public virtual Class Class { get; set; } = null!;
    }

}
