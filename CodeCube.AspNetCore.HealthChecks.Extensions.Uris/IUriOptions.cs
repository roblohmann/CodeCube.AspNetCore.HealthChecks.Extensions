using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CodeCube.AspNetCore.HealthChecks.Extensions.Uris
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
    }
}
