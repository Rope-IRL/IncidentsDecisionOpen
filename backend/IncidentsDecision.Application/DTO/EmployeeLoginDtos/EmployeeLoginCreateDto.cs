namespace IncidentsDecision.Application.DTO.EmployeeLoginDtos;

public class EmployeeLoginCreateDto
{
    public string Login { get; set; } = string.Empty;
    public string HashedPassword { get; set; } = string.Empty; 
}