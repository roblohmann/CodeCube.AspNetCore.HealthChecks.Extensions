# CodeCube.AspNetCore.HealthChecks.Extensions
Small library with extensions on AspNetCore.Diagnostics.HealthChecks

![Nuget](https://img.shields.io/nuget/dt/CodeCube.AspNetCore.HealthChecks.Extensions.Response?style=for-the-badge)
![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/CodeCube.AspNetCore.HealthChecks.Extensions.Response?style=for-the-badge)
![GitHub](https://img.shields.io/github/license/roblohmann/CodeCube.AspNetCore.HealthChecks.Extensions.Response?style=for-the-badge)

![Nuget](https://img.shields.io/nuget/dt/CodeCube.AspNetCore.HealthChecks.Extensions.Versioning?style=for-the-badge)
![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/CodeCube.AspNetCore.HealthChecks.Extensions.Versioning?style=for-the-badge)
![GitHub](https://img.shields.io/github/license/roblohmann/CodeCube.AspNetCore.HealthChecks.Extensions.Versioning?style=for-the-badge)

![Nuget](https://img.shields.io/nuget/dt/CodeCube.AspNetCore.HealthChecks.Extensions.Uris.Versioning?style=for-the-badge)
![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/CodeCube.AspNetCore.HealthChecks.Extensions.Uris.Versioning?style=for-the-badge)
![GitHub](https://img.shields.io/github/license/roblohmann/CodeCube.AspNetCore.HealthChecks.Extensions.Uris.Versioning?style=for-the-badge)

## Implementation .NET Core (>= 2.2)
The steps below describe how this package can be used after installing it.
Implementing it should be done in the Startup.cs for your .NET Core project (>= 2.2).

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

    app.UseHealthChecksWithVersioning("/health");
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
