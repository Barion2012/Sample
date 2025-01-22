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
        job.User = TEST_USER;
        job.Completed = DateTime.Now;
        await _db.Jobs.AddAsync(job!);
        await _db.SaveChangesAsync();
    }

    public async Task<JobData> GetJobData()
    {
        var data = new JobData();
        data.Jobs = await _db.Jobs.Include(f => f.UserNavigation)
            .OrderByDescending(f => f.RowId)
            .Select(f=>new JobItem { 
                Completed=f.Completed.ToString("dd.MM.yyyy"), 
                Description=f.Description, 
                Cost=f.Cost.ToString("hh:mm"), 
                UserName=f.UserNavigation.Name,
                CostNum = f.CostNum
            })
            .ToListAsync();

        data.TotalMinutes = data.Jobs.Sum(f => f.CostNum);
        return data;
    }
}
