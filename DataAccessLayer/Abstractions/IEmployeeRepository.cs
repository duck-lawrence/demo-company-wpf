using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Abstractions
{
    public interface IEmployeeRepository
    {
        Task AddAsync(Employee employee);
        Task DeleteAsync(Employee employee);

        Task<IEnumerable<Employee>> GetAllAsync(string? key);
        Task UpdateAsync(Employee employee);
    }
}