using System;
using System.Linq;
using System.Threading.Tasks;
using SuiteAccount.Shared.Exceptions;
using SuiteAccount.SqlModel.Dtos;
using SuiteAccount.SqlModel.Persistors;
using SuiteAccount.SqlModel.Services.Abstracts;

namespace SuiteAccount.SqlModel.Services.Concretes
{
    public class SqlAccountService : ISqlAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SqlAccountService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task CreateAccountAsync(Guid accountId, string userName, string password)
        {
            if (accountId == Guid.Empty)
                throw new ArgumentNullException("accountId", DomainExceptions.AccountIdNullException);

            if (String.IsNullOrEmpty(userName))
                throw new ArgumentNullException("userName", DomainExceptions.UserNameNullException);

            if (String.IsNullOrEmpty(password))
                throw new ArgumentNullException("password", DomainExceptions.PasswordNullException);

            var userResults = await this._unitOfWork.AccountPersistor.QueryAsync(a => a.Id == accountId);
            if (userResults.Any()) return;

            var currentuser = new DtoAccount
            {
                Id = accountId,
                UserName = userName,
                Password = password,
                CreationDate = DateTime.UtcNow,
                IsApproved = false,
                IsLockedOut = true,
                IsOnline = true
            };
            this._unitOfWork.AccountPersistor.Insert(currentuser);
            await this._unitOfWork.CommitAsync();
        }

        public async Task UpdateEmailAsync(Guid accountId, string email)
        {
            if (accountId == Guid.Empty)
                throw new ArgumentNullException("accountId", DomainExceptions.AccountIdNullException);

            var currentUser = await this._unitOfWork.AccountPersistor.GetByIdAsync(accountId);
            if (currentUser == null) return;

            currentUser.Email = email;
            this._unitOfWork.AccountPersistor.Update(currentUser);
            await this._unitOfWork.CommitAsync();
        }

        public async Task<Guid> VerifyAccountAsync(string userName, string password)
        {
            var accountResults =
                await
                    this._unitOfWork.AccountPersistor.QueryAsync(u => u.UserName == userName && u.Password == password);

            if (!accountResults.Any()) return Guid.Empty;

            var accountVerified = accountResults.First();

            // TODO: Check if IsLocked, IsApproved, ....
            return accountVerified.Id;
        }
    }
}
