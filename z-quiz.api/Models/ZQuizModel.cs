using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace z_quiz.api.Models
{
    public partial class ZQuizModel: DbContext
    {
        public ZQuizModel()
            : base("name=ZQuizModel")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Choice> Choices { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<TesterQuestion> TesterQuestions { get; set; }
        public virtual DbSet<Tester> Testers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Question>()
                .HasMany(e => e.Choices)
                .WithRequired(e => e.Question)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.TesterQuestions)
                .WithRequired(e => e.Question)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tester>()
                .HasMany(e => e.TesterQuestions)
                .WithRequired(e => e.Tester)
                .WillCascadeOnDelete(false);
        }
    }
}