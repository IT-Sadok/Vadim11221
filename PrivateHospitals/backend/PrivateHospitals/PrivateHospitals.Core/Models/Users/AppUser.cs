using Microsoft.AspNetCore.Identity;

namespace PrivateHospitals.Core.Models.Users;

public class AppUser : IdentityUser
{
    public string? ExternalId { get; set; }
    public string? CompanyId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
}