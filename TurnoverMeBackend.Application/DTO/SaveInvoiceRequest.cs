using System.ComponentModel.DataAnnotations;
using TurnoverMeBackend.Application.Validators;

namespace TurnoverMeBackend.Application.DTO;

public class SaveInvoiceRequest : IValidatableObject
{
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public string InvoiceNumber { get; set; }
    public InvoiceRequestSeller Seller { get; set; }
    public InvoiceRequestBuyer? Buyer { get; set; }
    public InvoiceRequestReceiver? Receiver { get; set; }
    public List<InvoiceRequestItem>? Items { get; set; } = [];
    public decimal TotalNetAmount { get; set; }
    public decimal TotalTaxAmount { get; set; }
    public decimal TotalGrossAmount { get; set; }
    public string Currency { get; set; }
    public string Remarks { get; set; }
    public string InvoiceFileAsBase64 { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        ValidatorHelper.Validate(Buyer);
        
        if(Seller != null)
            ValidatorHelper.Validate(Seller);
        
        if(Receiver != null)
            ValidatorHelper.Validate(Receiver);
        
        if (Items == null || Items.Count == 0)
            yield return new ValidationResult("At least one item is required", [nameof(Items)]);
        
        if (TotalNetAmount <= 0)
            yield return new ValidationResult("Total net amount must be greater than 0", [nameof(TotalNetAmount)]);
        
        if (TotalGrossAmount <= 0)
            yield return new ValidationResult("Total gross amount must be greater than 0", [nameof(TotalGrossAmount)]);
        
        if (IssueDate == default)
            yield return new ValidationResult("Issue date is required", [nameof(IssueDate)]);
    }
}

public class InvoiceRequestSeller : IValidatableObject
{
    public string Name { get; set; }
    public InvoiceRequestAddress? Address { get; set; }
    public TaxNumberDto TaxNumber { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!TaxNumber.IsValid())
            yield return new ValidationResult("Invalid tax number", [nameof(TaxNumber)]);

        if (string.IsNullOrEmpty(Name))
            yield return new ValidationResult("Name is required", [nameof(Name)]);

        if (Address == null)
            yield return new ValidationResult("Address is required", [nameof(Address)]);
    }
}

public class InvoiceRequestBuyer : IValidatableObject
{
    public string Name { get; set; } 
    public InvoiceRequestAddress Address { get; set; }
    public TaxNumberDto TaxNumber { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!TaxNumber.IsValid())
            yield return new ValidationResult("Invalid tax number", [nameof(TaxNumber)]);

        if (string.IsNullOrEmpty(Name))
            yield return new ValidationResult("Name is required", [nameof(Name)]);

        if (Address == null)
            yield return new ValidationResult("Address is required", [nameof(Address)]);    }
}

public class InvoiceRequestReceiver : IValidatableObject
{
    public string Name { get; set; }
    public InvoiceRequestAddress Address { get; set; }
    public TaxNumberDto? TaxNumber { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (TaxNumber != null && !TaxNumber.IsValid())
            yield return new ValidationResult("Invalid tax number", [nameof(TaxNumber)]);

        if (string.IsNullOrEmpty(Name))
            yield return new ValidationResult("Name is required", [nameof(Name)]);

        if (Address == null)
            yield return new ValidationResult("Address is required", [nameof(Address)]);    }
}

public class InvoiceRequestItem
{
    public string Name { get; set; } // Nazwa towaru lub usługi
    public string Unit { get; set; } // Miara
    public decimal Quantity { get; set; } // Ilość
    public decimal UnitNetPrice { get; set; } // Cena jednostkowa netto
    public decimal Discount { get; set; } // Upusty, rabaty
    public decimal NetValue { get; set; } // Wartość netto
    public decimal TaxRate { get; set; } // Stawka VAT
    public decimal TaxAmount { get; set; } // Kwota podatku
    public decimal GrossValue { get; set; } // Wartość brutto
}

public class InvoiceRequestAddress
{
    public string Street { get; set; }
    public string StreetNumber { get; set; }
    public string FlatNumber { get; set; }
    public string City { get; set; }
    public string PostCode { get; set; }
    public string Country { get; set; }
}

public class TaxNumberDto
{
    public string TaxPrefix { get; set; }
    public string TaxNumber { get; set; }
    
    public bool IsValid()
    {
        if (string.IsNullOrEmpty(TaxNumber) || string.IsNullOrEmpty(TaxPrefix)) return false;
        
        if (TaxPrefix.Equals("PL", StringComparison.OrdinalIgnoreCase))
            return ValidatePolishNIP(TaxNumber);
        else
            return ValidateForeignTaxNumber(TaxNumber);
    }
    
    private bool ValidatePolishNIP(string nip)
    {
        if (nip.Length != 10 || !long.TryParse(nip, out _)) return false;
        int[] weights = { 6, 5, 7, 2, 3, 4, 5, 6, 7 };
        int sum = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            sum += weights[i] * (nip[i] - '0');
        }
        int checksum = sum % 11;
        return checksum == (nip[9] - '0');
    }
    
    private bool ValidateForeignTaxNumber(string taxNumber)
    {
        return taxNumber.Length > 0;
    }
}
