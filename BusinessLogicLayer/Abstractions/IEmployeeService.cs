using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Abstractions
{
    public interface IEmployeeService
    {
        Task AddAsync(Employee employee);
        Task DeleteAsync(Employee employee);

        Task<IEnumerable<Employee>> GetAllAsync(string? key);
    }
}