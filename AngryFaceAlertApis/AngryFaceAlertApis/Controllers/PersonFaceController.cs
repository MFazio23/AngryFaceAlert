using System.Threading.Tasks;
using System.Web.Http;
using AngryFaceAlertApis.Models.ApiModels;

namespace AngryFaceAlertApis.Controllers
{
    [Route("1/person/face")]
    public class PersonFaceController : ApiController
    {
        [HttpGet]
        public async Task<IHttpActionResult> GetPersonFace(string personGroupId, string personId, string faceId)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IHttpActionResult> AddPersonFace(string personGroupId, string personId, UserDataApiModel userData)
        {
            //TODO: Default person ID to a generated GUID if null.  Name should be required.
            var personFaceByteArray = await Request.Content.ReadAsByteArrayAsync();

            return Ok();
        }

        [HttpPatch]
        public async Task<IHttpActionResult> UpdatePersonFace(string personGroupId, string personId, string faceId, UserDataApiModel userData)
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
