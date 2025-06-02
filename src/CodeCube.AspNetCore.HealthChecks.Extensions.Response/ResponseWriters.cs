using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CodeCube.AspNetCore.HealthChecks.Extensions.Response
{
    public static class ResponseWriters
    {
        public static Func<HttpContext, HealthReport, Task> AsJson(string version)
        {
            return async (httpContext, healthReport) =>
            {
                httpContext.Response.ContentType = "application/json";
                var result = CreateResponseAsJSON(healthReport, version);

                await httpContext.Response.WriteAsync(result).ConfigureAwait(false);
            };
        }

        public static Func<HttpContext, HealthReport, Task> AsText(string version)
        {
            return async (httpContext, healthReport) =>
            {
                httpContext.Response.ContentType = "text/html";
                var result = CreateResponseAsText(healthReport, version);

                await httpContext.Response.WriteAsync(result).ConfigureAwait(false);
            };
        }

        private static string CreateResponseAsJSON(HealthReport healthReport, string version)
        {
            var settings = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var objectToSerialize = new HealthResponse
            {
                Status = healthReport.Status.ToString(),
                Version = version,
                Entries = healthReport.Entries.Select(e => new HealthResponseEntry
                {
                    Key = e.Key,
                    Value = Enum.GetName(typeof(HealthStatus), e.Value.Status),
                    Error = e.Value.Exception?.GetBaseException().Message,
                    Description = e.Value.Description,
                    Data = e.Value.Data.Count > 0 ? e.Value.Data.ToArray() : null
                })
            };


            return JsonSerializer.Serialize(objectToSerialize, settings);
        }

        private static string CreateResponseAsText(HealthReport healthReport, string version)
        {
            var result = $"{healthReport.Status} | Version: {version}";

            if (healthReport.Entries.Count <= 0) return result;

            result = $"{result} | Entries: ";
            foreach (var entry in healthReport.Entries)
            {
                result = $"{result} {entry.Key}:{entry.Value.Status}";
            }

            return result;
        }
    }
}
