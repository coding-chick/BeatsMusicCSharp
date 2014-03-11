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


            // *** uncomment the next three lines and fill your Beats Music app details here! *** 
            //this.ClientId = "<your Beats Music app client ID here>";
            //this.ClientSecret = "<your Beats Music app client Secret here>";
            //this.RedirectUrl = "<your Beats Music app Redirect Uri here>";
          



            client = new BeatsMusicClient(ClientId, RedirectUrl, ClientSecret);
            BeatsMusicWebBrowser.Source = new Uri(client.UriAddressToNavigateForPermissions());
            BeatsMusicWebBrowser.Navigating += BeatsMusicWebBrowser_Navigating;
        }

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUrl { get; set; }

        public async void BeatsMusicWebBrowser_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.Uri != null && e.Uri.AbsoluteUri.Contains("code"))
            {
                var queryStringParams = HttpUtility.ParseQueryString(e.Uri.Query);
                //if (queryStringParams.AllKeys.Contains    ("access_token"))
                if (queryStringParams.AllKeys.Contains("code"))
                {
                    BeatsMusicWebBrowser.NavigateToString(@"<html><body style=""background: #F2F3F5"" /></html>");

                    //client.ReadOnlyAccessToken = queryStringParams.GetValues("access_token").FirstOrDefault();
                    client.Code = queryStringParams.GetValues("code").FirstOrDefault();

                    //var result = await client.Albums.GetAlbumById(string.Empty);
                    //var result2 = await client.Search.SearchByTrack("What's My Name");

                    var result = await client.Playlists.GetMultiplePlaylists(new string[] { "pl157622068769194496", "pl157588227841065472" });
                    Debug.Assert(true);

                    //beatsAccessor.GetToken()
                }
            }
        }
    }
}
