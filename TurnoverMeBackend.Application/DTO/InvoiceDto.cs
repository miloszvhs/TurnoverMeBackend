using TurnoverMeBackend.Application.DTO.Enums;
using TurnoverMeBackend.Domain.Entities;

namespace TurnoverMeBackend.Application.DTO;

public class InvoiceDto
{
    public string Id { get; set; }
    public string InvoiceNumber { get; set; }
    public string Contractor { get; set; }
    public DateTime? IssueDate { get; set; }
    public DateTime? DueDate { get; set; }
    public InvoiceSellerDto Seller { get; set; }
    public InvoiceBuyerDto Buyer { get; set; }
    public InvoiceReceiverDto? Receiver { get; set; }
    public IList<InvoicePositionItemDto> Items { get; set; }
    public IList<InvoiceCircuitDto> Approvals { get; set; }
    public IList<InvoiceApprovalHistoryDto> ApprovalHistories { get; set; }
    public decimal TotalNetAmount { get; set; }
    public decimal TotalTaxAmount { get; set; }
    public decimal TotalGrossAmount { get; set; }
    public string Currency { get; set; }
    public string? Remarks { get; set; }
    public string Status { get; set; }
    public string invoiceFileAsBase64 { get; set; }

    public class InvoiceSellerDto
    {
        public string Name { get; set; }
        public AddressDto Address { get; set; }
        public TaxNumberDto TaxNumberDto { get; set; }
    }

    public class InvoiceApprovalHistoryDto
    {
        public string InvoiceId { get; set; }
        public string? Executor { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? ExecutionTime { get; set; }
        public string StageName { get; set; }
        public bool IsAccepted { get; set; }
        public string? Note { get; set; }
    }
    
    public class InvoiceBuyerDto
    {
        public string Name { get; set; }
        public AddressDto Address { get; set; }
        public TaxNumberDto TaxNumberDto { get; set; }
    }
    
    public class InvoiceReceiverDto
    {
        public string Name { get; set; }
        public AddressDto Address { get; set; }
        public TaxNumberDto? TaxNumberDto { get; set; }
    }
    
    public class AddressDto
    {
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string FlatNumber { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
    }
    
    public class InvoicePositionItemDto
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitNetPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal NetValue { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal GrossValue { get; set; }
    }
    
    public class InvoiceCircuitDto
    {
        public string? Note { get; set; }
        public int StageLevel { get; set; }
        public string? GroupId { get; set; }
        public string? UserId { get; set; }
        public string? ApproverName { get; set; }
        public DateTime? AcceptationTime { get; set; }
        public string Status { get; set; }
    }
}

