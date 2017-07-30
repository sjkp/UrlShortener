#load "..\models.csx"

using System.Net;
using System.Web;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, ShortKey shortKeys, string shortCode, ICollector<Analytic> analytics, TraceWriter log)
{
    log.Info("C# HTTP trigger function processed a request.");
    analytics.Add(new Analytic()
    {
        PartitionKey = shortCode,
        RowKey = Guid.NewGuid().ToString(),
        Url = shortKeys?.Url,
        UserAgent = string.Join(",", req.Headers.GetValues("User-Agent")),
        ClientIp = GetUserIp(req)
    });
    if (shortKeys == null)
    {
        var res404 = req.CreateResponse(HttpStatusCode.NotFound);
        return res404;
    }

    log.Info(string.Join(",", req.Headers.GetValues("X-Forwarded-For")));
    var res = req.CreateResponse(HttpStatusCode.Redirect);
    res.Headers.Add("Location", shortKeys.Url);
    return res;
}

/// <summary>
/// This method is required for getting the correct IP as the azure function proxy, is handling all request and thus hiding the real client ip. 
/// Unfortunately the X-Forwarded-For header contains extra ip adress and ip adresses with ports which we dont want to save.
/// </summary>
/// <param name="req"></param>
/// <returns></returns>
public static string GetUserIp(HttpRequestMessage req)
{
    var ip = string.Join(",", req.Headers.GetValues("X-Forwarded-For")).Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Where(s => !s.Contains(":") && s.Contains(".")).FirstOrDefault();
    return ip;
}