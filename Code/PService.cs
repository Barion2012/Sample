using Sample.Models;
using Microsoft.EntityFrameworkCore;
namespace Sample.Code;


public class PService : IService
{
    const long TEST_USER = 1;
    public const int HOUR = 60;

    private readonly PContext _db;
    public PService(PContext db)
    {
        _db = db;
    }

    public async Task AddNewJob(Job job)
    {
        if (string.IsNullOrWhiteSpace(job.Description)) {
            throw new Exception("Заполните пожалуйста описание задачи");
        }
        else if (job.Cost.Hour==0 && job.Cost.Minute==0)
        {
            throw new Exception("Пожалуйста укажите время в формате ЧЧ:ММ, потраченное время должно быть больше нуля");
        }

        job.User = TEST_USER;
        job.Completed = DateTime.Now;

        try
        {
            await _db.Jobs.AddAsync(job!);
            await _db.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<JobData> GetJobData()
    {
        var data = new JobData();
        data.Jobs = await _db.Jobs.Include(f => f.UserNavigation)
            .OrderByDescending(f => f.RowId)
            .Select(f=>new JobItem { 
                Completed=f.Completed.ToString("dd.MM.yyyy"), 
                Description=f.Description, 
                Cost=f.Cost.ToString("HH:mm"), 
                UserName=f.UserNavigation.Name,
                CostNum = f.CostNum
            })
            .ToListAsync();

        data.TotalMinutes = data.Jobs.Sum(f => f.CostNum);
        return data;
    }
}
