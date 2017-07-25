using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using z_quiz.api.Models;

namespace z_quiz.api.Services
{
    /// <summary>
    /// Implement concret service class.
    /// </summary>
    /// <typeparam name="TContext">Must be DbContext of ZQuizModel</typeparam>
    public class ZQuizService<TContext> : IZQuizService
        where TContext : ZQuizModel
    {
        protected readonly TContext _db;

        public ZQuizService(TContext db)
        {
            this._db = db;
        }

        public Tester Load(string name)
        {
            var tester = this._db.Testers.Where(tt => tt.Name == name).SingleOrDefault();

            if (tester == null)
            {
                return null;
            }

            _db.Entry(tester).Collection(tt => tt.TesterQuestions).Load();
            if(tester.IsCompleted && (tester.Rank <= 0 || tester.Score <= 0 || tester.TotalScore <= 0))
            {
                this._calScore(tester);
                this._db.SaveChanges();
                this._calRank(tester);
            }

            return tester;
        }

        public ICollection<Question> Quiz()
        {
            return _db.Questions.Include(qt => qt.Choices).ToList();
        }

        public Tester Register(string name)
        {
            var tester = this._db.Testers.Where(tt => tt.Name == name).SingleOrDefault();

            if (tester == null)
            {
                //Save new tester
                tester = this._db.Testers.Add(new Tester
                {
                    Name = name,
                    IsCompleted = false
                });
                this._db.SaveChanges();
            }
            else
            {
                //Load current
                return this.Load(name);
            }

            return tester;
        }

        public void Save(Tester tester)
        {
            this._save(tester);
            this._db.SaveChanges();
        }

        public Tester Submit(Tester tester)
        {

            tester = this._save(tester);
            this._calScore(tester);

            tester.IsCompleted = true;
            this._calRank(tester);

            this._db.SaveChanges();

            return tester;
        }

        private Tester _save(Tester tester)
        {
            var exTester = this._db.Testers.Where(tt => tt.TesterId == tester.TesterId)
                .Include(tt => tt.TesterQuestions)
                .SingleOrDefault();
            if (exTester != null)
            {
                this._db.Entry(exTester).CurrentValues.SetValues(tester);
                foreach (var tq in tester.TesterQuestions)
                {
                    tq.TesterId = tester.TesterId;
                    var exTq = exTester.TesterQuestions
                        .Where(c => (c.TesterId == tq.TesterId && c.QuestionId == tq.QuestionId))
                        .SingleOrDefault();
                    if (exTq != null)
                    {
                        this._db.Entry(exTq).CurrentValues.SetValues(tq);
                        exTq.AnswerId = tq.AnswerId;
                        exTq.QuestionId = tq.QuestionId;
                        exTq.TesterId = tq.TesterId;
                    }
                    else
                    {
                        var newTq = new TesterQuestion
                        {
                            TesterId = tq.TesterId,
                            QuestionId = tq.QuestionId,
                            AnswerId = tq.Choice.ChoiceId,
                        };
                        exTester.TesterQuestions.Add(newTq);
                    }
                }
                tester = exTester;
            }

            return tester;
        }

        private Tester _calScore(Tester tester)
        {
            tester.Score = 0;
            tester.TotalScore = 0;
            foreach (var tq in tester.TesterQuestions)
            {
                if(tq.AnswerId > 0)
                {
                    var ch = this._db.Choices.Find(tq.AnswerId);
                    tq.Choice = ch;
                }

                if (tq.QuestionId > 0)
                {
                    var qt = this._db.Questions.Find(tq.QuestionId);
                    tq.Question = qt;
                }

                tester.Score += tq.Choice.Score;
                tester.TotalScore += tq.Question.TotalScore;
            }

            return tester;
        }

        private void _calRank(Tester tester)
        {

            var testers = this._db.Testers.Where(tt => tt.IsCompleted)
                .OrderByDescending(tt => tt.Score);

            tester.Rank = 1;
            foreach (var tt in testers)
            {
                if (tester.Score >= tt.Score)
                {
                    break;
                }
                tester.Rank += 1;
            }

        }

        public TContext GetContext()
        {
            return this._db;
        }
    }
}