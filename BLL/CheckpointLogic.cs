using BLL_Interface;
using Debtus.IDAL;
using Debtus.TestTask.Entities;
using Debtus.TestTask.Entities.Exceptions;

namespace BLL
{
    public class CheckpointLogic : ICheckpointLogic
    {
        private readonly IWorkingShift_DAO _workingShift_DAO;

        public CheckpointLogic(IWorkingShift_DAO workingShift_DAO)
        {
            _workingShift_DAO = workingShift_DAO;
        }

        public void StartShift(WorkingShift workingShift)
        {
            try
            {
                _workingShift_DAO.StartShift(workingShift);
            }
            catch (KeyNotFoundException ex)
            {
                throw ex;
            }
            catch (ShiftException ex)
            {
                throw ex;
            }
        }

        public void EndShift(WorkingShift workingShift)
        {
            try
            {
                _workingShift_DAO.EndShift(workingShift);
            }
            catch (KeyNotFoundException ex)
            {
                throw ex;
            }
            catch (ShiftException ex)
            {
                throw ex;
            }
        }
    }
}