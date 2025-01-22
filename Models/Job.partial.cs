using Sample.Code;
namespace Sample.Models;

partial class Job
{
    public int CostNum { get => Cost.Hour * PService.HOUR + Cost.Minute; }
}


