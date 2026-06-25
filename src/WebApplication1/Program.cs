using CodeCube.AspNetCore.HealthChecks.Extensions.Uris.Versioning;
using CodeCube.AspNetCore.HealthChecks.Extensions.Uris.Versioning.DependencyInjection;
using CodeCube.AspNetCore.HealthChecks.Extensions.Versioning;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddHealthChecks().AddUrlGroupWithVersioning(options =>
{
    options.AddUri(new Uri("https://google.com"), setup =>
    {
        setup.WithVersioningHeader();
        setup.ExpectHttpCode(200);
    }
    );
}, 
name: "TestName",
tags: new[] { "ready" 
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHealthChecksWithVersioning("health");

app.Run();
