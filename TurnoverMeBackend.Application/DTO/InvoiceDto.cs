using TurnoverMeBackend.Application.DTO.Enums;

namespace TurnoverMeBackend.Application.DTO;

public class InvoiceDto
{
    public string InvoiceNumber { get; set; }
    public DateTime IssueDate { get; set; }
    public InvoiceSellerDto Seller { get; set; }
    public InvoiceBuyerDto Buyer { get; set; }
    public InvoiceReceiverDto? Receiver { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public IList<InvoicePositionItemDto> Items { get; set; }
    public IList<InvoiceCircuitDto> Circuits { get; set; }
    public decimal TotalNetAmount { get; set; }
    public decimal TotalTaxAmount { get; set; }
    public decimal TotalGrossAmount { get; set; }
    public string Currency { get; set; }
    public string? Remarks { get; set; }
    
    public class InvoiceSellerDto
    {
        public string Name { get; set; }
        public AddressDto Address { get; set; }
        public TaxNumberDto TaxNumberDto { get; set; }
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
        public string From { get; set; }
        public string To { get; set; }
        public DateTime IssueDate { get; set; }
        public string Stage { get; set; }
        public string Note { get; set; }
    }
}

