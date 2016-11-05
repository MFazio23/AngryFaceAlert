using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Routing;
using AngryFaceAlertApis.Models.ApiModels;

namespace AngryFaceAlertApis.Controllers
{
    [Route("1/persongroup")]
    public class PersonGroupController : ApiController
    {
        [HttpGet]
        public async Task<IHttpActionResult> GetPersonGroups()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetPersonGroup(string personGroupId)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IHttpActionResult> AddPersonGroup(string personGroupId = null, NameUserDataInfoApiModel personGroupInfo = null)
        {
            //TODO: Default personGroupId to a random GUID if it's not entered.

            return Ok();
        }

        [HttpPatch]
        public async Task<IHttpActionResult> UpdatePersonGroupInfo(string personGroupId, NameUserDataInfoApiModel personGroupInfo)
        {
            //TODO: Update person group info based on what's sent in.  In other words, only update the name and user data when they're not null.
            
            return Ok();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeletePersonGroup(string personGroupId)
        {
            return Ok();
        }

        [HttpPost]
        [Route("/training")]
        public async Task<IHttpActionResult> TrainPersonGroup(string personGroupId)
        {
            return Ok();
        }
    }
}
