using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;

namespace CodeCube.AspNetCore.HealthChecks.Extensions.Versioning
{
    public static class HealthCheckWithVersioningExtension
    {
        /// <summary>
        /// Adds a middleware that provides healthcheck status.
        /// The executing assembly is used to get the versionnumber.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="path">The path you want the middleware to respond to.</param>
        /// <param name="responseAsJson">Should the response be outputted as JSON?</param>
        /// <returns></returns>
        public static IApplicationBuilder UseHealthChecksWithVersioning2(this IApplicationBuilder app, string path, bool responseAsJson = false)
        {
            return app.UseHealthChecks(path, new HealthCheckOptions { ResponseWriter = ResponseWriter(responseAsJson: responseAsJson) });
        }

        /// <summary>
        /// Adds a middleware that provides healthcheck status.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="path">The path you want the middleware to respond to.</param>
        /// <param name="assemblyName">The name of the assembly to be used to get the version from. If empty the executing assembly will be used.</param>
        /// <param name="responseAsJson">Should the response be outputted as JSON?</param>
        /// <returns></returns>
        public static IApplicationBuilder UseHealthChecksWithVersioning2(this IApplicationBuilder app, string path, string assemblyName, bool responseAsJson = false)
        {
            return app.UseHealthChecks(path, new HealthCheckOptions { ResponseWriter = ResponseWriter(assemblyName, responseAsJson) });
        }


        #region privates
        private static Func<HttpContext, HealthReport, Task> ResponseWriter(string assemblyName = null, bool responseAsJson = false)
        {
            return async (httpContext, healthReport) =>
            {
                var theAssembly = !string.IsNullOrWhiteSpace(assemblyName) ? Assembly.Load(assemblyName) : Assembly.GetExecutingAssembly();
                string version = FileVersionInfo.GetVersionInfo(theAssembly.Location).FileVersion;

                string result;
                if (responseAsJson)
                {
                    httpContext.Response.ContentType = "application/json";
                    result = CreateResponseAsJSON(healthReport, version);
                }
                else
                {
                    result = CreateResponseAsText(healthReport, version);
                }

                httpContext.Response.Headers.Add("x-deployment-version",version);

                await httpContext.Response.WriteAsync(result).ConfigureAwait(false);
            };
        }

        private static string CreateResponseAsJSON(HealthReport healthReport, string version)
        {
            return JsonConvert.SerializeObject(
                    new
                    {
                        Status = healthReport.Status.ToString(),
                        applicationVersion = version,
                        Entries = healthReport.Entries.Select(e => new { key = e.Key, value = Enum.GetName(typeof(HealthStatus), e.Value.Status) })
                    });
        }

        private static string CreateResponseAsText(HealthReport healthReport, string version)
        {
            var result = $"{healthReport.Status} | Version: {version}";

            if (healthReport.Entries != null && healthReport.Entries.Count > 0)
            {
                result = $"{result} | Entries: ";

                foreach (var entry in healthReport.Entries)
                {
                    result = $"{result} {entry.Key}:{entry.Value.Status}";
                }
            }

            return result;
        }
        #endregion
    }
}
