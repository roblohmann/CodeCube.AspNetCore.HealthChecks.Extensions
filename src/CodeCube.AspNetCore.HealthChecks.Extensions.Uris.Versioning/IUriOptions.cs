using System;
using System.Net.Http;

namespace CodeCube.AspNetCore.HealthChecks.Extensions.Uris.Versioning
{
    public interface IUriOptions
    {
        IUriOptions UseGet();
        IUriOptions UsePost();
        IUriOptions UseHttpMethod(HttpMethod methodToUse);
        IUriOptions UseTimeout(TimeSpan timeout);
        IUriOptions ExpectHttpCode(int codeToExpect);
        IUriOptions ExpectHttpCodes(int minCodeToExpect, int maxCodeToExpect);
        IUriOptions AddCustomHeader(string name, string value);
        IUriOptions WithVersioningHeader(string headername = "x-application-version");
    }
}