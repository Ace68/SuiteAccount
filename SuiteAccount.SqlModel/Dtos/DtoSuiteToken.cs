using System;

namespace SuiteAccount.SqlModel.Dtos
{
    public class DtoSuiteToken : DtoBase
    {
        public bool? IsAlive { get; set; }
        public DateTime TokenGenerationDateUtc { get; set; }
        public DateTime TokenExpirationDateUtc { get; set; }
        public int? TokenDuration { get; set; }
        public Guid AccountId { get; set; }
        public string AccountName { get; set; }
        public Guid ApplicationId { get; set; }
        public string ApplicationName { get; set; }
    }
}
