using AutoMapper;
using Entities;
using Factory.WebApi.Views;

namespace Factory.WebApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<StartWorkingShiftView, WorkingShift>();
            CreateMap<EndWorkingShiftView, WorkingShift>();
            CreateMap<EmployeeView, Employee>();
        }
    }
}
