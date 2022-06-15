using AutoMapper;
using Debtus.TestTask.Entities;
using Debtus.TestTask.Model;

namespace Debtus.TestTask
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<WorkingShiftView, WorkingShift>();
            CreateMap<EmployeeView, Employee>();
        }
    }
}
