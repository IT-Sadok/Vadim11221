using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrivateHospitals.Core.Models;
using PrivateHospitals.Core.Models.Users;

namespace PrivateHospitals.Infrastructure.Data;

public class HospitalDbContext: IdentityDbContext<AppUser>
{
    public HospitalDbContext(DbContextOptions<HospitalDbContext> options): base(options)
    {
    }
    
    public HospitalDbContext()
    {
    }
    
    public DbSet<Appointment> Appointments { get; set; }
    

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppUser>()
            .HasDiscriminator<string>("Role")
            .HasValue<Doctor>("Doctor")
            .HasValue<Patient>("Patient");

        List<IdentityRole> roles = new List<IdentityRole>()
        {
            new IdentityRole
            {
                Name = "Doctor",
                NormalizedName = "DOCTOR",
            },
            new IdentityRole
            {
                Name= "Patient",
                NormalizedName= "PATIENT",
            } 
        };
        builder.Entity<IdentityRole>().HasData(roles);
    }
    
}