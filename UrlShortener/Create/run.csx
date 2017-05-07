#load "..\models.csx"
#load "..\shortcode.csx"

using System.Net;

public static HttpResponseMessage Run(HttpRequestMessage req, ICollector<ShortKey> shortKeys, TraceWriter log)
{
    log.Info("C# HTTP trigger function processed a request.");

    // parse query parameter
    var qs = req.GetQueryNameValuePairs();
    string url = qs.FirstOrDefault(q => string.Compare(q.Key, "Url", true) == 0).Value;

    var id = qs.FirstOrDefault(q => string.Compare(q.Key, "ShortCode", true) == 0).Value ?? ShortCode.NewShortCodeByDate(DateTime.UtcNow);
    if (url != null)
    {
        shortKeys.Add(new ShortKey()
        {
            PartitionKey = id,
            RowKey = "A",
            Url = url
        });
    }

    return url == null
        ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a Url on the query string")
        : req.CreateResponse(HttpStatusCode.OK, "Your short key is: " + id);
}
