namespace TournamentCardMaker;

using System.Net;

public class HttpClientFactory
{
    public static HttpClient CreateClient()
    {
        var cookieContainer = new CookieContainer();
        var cookieCollection = new CookieCollection()
    {
        new Cookie("_ga", "GA1.1.1747355703.1753776543"),
        new Cookie("_ga_5D55QK6VCZst", "GS2.1.s1753776543$o1$g0$t1753776543$j60$l0$h0"),
        new Cookie("_hjSession_123538", "eyJpZCI6ImE4YmFjZWY1LTM5ZGUtNGUyNC1iNzc4LWJlMjI3NzY0Mzg3NyIsImMiOjE3NTM3NzY1NDM2NjYsInMiOjAsInIiOjAsInNiIjowLCJzciI6MCwic2UiOjAsImZzIjoxLCJzcCI6MH0="),
        new Cookie("_hjSessionUser_123538", "eyJpZCI6ImE3NTE1ZmMzLTY0ZGQtNTNhNS05ODgyLWU1Y2E4MGMzNGEzNSIsImNyZWF0ZWQiOjE3NTM3NzY1NDM2NjUsImV4aXN0aW5nIjpmYWxzZX0="),
        new Cookie("ASP.NET_SessionId", "drdm4jhycgfppqmgqgvagbaj"),
        new Cookie("bcookie", "\"v=2&bde895d4-2ba5-45dc-8af3-44ddebfd33f7\""),
        new Cookie("euconsent-v2", "CQVQ2cAQVQ2cADsACAENB1FgAAAAAAAAAAhoAAAAAAAA.YAAAAAAAAAAA"),
        new Cookie("li_gc", "MTswOzE3NTM3NzY1NTc7MjswMjHK0GxGoYh8iTVxsUwqUS+e12EvU80dyxsjqPAUhb+mHA=="),
        new Cookie("lidc", "\"b=VGST06:s=V:r=V:a=V:p=V:g=3356:u=1:x=1:i=1753776557:t=1753862957:v=2:sig=AQHFdOQ8fcQWtOO3eXL71Jui8YD9vJD8\""),
        new Cookie("st", "l=1043&exp=46232.4229490856&c=1&cp=31"),
    };
        cookieContainer.Add(new Uri("https://mijnknltb.toernooi.nl"), cookieCollection);
        var handler = new HttpClientHandler
        {
            CookieContainer = cookieContainer
        };

        return new HttpClient(handler);
    }
}