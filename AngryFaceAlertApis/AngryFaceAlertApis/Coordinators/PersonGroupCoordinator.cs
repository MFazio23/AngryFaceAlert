using AngryFaceAlertApis.Coordinators.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.ProjectOxford.Face.Contract;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Face;
using AngryFaceAlertApis.Models.ApiModels;
using AngryFaceAlertApis.Utilities;

namespace AngryFaceAlertApis.Coordinators
{
    public class PersonGroupCoordinator : IPersonGroupCoordinator
    {
        public PersonGroupCoordinator()
        {
        }

        public async Task<PersonGroup[]> GetAllPersonGroups()
        {
            return await ApiClients.FaceServiceClient.ListPersonGroupsAsync();
        }

        public async Task<PersonGroup> GetPersonGroup(string id)
        {
            return await ApiClients.FaceServiceClient.GetPersonGroupAsync(id);
        }

        public async Task CreatePersonGroup(string id, NameUserDataInfoApiModel data)
        {
            await ApiClients.FaceServiceClient.CreatePersonGroupAsync(id, data.Name, data.UserData);
        }

        public async Task UpdatePersonGroup(string id, NameUserDataInfoApiModel data)
        {
            await ApiClients.FaceServiceClient.UpdatePersonGroupAsync(id, data.Name, data.UserData);
        }

        public async Task DeletePersonGroup(string id)
        {
            await ApiClients.FaceServiceClient.DeletePersonGroupAsync(id);
        }

        public async Task TrainPersonGroup(string id)
        {
            await ApiClients.FaceServiceClient.TrainPersonGroupAsync(id);
        }
    }
}