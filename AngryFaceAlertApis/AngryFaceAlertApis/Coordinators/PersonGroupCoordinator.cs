using AngryFaceAlertApis.Coordinators.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.ProjectOxford.Face.Contract;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Face;
using AngryFaceAlertApis.Models.ApiModels;

namespace AngryFaceAlertApis.Coordinators
{
    public class PersonGroupCoordinator : IPersonGroupCoordinator
    {
        private readonly string _apiKey;

        public PersonGroupCoordinator(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<PersonGroup[]> GetAllPersonGroups()
        {
            var client = new FaceServiceClient(_apiKey);
            return await client.ListPersonGroupsAsync();
        }

        public async Task<PersonGroup> GetPersonGroup(string id)
        {;
            var client = new FaceServiceClient(_apiKey);
            return await client.GetPersonGroupAsync(id);
        }

        public async Task CreatePersonGroup(string id, NameUserDataInfoApiModel data)
        {
            var client = new FaceServiceClient(_apiKey);
            await client.CreatePersonGroupAsync(id, data.Name, data.UserData);
        }

        public async Task UpdatePersonGroup(string id, NameUserDataInfoApiModel data)
        {
            var client = new FaceServiceClient(_apiKey);
            await client.UpdatePersonGroupAsync(id, data.Name, data.UserData);
        }

        public async Task DeletePersonGroup(string id)
        {
            var client = new FaceServiceClient(_apiKey);
            await client.DeletePersonGroupAsync(id);
        }

        public async Task TrainPersonGroup(string id)
        {
            var client = new FaceServiceClient(_apiKey);
            await client.TrainPersonGroupAsync(id);
        }
    }
}