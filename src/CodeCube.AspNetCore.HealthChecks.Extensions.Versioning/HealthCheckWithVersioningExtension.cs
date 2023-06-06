using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using CodeCube.AspNetCore.HealthChecks.Extensions.Response;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

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
        public static IApplicationBuilder UseHealthChecksWithVersioning(this IApplicationBuilder app, string path, bool responseAsJson = false)
        {
            return app.UseHealthChecks(path, new HealthCheckOptions { ResponseWriter = CreateResponse(responseAsJson: responseAsJson) });
        }

        /// <summary>
        /// Adds a middleware that provides healthcheck status.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="path">The path you want the middleware to respond to.</param>
        /// <param name="assemblyName">The name of the assembly to be used to get the version from. If empty the executing assembly will be used.</param>
        /// <param name="responseAsJson">Should the response be outputted as JSON?</param>
        /// <returns></returns>
        public static IApplicationBuilder UseHealthChecksWithVersioning(this IApplicationBuilder app, string path, string assemblyName, bool responseAsJson = false)
        {
            return app.UseHealthChecks(path, new HealthCheckOptions { ResponseWriter = CreateResponse(assemblyName, responseAsJson) });
        }

        /// <summary>
        /// Adds a middleware that provides healthcheck status, default response is as JSON.
        /// The calling assembly is used to get the versionnumber.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="path">The path you want the middleware to respond to.</param>
        /// <param name="responseAsJson">Should the response be outputted as JSON?</param>
        /// <returns></returns>
        public static IApplicationBuilder UseHealthChecksWithVersioning(this IApplicationBuilder app, bool responseAsJson = true)
        {
            var callingAssemblyName = Assembly.GetCallingAssembly().GetName().Name;

            return app.UseHealthChecks(callingAssemblyName, new HealthCheckOptions { ResponseWriter = CreateResponse(responseAsJson: responseAsJson) });
        }


        #region privates
        private static Func<HttpContext, HealthReport, Task> CreateResponse(string assemblyName = null, bool responseAsJson = false)
        {
            var theAssembly = !string.IsNullOrWhiteSpace(assemblyName) ? Assembly.Load(assemblyName) : Assembly.GetExecutingAssembly();
            var version = FileVersionInfo.GetVersionInfo(theAssembly.Location).FileVersion;

            if (responseAsJson)
                return ResponseWriters.AsJson(version);

            return ResponseWriters.AsText(version);
        }
        #endregion
    }
}
