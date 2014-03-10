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

        }

        public async void BeatsMusicWebBrowser_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.Uri.AbsoluteUri.Contains("code"))
            {
                var queryStringParams = HttpUtility.ParseQueryString(e.Uri.Query);
                //if (queryStringParams.AllKeys.Contains("access_token"))
                if (queryStringParams.AllKeys.Contains("code"))
                {
                    //client.ReadOnlyAccessToken = queryStringParams.GetValues("access_token").FirstOrDefault();
                    client.Code = queryStringParams.GetValues("code").FirstOrDefault();

                    //var result = await client.Search.SearchByArtist("Sting");
                    //var result2 = await client.Search.SearchByTrack("What's My Name");

                    var result = await client.PlaylistsEndpoint.CreatePlaylist("My new Playlist", "test");
                    Debug.Assert(true);

                    //beatsAccessor.GetToken()
                }
            }
        }

        private void Browse_OnClick(object sender, RoutedEventArgs e)
        {
            client = new BeatsMusicClient(ClientId.Text, RedirectUrl.Text, ClientSecret.Text);
            BeatsMusicWebBrowser.Source = new Uri(client.UriAddressToNavigateForPermissions());
            BeatsMusicWebBrowser.Navigating += BeatsMusicWebBrowser_Navigating;
        }
    }

}
