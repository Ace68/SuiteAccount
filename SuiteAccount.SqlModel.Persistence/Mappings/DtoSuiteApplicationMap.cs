using SuiteAccount.SqlModel.Dtos;

namespace SuiteAccount.SqlModel.Persistence.Mappings
{
    public class DtoSuiteApplicationMap : BaseMapping<DtoSuiteApplication>
    {
        public DtoSuiteApplicationMap()
            : base("SuiteApplication")
        {
            this.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("ApplicationId");

            this.Property(t => t.ApplicationName).HasMaxLength(256).HasColumnName("ApplicationName");
        }
    }
}
