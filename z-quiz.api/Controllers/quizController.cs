using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using z_quiz.api.Models;
using z_quiz.api.Services;

namespace z_quiz.api.Controllers
{
    public class quizController : ApiController
    {
        private IZQuizService _service = new ZQuizService<ZQuizModel>(new ZQuizModel());

        /// <summary>
        /// Load all questions to do a quiz session.
        /// </summary>
        /// <returns></returns>
        // GET: api/quiz
        public IEnumerable<Question> Get()
        {
            return this._service.Quiz();
        }
    }
}
