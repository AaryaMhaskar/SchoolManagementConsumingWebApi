namespace SchoolManagementConsumingWebApi.Models
{
    
        public class Attendance
        {
            public int attendanceId { get; set; }
            public int studentId { get; set; }
            public DateTime date { get; set; }
            public bool isPresent { get; set; }
            public object student { get; set; }
        }

    }

