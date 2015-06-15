using System;
using System.Linq;
using System.Threading.Tasks;

using SuiteAccount.Domain.Shared.Exceptions;
using SuiteAccount.QueryModel.Denormalizer.Abstracts;
using SuiteAccount.QueryModel.Dtos;
using SuiteAccount.QueryModel.Persistors;

namespace SuiteAccount.QueryModel.Denormalizer.Concretes
{
    public class AccountDenormalizer : IAccountDenormalizer
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountDenormalizer(IUnitOfWork unitOfWork)
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

            var userResults = await this._unitOfWork.AccountPersistor.QueryAsync(a => a.UserName == userName);
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
    }
}
