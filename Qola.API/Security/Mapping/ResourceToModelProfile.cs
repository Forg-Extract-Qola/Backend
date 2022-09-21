using AutoMapper;
using Qola.API.Security.Domain.Models;
using Qola.API.Security.Domain.Services.Communication;

namespace Qola.API.Security.Mapping;

public class ResourceToModelProfile :Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<RegisterRequest, Manager>();
        CreateMap<UpdateRequest, Manager>().ForAllMembers(options => 
            options.Condition((source, target, property) =>
                {
                    if (property == null) return false;
                    if (property.GetType() == typeof(string) && string.IsNullOrEmpty((string)property)) return false;
                    return true;
                }
                
            ));
    }
}