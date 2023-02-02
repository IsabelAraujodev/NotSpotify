using Newtonsoft.Json;
using NotSpotify.MVVM.Model.QuickType;
using RestSharp;
using RestSharp.Authenticators.OAuth2;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace NotSpotify.MVVM.ViewModel
{
   
    internal class MainViewModel
    {
        private int lenght;

        public ObservableCollection<Item> Songs { get; set; }
        public MainViewModel() {
             Songs=new ObservableCollection<Item>();
            PopulateCollection();
        }
        void PopulateCollection()
        {
            var client = new RestClient();
            client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator("BQCRVEE8pHSfUm5YB-uiQtAIQFYk5icvkVKCbeTLnqDPNY0Ax1G5iYiN_eTdenc_Kv1Bfw5luHUZjFnV7AOnU917vRDNKZkTsZs9lpy9ek6-vhB4EKCGi9Up-F0lPLt58k20nOGLLIq80KnVOn2pGqgX7R10RT5gMnetdIAWXamlE3ITEorPyOiG8PGwL3TwZHLl", "Bearer");
            var request = new RestRequest("https://api.spotify.com/v1/browse/new-releases", Method.Get);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            var response = client.GetAsync(request).GetAwaiter().GetResult();
            var data = JsonConvert.DeserializeObject<TrackModel>(response.Content);

            for (int i = 0; i < data.Albums.Limit; i++)
            {
                var track = data.Albums.Items[i];
                track.Duration = "2:32";
                Songs.Add(track);
            }
        }
    }
}
