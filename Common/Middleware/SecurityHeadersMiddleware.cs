using Microsoft.Extensions.Primitives;

namespace Tokenomics.Common.Middleware
{
    public class SecurityHeadersMiddleware
    {
        private readonly RequestDelegate _next;

        private static readonly StringValues ReferrerPolicyValue = new StringValues("strict-origin-when-cross-origin");
        private static readonly StringValues XContentTypeOptionsValue = new StringValues("nosniff");
        private static readonly StringValues XFrameOptionsValue = new StringValues("DENY");
        private static readonly StringValues XPermittedCrossDomainPoliciesValue = new StringValues("none");
        private static readonly StringValues XXSSProtectionValue = new StringValues("1; mode=block");
        private static readonly StringValues ExpectCTValue = new StringValues("max-age=0, enforce, report-uri=''");
        private static readonly StringValues FeaturePolicyValue = new StringValues(
            "accelerometer 'none'; ambient-light-sensor 'none'; autoplay 'none'; battery 'none'; camera 'none'; " +
            "display-capture 'none'; document-domain 'none'; encrypted-media 'none'; execution-while-not-rendered 'none'; " +
            "execution-while-out-of-viewport 'none'; gyroscope 'none'; magnetometer 'none'; microphone 'none'; midi 'none'; " +
            "navigation-override 'none'; payment 'none'; picture-in-picture 'none'; publickey-credentials-get 'none'; " +
            "sync-xhr 'none'; usb 'none'; wake-lock 'none'; xr-spatial-tracking 'none';");

        private static readonly StringValues ContentSecurityPolicyValue = new StringValues(
            "base-uri 'none'; block-all-mixed-content; child-src 'none'; connect-src 'none'; default-src 'none'; font-src 'none'; " +
            "form-action 'none'; frame-ancestors 'none'; frame-src 'none'; img-src 'none'; manifest-src 'none'; media-src 'none'; " +
            "object-src 'none'; sandbox; script-src 'none'; style-src 'none'; upgrade-insecure-requests; worker-src 'none';");

        public SecurityHeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            var headers = context.Response.Headers;

            if (!headers.ContainsKey("referrer-policy"))
                headers.Append("referrer-policy", ReferrerPolicyValue);

            //if (!headers.ContainsKey("x-content-type-options"))
            //    headers.Append("x-content-type-options", XContentTypeOptionsValue);

            //if (!headers.ContainsKey("x-frame-options"))
            //    headers.Append("x-frame-options", XFrameOptionsValue);

            //if (!headers.ContainsKey("X-Permitted-Cross-Domain-Policies"))
            //    headers.Append("X-Permitted-Cross-Domain-Policies", XPermittedCrossDomainPoliciesValue);

            //if (!headers.ContainsKey("x-xss-protection"))
            //    headers.Append("x-xss-protection", XXSSProtectionValue);

            //if (!headers.ContainsKey("Expect-CT"))
            //    headers.Append("Expect-CT", ExpectCTValue);

            //if (!headers.ContainsKey("Feature-Policy"))
            //    headers.Append("Feature-Policy", FeaturePolicyValue);

            //if (!headers.ContainsKey("Content-Security-Policy"))
            //    headers.Append("Content-Security-Policy", ContentSecurityPolicyValue);

            return _next(context);
        }
    }
}
