using BLL.Interfaces;
using DAL.Interfaces;
using Entities;
using Entities.Exceptions;
using Microsoft.Extensions.Logging;

namespace BLL
{
    public class CheckpointLogic : BaseLogic, ICheckpointLogic
    {
        private readonly IWorkingShiftDao _workingShiftDao;

        public CheckpointLogic(
            IWorkingShiftDao workingShiftDao,
            ILogger<CheckpointLogic> logger)
            : base(logger)
        {
            _workingShiftDao = workingShiftDao;
        }

        public void StartShift(WorkingShift workingShift)
        {
            try
            {
                _workingShiftDao.StartShift(workingShift);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("There is no such employee!");
            }
        }

        public void EndShift(WorkingShift workingShift)
        {
            try
            {
                _workingShiftDao.EndShift(workingShift);
            }
            catch (ShiftException ex)
            {
                throw ex;
            }
        }
    }
}