using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mosef
{
    public abstract class ServiceRequestHandler
    {
        protected ServiceRequestHandler _nextHandler;

        public void SetNextHandler(ServiceRequestHandler nextHandler)
        {
            _nextHandler = nextHandler;
        }

        public abstract Task<IActionResult> HandleServiceRequest(ServiceRequestDto serviceRequestDto);
    }

    public class PatientHealthCheckHandler : ServiceRequestHandler
    {
        private readonly MosefDbContext _context;

        public PatientHealthCheckHandler(MosefDbContext context)
        {
            _context = context;
        }

        public override async Task<IActionResult?> HandleServiceRequest(ServiceRequestDto serviceRequestDto)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.PatientId == serviceRequestDto.PatientId);
            if (patient == null || patient.PatientStatus != "Healthy")
            {
                return new BadRequestObjectResult("Patient is not healthy for the service.");
            }

            return (IActionResult?)(_nextHandler?.HandleServiceRequest(serviceRequestDto));
        }
    }
    public class NurseAvailabilityHandler : ServiceRequestHandler
    {
        private readonly MosefDbContext _context;

        public NurseAvailabilityHandler(MosefDbContext context)
        {
            _context = context;
        }

        public override async Task<IActionResult?> HandleServiceRequest(ServiceRequestDto serviceRequestDto)
        {
            var nurseAvailable = await _context.Nurses.FirstOrDefaultAsync(n => n.NurseStatus == "Available");
            if (nurseAvailable == null)
            {
                return new BadRequestObjectResult("No nurse is available at the moment.");
            }

            serviceRequestDto.AssignedNurseId = nurseAvailable.NurseId;

            return (IActionResult?)(_nextHandler?.HandleServiceRequest(serviceRequestDto));
        }
    }

    public class RegisterServiceRequestHandler : ServiceRequestHandler
    {
        private readonly MosefDbContext _context;

        public RegisterServiceRequestHandler(MosefDbContext context)
        {
            _context = context;
        }

        public override async Task<IActionResult?> HandleServiceRequest(ServiceRequestDto serviceRequestDto)
        {
            var service = new Service
            {
                ServiceId = GeneratorId.GenerateRandomId(),
                ServiceName = serviceRequestDto.ServiceName,
                ServiceDescription = serviceRequestDto.ServiceDescription,
                ServiceStartDate = serviceRequestDto.ServiceStartDate,
                PatientId = serviceRequestDto.PatientId,
                ServiceStatus = "Pending"
            };

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return (IActionResult?)(_nextHandler?.HandleServiceRequest(serviceRequestDto));
        }
    }

    public class NotifyAdminHandler : ServiceRequestHandler
    {
        public override async Task<IActionResult> HandleServiceRequest(ServiceRequestDto serviceRequestDto)
        {
            await SendNotificationToAdmin(serviceRequestDto);

            return new OkObjectResult("Service request successfully processed and notified to admin.");
        }

        private async Task SendNotificationToAdmin(ServiceRequestDto serviceRequestDto)
        {
            Console.WriteLine("Admin notified about the service request.");
            await Task.CompletedTask;
        }
    }

    public class ServiceRequestDto
    {
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public DateTime ServiceStartDate { get; set; }
        public string PatientId { get; set; }
        public string? AssignedNurseId { get; set; }
    }


}
