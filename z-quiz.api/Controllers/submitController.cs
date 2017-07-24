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
    public class submitController : ApiController
    {
        private IZQuizService _service = new ZQuizService<ZQuizModel>(new ZQuizModel());

        /// <summary>
        /// Submit quiz session to evaluate score and rank
        /// </summary>
        /// <param name="tester"></param>
        /// <returns>Tester with score and rank result</returns>
        // POST: api/submit
        public IHttpActionResult Post([FromBody]Tester tester)
        {
            tester = this._service.Submit(tester);
            return Ok(tester);
        }

    }
}
