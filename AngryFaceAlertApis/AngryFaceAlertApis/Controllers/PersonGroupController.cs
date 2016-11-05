using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Routing;
using AngryFaceAlertApis.Models.ApiModels;
using AngryFaceAlertApis.Coordinators;

namespace AngryFaceAlertApis.Controllers
{
    [Route("1/persongroup")]
    public class PersonGroupController : ApiController
    {
        private readonly PersonGroupCoordinator _personGroupCoordinator;

        public PersonGroupController()
        {
            _personGroupCoordinator = new PersonGroupCoordinator("4d93ff0e896041b8bec6ab4ce9c3c8b1");
        }

        [HttpGet]
        [Route("1/persongroup/getall")]
        public async Task<IHttpActionResult> GetPersonGroups()
        {
            var groups = await _personGroupCoordinator.GetAllPersonGroups();
            return Ok(groups);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetPersonGroup(string personGroupId)
        {
            var group = await _personGroupCoordinator.GetPersonGroup(personGroupId);
            return Ok(group);
        }

        [HttpPost]
        public async Task<IHttpActionResult> AddPersonGroup([FromBody] NameUserDataInfoApiModel personGroupInfo, string personGroupId = null)
        {
            //TODO: Default personGroupId to a random GUID if it's not entered.
            await _personGroupCoordinator.CreatePersonGroup(personGroupId, personGroupInfo);
            return Ok();
        }

        [HttpPatch]
        public async Task<IHttpActionResult> UpdatePersonGroupInfo(string personGroupId, [FromBody] NameUserDataInfoApiModel personGroupInfo)
        {
            //TODO: Update person group info based on what's sent in.  In other words, only update the name and user data when they're not null.
            await _personGroupCoordinator.UpdatePersonGroup(personGroupId, personGroupInfo);
            return Ok();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeletePersonGroup(string personGroupId)
        {
            await _personGroupCoordinator.DeletePersonGroup(personGroupId);
            return Ok();
        }

        [HttpPost]
        [Route("training")]
        public async Task<IHttpActionResult> TrainPersonGroup(string personGroupId)
        {
            return Ok();
        }
    }
}
