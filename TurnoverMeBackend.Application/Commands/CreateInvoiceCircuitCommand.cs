using TurnoverMeBackend.Application.Abstractions;

namespace TurnoverMeBackend.Application.Commands;

public class CreateInvoiceCircuitCommand : ICommand
{
    public string InvoiceId { get; set; }
    public string Remarks { get; set; }
    public string? UserId { get; set; }
    public string? GroupId { get; set; }
    public int StageLevel { get; set; }
}
