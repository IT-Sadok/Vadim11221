namespace PrivateHospitals.Application.Dtos.User;

public record RegisterDto
{
    public required string CompanyId { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string UserName { get; init; }
    public required string Email { get; init; }
    public required string Role { get; init; }
    public required string Password { get; init; }
}