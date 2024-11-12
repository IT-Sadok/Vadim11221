using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrivateHospitals.Core.Models;

namespace PrivateHospitals.Data.Data;

public class HospitalDbContext: IdentityDbContext<AppUser>
{
    public HospitalDbContext(DbContextOptions<HospitalDbContext> options): base(options)
    {
    }
    
    public HospitalDbContext()
    {
    }
    

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

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