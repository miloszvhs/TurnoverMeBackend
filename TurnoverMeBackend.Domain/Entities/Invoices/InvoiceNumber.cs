﻿using TurnoverMeBackend.Domain.Common;

namespace TurnoverMeBackend.Domain.Entities.Invoices;

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