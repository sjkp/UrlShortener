#load "..\models.csx"

using System.Net;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, ShortKey shortKeys, string shortCode, ICollector<Analytic> analytics, TraceWriter log)
{
    log.Info("C# HTTP trigger function processed a request.");
    analytics.Add(new Analytic()
    {
        PartitionKey = shortCode,
        RowKey = Guid.NewGuid().ToString(),
        Url = shortKeys.Url,
        UserAgent = string.Join(",", req.Headers.GetValues("User-Agent")),
        ClientIp = string.Join(",", req.Headers.GetValues("X-Forwarded-For"))
    });
    var res = req.CreateResponse(HttpStatusCode.Redirect);
    res.Headers.Add("Location", shortKeys.Url);
    return res;
}