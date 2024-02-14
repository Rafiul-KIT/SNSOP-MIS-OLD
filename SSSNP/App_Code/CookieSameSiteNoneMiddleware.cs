using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


public class CookieSameSiteNoneMiddleware : OwinMiddleware
{
    public CookieSameSiteNoneMiddleware(OwinMiddleware next) : base(next)
    {
        // do nothing
    }
    public override async Task Invoke(IOwinContext context)
    {
        context.Response.OnSendingHeaders(x =>
        {
            var scv = context.Response.Headers.FirstOrDefault(h => h.Key == "Set-Cookie");
            if (!scv.Equals(default(KeyValuePair<string, string[]>)))
            {
                var cookieValues = context.Response.Headers.GetValues("Set-Cookie");
                var updatedValues = cookieValues.Select(v => v + "; SameSite=none; Secure").ToArray();
                context.Response.Headers.SetValues("Set-Cookie", updatedValues);
            }
        }, null);

        await Next.Invoke(context);
    }
}
