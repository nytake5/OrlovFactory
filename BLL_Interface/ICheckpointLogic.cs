using Debtus.TestTask.Entities;

namespace BLL_Interface
{
    public interface ICheckpointLogic
    {
        public void StartShift(WorkingShift workingShift);
        public void EndShift(WorkingShift workingShift);
    }
}
