namespace Mosef
{
    public static class PatientFactory
    {
        public static Patient CreatePatient(
            string PatientSSN, string PatientFirstName, string PatientLastName,
            string PatientEmail, string PatientPassword, string PatientPhone,
            string PatientLocation, string? PatientGender, string? birthDate = null,
            string? PatientStatus = null)
        {
            return new Patient
            {
                PatientId = GeneratorId.GenerateRandomId(),
                PatientSSN = PatientSSN,
                PatientFirstName = PatientFirstName,
                PatientLastName = PatientLastName,
                PatientEmail = PatientEmail,
                PatientPassword = PatientPassword,
                PatientPhone = PatientPhone,
                PatientLocation = PatientLocation,
                PatientGender = PatientGender,
                BirthDate = birthDate,
                PatientStatus = PatientStatus
            };
        }

        public static Patient CreatePatientObj(Patient patient)
        {
            return new Patient
            {
                PatientId = patient.PatientId,
                PatientSSN = patient.PatientSSN,
                PatientFirstName = patient.PatientFirstName,
                PatientLastName = patient.PatientLastName,
                PatientEmail = patient.PatientEmail,
                PatientPassword = patient.PatientPassword,
                PatientPhone = patient.PatientPhone,
                PatientLocation = patient.PatientLocation,
                PatientGender = patient.PatientGender,
                BirthDate = patient.BirthDate,
                PatientStatus = patient.PatientStatus
            };
        }
    }

}
