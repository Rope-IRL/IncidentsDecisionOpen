namespace IncidentsDecision.Application.DTO.NotResolvedIncidentsDtos;

public class NotResolvedIncidentUpdateDto
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Day { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public int Hour { get; set; }
    public int Minutes { get; set; }
}