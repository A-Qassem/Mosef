namespace Mosef
{
    public static class FeedbackFactory
    {
        public static Feedback CreateFeedback(string patientId, string nurseId, string serviceId, int rating, string? comments)
        {
            return new Feedback(GeneratorId.GenerateRandomId(), patientId, nurseId, serviceId, rating, comments);
        }
    }
}
