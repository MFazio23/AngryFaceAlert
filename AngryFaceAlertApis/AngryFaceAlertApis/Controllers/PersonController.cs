using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AngryFaceAlertApis.Models.ApiModels;

namespace AngryFaceAlertApis.Controllers
{

    [Route("1/person")]
    public class PersonController : ApiController
    {
        [HttpGet]
        public async Task<IHttpActionResult> GetPerson(string personGroupId, string personId)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreatePerson(string personGroupId, NameUserDataInfoApiModel personInfo, string personId = null)
        {
            //TODO: Default person ID to a generated GUID if null.  Name should be required.

            return Ok();
        }

        [HttpPatch]
        public async Task<IHttpActionResult> UpdatePerson(string personGroupId, string personId, NameUserDataInfoApiModel personInfo)
        {
            //TODO: Update person group info based on what's sent in.  In other words, only update the name and user data when they're not null.

            return Ok();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeletePerson(string personGroupId, string personId)
        {
            return Ok();
        }
    }
}
