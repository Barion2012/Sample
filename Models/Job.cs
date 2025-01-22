namespace Sample.Models;

public partial class Job
{
    public long RowId { get; set; }

    public string Description { get; set; } = null!;

    public DateTime Completed { get; set; }

    public TimeOnly Cost { get; set; }

    public long User { get; set; }

    public virtual User UserNavigation { get; set; } = null!;
}


