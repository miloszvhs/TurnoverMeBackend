using Microsoft.AspNetCore.Identity;
using TurnoverMeBackend.Domain.Entities.Invoices;

namespace TurnoverMeBackend.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public IList<InvoiceCircuit> InvoiceCircuits { get; set; }

    public int BranchId
    {
        get => default;
        set
        {
        }
    }

    public int GroupId
    {
        get => default;
        set
        {
        }
    }
}