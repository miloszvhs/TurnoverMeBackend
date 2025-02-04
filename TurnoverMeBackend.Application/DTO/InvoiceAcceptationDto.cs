using TurnoverMeBackend.Application.DTO.Enums;

namespace TurnoverMeBackend.Application.DTO;

public class InvoiceAcceptationDto
{
    public string Guid { get; set; }
    public string Name { get; set; }
    public InvoiceStatusEnumDto Status { get; set; }
    public string CreationDate { get; set; }
    public string ModificationDate { get; set; }
    public string InvoiceFileAsBase64 { get; set; }
}