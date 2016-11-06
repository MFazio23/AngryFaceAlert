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
        public SlackMessage SendSlackMessage(IList<PersonEmotion> emotions)
        {
            var client = new RestClient("https://hooks.slack.com");
            client.AddDefaultHeader("Accept", "application/json");
            client.AddDefaultHeader("Content-Type", "application/json");
            var request = new RestRequest("services/T0AJQT74L/B1QMH7M5Y/08SVo5J21dpcRnxCPAW2m7Ya")
            {
                Method = Method.POST,
                RequestFormat = DataFormat.Json
            };

            var body = new SlackMessage
            {
                Text = this.GenerateSlackMessage(emotions),
                Username = "AFA Test",
                IconUrl = "https://images-na.ssl-images-amazon.com/images/I/51WgT7YSg-L._AC_SR160,160_.jpg",
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

            //TODO: Turn this into a foreach loop.
            emotions.ForEach(e =>
            {
                if (!emotionGroups.ContainsKey(e.Emotion)) emotionGroups.Add(e.Emotion, new List<PersonEmotion>());

                emotionGroups[e.Emotion].Add(e);
            });

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
    }
}