namespace IncidentsDecision.Application.DTO.TechSupportLoginDtos;

public class TechSupportLoginDto
{
    public int Id { get; set; } = 0;
    public string Login { get; set; } = string.Empty;
    public string HashedPassword { get; set; } = string.Empty;
}