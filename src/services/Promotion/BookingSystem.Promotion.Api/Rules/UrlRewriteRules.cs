using Microsoft.AspNetCore.Rewrite;
using System.Text.RegularExpressions;

namespace BookingSystem.Promotion.Api.Rules
{
    class UrlRewriteRules : IRule
    {
        public void ApplyRule(RewriteContext context)
        {
            SetDefaultUrlVersioningBase(context);
        }


        private static void SetDefaultUrlVersioningBase(RewriteContext context)
        {
            var request = context.HttpContext.Request;
            var partern = @"\/(v\d+.?\d*)\/";
            Regex rg = new(partern);
            if (request.Path.Value != "/index.html" && !rg.IsMatch(request.Path.Value))
            {
                var originalPath = request.Path.Value;
                var newPath = originalPath.ToString().Replace("/api/", "/api/v1.0/");
                request.Path = newPath;
            }
        }
    }
}
