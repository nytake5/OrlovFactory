using Debtus.TestTask.Entities;

namespace Debtus.IDAL
{
    public interface IWorkingShift_DAO
    {
        void StartShift(WorkingShift workingShift);
        void EndShift(WorkingShift workingShift);
    }
}
