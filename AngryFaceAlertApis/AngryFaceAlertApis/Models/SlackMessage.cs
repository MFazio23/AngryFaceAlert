using Newtonsoft.Json;

namespace AngryFaceAlertApis.Models
{
    public class SlackMessage
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("icon_url")]
        public string IconUrl { get; set; }
        [JsonProperty("mrkdwn")]
        public bool Markdown { get; set; }
        [JsonProperty("channel")]
        public string Channel { get; set; }
    }
}