using System;
using System.Threading.Tasks;
using System.Collections.Generic;

// --- 1. MOCK NAMESPACES (Pure C# - No Packages Required) ---

namespace GordonApi.Security
{
    public static class IntegrityGuard
    {
        public static void VerifyIntegrity()
        {
            Console.WriteLine("🔒 [Security] Integrity Check Passed");
        }
    }
}

namespace GordonApi.Data
{
    // Mock DbContext: Replaces EF Core with a simple class
    public class SupportDbContext
    {
        public List<object> Items { get; set; } = new List<object>();
        
        public int SaveChanges()
        {
            Console.WriteLine("💾 [DB] Changes saved (Mocked)");
            return 1;
        }
    }
}

namespace GordonApi.Services
{
    // Mock Services: Empty classes to satisfy dependencies
    public class AiSentimentService { }
    public class AutoResolutionService { }
    public class ReportGenerator { }
}

// --- 2. MAIN PROGRAM ---

public class Program
{
    public static async Task Main()
    {
        Console.WriteLine("🚀 Starting Application (No Packages Mode)...\n");

        // A. Run Security Check
        GordonApi.Security.IntegrityGuard.VerifyIntegrity();

        // B. Initialize "Database" (Mocked)
        // In a real app, this would be: services.AddDbContext...
        var db = new GordonApi.Data.SupportDbContext();
        Console.WriteLine("🗄️  [DB] Context initialized (In-Memory Mock)");

        // C. Initialize Services (Manual Dependency Injection)
        // In a real app, this would be: services.AddScoped...
        var sentimentService = new GordonApi.Services.AiSentimentService();
        var resolutionService = new GordonApi.Services.AutoResolutionService();
        var reportGenerator = new GordonApi.Services.ReportGenerator();
        
        Console.WriteLine("⚙️  [Services] All services registered manually.\n");

        // D. Simulate GoAlert Heartbeat
        var goAlertUrl = "http://goalert:8080/integrations/generic-webhook/REPLACE_ME";
        Console.WriteLine($"📡 Configured GoAlert URL: {goAlertUrl}");
        Console.WriteLine("⏱️  Starting Heartbeat Loop (3 cycles)...\n");

        int heartbeatsToSend = 3;
        int count = 0;

        while (count < heartbeatsToSend)
        {
            try
            {
                // NOTE: Real HTTP calls (HttpClient) often fail in sandboxes without setup.
                // We simulate the logic flow here.
                Console.WriteLine($"✅ [GoAlert] Heartbeat #{count + 1} sent at {DateTime.UtcNow:HH:mm:ss}");
                
                // Simulate DB interaction
                db.Items.Add(new { Status = "Up", Time = DateTime.UtcNow });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ [GoAlert] Error: {ex.Message}");
            }

            count++;
            await Task.Delay(500); // Wait 0.5 seconds
        }

        db.SaveChanges();
        Console.WriteLine("\n🏁 Application Finished Successfully.");
    }
}   