using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Abstractions;
using DataAccessLayer.Abstractions;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;

namespace BusinessLogicLayer.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repo;

        public DepartmentService()
        {
            _repo = new DepartmentRepository();
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }
    }
}