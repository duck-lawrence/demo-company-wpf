using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Abstractions;
using DataAccessLayer.AppDbContext;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ICompanyDbContext _dbContext;

        public EmployeeRepository()
        {
            _dbContext = new CompanyDbContext();
        }

        public async Task DeleteAsync(Employee employee)
        {
            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(string? key)
        {
            var query = _dbContext.Employees
                .Include(e => e.Department)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(key))
            {
                var keyLower = key.ToLower();
                query = query.Where
                    (e =>
                        e.Name.ToLower().Contains(keyLower)
                        || (e.Address != null
                            && e.Address.ToLower().Contains(keyLower))
                    );
            }
            return await query.ToListAsync();
        }

        public async Task AddAsync(Employee employee)
        {
            employee.Id = _dbContext.Employees.Max(e => e.Id) + 1;
            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            var employeeFromDb = await _dbContext.Employees.FindAsync(employee.Id)
                ?? throw new DirectoryNotFoundException("employee not found");

            if (!string.IsNullOrEmpty(employee.Name)) employeeFromDb.Name = employee.Name;

            if (!string.IsNullOrEmpty(employee.Address)) employeeFromDb.Address = employee.Address;

            if (employee.DepartmentId != null) employeeFromDb.DepartmentId = employee.DepartmentId;

            if (employee.Age != null) employeeFromDb.Age = employee.Age;

            await _dbContext.SaveChangesAsync();
        }
    }
}