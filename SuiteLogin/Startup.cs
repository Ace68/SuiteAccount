// http://bitoftech.net/2014/06/01/token-based-authentication-asp-net-web-api-2-owin-asp-net-identity/

using System;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

// This class will be fired once our server starts
using SuiteAccount.Logging.Concretes;
using SuiteLogin.Providers;

[assembly: OwinStartup(typeof(SuiteLogin.Startup))]
namespace SuiteLogin
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureOAuth(app);

            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

            NinjectHttpContainer.RegisterModules();
        }

        /// <summary>
        /// Path for generating tokens will be as : http://mywebsite:port/token
        /// Expiry for token to be 24 hours
        /// Implementation on how to validate the credentials for users asking for tokens 
        /// in custom class SuiteAuthorizationServerProvider.
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureOAuth(IAppBuilder app)
        {
            var logService = new LogService();
            var oAuthServerOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SuiteAuthorizationServerProvider(logService)
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}