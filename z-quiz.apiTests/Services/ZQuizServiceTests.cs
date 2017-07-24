using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using z_quiz.api.Services;
using z_quiz.api.Models;

namespace z_quiz.api.Services.Tests
{
    [TestFixture()]
    public class ZQuizServiceTests
    {
        private ZQuizService<ZQuizModel> _service;

        [TestFixtureSetUp()]
        public void Init()
        {
            this._service = new ZQuizService<ZQuizModel>(new ZQuizModel());
            this.mockQuestionData();
            this.mockTester();
        }

        [TestFixtureTearDown()]
        public void Dispose()
        {
            
            this._service.GetContext().Database.ExecuteSqlCommand("DELETE FROM TesterQuestions");
            this._service.GetContext().Database.ExecuteSqlCommand("DELETE FROM Testers");
            this._service.GetContext().Database.ExecuteSqlCommand("DELETE FROM Choices");
            this._service.GetContext().Database.ExecuteSqlCommand("DELETE FROM Questions");
            
        }

        private void mockQuestionData()
        {
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
                this._service.GetContext().Questions.Add(qt);
            }
            this._service.GetContext().SaveChanges();
        }

        private void mockTester()
        {
            Tester tester = new Tester
            {
                Name = "Tester without test save",
                IsCompleted = false
            };
            this._service.GetContext().Testers.Add(tester);

            tester = new Tester
            {
                Name = "Tester with some test save",
                IsCompleted = false
            };
            var questions = this._service.Quiz();
            int tmpCount = 0;
            foreach (var qt in questions)
            {
                TesterQuestion tq = new TesterQuestion
                {
                    Question = qt,
                    Choice = null
                };
                if (tmpCount < 2)
                {
                    tq.Choice = qt.Choices.ElementAt(tmpCount);
                }
                tmpCount++;
                tester.TesterQuestions.Add(tq);
            }
            this._service.GetContext().Testers.Add(tester);

            tester = new Tester
            {
                Name = "Tester with complete submit but not cal total yet",
                IsCompleted = true
            };
            foreach (var qt in questions)
            {
                TesterQuestion tq = new TesterQuestion
                {
                    Question = qt,
                    Choice = qt.Choices.ElementAt(3)
                };
                tester.TesterQuestions.Add(tq);
            }
            this._service.GetContext().Testers.Add(tester);

            tester = new Tester
            {
                Name = "Tester with complete submitted",
                IsCompleted = true
            };
            foreach (var qt in questions)
            {
                TesterQuestion tq = new TesterQuestion
                {
                    Question = qt,
                    Choice = qt.Choices.ElementAt(3)
                };
                tester.TesterQuestions.Add(tq);
            }
            tester.Score = 40;
            tester.TotalScore = 50;
            this._service.GetContext().Testers.Add(tester);

            tester = new Tester
            {
                Name = "Tester with complete Full score",
                IsCompleted = true
            };
            foreach (var qt in questions)
            {
                TesterQuestion tq = new TesterQuestion
                {
                    Question = qt,
                    Choice = qt.Choices.ElementAt(4)
                };
                tester.TesterQuestions.Add(tq);
            }
            tester.Score = 50;
            tester.TotalScore = 50;
            this._service.GetContext().Testers.Add(tester);

            this._service.GetContext().SaveChanges();
        }

        [Test()]
        public void RegisterTest()
        {
            string testName = "Tester_Name";
            var tester = this._service.Register(testName);

            Assert.IsNotNull(tester);
            Assert.AreEqual(testName, tester.Name);

            int id1 = tester.TesterId;
            var tester2 = this._service.Register(testName);
            Assert.IsNotNull(tester2);
            Assert.AreEqual(testName, tester2.Name);
            Assert.AreEqual(id1, tester2.TesterId, "Register with the same name must not create new record.");
        }

        [Test()]
        public void RegisterTest_for_Complete()
        {
            string testerName = "Tester with complete Full score";
            var tester = this._service.Register(testerName);

            Assert.IsNotNull(tester, testerName + " should not be null");

            Assert.IsInstanceOf<Tester>(tester, "Return type should be Models.Tester");

            Assert.AreEqual(testerName, tester.Name, "Name should be '" + testerName + "' but got '" + tester.Name + "'");
            Assert.IsNotNull(tester.TesterQuestions, "TesterQuestions should not be null.");
            Assert.IsTrue(tester.IsCompleted, "IsCompleted status should be true");
            Assert.AreEqual( 50, tester.Score, "Score not correct");
            Assert.AreEqual( 50, tester.TotalScore, "Total score not correct");
            Assert.AreEqual(1, tester.Rank, "Rank not correct");

        }

