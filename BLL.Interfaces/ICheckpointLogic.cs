using Entities;

namespace BLL.Interfaces
{
    public interface ICheckpointLogic
    {
        public void StartShift(WorkingShift workingShift);
        public void EndShift(WorkingShift workingShift);
    }
}
