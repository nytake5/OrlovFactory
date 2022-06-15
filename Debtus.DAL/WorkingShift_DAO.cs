using Debtus.IDAL;
using Debtus.TestTask.Entities;
using Debtus.TestTask.Entities.Exceptions;

namespace Debtus.DAL
{
    public class WorkingShift_DAO : IWorkingShift_DAO
    {
        public void EndShift(WorkingShift workingShift)
        {
            using (var context = new MyDbContext())
            {
                var shifts = context.WorkingShifts
                    .Where(w => w.EmployeeId == workingShift.EmployeeId).ToList();

                try
                {
                    context.Employees
                        .Where(e => e.PassNumber == workingShift.EmployeeId)
                        .ToList()
                        .First();
                }
                catch
                {
                    throw new KeyNotFoundException("There is no such employee!");
                }

                if (shifts == null || shifts.Count == 0)
                {
                    throw new ShiftException("Employee did not open the last shift!");
                }
                else
                {
                    if (shifts.Last().EndShift != null)
                    {
                        throw new ShiftException("Employee did not open the last shift!");
                    }
                    else
                    {
                        var lastShift = shifts.Last();
                        lastShift.EndShift = workingShift.EndShift;
                        context.WorkingShifts.Update(lastShift);
                        context.SaveChanges();
                    }
                }
            }
        }

        public void StartShift(WorkingShift workingShift)
        {
            using (var context = new MyDbContext())
            {
                var shifts = context.WorkingShifts
                    .Where(w => w.EmployeeId == workingShift.EmployeeId).ToList();

                try
                {
                    context.Employees
                        .Where(e => e.PassNumber == workingShift.EmployeeId)
                        .ToList()
                        .First();
                }
                catch
                {
                    throw new KeyNotFoundException("There is no such employee!");
                }

                if (shifts.Count != 0)
                {
                    if (shifts.Last().EndShift == null)
                    {
                        throw new ShiftException("Employee did not close the last shift!");
                    }
                    else
                    {
                        context.WorkingShifts.Add(workingShift);
                        context.SaveChanges();
                    }
                }
                else
                {
                    context.WorkingShifts.Add(workingShift);
                    context.SaveChanges();
                }
            }
        }
    }
}
