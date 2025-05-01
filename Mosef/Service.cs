namespace Mosef
{
    public class Service
    {
        public string? ServiceId { get; set; }  
        public string? ServiceName { get; set; }  
        public string? ServiceDescription { get; set; }  
        public DateTime ServiceStartDate { get; set; }  
        public DateTime? ServiceEndDate { get; set; }  
        public string? AssignedNurseId { get; set; }  
        public string? PatientId { get; set; }  
        public string? ServiceStatus { get; set; }


        public ICollection<Appointment> Appointments { get; set; }

    }

}
