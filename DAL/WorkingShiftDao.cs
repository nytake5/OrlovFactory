using DAL.Interfaces;
using Entities;
using Entities.Exceptions;

namespace DAL
{
    public class WorkingShiftDao : BaseDao, IWorkingShiftDao
    {
        public WorkingShiftDao(
            FactoryContext dbContext) 
            : base(dbContext)
        {
        }
        
        public void EndShift(WorkingShift workingShift)
        {
            var shifts = DbContext.WorkingShifts
                .Where(w => w.EmployeeId == workingShift.EmployeeId);
            
            if (shifts == null || !shifts.Any())
            {
                throw new ShiftException("Employee did not open the last shift!");
            }

            if (shifts.Last().EndShift != null)
            {
                throw new ShiftException("Employee did not open the last shift!");
            }

            var lastShift = shifts.Last();
            lastShift.EndShift = workingShift.EndShift;
            DbContext.WorkingShifts.Update(lastShift);
            DbContext.SaveChanges();
        }

        public void StartShift(WorkingShift workingShift)
        {
            var shifts = DbContext.WorkingShifts
                .Where(w => w.EmployeeId == workingShift.EmployeeId).ToList();

            
            DbContext.Employees
                .Where(e => e.PassNumber == workingShift.EmployeeId)
                .ToList()
                .First();
            

            if (shifts.Count != 0)
            {
                if (shifts.Last().EndShift == null)
                {
                    throw new ShiftException("Employee did not close the last shift!");
                }

                DbContext.WorkingShifts.Add(workingShift);
                DbContext.SaveChanges();
            }
            else
            {
                DbContext.WorkingShifts.Add(workingShift);
                DbContext.SaveChanges();
            }
        }
    }
}
