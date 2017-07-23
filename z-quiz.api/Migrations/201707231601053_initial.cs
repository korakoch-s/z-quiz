namespace z_quiz.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Choices",
                c => new
                    {
                        ChoiceId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 500),
                        Score = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ChoiceId)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 2000),
                        TotalScore = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionId);
            
            CreateTable(
                "dbo.TesterQuestions",
                c => new
                    {
                        TesterId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        AnswerId = c.Int(),
                        Choice_ChoiceId = c.Int(),
                    })
                .PrimaryKey(t => new { t.TesterId, t.QuestionId })
                .ForeignKey("dbo.Choices", t => t.Choice_ChoiceId)
                .ForeignKey("dbo.Testers", t => t.TesterId)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .Index(t => t.TesterId)
                .Index(t => t.QuestionId)
                .Index(t => t.Choice_ChoiceId);
            
            CreateTable(
                "dbo.Testers",
                c => new
                    {
                        TesterId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        IsCompleted = c.Boolean(nullable: false),
                        Score = c.Int(nullable: false),
                        TotalScore = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TesterId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TesterQuestions", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.TesterQuestions", "TesterId", "dbo.Testers");
            DropForeignKey("dbo.TesterQuestions", "Choice_ChoiceId", "dbo.Choices");
            DropForeignKey("dbo.Choices", "QuestionId", "dbo.Questions");
            DropIndex("dbo.TesterQuestions", new[] { "Choice_ChoiceId" });
            DropIndex("dbo.TesterQuestions", new[] { "QuestionId" });
            DropIndex("dbo.TesterQuestions", new[] { "TesterId" });
            DropIndex("dbo.Choices", new[] { "QuestionId" });
            DropTable("dbo.Testers");
            DropTable("dbo.TesterQuestions");
            DropTable("dbo.Questions");
            DropTable("dbo.Choices");
        }
    }
}
