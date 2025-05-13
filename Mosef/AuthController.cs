
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
            var normalizedEmail = dto.Email.Trim().ToLower();
            if (_context.Patients.Any(p => p.PatientEmail.ToLower() == normalizedEmail))
                return BadRequest("Email already in use.");

            if (dto.Password.Length < 8 || !dto.Password.Any(char.IsUpper) || !dto.Password.Any(char.IsLower))
            {
                return BadRequest("Password is weak. It must be at least 8 characters long and contain both uppercase and lowercase letters.");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var NewPatient = PatientFactory.CreatePatient(dto.SSN, dto.FirstName,
                dto.LastName, dto.Email, hashedPassword, dto.Phone, dto.Location, dto.Gender, dto.BirthDate, dto.Status);


            _context.Patients.Add(NewPatient);
            await _context.SaveChangesAsync();
            return Ok(new { message = "User registered successfully", userId = NewPatient.PatientId });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var normalizedEmail = dto.Email.Trim().ToLower();
            var patient = await _context.Patients
                                        .FirstOrDefaultAsync(p => p.PatientEmail.ToLower() == normalizedEmail);

            if (patient == null)
                return BadRequest("Email or password is incorrect.");

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, patient.PatientPassword))
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


    }
    public class RegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string SSN { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Status { get; set; }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
