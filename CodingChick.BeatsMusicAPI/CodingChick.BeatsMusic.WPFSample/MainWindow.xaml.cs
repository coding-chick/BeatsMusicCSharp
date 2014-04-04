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
        BeatsMusicClient client;

        public MainWindow()
        {
            InitializeComponent();
            this.StretchToMaximum();


            // *** Fill your Beats Music app details here! *** 
            this.ClientId = "<your Beats Music app client ID here>";
            this.ClientSecret = "<your Beats Music app client Secret here>";
            this.RedirectUrl = "<your Beats Music app Redirect Uri here>";

       

            client = new BeatsMusicClient(ClientId, RedirectUrl, ClientSecret);
            var addressString = client.UriAddressToNavigateForPermissions();
            BeatsMusicWebBrowser.Source = new Uri(addressString);
            BeatsMusicWebBrowser.Navigating += BeatsMusicWebBrowser_Navigating;
        }

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUrl { get; set; }

        public async void BeatsMusicWebBrowser_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.Uri != null && e.Uri.AbsoluteUri.Contains("codingchick"))
            {
                var queryStringParams = HttpUtility.ParseQueryString(e.Uri.Query);
                //if (queryStringParams.AllKeys.Contains("access_token"))
                if (queryStringParams.AllKeys.Contains("code"))
                {
                    BeatsMusicWebBrowser.NavigateToString(@"<html><body style=""background: #F2F3F5"" /></html>");

                    client.Code = queryStringParams.GetValues("code").FirstOrDefault();
                    //client.SetReadAccessTokenFromRedirectUri(queryStringParams.GetValues("access_token").FirstOrDefault(), int.Parse(queryStringParams.GetValues("expires_in").FirstOrDefault()));

                    //var result = await client.Albums.GetAlbumById(string.Empty);
                    //var result2 = await client.Search.SearchByTrack("What's My Name");
                    //var filters = new StreamabilityFilters() {Streamable = false};
                    var result = await client.Playlists.CreatePlaylist("Give me a new playlist", "ohhhh test");

                    //var result2 = await client.Playlists.GetTracksInPlaylist("pl157588227841065472", 0, 20, PlaylistRefType.Artists | PlaylistRefType.Album);

                    Debug.Assert(true);
                }
            }
        }
    }
}
