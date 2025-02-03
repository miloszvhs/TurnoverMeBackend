using System.ComponentModel.DataAnnotations;
using FrontTurnoverMe.Application.Validators;

namespace FrontTurnoverMe.Application.DTO;

public class SaveInvoiceRequest : IValidatableObject
{
    public DateTime IssueDate { get; set; } // Data wystawienia
    public string? InvoiceNumber { get; set; } // Kolejny numer faktury
    public InvoiceRequestSeller Seller { get; set; } // Dane sprzedawcy
    public InvoiceRequestBuyer Buyer { get; set; } // Dane nabywcy
    public InvoiceRequestReceiver? Receiver { get; set; } // Dane odbiorcy
    public DateTime? DeliveryDate { get; set; } // Data dostawy lub wykonania usługi
    public List<InvoiceRequestItem> Items { get; set; } = []; // Pozycje faktury
    public decimal TotalNetAmount { get; set; } // Suma wartości sprzedaży netto
    public decimal TotalTaxAmount { get; set; } // Kwota podatku VAT
    public decimal TotalAmountDue { get; set; } // Kwota należności ogółem
   
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        ValidatorHelper.Validate(Seller);
        ValidatorHelper.Validate(Buyer);
        
        if(Receiver != null)
            ValidatorHelper.Validate(Receiver);
        
        if (Items == null || Items.Count == 0)
            yield return new ValidationResult("At least one item is required", [nameof(Items)]);
        
        if (TotalNetAmount <= 0)
            yield return new ValidationResult("Total net amount must be greater than 0", [nameof(TotalNetAmount)]);
        
        if (TotalTaxAmount <= 0)
            yield return new ValidationResult("Total tax amount must be greater than 0", [nameof(TotalTaxAmount)]);
        
        if (TotalAmountDue <= 0)
            yield return new ValidationResult("Total amount due must be greater than 0", [nameof(TotalAmountDue)]);
        
        if (IssueDate == default)
            yield return new ValidationResult("Issue date is required", [nameof(IssueDate)]);
        
        if (InvoiceNumber != null && string.IsNullOrEmpty(InvoiceNumber))
            yield return new ValidationResult("Invoice number is required", [nameof(InvoiceNumber)]);
    }
}

public class InvoiceRequestSeller : IValidatableObject
{
    public string Name { get; set; } // Nazwa sprzedawcy
    public InvoiceRequestAddress Address { get; set; }
    public TaxNumber TaxNumber { get; set; }
    
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
    public string Name { get; set; } // Nazwa nabywcy
    public InvoiceRequestAddress Address { get; set; }
    public TaxNumber TaxNumber { get; set; }
    
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
    public string Name { get; set; } // Nazwa odbiorcy
    public InvoiceRequestAddress Address { get; set; }
    public TaxNumber? TaxNumber { get; set; }

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

public class TaxNumber
{
    public string Prefix { get; set; }
    public string Value { get; set; }
    
    public bool IsValid()
    {
        if (string.IsNullOrEmpty(Value) || string.IsNullOrEmpty(Prefix)) return false;
        
        if (Prefix.Equals("PL", StringComparison.OrdinalIgnoreCase))
            return ValidatePolishNIP(Value);
        else
            return ValidateForeignTaxNumber(Value);
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
