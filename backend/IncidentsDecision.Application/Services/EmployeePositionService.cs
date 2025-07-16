using IncidentsDecision.Application.DTO.EmployeePositionDtos;
using IncidentsDecision.Application.Interfaces;
using IncidentsDecision.Application.Mappers;
using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Models.EmployeePosition;
using IncidentsDecision.Core.Repositories;

namespace IncidentsDecision.Application.Services;

public class EmployeePositionService(IEmployeePositionRepository repo): IEmployeePositionService
{
    public async Task<IEnumerable<EmployeePositionDto>> GetEmployeePositions(CancellationToken cancellationToken)
    {
        var EmployeePositions = await repo.GetEmployeePositions(cancellationToken);
        var EmployeePositionDtos = new List<EmployeePositionDto>();
        foreach (var EmployeePosition in EmployeePositions)
        {
            EmployeePositionDtos.Add(EmployeePositionMapper.FromDomainToDto(EmployeePosition));
        }

        return EmployeePositionDtos;
    }

    public async Task<Result<EmployeePositionDto>> GetEmployeePositionById(int id, CancellationToken cancellationToken)
    {
        var EmployeePositionResult = await repo.GetEmployeePositionById(id, cancellationToken);

        if (EmployeePositionResult.IsSuccess == false)
        {
            return Result<EmployeePositionDto>.Failure(EmployeePositionResult.Error);
        }

        var EmployeePosition = EmployeePositionMapper.FromDomainToDto(EmployeePositionResult.Value);

        return Result<EmployeePositionDto>.Success(EmployeePosition);
    }

    public async Task<Result<EmployeePositionDto>> CreateEmployeePosition(EmployeePositionCreateDto dto, CancellationToken cancellationToken)
    {
        var EmployeePositionResult = EmployeePositionMapper.FromCreateDtoToDomain(dto);

        if (EmployeePositionResult.IsSuccess == false)
        {
            return Result<EmployeePositionDto>.Failure(EmployeePositionResult.Error);
        }

        var createRes = await repo.CreateEmployeePosition(EmployeePositionResult.Value, cancellationToken);

        if (createRes.IsSuccess == false)
        {
            return Result<EmployeePositionDto>.Failure(createRes.Error);
        }

        var res = Result<EmployeePositionDto>.Success(EmployeePositionMapper.FromDomainToDto(createRes.Value));

        return res;
    }

    public async Task<Result<EmployeePositionDto>> UpdateEmployeePosition(EmployeePositionUpdateDto dto, CancellationToken cancellationToken)
    {
        var EmployeePositionRes = EmployeePositionMapper.FromUpdateDtoToDomain(dto);

        if (EmployeePositionRes.IsSuccess == false)
        {
            return Result<EmployeePositionDto>.Failure(EmployeePositionRes.Error);
        }

        var updateRes = await repo.UpdateEmployeePosition(EmployeePositionRes.Value, cancellationToken);

        if (updateRes.IsSuccess == false)
        {
            return Result<EmployeePositionDto>.Failure(updateRes.Error);
        }

        var res = Result<EmployeePositionDto>.Success(EmployeePositionMapper.FromDomainToDto(updateRes.Value));

        return res;
    }

    public async Task<Result<EmployeePosition>> DeleteEmployeePosition(int id, CancellationToken cancellationToken)
    {
        var EmployeePositionRes = await repo.DeleteEmployeePosition(id, cancellationToken);

        if (EmployeePositionRes.IsSuccess == false)
        {
            return Result<EmployeePosition>.Failure(EmployeePositionRes.Error);
        }

        return Result<EmployeePosition>.Success(EmployeePositionRes.Value);
    }
}