# etsy-api-cs
Etsy API made with [Refit](https://github.com/reactiveui/refit).

Because simplicity is great and so is Refit.

### MyGet Dev Nuget Package Feed

https://www.myget.org/F/dotnet-etsy-api/api/v3/index.json


### Basic Usage

1. Install nuget Package `EtsyApi`
2. Create auth and service

```csharp
    var auth = new EtsyApiAuth
    {
        ConsumerKey = ConsumerKey, //get from step 1
        ConsumerSecret = ConsumerSecret, //1
        TokenSecret = TokenSecret, //2 temp
        VerifierSecret = VerifierSecret, //2
        Token = Token, //2 temp
        OAuthTokenSecret = OAuthTokenSecret, //3
        OAuthToken = OAuthToken //3
    };

    var etsyService = new EtsyService(auth);
```

If you are not requesting scoped API services (basically priveledged account info/side-effecting operations), you can make an "unauthenticated" request using just your consumer key:

```csharp
    var unAuth = new EtsyApiAuth
    {
        ConsumerKey = ConsumerKey,
    };

    var etsyServiceNoOauth = new EtsyService(unAuth);
```

Follow detailed instructions below.

### Getting your Etsy Tokens

There are three steps
1. Login to etsy developer account and create an API key pair.
2. Use this pair to obtain temp credentials and verifier.
3. Use those credentials to obtain permanent Oauth credentials

**Step 1:** Get temp access credentials, using your developer account key pair

```
    var auth = new EtsyApiAuth
    {
        ConsumerKey = ConsumerKey,
        ConsumerSecret = ConsumerSecret
    };

    var etsyService = new EtsyService(auth);

    //be sure to save this response
    var loginResponse = etsyService.Login();

```

To complete this step, save return values, and navigate to login url, authorize account, and save verifier token to auth, onto step 2.

**Step 2:** Get permanent OAuth Key

```
    var auth = new EtsyApiAuth
    {
        ConsumerKey = ConsumerKey, 
        ConsumerSecret = ConsumerSecret, 
        TokenSecret = TokenSecret, //from previous step
        VerifierSecret = VerifierSecret, //from previous step
        Token = Token, //from previous step
    };

    var etsyService = new EtsyService(auth);

    //get permanent oauth tokens, save these
    var permOauthTokens= etsyService.GetOauthTokens();
```

**Step 3:** Make authenticated requests

```
    var auth = new EtsyApiAuth
    {
        ConsumerKey = ConsumerKey, 
        ConsumerSecret = ConsumerSecret, 
        TokenSecret = TokenSecret,
        VerifierSecret = VerifierSecret, 
        Token = Token, 
        OAuthTokenSecret = OAuthTokenSecret, //from previous step
        OAuthToken = OAuthToken //from previous step
    };

    var etsyService = new EtsyService(auth);

    //perform authed action
    var result = etsyService.DoXyz();
```