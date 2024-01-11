using System.Net;

namespace CsvReader.API.Middlewares
{
    public class IpRestrictionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _allowedIP = "::1";

        public IpRestrictionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string ipAddress = context.Connection.RemoteIpAddress?.ToString();
            if (ipAddress != _allowedIP)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                await context.Response.WriteAsync("IP Forbidden");
                return;
            }

            await _next(context);
        }
    }
}
