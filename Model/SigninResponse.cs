using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Login_Page.Model
{
    public class SigninResponse
    {
        [JsonPropertyName("status")]
        public bool Status { get; set; }
        [JsonPropertyName("message")]

        public string Message { get; set; }
        [JsonPropertyName("result")]
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
