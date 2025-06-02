# CodeCube.AspNetCore.HealthChecks.Extensions
Small library with extensions on AspNetCore.Diagnostics.HealthChecks. The following packages are available;

**CodeCube.AspNetCore.HealthChecks.Extensions.Response**

![Nuget](https://img.shields.io/nuget/dt/CodeCube.AspNetCore.HealthChecks.Extensions.Response?style=for-the-badge)
![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/CodeCube.AspNetCore.HealthChecks.Extensions.Response?style=for-the-badge)
![GitHub](https://img.shields.io/github/license/roblohmann/CodeCube.AspNetCore.HealthChecks.Extensions?style=for-the-badge)

**CodeCube.AspNetCore.HealthChecks.Extensions.Versioning**

![Nuget](https://img.shields.io/nuget/dt/CodeCube.AspNetCore.HealthChecks.Extensions.Versioning?style=for-the-badge)
![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/CodeCube.AspNetCore.HealthChecks.Extensions.Versioning?style=for-the-badge)
![GitHub](https://img.shields.io/github/license/roblohmann/CodeCube.AspNetCore.HealthChecks.Extensions?style=for-the-badge)

**CodeCube.AspNetCore.HealthChecks.Extensions.Uris.Versioning**

![Nuget](https://img.shields.io/nuget/dt/CodeCube.AspNetCore.HealthChecks.Extensions.Uris.Versioning?style=for-the-badge)
![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/CodeCube.AspNetCore.HealthChecks.Extensions.Uris.Versioning?style=for-the-badge)
![GitHub](https://img.shields.io/github/license/roblohmann/CodeCube.AspNetCore.HealthChecks.Extensions?style=for-the-badge)

## Implementation .NET Core (>= 3.x)
The steps below describe how this package can be used after installing it.
Implementation should be made in the Startup.cs for your .NET Core project (>= 3.x).

### Configure Services
```` C#
public void ConfigureServices(IServiceCollection services)
{
    //Your other code
    
    services.AddHealthChecks();
}
````


#### Default implementation
``` C#
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    //Your other code

    app.UseHealthChecksWithVersioning("/healthz");
}
```

#### Provide assembly name to get version
``` C#
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    //Other code
    app.UseHealthChecksWithVersioning("/health", assemblyName: "My.Application.Namespace");
}
```

#### Return the output as JSON
``` C#
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    //Other code

    app.UseHealthChecksWithVersioning("/health", assemblyName: "My.Application.Namespace", responseAsJson: true);
}
```


### Grab healthcheck response from multiple dependant services
``` C#
public void ConfigureServices(IServiceCollection services)
{
    services.AddHealthChecks()
        .AddUrlGroupWithVersioning(options =>
        {
            options.AddUri(new Uri("https://www.my-web-site.com"), setup =>
            {
                setup.WithVersioningHeader();
            }, 
            "MyWebsite",
            HealthStatus.Degraded
            );

            options.AddUri(new Uri("https://www.my-second-website.com"), setup =>
            {
                setup.WithVersioningHeader();
            },
            "MySecondWebsite",
            HealthStatus.Degraded
            );

        });
}
```