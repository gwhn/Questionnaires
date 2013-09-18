namespace Questionnaires.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSessionIdTypeToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Answers", "SessionId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Answers", "SessionId", c => c.Guid(nullable: false));
        }
    }
}
