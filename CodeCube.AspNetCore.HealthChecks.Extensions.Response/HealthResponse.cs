using System.Collections.Generic;

namespace CodeCube.AspNetCore.HealthChecks.Extensions.Response
{
    internal sealed class HealthResponse
    {
        public string Status { get; set; }
        public string Version { get; set; }

        public IEnumerable<HealthResponseEntry> Entries { get; set; }
    }
}