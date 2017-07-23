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
            var tester = this._db.Testers.First(tt => tt.Name == name);

            if (tester == null)
            {
                return null;
            }

            _db.Entry(tester).Collection(tt => tt.TesterQuestions).Load();

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
                _db.Entry(tester).Collection(tt => tt.TesterQuestions).Load();
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
            this._save(tester);

            foreach (var tq in tester.TesterQuestions)
            {
                tester.Score += tq.Choice.Score;
                tester.TotalScore += tq.Question.TotalScore;
            }

            this._db.SaveChanges();

            var testers = this._db.Testers.Where(tt => tt.IsCompleted)
                .OrderBy(tt => tt.Score);

            tester.Rank = 0;
            foreach (var tt in testers)
            {
                tester.Rank += 1;
                if (tester.TesterId == tt.TesterId)
                {
                    break;
                }
            }

            tester.IsCompleted = true;

            this._db.SaveChanges();

            return tester;
        }

        private void _save(Tester tester)
        {
            var exTester = this._db.Testers.Where(tt => tt.TesterId == tester.TesterId)
                .Include(tt => tt.TesterQuestions)
                .SingleOrDefault();

            if (exTester != null)
            {
                foreach (var tq in tester.TesterQuestions)
                {
                    var exTq = exTester.TesterQuestions
                        .Where(c => (c.TesterId == tq.TesterId && c.QuestionId == tq.QuestionId))
                        .SingleOrDefault();
                    if (exTq != null)
                    {
                        this._db.Entry(exTq).CurrentValues.SetValues(tq);
                    }
                    else
                    {
                        var newTq = new TesterQuestion
                        {
                            TesterId = tq.TesterId,
                            QuestionId = tq.QuestionId,
                            AnswerId = tq.AnswerId
                        };
                        exTester.TesterQuestions.Add(newTq);
                    }
                }
            }
        }

        public TContext GetContext()
        {
            return this._db;
        }
    }
}