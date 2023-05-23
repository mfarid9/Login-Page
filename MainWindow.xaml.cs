using Haley.Abstractions;
using Haley.Models;
using Haley.Utils;
using Login_Page.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using static Login_Page.MainWindow;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;


namespace Login_Page
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //home.ClickMode = ClickMode.Press;
        }

        private async void btnlogin_Click(object sender, RoutedEventArgs e)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7206");
           
            Signin sign = new Signin();
            sign.Email = txtuser.Text;
            sign.Password = txtpass.Text;
            string jsonPayload = JsonSerializer.Serialize(sign);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/Signin", content);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("The API returned an error.");
            }
            var body = await response.Content.ReadAsStringAsync();

            var res = System.Text.Json.JsonSerializer.Deserialize<SigninResponse>(body);
            if (res.Status)
            {
                this.Hide();
                    CtlStartPage ctlStart = new CtlStartPage();
                    ctlStart.Show();
            }
            else {
                
                   System.Windows.MessageBox.Show(res.Message);

            }

        }
        



        class GoogleResponse
        {
            [JsonPropertyName("status")]
            public bool Status { get; set; }
            [JsonPropertyName("message")]

            public string Message { get; set; }
            [JsonPropertyName("result")]

            public string Result { get; set; }
        }

        private async void btngoogle_Click(object sender, RoutedEventArgs e)
        {
            var client = new HttpClient();

            // Set the HttpClient object's base address to the URL of the API.
            client.BaseAddress = new Uri("https://localhost:7206");


            // Make the HTTP request to the API.
            //  var response = await client.GetAsync();
            var response = await client.GetAsync("/ExternalLogin?AuthScheme=google");
            // Check the   status code.
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("The API returned an error.");
            }

            // Get the response body as a string.
            var body = await response.Content.ReadAsStringAsync();

            var res = System.Text.Json.JsonSerializer.Deserialize<GoogleResponse>(body);

            Process.Start(@"C:\Program Files\Google\Chrome\Application\chrome.exe", res.Result);
        }

        private async void SimpleButton_Click(object sender, RoutedEventArgs e)
        {

            var requrl = TokenService.GetRequestURL();
            if (string.IsNullOrWhiteSpace(requrl)) return;

            //Open the process.
            ProcessStartInfo pinfo = new ProcessStartInfo(requrl)
            {
                UseShellExecute = true
            };
            Process.Start(pinfo);


            //var client = new HttpClient();

            //// Set the HttpClient object's base address to the URL of the API.
            //client.BaseAddress = new Uri("https://localhost:7206");


            //// Make the HTTP request to the API.
            ////  var response = await client.GetAsync();
            //var response = await client.GetAsync("/ExternalLogin?AuthScheme=google");

            //// Check the   status code.
            //if (response.StatusCode != System.Net.HttpStatusCode.OK)
            //{
            //    throw new Exception("The API returned an error.");
            //}

            //// Get the response body as a string.
            //var content = await response.Content.ReadAsStringAsync();


            //var res = System.Text.Json.JsonSerializer.Deserialize<GoogleResponse>(content);

            //ProcessStartInfo pinfo = new(res.Result)
            //{
            //    UseShellExecute = true
            //};
            //Process.Start(pinfo);
        }
    }

    public static class TokenService
    {

        static IClient _gClient = new FluentClient();

        static string _clientID = @"1047530022498-ocgdcdo9g66d2rbqj1r12vp5dooah2m5.apps.googleusercontent.com";
        static string _clientSecret = @"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";
        static string _authURL = @"https://accounts.google.com/o/oauth2/v2/auth";
        static string _tokenURL = @"https://oauth2.googleapis.com/token";

        public static string GetRequestURL()
        {
            string state = randomDataBase64url(32);
            List<QueryParam> paramlist = new List<QueryParam>();
            paramlist.Add(new QueryParam("client_id", _clientID));
            paramlist.Add(new QueryParam("state", state));
            paramlist.Add(new QueryParam("redirect_uri", @"https://localhost:7206/signin-google"));
            paramlist.Add(new QueryParam("response_type", "code"));
            paramlist.Add(new QueryParam("scope", "email profile"));

            return _authURL + "?" + new QueryParamList(paramlist).GetConcatenatedString();
        }

        public static string randomDataBase64url(uint length)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] bytes = new byte[length];
            rng.GetBytes(bytes);
            return base64urlencodeNoPadding(bytes);
        }

        public static string base64urlencodeNoPadding(byte[] buffer)
        {
            string base64 = Convert.ToBase64String(buffer);

            // Converts base64 to base64url.
            base64 = base64.Replace("+", "-");
            base64 = base64.Replace("/", "_");
            // Strips padding.
            base64 = base64.Replace("=", "");

            return base64;
        }

        public static async Task<string> GetAccessToken(string code)
        {
            List<QueryParam> paramlist = new List<QueryParam>();
            paramlist.Add(new QueryParam("client_id", _clientID));
            paramlist.Add(new QueryParam("client_secret", _clientSecret));
            paramlist.Add(new QueryParam("redirect_uri", @"https://localhost:7206/signin-google"));
            paramlist.Add(new QueryParam("grant_type", "authorization_code"));
            paramlist.Add(new QueryParam("code", code));

            var raw_response = await _gClient
                .WithEndPoint(_tokenURL)
                .WithParameter(new FormEncodedRequest(paramlist))
                .PostAsync();
            var response = await raw_response.AsStringResponseAsync();
            if (!response.IsSuccessStatusCode) return null;

            var jObj = JsonObject.Parse(response.Content);
            if (jObj == null) return null;
            return jObj["access_token"]?.ToString();
        }
    }
}
