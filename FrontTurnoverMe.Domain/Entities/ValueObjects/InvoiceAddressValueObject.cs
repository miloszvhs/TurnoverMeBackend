namespace FrontTurnoverMe.Domain.Entities.ValueObjects;

public class InvoiceAddressValueObject
{
    public string Street { get; set; }
    public string StreetNumber { get; set; }
    public string FlatNumber { get; set; }
    public string City { get; set; }
    public string PostCode { get; set; }
    public string Country { get; set; }
}