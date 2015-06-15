using SuiteAccount.QueryModel.Dtos;

namespace SuiteAccount.QueryModel.Persistence.Mappings
{
    public class AccountMap : BaseMapping<DtoAccount>
    {
        public AccountMap()
            : base("Account")
        {
            this.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("AccountId");

            this.Property(t => t.UserName)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnName("UserName");

            this.Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnName("Password");

            this.Property(t => t.Email)
                .HasMaxLength(255)
                .HasColumnName("Email");

            this.Property(t => t.IsApproved)
                .HasColumnName("IsApproved");

            this.Property(t => t.IsLockedOut)
                .HasColumnName("IsLockedOut");

            this.Property(t => t.IsOnline)
                .HasColumnName("IsOnLine");

            this.Property(t => t.CreationDate)
                .HasColumnName("CreationDate");
        }
    }
}
