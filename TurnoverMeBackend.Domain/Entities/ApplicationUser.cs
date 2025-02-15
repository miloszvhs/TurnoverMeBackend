using Microsoft.AspNetCore.Identity;
using TurnoverMeBackend.Domain.Entities.Invoices;

namespace TurnoverMeBackend.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public IList<InvoiceApproval> InvoiceCircuits { get; set; }
    public string? GroupId { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
    public bool? ForcePasswordChange { get; set; }
}