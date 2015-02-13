[![Build status](https://ci.appveyor.com/api/projects/status/github/NaosFramework/Naos.FluentUri?branch=master&svg=true)](https://ci.appveyor.com/project/NaosLLC/naos-fluenturi)
<br/> 
[![NuGet Status](http://nugetstatus.com/Naos.FluentUri.png)](http://nugetstatus.com/packages/Naos.FluentUri)

Naos.FluentUri
=============
A fluent grammar on top of the .NET Uri object for building up the Uri of a RESTful service and calling it with the appropriate information.

Use - Referencing in your code
-----------
The entire implemenation is in a single file so it can be included without taking a dependency on the NuGet package if necessary preferred.
* Reference the NuGet package: <a href="http://www.nuget.org/packages/Naos.FluentUri">http://www.nuget.org/packages/Naos.FluentUri</a>.
  <br/><b>OR</b>
* Include the single file in your project: <a href="https://raw.githubusercontent.com/NaosFramework/Naos.FluentUri/master/Naos.FluentUri/Naos.FluentUri.cs">https://raw.githubusercontent.com/NaosFramework/Naos.FluentUri/master/Naos.FluentUri/Naos.FluentUri.cs</a>.

Use - Building up the Uri
-------------------
* Adding segments to the URL
 - To get `http://baseUrlOfService/subPath` use `new Uri("http://baseUrlOfService").AppendPathSegment("subPath");`
 - Trailing slashes are handled... `http://baseUrlOfService/subPath` can also be done using `new Uri("http://baseUrlOfService/").AppendPathSegment("subPath");`

* Adding query string parameters
 - To get `http://url?myParam=aValue` use `new Uri(http://url).AppendQueryStringParam("myParam", "aValue");`
 - Can also be done in batches by using `.AppendQueryStringParams(/* IDictionary<string, string> */);` or `.AppendQueryStringParams(/* ICollection<KeyValuePair<string, string>> */);`
 - Will work correctly out of order too... `http://url/path?q=hello` can be done using `new Uri("http://url").AppendQueryStringParam("q", "hello").AppendPathSegment("path");` although this is not really recommended as it doesn't read great...
 
Use - Calling the Uri
---------------
* Void Return vs. Typed Return
 - Void Return Usage:
     - `new Uri("http://api/Objects/42/Setting").Put();`
 - Typed Return Usage: 
     - `MyObject returnedData = net Uri("http://api/Objects/42").Get<MyObject>();`

* The four main verbs are currently supported as first class methods as well as a method to pass any verb as a string.
 - GET - `new Uri("http://api/Objects/42").Get();`
 - POST - `new Uri("http://api/Objects/").Post();`
 - PUT - `new Uri("http://api/Objects/42/Setting").Put();`
 - DELETE - `new Uri("http://api/Objects/42").Delete();`
 - "Custom Verb" - `new Uri("http://api/Objects/42").CallWithVerb("Custom Verb");`

* There are additional decorators for the call for standard operations.
 - Add a body to the request:
     - `new Uri("http://api/Objects/").WithBody(new MyObject()).Post();`
 - Add a cookie to the request:
     - `// Cookie can also be of type HttpCookie...`
     - `var cookie = new Cookie(".ASPXAUTH", "[AuthCookieValue]") { Expires = DateTime.Now.AddDays(30) };`
     - `var obj = new Uri("http://api/Objects/42").WithCookie(cookie).Get<MyObject>();`
 - Add headers to the request:
     - `// Headers can also be of type WebHeaderCollection or NameValueCollection..`
     - `var headers = new[] { new KeyValuePair<string, string>("Auth-Token", "[AuthTokenValue]") };`
     - `var obj = new Uri("http://api/Objects/42").WithHeaders(headers).Get<MyObject>();`
 - Update the timeout of the request:
     - `var obj = new Uri("http://api/Objects/42").WithTimeout(TimeSpan.FromHours(1)).Get<MyObject>();`
 - Save response headers using a lambda:
     - `KeyValuePair<string, string>[] responseHeadersFromCall;`
     - `new Uri("http://api/Auth/Login").WithResponseHeaderSaveAction(responseHeaders => responseHeadersFromCall = responseHeaders).Post();`

Combined Examples
------------
* Example of a request that might authenticate against a system.
 - ```
var loginResponse = new Uri("http://auth.services.com")
            .AppendPathSegment("login")
            .WithBody(loginRequest)
            .Post<LoginResponse>();
```
* Example of a request that might update a user's zip code in the system.
 - ```
new Uri("http://user.services.com")
            .AppendPathSegment("user")
            .AppendPathSegment(user.Id)
            .AppendPathSegment("zipCode")
            .AppendPathSegment(user.ZipCode)
            .WithCookie(authCookieToAllowThis)
            .Put();
```