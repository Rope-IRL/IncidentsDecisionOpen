using IncidentsDecision.Application.DTO.TechSupportDtos;
using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Models.TechSupport;

namespace IncidentsDecision.Application.Interfaces;

public interface ITechSupportService
{
    public Task<IEnumerable<TechSupportDto>> GetTechSupports(CancellationToken cancellationToken);
    public Task<Result<TechSupportDto>> GetTechSupportById(int id, CancellationToken cancellationToken);
    public Task<Result<TechSupportDto>> CreateTechSupport(TechSupportCreateDto dto, CancellationToken cancellationToken);
    public Task<Result<TechSupportDto>> UpdateTechSupport(TechSupportUpdateDto dto, CancellationToken cancellationToken);
    public Task<Result<TechSupport>> DeleteTechSupport(int id, CancellationToken cancellationToken);
}