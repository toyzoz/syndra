namespace Ordering.Domain.SeedWork;

public class AuditEntity
{
    public int CreateBy { get; set; }
    public DateTime CreateByAt { get; set; }

    public int ModifyBy { get; set; }
    public DateTime? ModifyAt { get; set; }
}