using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using AngryFaceAlertApis.Coordinators.Interfaces;
using AngryFaceAlertApis.Extensions;
using AngryFaceAlertApis.Models;
using AngryFaceAlertApis.Utilities;
using Microsoft.ProjectOxford.Emotion.Contract;
using Microsoft.ProjectOxford.Face.Contract;
using NLog;

namespace AngryFaceAlertApis.Coordinators
{
    public class PersonCoordinator : IPersonCoordinator
    {
        private readonly EmotionCoordinator _emotionCoordinator;
        private readonly FaceCoordinator _faceCoordinator;
        private readonly SlackCoordinator _slackCoordinator;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public PersonCoordinator()
        {
            _faceCoordinator = new FaceCoordinator();
            _emotionCoordinator = new EmotionCoordinator();
            _slackCoordinator = new SlackCoordinator();
        }

        public async Task<Person> GetPerson(string personGroupId, string personId)
        {
            var person = await ApiClients.FaceServiceClient.GetPersonAsync(personGroupId, Guid.Parse(personId));

            return person;
        }

        public async Task<IList<PersonEmotion>> GetEmotionsForPeopleFromImage(Stream imageStream = null, string imageUrl = null)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            var faces = imageStream != null
                ? await _faceCoordinator.GetListOfFaces(imageStream.CreateStreamCopy())
                : await _faceCoordinator.GetListOfFaces(imageUrl: imageUrl);
            stopwatch.Stop();
            _logger.Log(LogLevel.Info, $"List of faces took {stopwatch.ElapsedMilliseconds}ms.");
            stopwatch.Reset();

            stopwatch.Start();
            var emotions = imageStream != null
                ? await _emotionCoordinator.GetEmotionsForFaces(faces, imageStream.CreateStreamCopy())
                : await _emotionCoordinator.GetEmotionsForFaces(faces, imageUrl: imageUrl);
            stopwatch.Stop();
            _logger.Log(LogLevel.Info, $"List of emotions took {stopwatch.ElapsedMilliseconds}ms.");
            stopwatch.Reset();

            stopwatch.Start();
            //TODO: Don't hardcode the people group.  Please.
            var people = await _faceCoordinator.GetPeopleFromFaceIds("skyline", emotions.Keys);
            stopwatch.Stop();
            _logger.Log(LogLevel.Info, $"List of people from face IDs took {stopwatch.ElapsedMilliseconds}ms.");
            stopwatch.Reset();

            stopwatch.Start();
            var peopleEmotions = GetEmotionsForPeople(emotions, people);
            stopwatch.Stop();
            _logger.Log(LogLevel.Info, $"List of emotions for people took {stopwatch.ElapsedMilliseconds}ms.");
            stopwatch.Reset();

            stopwatch.Start();
            this._slackCoordinator.SendSlackMessage(peopleEmotions);
            stopwatch.Stop();
            _logger.Log(LogLevel.Info, $"Sending a message to Slack took {stopwatch.ElapsedMilliseconds}ms.");
            stopwatch.Reset();

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
                    Emotion = emotion.Scores.GetEmotion(),
                    PersonId = person?.PersonId ?? Guid.Empty,
                    Name = person?.Name ?? "Unknown Person",
                    Email = person?.UserData ?? "N/A"
                });
            }

            return personEmotions;
        }
    }
}