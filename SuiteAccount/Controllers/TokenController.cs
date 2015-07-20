using System.Threading.Tasks;
using System.Web.Http;
using SuiteAccount.Providers.Abstracts;

namespace SuiteAccount.Controllers
{
    [RoutePrefix("api/Token")]
    public class TokenController : ApiController
    {
        private readonly ISuiteTokenProvider _suiteTokenProvider;

        public TokenController(ISuiteTokenProvider suiteTokenProvider)
        {
            this._suiteTokenProvider = suiteTokenProvider;
        }

        [HttpGet]
        public async Task<bool> VerifyTokenAsync(string tokenId)
        {
            return await this._suiteTokenProvider.VerifiyTokenAsync(tokenId);
        }
    }
}
