namespace Questionnaires.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Questionnaires",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        QuestionnaireId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questionnaires", t => t.QuestionnaireId, cascadeDelete: true)
                .Index(t => t.QuestionnaireId);
            
            CreateTable(
                "dbo.Choices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SessionId = c.Guid(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        ChoiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: false)
                .ForeignKey("dbo.Choices", t => t.ChoiceId, cascadeDelete: false)
                .Index(t => t.QuestionId)
                .Index(t => t.ChoiceId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Answers", new[] { "ChoiceId" });
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropIndex("dbo.Choices", new[] { "QuestionId" });
            DropIndex("dbo.Questions", new[] { "QuestionnaireId" });
            DropForeignKey("dbo.Answers", "ChoiceId", "dbo.Choices");
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Choices", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Questions", "QuestionnaireId", "dbo.Questionnaires");
            DropTable("dbo.Answers");
            DropTable("dbo.Choices");
            DropTable("dbo.Questions");
            DropTable("dbo.Questionnaires");
        }
    }
}
