using BusinessLogicLayer.Abstractions;
using DataAccessLayer.Abstractions;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;

namespace BusinessLogicLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService()
        {
            _accountRepository = new AccountRepository();
        }

        public async Task<Account> LoginAsync(string email, string password)
        {
            var account = await _accountRepository.GetByEmailAndPasswordAsync(email, password)
                ?? throw new DirectoryNotFoundException("invalid email or password");
            if (account.Role == 4)
                throw new UnauthorizedAccessException("not authentication");
            return account;
        }
    }
}