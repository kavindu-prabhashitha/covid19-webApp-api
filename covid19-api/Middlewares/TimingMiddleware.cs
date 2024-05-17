using covid19_api.Middlewares;
using Microsoft.AspNetCore.Server.HttpSys;

namespace covid19_api.Middlewares
{
    public class TimingMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        public TimingMiddleware(ILogger<TimingMiddleware> logger, RequestDelegate next) {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext ctx)
        {
            var start = DateTime.Now;
            await _next.Invoke(ctx); // Passing the context
            _logger.LogInformation(
                $"Timing : {ctx.Request.Path} : {DateTime.UtcNow - start} ");
        }
    }
}

public static class TimingExtentions
{
    public static IApplicationBuilder UseTiming(this IApplicationBuilder app)
    {
        return app.UseMiddleware<TimingMiddleware>();
    }

//    public static void AddTiming(this IServiceCollection svcs)
//    {
//        svcs.AddTransient<ITiming, SomeTimingImplementation>();
//    }
}

//To use this class in Programe.cs 
// app.UseMiddleware<TimingMiddleware>();