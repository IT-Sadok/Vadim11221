using PrivateHospitals.Core.Models.Users;

namespace PrivateHospitals.Application.Interfaces.Token;

public interface ITokenService
{
    string CreateToken(AppUser user);
}