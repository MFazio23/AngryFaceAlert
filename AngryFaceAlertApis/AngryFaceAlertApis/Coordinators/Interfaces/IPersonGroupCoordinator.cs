using AngryFaceAlertApis.Models.ApiModels;
using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AngryFaceAlertApis.Coordinators.Interfaces
{
    public interface IPersonGroupCoordinator
    {
        Task<PersonGroup[]> GetAllPersonGroups();
        Task<PersonGroup> GetPersonGroup(string id);
        Task CreatePersonGroup(string id, NameUserDataInfoApiModel data);
        Task UpdatePersonGroup(string id, NameUserDataInfoApiModel data);
        Task DeletePersonGroup(string id);
        Task TrainPersonGroup(string id);

    }
}