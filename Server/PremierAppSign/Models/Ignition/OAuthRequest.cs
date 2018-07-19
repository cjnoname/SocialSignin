using Newtonsoft.Json;

namespace PremierAppSign.Models.Ignition
{
    public class OAuthRequest
    {
        [JsonProperty(PropertyName = "grant_type")]
        public string GrantType { get; set; }
        [JsonProperty(PropertyName = "client_id")]
        public string ClientId { get; set; }
        [JsonProperty(PropertyName = "client_secret")]
        public string ClientSecret { get; set; }
    }
}
