using AutoMapper;
using Debtus.TestTask.Entities;
using Debtus.TestTask.Model;
using Debtus.TestTask.Views;

namespace Debtus.TestTask
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
