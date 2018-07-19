using System.Collections.Generic;

namespace PremierAppSign.Configurations
{
    public class OAuthSettings
    {
        public List<Client> Clients { get; set; }
    }

    public class Client
    {
        public string ClientId { get; set; }
        public string Secret { get; set; }
    }
}
