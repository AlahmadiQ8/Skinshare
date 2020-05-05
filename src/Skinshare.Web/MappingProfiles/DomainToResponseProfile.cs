using AutoMapper;
using Skinshare.Core.Entities;
using Skinshare.Web.Contracts.Responses;

namespace Skinshare.Web.MappingProfiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Routine, RoutineResponse>();
            CreateMap<Step, StepResponse>();
        }
    }
}