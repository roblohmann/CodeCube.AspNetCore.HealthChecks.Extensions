using System;
using System.Collections.Generic;
using System.Net.Http;

namespace CodeCube.AspNetCore.HealthChecks.Extensions.Uris.Versioning
{
    public class UriOptions : IUriOptions
    {
        public HttpMethod HttpMethod { get; private set; }

        public TimeSpan Timeout { get; private set; }

        public (int Min, int Max)? ExpectedHttpCodes { get; private set; }

        public Uri Uri { get; }

        private readonly List<(string Name, string Value)> _headers = new List<(string Name, string Value)>();
        internal IEnumerable<(string Name, string Value)> Headers => _headers;

        public bool GetVersioningHeader { get; private set; }
        public string VersioningHeader { get; private set; }

        public UriOptions(Uri uri)
        {
            Uri = uri;
        }
        public IUriOptions AddCustomHeader(string name, string value)
        {
            _headers.Add((name, value));
            return this;
        }

        IUriOptions IUriOptions.UseGet()
        {
            HttpMethod = HttpMethod.Get;
            return this;
        }
        IUriOptions IUriOptions.UsePost()
        {
            HttpMethod = HttpMethod.Post;
            return this;
        }
        IUriOptions IUriOptions.ExpectHttpCode(int codeToExpect)
        {
            ExpectedHttpCodes = (codeToExpect, codeToExpect);
            return this;
        }
        IUriOptions IUriOptions.ExpectHttpCodes(int minCodeToExpect, int maxCodeToExpect)
        {
            ExpectedHttpCodes = (minCodeToExpect, maxCodeToExpect);
            return this;
        }
        IUriOptions IUriOptions.UseHttpMethod(HttpMethod methodToUse)
        {
            HttpMethod = methodToUse;
            return this;
        }
        IUriOptions IUriOptions.UseTimeout(TimeSpan timeout)
        {
            Timeout = timeout;
            return this;
        }

        public IUriOptions WithVersioningHeader(string headername)
        {
            GetVersioningHeader = true;
            VersioningHeader = headername;
            return this;
        }
    }
}