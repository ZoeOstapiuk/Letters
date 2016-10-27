namespace Letters.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredContent : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Letters", "Content", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Letters", "Content", c => c.String());
        }
    }
}
