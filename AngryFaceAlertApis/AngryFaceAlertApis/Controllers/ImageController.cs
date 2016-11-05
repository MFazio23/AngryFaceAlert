using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;

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

            var client = new FaceServiceClient("");

            var content = Request.Content;

            var stream = await content.ReadAsStreamAsync();

            MemoryStream stream2 = new MemoryStream();

            stream.CopyTo(stream2);
            stream.Seek(0, SeekOrigin.Begin);

            var faces = await client.DetectAsync(stream);

            var guids = new List<Guid>();

            foreach(var face in faces)
            {
                guids.Add(face.FaceId);
            }
            
            var person = new Person() { Name = "Unknown" };

            try
            {
                var possibilities = await client.IdentifyAsync("skyline", guids.ToArray(), 0.5f, 1);

                if (possibilities != null && possibilities.Length > 0)
                {
                    person = await client.GetPersonAsync("skyline", possibilities[0].Candidates[0].PersonId);
                }

   
            } catch (Exception e)
            {
                var stop = "";
            }

            return Ok(person);
        }
    }
}