using FrontTurnoverMe.Application.DTO.Enums;

namespace FrontTurnoverMe.Application.DTO;

public class InvoiceDto
{
    public int Id { get; set; }
    public string Number { get; set; }
    public string Currency { get; set; } = "PLN";
    public int? PaidPrice { get; set; }
    public string Notes { get; set; }
    public string Kind { get; set; }
    public string PaymentMethod { get; set; }
    public bool? SplitPayment { get; set; }
    public string SplitPaymentType { get; set; }
    public string RecipientSignature { get; set; }
    public string SellerSignature { get; set; }
    public DateTime InvoiceDate { get; set; }
    public DateTime SaleDate { get; set; }
    public InvoiceStatusEnumDto Status { get; set; }
    public DateTime PaymentDate { get; set; }
    public DateTime PaidDate { get; set; }
    public int? NetPrice { get; set; }
    public int? TaxPrice { get; set; }
    public int? GrossPrice { get; set; }
    public int? LeftToPay { get; set; }
    public int? ClientId { get; set; }
    public string ClientCompanyName { get; set; }
    public string ClientFirstName { get; set; }
    public string ClientLastName { get; set; }
    public string ClientBusinessActivityKind { get; set; }
    public string ClientStreet { get; set; }
    public string ClientStreetNumber { get; set; }
    public string ClientFlatNumber { get; set; }
    public string ClientCity { get; set; }
    public string ClientPostCode { get; set; }
    public string ClientTaxCode { get; set; }
    public string CleanClientNip { get; set; }
    public string ClientCountry { get; set; }
    public string BankName { get; set; }
    public string BankAccount { get; set; }
    public string Swift { get; set; }
    public DateTime CreatedAt { get; set; }
}