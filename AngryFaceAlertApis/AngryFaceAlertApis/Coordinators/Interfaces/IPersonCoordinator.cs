using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.ProjectOxford.Face.Contract;

namespace AngryFaceAlertApis.Coordinators.Interfaces
{
    public interface IPersonCoordinator
    {
        Task<Person> GetPerson(string personGroupId, string personId);
    }
}