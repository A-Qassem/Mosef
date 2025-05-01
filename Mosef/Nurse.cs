namespace Mosef
{
    public class Nurse
    {
        public string? NurseId { get; set; }
        public string? NurseFirstName { get; set; }
        public string? NurseLastName { get; set; }
        public string? NurseEmail { get; set; }
        public string? NursePassword { get; set; }
        public string? NursePhone { get; set; }
        public string? NurseLocation { get; set; }
        public string? NurseGender { get; set; }
        public string? NurseSpecialization { get; set; }
        public DateTime? HireDate { get; set; }
        public string? NurseStatus { get; set; }

        public ICollection<Appointment> Appointments { get; set; }

        
    }

}
