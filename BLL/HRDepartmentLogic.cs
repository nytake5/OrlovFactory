using BLL_Interface;
using Debtus.IDAL;
using Debtus.TestTask.Entities;

namespace BLL
{
    public class HRDepartmentLogic : IHRDeparmentLogic
    {
        private readonly IEmployee_DAO _employee_DAO;

        public HRDepartmentLogic(IEmployee_DAO employee_DAO)
        {
            _employee_DAO = employee_DAO;
        }

        public void CreateEmployee(Employee employee)
        {
            try
            {
                _employee_DAO.CreateEmployee(employee);
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
            return _employee_DAO.DeleteEmployee(passNumber);
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            IEnumerable<Employee> employees = _employee_DAO.GetAllEmployees();
            foreach (var employee in employees)
            {
                employee.NumberOfFines = GetNumberOfFines(employee.WorkingShifts.ToList(), employee.Post);
            }

            return employees;
        }

        public IEnumerable<Employee> GetAllEmployees(Post post)
        {
            IEnumerable<Employee> employees = _employee_DAO.GetAllEmployees(post);
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
                Employee employee = _employee_DAO.GetById(passNumber);
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
                _employee_DAO.UpdateEmployee(employee);
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
