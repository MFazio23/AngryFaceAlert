using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AngryFaceAlertApis.Utilities;
using Microsoft.ProjectOxford.Face.Contract;

namespace AngryFaceAlertApis.Coordinators
{
    public class FaceCoordinator
    {
        public async Task<Face[]> GetListOfFaces(Stream imageStream = null, string imageUrl = null)
        {
            try
            {
                return imageStream != null
                    ? await ApiClients.FaceServiceClient.DetectAsync(imageStream)
                    : await ApiClients.FaceServiceClient.DetectAsync(imageUrl);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception - {e}");
                throw e;
            }
        }

        public async Task<Dictionary<Guid, Person>> GetPeopleFromFaces(string peopleGroupId, Face[] faces)
        {
            return await GetPeopleFromFaceIds(peopleGroupId, faces.Select(f => f.FaceId));
        }

        public async Task<Dictionary<Guid, Person>> GetPeopleFromFaceIds(string peopleGroupId, IEnumerable<Guid> faceIds)
        {
            var faceIdArray = faceIds.ToArray();
            if (faceIdArray.Any())
            {
                var identities = await ApiClients.FaceServiceClient.IdentifyAsync(peopleGroupId, faceIdArray, 0.25f);

                if (identities.Any())
                {
                    var identifyResults = identities.Where(i => i.Candidates.Any());

                    var people = new Dictionary<Guid, Person>();

                    foreach (var id in identifyResults)
                    {
                        var person = await ApiClients.FaceServiceClient.GetPersonAsync(peopleGroupId, id.Candidates[0].PersonId);
                        people.Add(id.FaceId, person);
                    }

                    return people;
                }
            }

            return new Dictionary<Guid, Person>();
        }
    }
}