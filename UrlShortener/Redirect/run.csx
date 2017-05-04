#load "..\models.csx"

using System.Net;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, ShortKey shortKeys, string shortCode, TraceWriter log)
{
    log.Info("C# HTTP trigger function processed a request.");

    var res = req.CreateResponse(HttpStatusCode.Redirect);
    res.Headers.Add("Location", shortKeys.Url);
    return res;
}