using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AngryFaceAlertApis.Coordinators;
using AngryFaceAlertApis.Extensions;
using Microsoft.ProjectOxford.Face;

namespace AngryFaceAlertApis.Controllers
{
    [Route("1/face")]
    public class FaceController : ApiController
    {
        private readonly FaceCoordinator _faceCoordinator;

        public FaceController()
        {
            _faceCoordinator = new FaceCoordinator();
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetFaceIdentitiesFromPicture()
        {
            var imageStream = await Request.Content.ReadAsStreamAsync();

            var faces = await this._faceCoordinator.GetListOfFaces(imageStream.CreateStreamCopy());

            var people = await this._faceCoordinator.GetPeopleFromFaces("skyline", faces);
            
            return Ok(people);
        }
    }
}
