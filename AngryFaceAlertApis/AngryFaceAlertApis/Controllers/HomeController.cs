using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace AngryFaceAlertApis.Controllers
{
    public class HomeController : ApiController
    {
        public IHttpActionResult Index()
        { 

            return Ok();
        }
    }
}
