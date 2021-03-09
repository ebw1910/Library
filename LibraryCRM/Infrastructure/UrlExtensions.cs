using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace LibraryCRM.Infrastructure
{
    public static class UrlExtensions
    {

        public static string GetEncodedPathAndQuery(this HttpRequest request) =>
            request.QueryString.HasValue
                ? $"{request.Path}{request.QueryString}"
                : request.Path.ToString();
    }
}
