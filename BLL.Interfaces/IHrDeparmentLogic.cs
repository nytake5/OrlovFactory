using Entities;

namespace BLL.Interfaces
{
    public interface IHrDeparmentLogic
    {
        IEnumerable<Employee> GetAllEmployees();
        IEnumerable<Employee> GetAllEmployees(Post post);
        Employee GetById(int passNumber);
        void CreateEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        bool DeleteEmployee(int passNumber);
    }
}