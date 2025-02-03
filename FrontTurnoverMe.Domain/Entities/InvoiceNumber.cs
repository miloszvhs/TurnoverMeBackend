using FrontTurnoverMe.Domain.Common;

namespace FrontTurnoverMe.Domain.Entities;

public class InvoiceNumber : BaseEntity
{
    public int Number { get; set; }
    public string Type { get; set; }
    public DateTime LastUpdate { get; set; }
    public RefreshEnum Refresh { get; set; }
}

public enum RefreshEnum
{
    Daily,
    Weekly,
    Monthly,
    Yearly
}