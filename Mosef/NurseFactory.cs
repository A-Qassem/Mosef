namespace Mosef
{
    public class NurseFactory
    {
        public static Nurse Create(
        string firstName,
        string lastName,
        string email,
        string password,
        string phone,
        string location,
        string gender,
        string specialization,
        DateTime? hireDate)
        {
            return new Nurse
            {
                NurseFirstName = firstName,
                NurseLastName = lastName,
                NurseEmail = email,
                NursePassword = password,
                NursePhone = phone,
                NurseLocation = location,
                NurseGender = gender,
                NurseSpecialization = specialization,
                HireDate = hireDate,
                NurseId = GeneratorId.GenerateRandomId()
            };
        }
    }
}
