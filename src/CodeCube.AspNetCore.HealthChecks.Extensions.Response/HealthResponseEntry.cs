using System.Collections.Generic;

namespace CodeCube.AspNetCore.HealthChecks.Extensions.Response
{
    internal sealed class HealthResponseEntry
    {
        public string Key { get; set; }
        public object Value { get; set; }
        public string Error { get; set; }
        public string Description { get; set; }
        public KeyValuePair<string, object>[] Data { get; set; }
    }
}
