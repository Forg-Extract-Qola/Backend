using AutoMapper;
using Qola.API.Security.Domain.Models;
using Qola.API.Security.Domain.Services.Communication;
using Qola.API.Security.Resources;

namespace Qola.API.Security.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Manager, ManagerResource>();
        CreateMap<Manager, AuthenticateResponse>();
    }
}