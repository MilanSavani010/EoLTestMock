using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FileSystemBackend.Endpoints;

public static class EoLEndPoints
{
    public static void MapEoLTestEndpoints(this WebApplication app)
    {

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter() }
        };

        app.MapGet("/stream", async (HttpContext ctx,[FromServices] IEoLApplicationService eolService) =>
        {
            ctx.Response.ContentType = "text/event-stream";
            while(!ctx.RequestAborted.IsCancellationRequested)
            {
                if(eolService.CheckIfActive())
                {
                    var bmsData = await eolService.GetLatestBmsData();

                    if (bmsData != null)
                    {
                        var json = JsonSerializer.Serialize(bmsData, jsonOptions);
                        await ctx.Response.WriteAsync($"data: {json}\n\n");
                        await ctx.Response.Body.FlushAsync();
                    }
                    await Task.Delay(500);
                }
                
            }
        });

        app.MapPost("/start", ([FromServices] IEoLApplicationService eolService) =>
        {
            eolService.Start();
        });

        app.MapPost("/stop", (IEoLApplicationService eolService)=>
        {
            eolService.Stop();
        });

        app.MapPost("/init", (IEoLApplicationService eolService) =>
        {
            eolService.Initialize();
        });
    }

}