using Debtus.IDAL;
using Debtus.TestTask.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Core;

namespace Debtus.DAL
{
    public class Employee_DAO : IEmployee_DAO
    {
        public void CreateEmployee(Employee employee)
        {
            using (var context = new MyDbContext())
            {
                try
                {
                    context.Add(employee);
                    context.SaveChanges();
                }
                catch (EntityException)
                {
                    throw new ArgumentException("Data entered incorrectly"); 
                }
            }
        }

        public bool DeleteEmployee(int passNumber)
        {
            using (var context = new MyDbContext())
            {
                try
                {
                    var employee = context.Employees.Where(e => e.PassNumber == passNumber).First();
                    context.Employees.Remove(employee);
                    context.SaveChanges();
                    return true;
                }
                catch (InvalidOperationException)
                {
                    return false;
                }
            }
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            using (var context = new MyDbContext())
            {
                return context.Employees.Include(e => e.WorkingShifts).ToList();
            }
        }

        public IEnumerable<Employee> GetAllEmployees(Post post)
        {
            using (var context = new MyDbContext())
            {
                return context.Employees.Where(e => e.Post == post).Include(e => e.WorkingShifts).ToList();
            }
        }

        public Employee GetById(int passNumber)
        {
            using (var context = new MyDbContext())
            {
                try
                {
                    return context.Employees.Where(e => e.PassNumber == passNumber).Include(e => e.WorkingShifts).First();
                }
                catch (InvalidOperationException)
                {
                    throw new KeyNotFoundException("There is no such employee!");
                }
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            using (var context = new MyDbContext())
            {
                try
                {
                    var oldEmployee = context.Employees.Where(e => e.PassNumber == employee.PassNumber).First();

                    oldEmployee.FirstName = employee.FirstName;
                    oldEmployee.LastName = employee.LastName;
                    oldEmployee.Patronymic = employee.Patronymic;
                    oldEmployee.Post = employee.Post;

                    context.Employees.Update(oldEmployee);
                    context.SaveChanges();
                }
                catch (InvalidOperationException)
                {
                    throw new KeyNotFoundException("There is no such employee!");
                }
            }
        }
    }
}
