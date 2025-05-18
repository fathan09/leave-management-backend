namespace LeaveBackend.Dtos;

public record class LoginDto(
    string username, 
    string password
);