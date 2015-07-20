using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;

using SuiteAccount.Logging.Abstracts;
using SuiteAccount.SqlModel.Persistence.Persistors;

namespace SuiteLogin.Providers
{
    public class SuiteAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly ILogService _logService;

        public SuiteAuthorizationServerProvider(ILogService logService)
        {
            this._logService = logService;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        /// <summary>
        /// This method is responsible to validate the username and password 
        /// sent to the authorization server’s token endpoint. 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            using (var unitOfWork = new UnitOfWork(this._logService))
            {
                var account = await unitOfWork.AccountPersistor.QueryAsync(
                    a => a.UserName == context.UserName && a.Password == context.Password);

                if (!account.Any())
                {
                    this._logService.LoggerTrace(string.Format("Tentativo di Accesso; Username:{0} - Password:{1}",
                        context.UserName, context.Password));

                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));

            context.Validated(identity);
        }
    }
}