using Entities;

namespace DAL.Interfaces
{
    public interface IWorkingShiftDao
    {
        void StartShift(WorkingShift workingShift);
        void EndShift(WorkingShift workingShift);
    }
}