        [Test()]
        public void QuizTest()
        {
            var questions = this._service.Quiz();

            Assert.IsNotNull(questions, "Quiz method should not return null.");
            Assert.AreEqual(5, questions.Count(), "Mockup data 5 records but result = " + questions.Count());
            Assert.AreEqual(5, questions.ElementAt(0).Choices.Count(),
                "Mockup have choices 5 records but got = " + questions.ElementAt(0).Choices.Count());
        }

        [Test()]
        public void LoadTest_for_TesterNotExist()
        {
            string testerName = "Tester not in database";
            var tester = this._service.Load(testerName);

            Assert.IsNull(tester, testerName + " should be null");
        }

        [Test()]
        public void LoadTest_for_TesterWithoutSave()
        {
            string testerName = "Tester without test save";
            var tester = this._service.Load(testerName);

            Assert.IsNotNull(tester, testerName + " should not be null");

            Assert.IsInstanceOf<Tester>(tester, "Return type should be Models.Tester");

            Assert.AreEqual(testerName, tester.Name, "Name should be '" + testerName + "' but got '" + tester.Name + "'");
            Assert.That(tester.TesterQuestions == null || tester.TesterQuestions.Count() == 0, "TesterQuestions should be null.");
            Assert.AreEqual(false, tester.IsCompleted, "IsCompleted status should be false");
        }

        [Test()]
        public void LoadTest_for_TesterWithSomeSave()
        {
            string testerName = "Tester with some test save";
            var tester = this._service.Load(testerName);

            Assert.IsNotNull(tester, testerName + " should not be null");

            Assert.IsInstanceOf<Tester>(tester, "Return type should be Models.Tester");

            Assert.AreEqual(testerName, tester.Name, "Name should be '" + testerName + "' but got '" + tester.Name + "'");
            Assert.IsNotNull(tester.TesterQuestions, "TesterQuestions should not be null.");
            Assert.AreEqual(false, tester.IsCompleted, "IsCompleted status should be false");
        }

        [Test()]
        public void LoadTest_for_TesterWithComplete()
        {
            string testerName = "Tester with complete submit but not cal total yet";
            var tester = this._service.Load(testerName);

            Assert.IsNotNull(tester, testerName + " should not be null");

            Assert.IsInstanceOf<Tester>(tester, "Return type should be Models.Tester");

            Assert.AreEqual(testerName, tester.Name, "Name should be '" + testerName + "' but got '" + tester.Name + "'");
            Assert.IsNotNull(tester.TesterQuestions, "TesterQuestions should not be null.");
            Assert.AreEqual(true, tester.IsCompleted, "IsCompleted status should be true");
            Assert.Greater(tester.Score, 0, "Score should be greater than 0");
            Assert.Greater(tester.TotalScore, 0, "Total score should be greater than 0");
            Assert.Greater(tester.Rank, 0, "Rank should be greater than 0");
        }

        [Test()]
        public void SaveTest()
        {
            string testerName = "Tester with some test save";
            var tester = this._service.Load(testerName);

            Assert.IsNotNull(tester, testerName + " should not be null");
            Assert.IsInstanceOf<Tester>(tester, "Return type should be Models.Tester");
            Assert.AreEqual(testerName, tester.Name, "Name should be '" + testerName + "' but got '" + tester.Name + "'");
            Assert.IsNotNull(tester.TesterQuestions, "TesterQuestions should not be null.");
            Assert.AreEqual(false, tester.IsCompleted, "IsCompleted status should be false");

            //modify some data and call save
            List<TesterQuestion> expTqs = new List<TesterQuestion>();
            foreach (var tq in tester.TesterQuestions)
            {
                //simulate select choice number 2
                tq.Choice = tq.Question.Choices.ElementAt(2);
                expTqs.Add(tq);
            }
            this._service.Save(tester);
            tester = this._service.Load(testerName);

            Assert.IsInstanceOf<Tester>(tester, "Return type should be Models.Tester");
            Assert.AreEqual(expTqs, tester.TesterQuestions.ToList<TesterQuestion>());
            Assert.IsFalse(tester.IsCompleted, "IsCompleted status should be false");
        }

        [Test()]
        public void SubmitTest()
        {
            string testerName = "Tester with complete submit but not cal total yet";
            var tester = this._service.Load(testerName);

            //modify some data and call save
            int expScore = 0;
            int expTotal = 0;
            foreach (var tq in tester.TesterQuestions)
            {
                //simulate select choice number 2
                tq.Choice = tq.Question.Choices.ElementAt(2);
                expScore += tq.Question.Choices.ElementAt(2).Score;
                expTotal += tq.Question.TotalScore;
            }

            //From provide mock dataset score = 30, Total = 50, Rank = 2
            tester = this._service.Submit(tester);
            Assert.IsTrue(tester.IsCompleted, "IsCompleted status should be true");
            Assert.AreEqual(expScore, tester.Score, "Score not correct.");
            Assert.AreEqual(expTotal, tester.TotalScore, "Total score not correct");
            Assert.AreEqual(2, tester.Rank, "Rank not correct");
        }
    }
}