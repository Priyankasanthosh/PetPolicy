using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace FunctionApp;

public class HttpExample
{
    private readonly ILogger<HttpExample> _logger;

    public HttpExample(ILogger<HttpExample> logger)
    {
        _logger = logger;
    }

    [Function("HttpExample")]
    public HttpResponseData Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData request)
    {
        _logger.LogInformation("HTTP trigger function processed a request.");

        var name = System.Web.HttpUtility.ParseQueryString(request.Url.Query).Get("name");
        var response = request.CreateResponse(System.Net.HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        response.WriteString(string.IsNullOrWhiteSpace(name)
            ? "This HTTP triggered function executed successfully. Pass a name in the query string for a personalized response."
            : $"Hello, {name}. This HTTP triggered function executed successfully.");

        return response;
    }
}
