using System;
using System.Collections.Generic;
using System.Linq;
using AngryFaceAlertApis.Models;
using Newtonsoft.Json;
using RestSharp;
using WebGrease.Css.Extensions;

namespace AngryFaceAlertApis.Coordinators
{
    public class SlackCoordinator
    {
        private static Dictionary<string, List<string>> feelings = new Dictionary<string, List<string>>()
            {
                { "happiness", new List<string>() { "happy", "ecstatic", "loving everything about life" } },
                { "anger", new List<string>() { "upset", "angry", "so pissed you don't even know" } },
                { "disgust", new List<string>() { "feeling repulsed", "disgusted", "about ready to vom" } },
                { "neutral", new List<string>() { "feeling neutral", "feeling meh", "contemplating whether life has any meaning" } },
                { "fear", new List<string>() { "scared", "horrified", "currently wetting themselves from fear" } },
                { "sadness", new List<string>() { "sad", "heartbroken", "might be clinically depressed right now. Who wants a hug??" } },
                { "contempt", new List<string>() { "not having none of that", "feeling contemptuous", "feeling pretty vitrolic. Avoid if at all possible." } }
            };

        public SlackMessage SendSlackMessage(IList<PersonEmotion> emotions)
        {
            var client = new RestClient("https://hooks.slack.com/services");
            client.AddDefaultHeader("Accept", "application/json");
            client.AddDefaultHeader("Content-Type", "application/json");
            var request = new RestRequest(System.Configuration.ConfigurationManager.AppSettings["SlackMessageEndUrl"])
            {
                Method = Method.POST,
                RequestFormat = DataFormat.Json
            };

            var body = new SlackMessage
            {
                Text = this.GenerateSlackMessage(emotions),
                Username = "Angry Kevin Bot",
                IconUrl = System.Configuration.ConfigurationManager.AppSettings["SlackMessageIconUrl"],
                Markdown = true
            };

            request.AddParameter("application/json", JsonConvert.SerializeObject(body), ParameterType.RequestBody);

            var response = client.Post(request);

            return body;
        }

        public string GenerateSlackMessage(IList<PersonEmotion> emotions)
        {
            //TODO: Make this good.
            var text = "We found some people!\n";

            var emotionGroups = new Dictionary<string, List<PersonEmotion>>();
            
            foreach(var e in emotions)
            {
                var emotionString = GetEmotionText(e);
                if (!emotionGroups.ContainsKey(emotionString)) emotionGroups.Add(emotionString, new List<PersonEmotion>());
                emotionGroups[emotionString].Add(e);
            }

            foreach (var emotion in emotionGroups)
            {
                if (emotion.Value.Any())
                {
                    var namesList = emotion.Value.Where(e => !e.Name.Equals("Unknown Person")).Select(e => e.Name).ToList();

                    var unknownPeople = emotion.Value.Count(e => e.Name.Equals("Unknown Person"));
                    if (unknownPeople > 0)
                    {
                        namesList.Add((unknownPeople > 1 ? unknownPeople + "" : (namesList.Any() ? "an" : "An")) + $" unknown {(unknownPeople > 1 ? "people" : "person")}");
                    }
                    
                    var emotionLine = string.Join(", ", namesList) +
                                      (emotion.Value.Count > 1 ? $" are all " : " is ") + $"{emotion.Key}.\n";
                    var lastIndexOfComma = emotionLine.LastIndexOf(", ", StringComparison.CurrentCulture);
                    if (lastIndexOfComma > 0)
                    {
                        emotionLine =
                            emotionLine.Substring(0, lastIndexOfComma) +
                            ", and" +
                            emotionLine.Substring(lastIndexOfComma + 1);
                    }
                    text += emotionLine;
                }
            }

            return text;
        }

        public string GetEmotionText(PersonEmotion person)
        {
            var newEmotion = "";
            var score = person.Emotion.Value;

            var test = feelings.Where(a => a.Key == person.Emotion.Key.ToLower()).FirstOrDefault();

            if(score <= .60)
            {
                newEmotion = test.Value[0];
            } 
            else if(score > .6 && score <= .90)
            {
                newEmotion = test.Value[1];
            }
            else
            {
                newEmotion = test.Value[2];
            }
            return newEmotion;
        }
    }
}