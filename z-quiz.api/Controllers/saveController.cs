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
    public class saveController : ApiController
    {
        private IZQuizService _service = new ZQuizService<ZQuizModel>(new ZQuizModel());

        /// <summary>
        /// Save current tester data
        /// </summary>
        /// <param name="tester">Tester with test question answer.</param>
        // POST: api/save
        public IHttpActionResult Post([FromBody]Tester tester)
        {
            this._service.Save(tester);
            return Ok();
        }
    }
}
