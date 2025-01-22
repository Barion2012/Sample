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
                try
                {
                    await service.AddNewJob(job);
                }
                catch(Exception)
                {
                    if (string.IsNullOrWhiteSpace(job.Description))
                    {
                        throw new Exception("Заполните пожалуйста описание задачи");
                    }
                    else
                    {
                        throw new Exception("Пожалуйста укажите время в формате ЧЧ:ММ");
                    }
                }
                return await service.GetJobData();
            }
        });
    }
}
