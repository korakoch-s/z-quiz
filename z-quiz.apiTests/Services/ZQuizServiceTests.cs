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

            this._service.GetContext().SaveChanges();
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
    }
}