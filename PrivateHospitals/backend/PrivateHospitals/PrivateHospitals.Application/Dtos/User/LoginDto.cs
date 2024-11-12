namespace PrivateHospitals.Application.Dtos.User;

public record LoginDto
{
    public string Email { get; init; }
    public string Password { get; init; }
}