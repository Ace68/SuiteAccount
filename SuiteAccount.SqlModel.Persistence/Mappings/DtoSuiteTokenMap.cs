using SuiteAccount.SqlModel.Dtos;

namespace SuiteAccount.SqlModel.Persistence.Mappings
{
    public class DtoSuiteTokenMap : BaseMapping<DtoSuiteToken>
    {
        public DtoSuiteTokenMap()
            : base("SuiteToken")
        {
            this.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("TokenId");

            this.Property(t => t.AccountId).IsRequired().HasColumnName("AccountId");
            this.Property(t => t.AccountName).HasMaxLength(256).HasColumnName("AccountName");
            this.Property(t => t.ApplicationId).IsRequired().HasColumnName("ApplicationId");
            this.Property(t => t.ApplicationName).HasMaxLength(256).HasColumnName("ApplicationName");
            this.Property(t => t.IsAlive).HasColumnName("IsAlive");
            this.Property(t => t.TokenDuration).HasColumnName("TokenDuration");
            this.Property(t => t.TokenExpirationDateUtc).IsRequired().HasColumnName("TokenExpirationDateUtc");
            this.Property(t => t.TokenGenerationDateUtc).IsRequired().HasColumnName("TokenGenerationDateUtc");
        }
    }
}
