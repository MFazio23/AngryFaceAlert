using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AngryFaceAlertApis.Coordinators.Interfaces;
using AngryFaceAlertApis.Extensions;
using AngryFaceAlertApis.Models;
using Microsoft.ProjectOxford.Emotion.Contract;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using Newtonsoft.Json;
using RestSharp;

namespace AngryFaceAlertApis.Coordinators
{
    public class PersonCoordinator : IPersonCoordinator
    {
        private readonly EmotionCoordinator _emotionCoordinator;
        private readonly FaceCoordinator _faceCoordinator;

        public PersonCoordinator()
        {
            _faceCoordinator = new FaceCoordinator();
            _emotionCoordinator = new EmotionCoordinator();
        }

        public async Task<Person> GetPerson(string personGroupId, string personId)
        {
            //TODO: Get key from header/static class/etc.
            var client = new FaceServiceClient("");

            var person = await client.GetPersonAsync(personGroupId, Guid.Parse(personId));

            return person;
        }

        public async Task<IList<PersonEmotion>> GetEmotionsForPeopleFromImage(Stream imageStream = null, string imageUrl = null)
        {
            var faces = imageStream != null
                ? await _faceCoordinator.GetListOfFaces(imageStream.CreateStreamCopy())
                : await _faceCoordinator.GetListOfFaces(imageUrl: imageUrl);

            var emotions = imageStream != null
                ? await _emotionCoordinator.GetEmotionsForFaces(faces, imageStream.CreateStreamCopy())
                : await _emotionCoordinator.GetEmotionsForFaces(faces, imageUrl: imageUrl);

            //TODO: Don't hardcode the people group.  Please.
            var people = await _faceCoordinator.GetPeopleFromFaceIds("skyline", emotions.Keys);

            var peopleEmotions = GetEmotionsForPeople(emotions, people);

            this.SendMessageToSlack(peopleEmotions);

            return peopleEmotions;
        }

        public IList<PersonEmotion> GetEmotionsForPeople(Dictionary<Guid, Emotion> emotions,
            Dictionary<Guid, Person> people)
        {
            var personEmotions = new List<PersonEmotion>();

            foreach (var e in emotions)
            {
                var faceId = e.Key;
                var emotion = e.Value;
                var person = people.ContainsKey(faceId) ? people[faceId] : null;

                personEmotions.Add(new PersonEmotion
                {
                    Emotion = emotion.Scores.GetTopEmotion(),
                    PersonId = person?.PersonId ?? Guid.Empty,
                    Name = person?.Name ?? "Unknown Person"
                });
            }

            return personEmotions;
        }

        public void SendMessageToSlack(IList<PersonEmotion> personEmotions)
        {
            var client = new RestClient("https://hooks.slack.com");
            client.AddDefaultHeader("Accept", "application/json");
            client.AddDefaultHeader("Content-Type", "application/json");
            var request = new RestRequest("")
            {
                Method = Method.POST,
                RequestFormat = DataFormat.Json
            };

            var body = new
            {
                text = JsonConvert.SerializeObject(personEmotions),
                username = "AFA Test II",
                icon_url = "https://images-na.ssl-images-amazon.com/images/I/51WgT7YSg-L._AC_SR160,160_.jpg",
                mrkdwn = false
            };
            request.AddBody(body);

            var response = client.Post(request);

            Console.WriteLine(response);
        }
    }
}