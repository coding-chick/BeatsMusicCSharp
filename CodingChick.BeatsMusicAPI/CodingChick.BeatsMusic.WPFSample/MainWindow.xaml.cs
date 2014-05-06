using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CodingChick.BeatsMusicAPI.Core;
using CodingChick.BeatsMusicAPI.Core.Base;
using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Data.Audio;
using CodingChick.BeatsMusicAPI.Core.Endpoints;
using CodingChick.BeatsMusicAPI.Core.Endpoints.DataFilters;
using CodingChick.BeatsMusicAPI.Core.Endpoints.Enums;

namespace CodingChick.BeatsMusic.WPFSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BeatsMusicClient client;

        public MainWindow()
        {
            InitializeComponent();
            this.StretchToMaximum();

            // *** Fill your Beats Music app details here! *** 
            //this.ClientId = "<your Beats Music app client ID here>";
            //this.ClientSecret = "<your Beats Music app client Secret here>";
            //this.RedirectUrl = "<your Beats Music app Redirect Uri here>";


            // Create the beats music API client that will call all services, can be called with ClientSecret for enhanced long term security,
            // or without for short term limited security. More infomation on types of security @ https://developer.beatsmusic.com/docs/read/getting_started/Client_Side_Applications
            // and https://developer.beatsmusic.com/docs/read/getting_started/Web_Server_Applications
            client = new BeatsMusicClient(ClientId, RedirectUrl, ClientSecret);

            // Get the address the web browser needs to navigate for OAuth 2.0 protocol authentication.
            var addressString = client.UriAddressToNavigateForPermissions();
            // Navigate to the BeatsMusic OAuth page.
            BeatsMusicWebBrowser.Source = new Uri(addressString);
            BeatsMusicWebBrowser.Navigating += BeatsMusicWebBrowser_Navigating;
        }

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUrl { get; set; }

        public async void BeatsMusicWebBrowser_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            // A check that the OAuth page has redirected to the redirected url provided.
            if (e.Uri != null && e.Uri.AbsoluteUri.Contains("insert redirected domain here"))
            {
                var queryStringParams = HttpUtility.ParseQueryString(e.Uri.Query);

                // The first (commented) if statement is the key required for Client Side application (lower security), 
                // the second (uncommented) if is for Web Server applications (higher security).

                //if (queryStringParams.AllKeys.Contains("access_token"))
                if (queryStringParams.AllKeys.Contains("code"))
                {
                    BeatsMusicWebBrowser.NavigateToString(@"<html><body style=""background: #FFFFFF"" /></html>");

                    // The first (commented) if statement is the key required for Client Side application (lower security), 
                    // the second (uncommented) if is for Web Server applications (higher security).

                    //client.SetClientAccessTokenFromRedirectUri(queryStringParams.GetValues("access_token").FirstOrDefault(), int.Parse(queryStringParams.GetValues("expires_in").FirstOrDefault()));
                    client.ServerCode = queryStringParams.GetValues("code").FirstOrDefault();

                    // This is an example of calling the BeatsMusic API, this call will get an audio track info required for streaming. 
                    // Calling this method with the aquire set to true just to make sure this works every time.
                    // more info @https://developer.beatsmusic.com/docs/read/audio/Playback
                    SingleRootObject<AudioData> result = await client.Audio.GetAudioStreamingInfo("tr61032803", Bitrate.Highest, true);


                    // To demonstrate how the information can be used, I'm using an OS web music player- SoundManager 2 (from @http://www.schillmania.com/projects/soundmanager2/) to play this file.
                    // The files required and the HTML file are included in this project under the SoundManager directory.
                    // SoundManager directory is hosted on local iis due to security issues with flash, js and soundmanager when running the local file.
                    // If SoundManager retunes an exception try refreshing the page and/or update the WebBrowser version to a newer IE version by running the UpgradeBrowserToIE11.reg file included.
                    BeatsMusicWebBrowser.Navigate(new Uri(String.Format("http://localhost:8081/soundManager/SoundManager/HTMLAudioPlayer.html?&trackId={0}&trackUrl={1}&serverUrl={2}",
                        result.Data.Refs.Track.Id, result.Data.Resource, result.Data.Location)));
                }
            }
        }

    }

 
}
