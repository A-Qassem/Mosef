namespace Mosef
{
    public static class ServiceFactory
    {
        public static Service CreateService(
            string ServiceName, string ServiceDescription, DateTime ServiceStartDate,
            DateTime? ServiceEndDate, string AssignedNurseId, string PatientId, string? ServiceStatus)
        {
            return new Service
            {
                ServiceId = GeneratorId.GenerateRandomId(),
                ServiceName = ServiceName,
                ServiceDescription = ServiceDescription,
                ServiceStartDate = ServiceStartDate,
                ServiceEndDate = ServiceEndDate,
                AssignedNurseId = AssignedNurseId,
                PatientId = PatientId,
                ServiceStatus = ServiceStatus
            };
        }
    }

}
