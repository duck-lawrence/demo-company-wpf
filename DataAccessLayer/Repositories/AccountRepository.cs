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
    public class AccountRepository : IAccountRepository
    {
        private readonly ICompanyDbContext _dbContext;

        public AccountRepository()
        {
            _dbContext = new CompanyDbContext();
        }

        public async Task<Account?> GetByEmailAndPasswordAsync(string email, string password)
        {
            return await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Email == email && a.Password == password);
        }
    }
}