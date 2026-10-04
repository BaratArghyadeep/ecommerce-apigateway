using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(
        builder.Configuration
            .GetSection("ReverseProxy"));
builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource =>
        resource.AddService("ApiGateway"))
    .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddOtlpExporter(options =>
            {
                options.Endpoint =
                    new Uri("http://jaeger:4317");
            });
    });

var app = builder.Build();

app.MapReverseProxy();

app.Run();