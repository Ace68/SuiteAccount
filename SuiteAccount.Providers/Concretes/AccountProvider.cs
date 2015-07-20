using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using SuiteAccount.Messages.Commands;
using SuiteAccount.Infrastructure.Abstracts;
using SuiteAccount.NoSql.Services.Abstracts;
using SuiteAccount.Providers.Abstracts;
using SuiteAccount.Shared.ValueObjects;
using SuiteAccount.Shared.ViewModels;

namespace SuiteAccount.Providers.Concretes
{
    public class AccountProvider : IAccountProvider
    {
        private readonly IServiceBus _serviceBus;
        private readonly INoSqlAccountService _noSqlAccountService;

        private readonly byte[] _passwordBytes;
        private readonly byte[] _saltBytes;

        public AccountProvider(IServiceBus serviceBus,
            INoSqlAccountService noSqlAccountService)
        {
            this._serviceBus = serviceBus;
            this._noSqlAccountService = noSqlAccountService;

            this._passwordBytes = Encoding.UTF8.GetBytes("InfoCopy");
            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            this._saltBytes = new byte[] { 1, 5, 0, 6, 1, 9, 9, 9 };
        }

        #region Run Command
        public void CreateAccount(AccountId accountId, string userName, string password)
        {
            var passwordCriptata = this.GetPasswordCriptata(password);

            this._serviceBus.Send(new CreateAccount(accountId, userName, passwordCriptata));
        }

        public void UpdateEmail(AccountId accountId, string email)
        {
            this._serviceBus.Send(new UpdateEmail(accountId, email));
        }
        #endregion

        #region Run Query
        public async Task<Guid> VerifyAccountAsync(string userName, string password)
        {
            var passwordCriptata = this.GetPasswordCriptata(password);

            var accountId = await this._noSqlAccountService.VerifyAccountAsync(userName, passwordCriptata);

            return accountId;
        }

        public async Task<IReadOnlyCollection<AccountViewModel>> GetElencoAccountAsync()
        {
            return await this._noSqlAccountService.GetElencoAccountAsync();
        }
        #endregion

        #region Helpers
        private string GetPasswordCriptata(string password)
        {
            byte[] passwordToBeEncrypted = Encoding.UTF8.GetBytes(password);
            var bytesEncrypted = this.PasswordEncrypt(passwordToBeEncrypted);
            return Convert.ToBase64String(bytesEncrypted);
        }

        private byte[] PasswordEncrypt(byte[] passwordToBeEncrypted)
        {
            byte[] encryptedBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var rijndaelManaged = new RijndaelManaged())
                {
                    rijndaelManaged.KeySize = 256;
                    rijndaelManaged.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(this._passwordBytes, this._saltBytes, 1000);
                    rijndaelManaged.Key = key.GetBytes(rijndaelManaged.KeySize / 8);
                    rijndaelManaged.IV = key.GetBytes(rijndaelManaged.BlockSize / 8);

                    rijndaelManaged.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(memoryStream, rijndaelManaged.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(passwordToBeEncrypted, 0, passwordToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = memoryStream.ToArray();
                }
            }

            return encryptedBytes;
        }

        private byte[] PasswordDecrypt(byte[] passwordToBeDecrypted)
        {
            byte[] decryptedBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var rijndaelManaged = new RijndaelManaged())
                {
                    rijndaelManaged.KeySize = 256;
                    rijndaelManaged.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(this._passwordBytes, this._saltBytes, 1000);
                    rijndaelManaged.Key = key.GetBytes(rijndaelManaged.KeySize / 8);
                    rijndaelManaged.IV = key.GetBytes(rijndaelManaged.BlockSize / 8);

                    rijndaelManaged.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(memoryStream, rijndaelManaged.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(passwordToBeDecrypted, 0, passwordToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = memoryStream.ToArray();
                }
            }

            return decryptedBytes;
        }
        #endregion
    }
}
