using GordonApi.Security;
using GordonApi.Services;
using GordonApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// SECURITY: Run integrity check (skip in development for ease)
if (!builder.Environment.IsDevelopment())
{
    IntegrityGuard.VerifyIntegrity();
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

// Database
builder.Services.AddDbContext<SupportDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Services
builder.Services.AddScoped<AiSentimentService>();
builder.Services.AddScoped<AutoResolutionService>();
builder.Services.AddScoped<ReportGenerator>();

var app = builder.Build();

// GoAlert Heartbeat - sends "up" every 60s
var goAlertUrl = Environment.GetEnvironmentVariable("GOALERT_WEBHOOK_URL")
                 ?? "http://goalert:8080/integrations/generic-webhook/REPLACE_ME";

var cts = new CancellationTokenSource();
_ = Task.Run(async () =>
{
    using var client = new HttpClient();
    while (!cts.IsCancellationRequested)
    {
        try
        {
            await client.PostAsync(goAlertUrl,
                new StringContent("{\"status\":\"up\"}"));
            Console.WriteLine($"[GoAlert] Heartbeat: {DateTime.UtcNow:HH:mm:ss}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[GoAlert] Failed: {ex.Message}");
        }
        await Task.Delay(60000);
    }
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();   