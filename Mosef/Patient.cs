using System.Net.Sockets;

namespace Mosef
{
    public class Patient
    {
        public string? PatientId { get; set; }
        public string? PatientSSN { get; set; }
        public string? PatientFirstName { get; set; }
        public string? PatientLastName { get; set; }
        public string? PatientEmail { get; set; }
        public string? PatientPassword { get; set; }
        public string? PatientPhone { get; set; }
        public string? PatientLocation { get; set; }
        public string? PatientGender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? PatientStatus { get; set; }

        public ICollection<Appointment> Appointments { get; set; }

        public ICollection<Feedback> Feedbacks { get; set; }

    }
}
