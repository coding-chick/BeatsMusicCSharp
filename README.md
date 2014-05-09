Beats Music C# API
================

BeatsMusic C# API is a C# facade that accesses the Beats Music Developer API (@[https://developer.beatsmusic.com/](https://developer.beatsmusic.com/)
) from C#. It provides a consistent C# API and data types for the Beats Music REST API. This API is a portable C# class library which is designed to work in multiple .Net environments: 
* .Net framework 4.5
* Silverlight 5
* Windows 8
* Windows Phone Silverlight 8

Getting started
---------------
#### Getting Started #1: Get the framework
You can use nuget to get the BeatsMusic C# API
```console
PM> Install-Package BeatsMusicCSharpAPI
```

#### Getting Started #2: Get Beats Music developer credentials
You will need to register your software at Beats Music Developer website (@[https://developer.beatsmusic.com/member/register](https://developer.beatsmusic.com/member/register)) to obtain your application's ClientId and an optional ClientSecret.

#### Getting Started #3: Initialize a BeatsMusicClient
You will then use the ClientId and optionally the ClientSecret to initialize the BeatsMusicClient object which will serve as gateway to all the API calls (depends on the security level you wish to give your application).
```csharp
BeatsMusicClient client = new BeatsMusicClient(ClientId, RedirectUrl, ClientSecret); // For "Web Server applications" type authentication. 
// Or
BeatsMusicClient client = new BeatsMusicClient(ClientId, RedirectUrl); // For "Client Side applications" type authentication.  
```
#### Getting Started #4: Do stuff
That's it! You're ready to use API calls which do not require user- specific permissions.
```csharp
var result2 = await client.Search.SearchByArtist("Connie");
```

#### Getting Started #5: Advanced actions and authentication
##### Getting the authentication URI
If you need to perform actions which would require user specific permissions, have your app navigate to Beats Music's OAuth webpage. You need to obtain the relevant address from the BeatsMusicClient, and have a web browser navigate to this address. 
The URI address you recieve from the Beats Music client depends on how you intialized your client.
```csharp
string addressToNavigate = client.UriAddressToNavigateForPermissions();
```

##### Getting the authentication details from the redirected URI and setting them up 
After the user inputs the credentials, the redirected URI's query string parameters contains the relevant authorization information, which your application will need to provide when making API calls. You will need to let the BeatsMusicClient know this information.
```csharp
client.Code = queryStringParams.GetValues("code").FirstOrDefault(); // For "Web Server applications" type authentication.
// Or
client.SetClientAccessTokenFromRedirectUri(queryStringParams.GetValues("access_token").FirstOrDefault(), 
		int.Parse(queryStringParams.GetValues("expires_in").FirstOrDefault())); // For "Client Side applications" type authentication.
```
##### Calling advanced methods
Now you can make more API calls, depends on the security level you've chosen.
```csharp
// Works with both permissions
var result = await client.Audio.GetAudioStreamingInfo("tr61032803", Bitrate.Highest, true);
// Works only with Web Server applications.
var result = await client.Playlists.CreatePlaylist("testPL", "only works with server side permissions");
```

Example
-------

``` csharp
// Initialize a BeatsMusicClient object which will serve as the endpoint for accessing Beats Music API.
BeatsMusicClient client = new BeatsMusicClient(ClientId, RedirectUrl, ClientSecret);

//You can immediately call methods which do not require user permissions.
MultipleRootObject<SearchData> result2 = await client.Search.SearchByArtist("Connie");

// If you need to use methods that require user permissions-
// Get the address the web browser needs to navigate to for OAuth 2.0 protocol authentication. 
var addressString = client.UriAddressToNavigateForPermissions();

// Navigate to the BeatsMusic OAuth page. This code is browser/ platform- specific.
BeatsMusicWebBrowser.Source = new Uri(addressString);

// After the user logs in Beats Music using AOuth the redirected URL contains the authorization code you need to 
// provide the Beats Music API in order to make calls against the API.
client.Code = queryStringParams.GetValues("code").FirstOrDefault();

// That's it, now you can use the API endpoints to make calls against the server.
SingleRootObject<AudioData> result = await client.Audio.GetAudioStreamingInfo("tr61032803", Bitrate.Highest, true);
```

Authentication levels
---------------------

There are two types of authentication levels you can give your application: 
* Client Side Application (@[https://developer.beatsmusic.com/docs/read/getting_started/Client_Side_Applications](https://developer.beatsmusic.com/docs/read/getting_started/Client_Side_Applications)). This authentication requires your application to provide only the ClientId when initializing the client. It provides a short- term more limited access that is not renewable.
* Web Server Application (@[https://developer.beatsmusic.com/docs/read/getting_started/Web_Server_Applications](https://developer.beatsmusic.com/docs/read/getting_started/Web_Server_Applications)). This authentication requires your application to provide ClientId and SecretId when initializing the client. It provides a long- term full access and renews automatically after timing out as long as the application is running.



