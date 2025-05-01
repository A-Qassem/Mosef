namespace Mosef
{
    public class Feedback
    {
        public string? FeedbackId { get; set; }  
        public string? PatientId { get; set; }  
        public string? NurseId { get; set; }  
        public string? ServiceId { get; set; }  
        public int Rating { get; set; }  
        public string? Comments { get; set; }  

        public Feedback(string feedbackId, string patientId, string nurseId, string serviceId, int rating, string? comments)
        {
            FeedbackId = GeneratorId.GenerateRandomId();
            PatientId = patientId;
            NurseId = nurseId;
            ServiceId = serviceId;
            Rating = rating;
            Comments = comments;
        }

        public Patient Patient { get; set; }
    }

}
