using Microsoft.AspNetCore.Identity;
using PrivateHospitals.Core.Models.Users;

namespace PrivateHospitals.Infrastructure.Interfaces.User;

public interface IUserRepository
{
    Task<AppUser?> GetUserByEmailAsync(string email);
    Task<IdentityResult> CreateUserAsync(AppUser user, string password);
    Task<IdentityResult> AddUserToRoleAsync(AppUser user, string roleName);
}