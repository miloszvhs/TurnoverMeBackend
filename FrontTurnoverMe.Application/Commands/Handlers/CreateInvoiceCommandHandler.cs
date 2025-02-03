using FrontTurnoverMe.Application.Abstractions;
using FrontTurnoverMe.Application.DTO;
using FrontTurnoverMe.Application.Interfaces;
using FrontTurnoverMe.Application.Services;
using FrontTurnoverMe.Domain.Entities;
using FrontTurnoverMe.Domain.Entities.Invoices;
using FrontTurnoverMe.Domain.Entities.ValueObjects;

namespace FrontTurnoverMe.Application.Commands.Handlers;

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
        var newInvoice = new Invoice();

        if(invoice.InvoiceNumber is null)
            invoice.InvoiceNumber = invoiceNumberGenerationService.GenerateInvoiceNumber("FV");
        else
            newInvoice.InvoiceNumber = invoice.InvoiceNumber;
        
        MapBuyer();
        MapSeller();
        MapReceiver();

        MapItems();
        MapTaxes();
        MapPayment();

        return newInvoice;

        void MapBuyer()
        {
            newInvoice.Buyer = new InvoiceBuyer()
            {
                Name = invoice.Buyer.Name,
                TaxNumber = "1234567890",
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
                TaxNumber = "0987654321",
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
                TaxNumber = "0987654321",
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
            newInvoice.TotalNetAmount = invoice.Items.Sum(x => x.NetValue);
            newInvoice.TotalTaxAmount = invoice.Items.Sum(x => x.TaxAmount);
            newInvoice.TotalAmountDue = invoice.Items.Sum(x => x.GrossValue);
            newInvoice.Currency = "PLN";
        }
        
        void MapPayment()
        {
            newInvoice.IssueDate = DateTime.Now;
            newInvoice.DeliveryDate = DateTime.Now;
        }
    }
}