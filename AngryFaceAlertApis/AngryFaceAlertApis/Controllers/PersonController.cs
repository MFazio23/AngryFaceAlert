using System.Threading.Tasks;
using System.Web.Http;
using AngryFaceAlertApis.Coordinators;
using AngryFaceAlertApis.Coordinators.Interfaces;
using AngryFaceAlertApis.Models.ApiModels;

namespace AngryFaceAlertApis.Controllers
{
    [Route("1/person")]
    public class PersonController : ApiController
    {
        private readonly IPersonCoordinator _personCoordinator;

        public PersonController()
        {
            this._personCoordinator = new PersonCoordinator();
        }

        public PersonController(IPersonCoordinator personCoordinator)
        {
            _personCoordinator = personCoordinator;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetPerson(string personGroupId, string personId)
        {
            var person = await this._personCoordinator.GetPerson(personGroupId, personId);
            return Ok(person);
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreatePerson(string personGroupId, NameUserDataInfoApiModel personInfo, string personId = null)
        {
            //TODO: Default person ID to a generated GUID if null.  Name should be required.

            return Ok();
        }

        [HttpPatch]
        [Route("")]
        public async Task<IHttpActionResult> UpdatePerson(string personGroupId, string personId, NameUserDataInfoApiModel personInfo)
        {
            //TODO: Update person group info based on what's sent in.  In other words, only update the name and user data when they're not null.

            return Ok();
        }

        [HttpDelete]
        [Route("")]
        public async Task<IHttpActionResult> DeletePerson(string personGroupId, string personId)
        {
            return Ok();
        }
    }
}
