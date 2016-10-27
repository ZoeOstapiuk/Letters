namespace Letters.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Letters",
                c => new
                    {
                        LetterId = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.LetterId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Letters");
        }
    }
}
