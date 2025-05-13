
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mosef.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly MosefDbContext _context;
        private readonly string _jwtSecret = "9d6a4fcbad06b3c7a23e9b41ac65d6bc9dff9ff038c5721b6b9b3d76c44b39f1";

        public AuthController(MosefDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var normalizedEmail = dto.email.Trim().ToLower();
            if (_context.Patients.Any(p => p.PatientEmail.ToLower() == normalizedEmail))
                return BadRequest("Email already in use.");

            if (dto.password.Length < 8 || !dto.password.Any(char.IsUpper) || !dto.password.Any(char.IsLower))
            {
                return BadRequest("Password is weak. It must be at least 8 characters long and contain both uppercase and lowercase letters.");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.password);

            var NewPatient = PatientFactory.CreatePatient(dto.ssn, dto.firstName,
                dto.lastName, dto.email, hashedPassword, dto.phone, dto.location, dto.gender , dto.birthDate , dto.healthState);


            _context.Patients.Add(NewPatient);
            await _context.SaveChangesAsync();

            var patientData = PatientFactory.CreatePatientObj(NewPatient);

            return Ok(new { message = "User registered successfully", patient = patientData });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var normalizedEmail = dto.email.Trim().ToLower();
            var patient = await _context.Patients
                                        .FirstOrDefaultAsync(p => p.PatientEmail.ToLower() == normalizedEmail);

            if (patient == null)
                return BadRequest("Email or password is incorrect.");

            if (!BCrypt.Net.BCrypt.Verify(dto.password, patient.PatientPassword))
            {
                return BadRequest("Email or password is incorrect.");
            }

            var patientData = PatientFactory.CreatePatientObj(patient);

            return Ok(new { message = "Login successful", patient = patientData });
        }

        [HttpPost("request-service")]
        public async Task<IActionResult> RequestService([FromBody] ServiceRequestDto serviceRequestDto)
        {
            var healthCheckHandler = new PatientHealthCheckHandler(_context);
            var nurseAvailabilityHandler = new NurseAvailabilityHandler(_context);
            var registerServiceHandler = new RegisterServiceRequestHandler(_context);
            var notifyAdminHandler = new NotifyAdminHandler();

            healthCheckHandler.SetNextHandler(nurseAvailabilityHandler);
            nurseAvailabilityHandler.SetNextHandler(registerServiceHandler);
            registerServiceHandler.SetNextHandler(notifyAdminHandler);

            var result = await healthCheckHandler.HandleServiceRequest(serviceRequestDto);

            return result;
        }

        [HttpGet("all-patients")]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = await _context.Patients
                .Select(p => new
                {
                    p.PatientId,
                    p.PatientFirstName,
                    p.PatientLastName,
                    p.PatientEmail,
                    p.PatientPhone,
                    p.PatientLocation,
                    p.PatientGender,
                    p.BirthDate,
                    p.PatientStatus
                })
                .ToListAsync();

            return Ok(patients);
        }

        [HttpPost("create-appointment")]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentDto dto)
        {
            var patient = await _context.Patients.FindAsync(dto.patientId);

            if (patient == null)
                return BadRequest("Patient not found.");

            var appointment = new Appointment(dto.patientId, null, dto.date, null, null, dto.time, dto.notes);

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Appointment created successfully",
                appointmentId = appointment.AppointmentId,
                date = appointment.AppointmentDate,
                patientId = appointment.PatientId
            });

        }

    }
    public class RegisterDto
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string ssn { get; set; }
        public string phone { get; set; }
        public string location { get; set; }
        public string ?gender { get; set; }
        public string? birthDate { get; set; }
        public string? healthState { get; set; }
    }

    public class LoginDto
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}
public class AppointmentDto
{
    public string patientId { get; set; }
    public DateTime date { get; set; }
    public string? time { get; set; }
    public string? notes { get; set; }
}
