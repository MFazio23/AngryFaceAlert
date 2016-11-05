using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AngryFaceAlertApis.Coordinators.Interfaces;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;

namespace AngryFaceAlertApis.Coordinators
{
    public class PersonCoordinator : IPersonCoordinator
    {
        public async Task<Person> GetPerson(string personGroupId, string personId)
        {
            //TODO: Get key from header/static class/etc.
            var client = new FaceServiceClient("");

            var person = await client.GetPersonAsync(personGroupId, Guid.Parse(personId));

            return person;
        }
    }
}