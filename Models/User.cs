namespace Sample.Models;

public partial class User
{
    public long RowId { get; set; }

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string PhoneCode { get; set; } = null!;

    public DateTime? LastLogin { get; set; }

    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();

}
