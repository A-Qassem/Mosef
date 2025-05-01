using Microsoft.EntityFrameworkCore;
using Mosef;

namespace Mosef
{
    public class MosefDbContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=Mosef;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;");
        }

        public MosefDbContext(DbContextOptions<MosefDbContext> options) : base(options) { }

    }
}
