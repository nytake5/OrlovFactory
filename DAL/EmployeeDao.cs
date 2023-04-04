using DAL.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using CommunityToolkit.Diagnostics;

namespace DAL
{
    public class EmployeeDao : BaseDao, IEmployeeDao
    {
        public EmployeeDao(
            FactoryContext dbContext) 
            : base(dbContext)
        {
        }
        
        public void CreateEmployee(Employee employee)
        {
            try
            {
                DbContext.Add(employee);
                DbContext.SaveChanges();
            }
            catch (Exception)
            {
                ThrowHelper.ThrowArgumentException("Data entered incorrectly"); 
            }
        }

        public bool DeleteEmployee(int passNumber)
        {
            try
            {
                var employee = DbContext.Employees
                    .First(e => e.PassNumber == passNumber);
                DbContext.Employees.Remove(employee);
                DbContext.SaveChanges();    
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return DbContext.Employees.Include(e => e.WorkingShifts).ToList();
        }

        public IEnumerable<Employee> GetAllEmployees(Post post)
        {
            var employees = DbContext.Employees
                .Where(e => e.Post == post)
                .Include(e => e.WorkingShifts)
                .ToList();
            
            return employees;
        }

        public Employee GetById(int passNumber)
        {
            try
            {
                return DbContext.Employees
                    .Where(e => e.PassNumber == passNumber)
                    .Include(e => e.WorkingShifts)
                    .First();
            }
            catch (InvalidOperationException)
            {
                throw new KeyNotFoundException("There is no such employee!");
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            try
            {
                var oldEmployee = DbContext.Employees
                    .AsNoTracking()
                    .First(e => e.PassNumber == employee.PassNumber);

                oldEmployee.FirstName = employee.FirstName;
                oldEmployee.LastName = employee.LastName;
                oldEmployee.Patronymic = employee.Patronymic;
                oldEmployee.Post = employee.Post;

                DbContext.Employees.Update(oldEmployee);
                DbContext.SaveChanges();
            }
            catch (InvalidOperationException)
            {
                throw new KeyNotFoundException("There is no such employee!");
            }
        }

    }
}
