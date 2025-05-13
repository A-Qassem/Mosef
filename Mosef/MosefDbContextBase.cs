using Microsoft.EntityFrameworkCore;
using Mosef;
using System;

namespace Mosef
{
    public class MosefDbContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public MosefDbContext(DbContextOptions<MosefDbContext> options) : base(options) { }
    }
}
