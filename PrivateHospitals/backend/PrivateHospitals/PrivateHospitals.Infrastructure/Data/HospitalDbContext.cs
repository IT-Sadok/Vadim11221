using System.Text.Json;
using System.Text.Json.Serialization;
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

        builder.Entity<Doctor>()
            .HasBaseType<AppUser>();
        
        builder.Entity<Doctor>()
            .Property(d => d.WorkingHours)
            .HasColumnType("jsonb")
            .HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }),
                v => JsonSerializer.Deserialize<List<WorkingHours>>(v, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            );

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