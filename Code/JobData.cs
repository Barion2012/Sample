using Sample.Models;
namespace Sample.Code;


public class JobItem
{
    public string Description { get; set; } = null!;
    public string Completed { get; set; } = null!;
    public string Cost { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public int CostNum { get; set; }

}

public class JobSummary
{
    public int TotalMinutes { get; set; }
    public int Hours { get => TotalMinutes / PService.HOUR; }
    public int Minutes { get => TotalMinutes % PService.HOUR; }
}


public class JobData : JobSummary
{
    public IList<JobItem> Jobs { get; set; } = new List<JobItem>();
}