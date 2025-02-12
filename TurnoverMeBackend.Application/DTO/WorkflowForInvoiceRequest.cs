using Microsoft.AspNetCore.Mvc;

namespace TurnoverMeBackend.Application.DTO;

public record WorkflowForInvoiceRequest(string CircuitPathId, string InvoiceId);