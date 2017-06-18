using System.Collections.Generic;
using Newtonsoft.Json;

namespace FreelancerBlog.Core.Types
{
    public class CaptchaResponse
    {
        [JsonProperty("success")]
        public string Success { get; set; }

        [JsonProperty("challenge_ts")]
        public string ChallengeTimeStamp { get; set; }

        [JsonProperty("hostname")]
        public string HostName { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
}
