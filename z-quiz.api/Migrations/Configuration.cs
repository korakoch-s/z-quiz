namespace z_quiz.api.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using z_quiz.api.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<z_quiz.api.Models.ZQuizModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(z_quiz.api.Models.ZQuizModel context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //Mock Questions
            /****************
            for (int i = 0; i < 5; i++)
            {
                Question qt = new Question
                {
                    Title = "Question number " + (i + 1),
                    TotalScore = 10
                };
                for (int j = 0; j < 5; j++)
                {
                    Choice ch = new Choice
                    {
                        Title = "Choice item " + (j + 1) + " of question " + (i + 1),
                        Score = (j + 1) * 2
                    };
                    qt.Choices.Add(ch);
                }
                context.Questions.Add(qt);
            }
            context.SaveChanges();
            ******************/

        }
    }
}
