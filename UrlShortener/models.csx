

public class ShortKey
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public string Url { get; set; }
}

public class Analytic
{
    //The shortkey
    public string PartitionKey { get; set; }

    // Unique Id
    public string RowKey { get; set; }

    public string Url { get; set; }

    public string UserAgent { get; set; }

    public string ClientIp { get; set; }
}