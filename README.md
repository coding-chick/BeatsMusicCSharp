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
First, you will need to register your software at Beats Music Developer website (@[https://developer.beatsmusic.com/member/register](https://developer.beatsmusic.com/member/register)) to obtain your application's ClientId and an optional ClientSecret.

You can use nuget to get the BeatsMusic C# API
```console
PM> Install-Package BeatsMusicCSharpAPI
```

You will then use the ClientId and optionally the ClientSecret to initialize the BeatsMusicClient object which will serve as gateway to all the API calls (depends on the security level you wish to give your application).
```csharp
BeatsMusicClient client = new BeatsMusicClient(ClientId, RedirectUrl, ClientSecret); // For "Web Server applications" type authentication. 
// Or
BeatsMusicClient client = new BeatsMusicClient(ClientId, RedirectUrl); // For "Client Side applications" type authentication.  
```

That's it! You're ready to use API calls which do not require user- specific permissions.
```csharp
MultipleRootObject<SearchData> result2 = await client.Search.SearchByArtist("Connie");
```

If you need to perform actions which would require user specific permissions, have your app navigate to Beats Music's OAuth webpage. You need to obtain the relevant address from the BeatsMusicClient, and have a web browser navigate to this address. 
```csharp
string addressToNavigate = client.UriAddressToNavigateForPermissions();
```

After the user inputs the credentials, the redirected URI's query string parameters contains the relevant authorization information, which your application will need to provide when making API calls. You will need to let the BeatsMusicClient know this information.
```csharp
client.Code = queryStringParams.GetValues("code").FirstOrDefault(); // For "Web Server applications" type authentication.
// Or
client.SetClientAccessTokenFromRedirectUri(queryStringParams.GetValues("access_token").FirstOrDefault(), 
		int.Parse(queryStringParams.GetValues("expires_in").FirstOrDefault())); // For "Client Side applications" type authentication.
```

Now you can freely make any API calls.
```csharp
SingleRootObject<AudioData> result = await client.Audio.GetAudioStreamingInfo("tr61032803", Bitrate.Highest, true);
```

Example
-------

``` csharp
// Initialize a BeatsMusicClient object which will serve as the endpoint for accessing Beats Music API.
BeatsMusicClient client = new BeatsMusicClient(ClientId, RedirectUrl, ClientSecret);

//You can immidiatly call methods which do not require user permissions.
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



