using Entities;

namespace DAL.Interfaces
{
    public interface IEmployeeDao
    {
        IEnumerable<Employee> GetAllEmployees();
        IEnumerable<Employee> GetAllEmployees(Post post);
        Employee GetById(int passNumber);
        void CreateEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        bool DeleteEmployee(int passNumber);
    }
}