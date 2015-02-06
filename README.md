[![Build status](https://ci.appveyor.com/api/projects/status/github/NaosFramework/Naos.FluentUri?branch=master&svg=true)](https://ci.appveyor.com/project/NaosLLC/naos-fluenturi)
<br/> 
[![NuGet Status](http://nugetstatus.com/Naos.FluentUri.png)](http://nugetstatus.com/packages/Naos.FluentUri)

Naos.FluentUri
=============
A fluent grammar on top of the .NET Uri object for calling RESTful services.

Overview
--------
This library is concerned with the standard calls to RESTful services from a .NET application.  You can call all four verbs and you can call them either void return or a typed return.

1. Void Return vs. Typed Return
 - Void Return Usage:
     - `new Uri("http://api/Objects/42/Setting").Put();`
 - Typed Return Usage: 
     - `MyObject returnedData = net Uri("http://api/Objects/42").Get<MyObject>();`

2. The four main verbs are currently supported.
 - GET - `new Uri("http://api/Objects/42").Get();`
 - POST - `new Uri("http://api/Objects/").Post();`
 - PUT - `new Uri("http://api/Objects/42/Setting").Put();`
 - DELETE - `new Uri("http://api/Objects/42").Delete();`
 - "Custom Verb" - `new Uri("http://api/Objects/42").CallWithVerb("Custom Verb");`

3. There are additional decorators for the call for standard operations.
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
