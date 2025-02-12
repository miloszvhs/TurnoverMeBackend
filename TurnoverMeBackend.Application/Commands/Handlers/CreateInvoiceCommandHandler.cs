using TurnoverMeBackend.Application.Abstractions;
using TurnoverMeBackend.Application.DTO;
using TurnoverMeBackend.Application.Interfaces;
using TurnoverMeBackend.Application.Services;
using TurnoverMeBackend.Domain.Entities.Invoices;
using TurnoverMeBackend.Domain.Entities.ValueObjects;

namespace TurnoverMeBackend.Application.Commands.Handlers;

public class CreateInvoiceCommandHandler(IInvoiceRepository invoiceRepository,
    IInvoiceNumberGenerationService invoiceNumberGenerationService) : ICommandHandler<CreateInvoiceCommand>
{
    public void Handle(CreateInvoiceCommand command)
    {
        var invoice = ProcessRequest(command.InvoiceRequest);
        invoiceRepository.Add(invoice);
    }

    private Invoice ProcessRequest(SaveInvoiceRequest invoice)
    {
        var newInvoice = new Invoice
        {
            InvoiceNumber = string.IsNullOrWhiteSpace(invoice.InvoiceNumber)
                ? invoiceNumberGenerationService.GenerateInvoiceNumber("FV") 
                : invoice.InvoiceNumber,
            Remarks = invoice.Remarks
        };
        
        newInvoice.DueDate = invoice.DueDate;
        newInvoice.IssueDate = invoice.IssueDate;
        newInvoice.Status = Invoice.InvoiceStatus.New;

        MapBuyer();
        MapSeller();
        MapReceiver();

        MapItems();
        MapTaxes();

        return newInvoice;

        void MapBuyer()
        {
            newInvoice.Buyer = new InvoiceBuyer()
            {
                Name = invoice.Buyer.Name,
                TaxNumber = invoice.Buyer.TaxNumber.TaxNumber,
                AddressValueObject = new InvoiceAddressValueObject()
                {
                    City = invoice.Buyer.Address.City,
                    Country = invoice.Buyer.Address.Country,
                    PostCode = invoice.Buyer.Address.PostCode,
                    Street = invoice.Buyer.Address.Street,
                    StreetNumber = invoice.Buyer.Address.StreetNumber,
                    FlatNumber = invoice.Buyer.Address.FlatNumber
                }
            };
        }
        
        void MapSeller()
        {
            newInvoice.Seller = new InvoiceSeller()
            {
                Name = invoice.Seller.Name,
                TaxNumber = invoice.Seller.TaxNumber.TaxNumber,
                AddressValueObject = new InvoiceAddressValueObject()
                {
                    City = invoice.Seller.Address.City,
                    Country = invoice.Seller.Address.Country,
                    PostCode = invoice.Seller.Address.PostCode,
                    Street = invoice.Seller.Address.Street,
                    StreetNumber = invoice.Seller.Address.StreetNumber,
                    FlatNumber = invoice.Seller.Address.FlatNumber
                }
            };
        }
        
        void MapReceiver()
        {
            if (invoice.Receiver is null)
                return;

            newInvoice.Receiver = new InvoiceReceiver()
            {
                Name = invoice.Receiver.Name,
                TaxNumber = invoice.Receiver.TaxNumber.TaxNumber,
                AddressValueObject = new InvoiceAddressValueObject()
                {
                    City = invoice.Receiver.Address.City,
                    Country = invoice.Receiver.Address.Country,
                    PostCode = invoice.Receiver.Address.PostCode,
                    Street = invoice.Receiver.Address.Street,
                    StreetNumber = invoice.Receiver.Address.StreetNumber,
                    FlatNumber = invoice.Receiver.Address.FlatNumber
                }
            };
        }
        
        void MapItems()
        {
            newInvoice.Items = invoice.Items.Select(x => new InvoicePositionItem()
            {
                Name = x.Name,
                Unit = x.Unit,
                Quantity = x.Quantity,
                UnitNetPrice = x.UnitNetPrice,
                Discount = x.Discount,
                NetValue = x.NetValue,
                TaxRate = x.TaxRate,
                TaxAmount = x.TaxAmount,
                GrossValue = x.GrossValue
            }).ToList();
        }

        void MapTaxes()
        {
            newInvoice.TotalNetAmount = invoice.TotalNetAmount;
            newInvoice.TotalTaxAmount = invoice.TotalTaxAmount;
            newInvoice.TotalGrossAmount = invoice.TotalGrossAmount;
            newInvoice.Currency = invoice.Currency;
        }
    }
}