using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace AngryFaceAlertApis.Controllers
{
    public class ImageController : ApiController
    {
        [HttpPost]
        [Route("1/image")]
        public async Task<IHttpActionResult> ProcessImage(object image)
        {
            Console.WriteLine(image);
            Console.WriteLine(Request.Content);

            var content = Request.Content;

            var byteArray = await content.ReadAsByteArrayAsync();

            File.WriteAllBytes("C:/dev/Files/test-img.jpg", byteArray);

            return Ok();
        }
    }
}