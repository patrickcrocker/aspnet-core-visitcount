# Cloud Foundry ASP.NET Core Visit Count

A simple ASP.NET Core web application using redis cache for the [ASP.NET Core buildpack][].

## Push to Cloud Foundry

```
cf push -b https://github.com/cloudfoundry-community/dotnet-core-buildpack.git
cf create-service p-redis shared-vm my-core-redis
cf bind-service aspnet-core-visitcount my-core-redis
cf restage aspnet-core-visitcount

Browse application url (my url look like http://aspnet-core-visitcount.local.pcfdev.io/)
```

## Run the app locally

1. Install ASP.NET Core by following the [Getting Started][] instructions
+ Clone this app
+ cd into the app directory and then `src\aspnet-core-visitcount`
+ Run `dotnet restore`
+ Run `dotnet run`
+ Access the running app in a browser at [http://localhost:5000](http://localhost:5000)

Thanks to [Patrick Crocker aspnet-core-helloworld][].

[Getting Started]: http://docs.asp.net/en/latest/getting-started/index.html
[ASP.NET Core buildpack]: https://github.com/cloudfoundry-community/asp.net5-buildpack
[Patrick Crocker aspnet-core-helloworld]: https://github.com/patrickcrocker/aspnet-core-helloworld