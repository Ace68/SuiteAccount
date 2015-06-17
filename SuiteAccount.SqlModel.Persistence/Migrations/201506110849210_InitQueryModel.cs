namespace SuiteAccount.SqlModel.Persistence.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitSqlModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        AccountId = c.Guid(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 250),
                        Password = c.String(nullable: false, maxLength: 250),
                        Email = c.String(maxLength: 255),
                        IsApproved = c.Boolean(nullable: false),
                        IsOnLine = c.Boolean(nullable: false),
                        IsLockedOut = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.AccountId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Account");
        }
    }
}
