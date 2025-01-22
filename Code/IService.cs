using Sample.Models;
namespace Sample.Code;


public interface IService
{
    public Task<JobData> GetJobData();
    public Task AddNewJob(Job job);
}
