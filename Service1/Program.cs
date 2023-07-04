using Common;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var serviceNameVar = ParametersForLogger.GetServiceName(System.Reflection.Assembly.GetEntryAssembly());
var serviceVersionVar = ParametersForLogger.GetServiceVersion(System.Reflection.Assembly.GetEntryAssembly());

builder.Services.AddOpenTelemetry()
    .WithTracing(builder => builder
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddSqlClientInstrumentation()
        .AddConsoleExporter()
        .AddJaegerExporter(config =>
        {
            config.AgentHost = "jaeger";
            config.AgentPort = 6831;
        })
        .AddSource(serviceNameVar)
            .SetResourceBuilder(
                ResourceBuilder.CreateDefault()
                    .AddService(serviceName: serviceNameVar, serviceVersion: serviceVersionVar)
                    )
            );

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();



app.UseAuthorization();

app.MapControllers();

app.Run();
