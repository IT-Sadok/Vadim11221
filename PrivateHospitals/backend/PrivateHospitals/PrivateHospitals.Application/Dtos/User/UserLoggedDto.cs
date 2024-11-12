namespace PrivateHospitals.Application.Dtos.User;

public record UserLoggedDto
{
    public string Email { get; init; }
    public string Token { get; init; }
}