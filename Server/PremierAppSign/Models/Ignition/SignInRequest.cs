using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PremierAppSign.Models.Ignition
{
    public class SignInRequest
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
