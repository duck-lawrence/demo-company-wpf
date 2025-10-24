using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Abstractions;
using DataAccessLayer.AppDbContext;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ICompanyDbContext _dbContext;

        public DepartmentRepository()
        {
            _dbContext = new CompanyDbContext();
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _dbContext.Departments.ToListAsync();
        }
    }
}