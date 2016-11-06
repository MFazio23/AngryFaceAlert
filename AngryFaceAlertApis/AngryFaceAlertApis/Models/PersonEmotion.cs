using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.ProjectOxford.Emotion.Contract;

namespace AngryFaceAlertApis.Models
{
    public class PersonEmotion
    {
        public string Emotion { get; set; }
        public Guid PersonId { get; set; }
        public string Name { get; set; }
    }
}