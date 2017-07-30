using System;

public static async Task Run(TimerInfo myTimer, TraceWriter log)
{
    log.Info($"C# Timer trigger function executed at: {DateTime.Now}");
    HttpClient client = new HttpClient();
    client.DefaultRequestHeaders.UserAgent.ParseAdd("KeepAlive/1.0");
    var res = await client.GetAsync($"https://{Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME")}/api/redirect/keepalive");
}
