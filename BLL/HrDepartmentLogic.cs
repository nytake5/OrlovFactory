using BLL.Interfaces;
using DAL.Interfaces;
using Entities;
using Microsoft.Extensions.Logging;

namespace BLL
{
    public class HrDepartmentLogic : BaseLogic, IHrDeparmentLogic
    {
        private readonly IEmployeeDao _employeeDao;

        public HrDepartmentLogic(
            IEmployeeDao employeeDao,
            ILogger<HrDepartmentLogic> logger) : base(logger)
        {
            _employeeDao = employeeDao;
        }

        public void CreateEmployee(Employee employee)
        {
            try
            {
                _employeeDao.CreateEmployee(employee);
            }
            catch (ArgumentException ex)
            {
                throw ex;    
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteEmployee(int passNumber)
        {
            return _employeeDao.DeleteEmployee(passNumber);
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            IEnumerable<Employee> employees = _employeeDao.GetAllEmployees();
            foreach (var employee in employees)
            {
                employee.NumberOfFines = GetNumberOfFines(employee.WorkingShifts.ToList(), employee.Post);
            }

            return employees;
        }

        public IEnumerable<Employee> GetAllEmployees(Post post)
        {
            IEnumerable<Employee> employees = _employeeDao.GetAllEmployees(post);
            foreach (var employee in employees)
            {
                employee.NumberOfFines = GetNumberOfFines(employee.WorkingShifts.ToList(), employee.Post);
            }

            return employees;
        }

        public Employee GetById(int passNumber)
        {
            try
            {
                Employee employee = _employeeDao.GetById(passNumber);
                employee.NumberOfFines = GetNumberOfFines(employee.WorkingShifts.ToList(), employee.Post);
                return employee;
            }
            catch (KeyNotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            try
            {
                _employeeDao.UpdateEmployee(employee);
            }
            catch (KeyNotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region private methods
        private int GetNumberOfFines(List<WorkingShift> workingShifts, Post post)
        {
            int cntFines = 0;
            if (post != Post.RectalSuppositoriesTester)
            {
                foreach (var workingShift in workingShifts)
                {
                
                    if (workingShift.StartShift?.Hour > 9)
                    {
                        cntFines++;
                    }
                    if (workingShift.EndShift?.Hour <= 18)
                    {
                        cntFines++;
                    }
                }
            }
            else
            {
                foreach (var workingShift in workingShifts)
                {

                    if (workingShift.StartShift?.Hour > 9)
                    {
                        cntFines++;
                    }
                    if (workingShift.EndShift?.Hour <= 21)
                    {
                        cntFines++;
                    }
                }
            }

            return cntFines;
        }

        #endregion
    }
}
