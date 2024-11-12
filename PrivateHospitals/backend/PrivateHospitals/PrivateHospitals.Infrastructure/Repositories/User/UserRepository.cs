using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PrivateHospitals.Core.Models;
using PrivateHospitals.Data.Data;
using PrivateHospitals.Data.Interfaces.User;

namespace PrivateHospitals.Data.Repositories.User;

public class UserRepository(
    UserManager<AppUser> _userManager,
    HospitalDbContext _hospitalDbContext
): IUserRepository
{

    public async Task<AppUser?> GetUserByEmailAsync(string email)
    {
        return await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<IdentityResult> CreateUserAsync(AppUser user, string password)
    {
        await _userManager.CreateAsync(user, password);
        await _hospitalDbContext.SaveChangesAsync();
        
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> AddUserToRoleAsync(AppUser user, string roleName)
    {
        return await _userManager.AddToRoleAsync(user, roleName);
    }
}