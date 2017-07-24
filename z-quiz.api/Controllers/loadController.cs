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
    public class loadController : ApiController
    {
        private IZQuizService _service = new ZQuizService<ZQuizModel>(new ZQuizModel());

        /// <summary>
        /// Load all questions to do a quiz session.
        /// </summary>
        /// <returns></returns>
        // GET: api/load/{name}
        public IHttpActionResult Get(string name)
        {
            if (name.Trim() == "")
            {
                return BadRequest("Must provide name.");
            }
            else
            {
                var tester = this._service.Register(name);
                if (tester == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(tester);
                }
            }
        }
    }
}
