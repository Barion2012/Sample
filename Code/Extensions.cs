using Microsoft.AspNetCore.Mvc;
using Sample.Models;
namespace Sample.Code;

public static class Extensions
{
    public static void MapApi(this WebApplication app)
    {
        var api = app.MapGroup("/job");

        api.MapGet("/list", async () =>
        {
            await using (var scope = app.Services.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IService>();
                return await service.GetJobData();
            }
        });

        api.MapPost("/", async ([FromBody] Job job) =>
        {
            await using (var scope = app.Services.CreateAsyncScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IService>();

                await service.AddNewJob(job);
                return await service.GetJobData();
            }
        });
    }
}
