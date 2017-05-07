public static class ShortCode
{
    static Random r = new System.Random(Guid.NewGuid().GetHashCode());

    private static string UrlSafeString(string s)
    {
        return s.Replace('+', '-').Replace('/', '_').Replace("=", "");
    }

    private static string ToBinary(int x)
    {
        return System.Convert.ToString(x, 2);
    }

    private static int FromBinary(string x)
    {
        return System.Convert.ToInt32(x, 2);
    }

    private static byte[] RandomString(int len)
    {
        var a = new byte[len];
        r.NextBytes(a);
        return a;
    }

    private static string GetDatePart(DateTime d)
    {
        var b = ToBinary(Convert.ToInt32(d.ToString("yy"))).PadLeft(7, '0') + ToBinary(d.Month).PadLeft(4, '0') + ToBinary(d.Day).PadLeft(5, '0');
        var arr = new byte[2] {
                Convert.ToByte(FromBinary(b.Substring(0, 8))),
                Convert.ToByte(FromBinary(b.Substring(8))) };
        return Convert.ToBase64String(arr);
    }

    public static string NewShortCodeByDate(DateTime d)
    {
        return UrlSafeString(GetDatePart(d) + Convert.ToBase64String(RandomString(5)));
    }
}