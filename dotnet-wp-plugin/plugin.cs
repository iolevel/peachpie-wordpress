using System;
using Pchp.Core;

public static class MyWpPlugin
{
    public static void Register(Context ctx)
    {
        ctx.Call("add_filter", "the_content", new Func<string, string>(text =>
        {
            text = text
                .Replace("WordPress", "WordPress running on .NET")
                .Replace("Welcome", "Greetings");

            return text;
        }));
    }
}
