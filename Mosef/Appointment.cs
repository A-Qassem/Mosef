namespace Mosef
{
    public class Appointment
    {
        public string? AppointmentId { get; set; }  
        public string? PatientId { get; set; } 
        public string? NurseId { get; set; }  
        public DateTime AppointmentDate { get; set; }
        public string? AppointmentStatus { get; set; }  
        public string? ServiceId { get; set; } 

        public Appointment(string patientId, string nurseId, DateTime appointmentDate, string? appointmentStatus, string? serviceId)
        {
            AppointmentId = GeneratorId.GenerateRandomId();
            PatientId = patientId;
            NurseId = nurseId;
            AppointmentDate = appointmentDate;
            AppointmentStatus = appointmentStatus;
            ServiceId = serviceId;
        }

        public Patient Patient { get; set; }

        public Nurse Nurse { get; set; }

        public Service Service { get; set; }

    }

}
