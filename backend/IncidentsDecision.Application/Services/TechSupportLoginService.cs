using IncidentsDecision.Application.DTO.TechSupportLoginDtos;
using IncidentsDecision.Application.Interfaces;
using IncidentsDecision.Application.Mappers;
using IncidentsDecision.Core.Helpers;
using IncidentsDecision.Core.Repositories;

namespace IncidentsDecision.Application.Services;

public class TechSupportLoginService(ITechSupportLoginRepository repo, ITechSupportRepository techSupportRepository,
    ITokenProvider provider) : ITechSupportLoginService 
{
    public async Task<IEnumerable<TechSupportLoginDto>> GetTechSupportLogins(CancellationToken cancellationToken)
    {
        var TechSupportLogins = await repo.GetTechSupportLogins(cancellationToken);
        var TechSupportLoginDtos = new List<TechSupportLoginDto>();
        foreach (var TechSupportLogin in TechSupportLogins)
        {
            TechSupportLoginDtos.Add(TechSupportLoginMapper.FromDomainToDto(TechSupportLogin));
        }

        return TechSupportLoginDtos;
    }

    public async Task<Result<TechSupportLoginDto>> GetTechSupportLoginById(int id, CancellationToken cancellationToken)
    {
        var TechSupportLoginResult = await repo.GetTechSupportLoginById(id, cancellationToken);

        if (TechSupportLoginResult.IsSuccess == false)
        {
            return Result<TechSupportLoginDto>.Failure(TechSupportLoginResult.Error);
        }

        var TechSupportLogin = TechSupportLoginMapper.FromDomainToDto(TechSupportLoginResult.Value);

        return Result<TechSupportLoginDto>.Success(TechSupportLogin);
    }

    public async Task<Result<TechSupportLoginDto>> CreateTechSupportLogin(TechSupportLoginCreateDto dto, CancellationToken cancellationToken)
    {
        var TechSupportLoginResult = TechSupportLoginMapper.FromCreateDtoToDomain(dto);

        if (TechSupportLoginResult.IsSuccess == false)
        {
            return Result<TechSupportLoginDto>.Failure(TechSupportLoginResult.Error);
        }

        var createRes = await repo.CreateTechSupportLogin(TechSupportLoginResult.Value, cancellationToken);

        if (createRes.IsSuccess == false)
        {
            return Result<TechSupportLoginDto>.Failure(createRes.Error);
        }

        var res = Result<TechSupportLoginDto>.Success(TechSupportLoginMapper.FromDomainToDto(createRes.Value));

        return res;
    }

    public async Task<Result<TechSupportLoginDto>> UpdateTechSupportLogin(TechSupportLoginUpdateDto dto, CancellationToken cancellationToken)
    {
        var TechSupportLoginRes = TechSupportLoginMapper.FromUpdateDtoToDomain(dto);

        if (TechSupportLoginRes.IsSuccess == false)
        {
            return Result<TechSupportLoginDto>.Failure(TechSupportLoginRes.Error);
        }

        var updateRes = await repo.UpdateTechSupportLogin(TechSupportLoginRes.Value, cancellationToken);

        if (updateRes.IsSuccess == false)
        {
            return Result<TechSupportLoginDto>.Failure(updateRes.Error);
        }

        var res = Result<TechSupportLoginDto>.Success(TechSupportLoginMapper.FromDomainToDto(updateRes.Value));

        return res;
    }

    public async Task<Result<TechSupportLogin>> DeleteTechSupportLogin(int id, CancellationToken cancellationToken)
    {
        var TechSupportLoginRes = await repo.DeleteTechSupportLogin(id, cancellationToken);

        if (TechSupportLoginRes.IsSuccess == false)
        {
            return Result<TechSupportLogin>.Failure(TechSupportLoginRes.Error);
        }

        return Result<TechSupportLogin>.Success(TechSupportLoginRes.Value);
    }
    public async Task<Result<string>> LoginTechSupport(string login, string password, CancellationToken cancellationToken)
    {
        var techSupportLoginResult = await repo.GetTechSupportLoginByLogin(login, cancellationToken);

        if (techSupportLoginResult.IsSuccess == false)
        {
            return Result<string>.Failure(techSupportLoginResult.Error);
        }

        if (techSupportLoginResult.Value.HashedPassword != password)
        {
            return Result<string>.Failure("Failed to login with such password");
        }

        if (techSupportLoginResult.Value.SupportId == null)
        {
            return Result<string>.Failure("Failed to find employee with such credentials");
        }

        var techSupportResult = await techSupportRepository.GetTechSupportById((int)techSupportLoginResult.Value.SupportId,
            cancellationToken);

        if (techSupportResult.IsSuccess == false)
        {
            return Result<string>.Failure(techSupportResult.Error);
        }

        var token = provider.CreateForTechSup(techSupportResult.Value);

        return Result<string>.Success(token);

    }
}