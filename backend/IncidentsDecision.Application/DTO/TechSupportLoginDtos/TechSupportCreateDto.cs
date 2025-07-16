namespace IncidentsDecision.Application.DTO.TechSupportLoginDtos;

public class TechSupportLoginCreateDto
{
    public string Login { get; set; } = string.Empty;
    public string HashedPassword { get; set; } = string.Empty;
}