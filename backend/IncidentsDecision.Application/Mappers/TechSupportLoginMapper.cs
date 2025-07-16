using IncidentsDecision.Application.DTO.TechSupportLoginDtos;
using IncidentsDecision.Core.Helpers;

namespace IncidentsDecision.Application.Mappers;

public class TechSupportLoginMapper
{
    public static Result<TechSupportLogin> FromCreateDtoToDomain(TechSupportLoginCreateDto dto)
    {
        int? id = null;
        int? supportId = null;
        var techSupportLoginResult = TechSupportLogin.Create(id, dto.Login, dto.HashedPassword, supportId);

        if (techSupportLoginResult.IsSuccess == false)
        {
            return Result<TechSupportLogin>.Failure(techSupportLoginResult.Error);
        }

        return Result<TechSupportLogin>.Success(techSupportLoginResult.Value);
    }
    public static Result<TechSupportLogin> FromUpdateDtoToDomain(TechSupportLoginUpdateDto dto)
    {
        int? supportId = null;
        var techSupportLoginResult = TechSupportLogin.Create(dto.Id, dto.Login, dto.HashedPassword, supportId);

        if (techSupportLoginResult.IsSuccess == false)
        {
            return Result<TechSupportLogin>.Failure(techSupportLoginResult.Error);
        }

        return Result<TechSupportLogin>.Success(techSupportLoginResult.Value);
    }

    public static TechSupportLoginDto FromDomainToDto(TechSupportLogin techSupportLogin)
    {
        var techSupportLoginDto = new TechSupportLoginDto()
        {
            Id = (int)techSupportLogin.Id,
            Login = techSupportLogin.Login,
            HashedPassword = techSupportLogin.HashedPassword
        };

        return techSupportLoginDto;
    }
}