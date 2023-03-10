//using Polly.Extensions.Http;
//using Polly.Timeout;
//using Polly;  

namespace OstreCWEB.Services.Api
{
    public static class TestRestApiConfiguration
    {
        public const string TestRestApiClientName = "FithEditionApiClient";
        public const string TestRestApiClientNameAuthorized = "AuthorizedTestApiClient";

        //public static readonly IAsyncPolicy<HttpResponseMessage> RetryPolicy = HttpPolicyExtensions
        //    .HandleTransientHttpError()
        //    .Or<TimeoutRejectedException>()
        //    .OrResult(result => result.StatusCode != HttpStatusCode.OK)
        //    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromMilliseconds(100 * retryAttempt));
    }
}
