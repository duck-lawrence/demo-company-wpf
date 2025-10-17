using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Abstractions;
using DataAccessLayer.AppDbContext;

namespace DataAccessLayer.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ICompanyDbContext _dbContext;

        public AccountRepository()
        {
            _dbContext = new CompanyDbContext();
        }
    }
}