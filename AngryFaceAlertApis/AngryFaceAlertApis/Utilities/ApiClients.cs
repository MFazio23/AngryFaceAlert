using System.Configuration;
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Face;

namespace AngryFaceAlertApis.Utilities
{
    public static class ApiClients
    {
        public static FaceServiceClient FaceServiceClient =
            new FaceServiceClient(ConfigurationManager.AppSettings["FaceServiceClientKey"]);

        public static EmotionServiceClient EmotionServiceClient =
            new EmotionServiceClient(ConfigurationManager.AppSettings["EmotionServiceClientKey"]);
    }
}