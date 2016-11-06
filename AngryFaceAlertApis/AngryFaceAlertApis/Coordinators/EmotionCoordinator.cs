using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AngryFaceAlertApis.Extensions;
using AngryFaceAlertApis.Utilities;
using Microsoft.ProjectOxford.Emotion.Contract;
using Microsoft.ProjectOxford.Face.Contract;

namespace AngryFaceAlertApis.Coordinators
{
    public class EmotionCoordinator
    {
        public async Task<Dictionary<Guid, Emotion>> GetEmotionsForFaces(Face[] faces, Stream imageStream = null,
            string imageUrl = null)
        {
            var emotionRecon = imageStream != null
                ? await ApiClients.EmotionServiceClient.RecognizeAsync(imageStream)
                : await ApiClients.EmotionServiceClient.RecognizeAsync(imageUrl);

            var emotions = new Dictionary<Guid, Emotion>();

            foreach (var emotion in emotionRecon)
            {
                var faceId =
                    faces.FirstOrDefault(f => f.FaceRectangle.FaceRectanglesAreEqual(emotion.FaceRectangle))?.FaceId;

                if (faceId.HasValue)
                    emotions.Add(faceId.Value, emotion);
            }

            return emotions;
        }
    }
}