# CodeCube.AspNetCore.HealthChecks.Extensions
Small library with extensions on AspNetCore.Diagnostics.HealthChecks

## Implementation .NET Core (>= 2.2)
The steps below describe how this package can be used after installing it.
Implementing it should be done in the Startup.cs for your .NET Core project (>= 2.2).

#### Default implementation
``` C#
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    //Other code

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