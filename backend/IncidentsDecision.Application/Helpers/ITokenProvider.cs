using IncidentsDecision.Core.Models.Employee;
using IncidentsDecision.Core.Models.TechSupport;

public interface ITokenProvider
{
    public string Create(Employee employee);
    public string CreateForTechSup(TechSupport support);
}