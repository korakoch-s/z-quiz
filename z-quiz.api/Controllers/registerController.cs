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
    public class registerController : ApiController
    {
        private IZQuizService _service ;

        public registerController(IZQuizService service)
        {
            this._service = service;
        }

        /// <summary>
        /// Register new tester.
        /// </summary>
        /// <param name="name">Name of tester</param>
        /// <returns>Tester object json</returns>
        // GET: api/register/5
        public Tester Get(string name)
        {
            return this._service.Register(name);
        }
    }
}
