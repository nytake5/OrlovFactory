﻿using Debtus.TestTask.Entities;

namespace Debtus.IDAL
{
    public interface IEmployee_DAO
    {
        IEnumerable<Employee> GetAllEmployees();
        IEnumerable<Employee> GetAllEmployees(Post post);
        Employee GetById(int passNumber);
        void CreateEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        bool DeleteEmployee(int passNumber);
    }
}