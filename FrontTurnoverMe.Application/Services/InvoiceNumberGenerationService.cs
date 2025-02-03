using FrontTurnoverMe.Application.Interfaces;
using FrontTurnoverMe.Domain.Entities;

namespace FrontTurnoverMe.Application.Services;

public interface IInvoiceNumberGenerationService
{
    string GenerateInvoiceNumber(string type);
}

public class InvoiceNumberGenerationService(IInvoiceNumberRepository invoiceNumberRepository)
    : IInvoiceNumberGenerationService
{
    private const string InvoicePattern = "FV/<number>/<year>";
    
    public string GenerateInvoiceNumber(string type)
    {
        var invoiceNumber = invoiceNumberRepository.GetLastInvoiceNumber(type);
        invoiceNumber ??= new InvoiceNumber() { Type = type, Number = 0, Refresh = RefreshEnum.Yearly};

        var generatedInvoiceNumber = GenerateNextNumber(type, invoiceNumber.Number);
        invoiceNumber.Number++;        
        invoiceNumberRepository.SaveOrUpdate(invoiceNumber);
        return generatedInvoiceNumber;
    }

    private string GenerateNextNumber(string type, int number)
    {
        switch (type)
        {
            case "FV":
                return InvoicePattern
                    .Replace("<number>", number.ToString().PadRight(6, '0'))
                    .Replace("<year>", DateTime.Now.Year.ToString());
            default:
                throw new NotImplementedException();
        }
    }
}