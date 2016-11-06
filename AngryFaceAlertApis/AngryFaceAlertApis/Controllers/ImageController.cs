using System.Threading.Tasks;
using System.Web.Http;
using AngryFaceAlertApis.Coordinators;
using AngryFaceAlertApis.Extensions;

namespace AngryFaceAlertApis.Controllers
{
    public class ImageController : ApiController
    {
        private readonly PersonCoordinator _personCoordinator;

        public ImageController()
        {
            _personCoordinator = new PersonCoordinator();
        }

        [HttpPost]
        [Route("1/image")]
        public async Task<IHttpActionResult> ProcessImage()
        {
            var imageStream = await Request.Content.ReadAsStreamAsync();

            var peopleEmotions = await this._personCoordinator.GetEmotionsForPeopleFromImage(imageStream);

            return Ok(peopleEmotions);
        }

        [HttpPost]
        [Route("1/image")]
        public async Task<IHttpActionResult> ProcessImage(string imageUrl)
        {
            var peopleEmotions = await this._personCoordinator.GetEmotionsForPeopleFromImage(imageUrl: imageUrl);

            return Ok(peopleEmotions);
        }
    }
}