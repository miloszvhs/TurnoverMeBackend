using TurnoverMeBackend.Application.DTO;
using TurnoverMeBackend.Domain.Entities.Invoices;

namespace TurnoverMeBackend.Application.Helpers;

public class InvoiceDtoHelper
{
    public static InvoiceDto MapToDto(Invoice invoice)
    {
        var dto = new InvoiceDto()
        {
            Id = invoice.Id,
            InvoiceNumber = invoice.InvoiceNumber,
            DueDate = invoice.DueDate,
            IssueDate = invoice.IssueDate,
            Currency = invoice.Currency,
            Remarks = invoice.Remarks,
            Status = invoice.Status.ToString(),
            TotalGrossAmount = invoice.TotalGrossAmount,
            TotalNetAmount = invoice.TotalNetAmount,
            TotalTaxAmount = invoice.TotalTaxAmount,
            invoiceFileAsBase64 = invoice.InvoiceFileAsBase64,
            Buyer = invoice.Buyer == null ? null : new InvoiceDto.InvoiceBuyerDto()
            {
                Address = new InvoiceDto.AddressDto()
                {
                    City = invoice.Buyer.AddressValueObject.City,
                    Country = invoice.Buyer.AddressValueObject.Country,
                    Street = invoice.Buyer.AddressValueObject.Street,
                    FlatNumber = invoice.Buyer.AddressValueObject.FlatNumber,
                    PostCode = invoice.Buyer.AddressValueObject.PostCode,
                    StreetNumber = invoice.Buyer.AddressValueObject.StreetNumber
                },
                Name = invoice.Buyer.Name,
                TaxNumberDto = new TaxNumberDto()
                {
                    TaxNumber = invoice.Buyer.TaxNumber,
                    TaxPrefix = ""
                }
            },
            Seller = new InvoiceDto.InvoiceSellerDto()
            {
                Address = invoice.Seller.AddressValueObject == null 
                    ? null 
                    : new InvoiceDto.AddressDto()
                {
                    City = invoice.Seller.AddressValueObject.City,
                    Country = invoice.Seller.AddressValueObject.Country,
                    Street = invoice.Seller.AddressValueObject.Street,
                    FlatNumber = invoice.Seller.AddressValueObject.FlatNumber,
                    PostCode = invoice.Seller.AddressValueObject.PostCode,
                    StreetNumber = invoice.Seller.AddressValueObject.StreetNumber
                },
                Name = invoice.Seller.Name,
                TaxNumberDto = new TaxNumberDto()
                {
                    TaxNumber = invoice.Seller.TaxNumber,
                    TaxPrefix = ""
                }
            },
            Receiver = invoice.Receiver == null 
                ? null 
                : new InvoiceDto.InvoiceReceiverDto()
            {
                Address = new InvoiceDto.AddressDto()
                {
                    City = invoice.Receiver.AddressValueObject.City,
                    Country = invoice.Receiver.AddressValueObject.Country,
                    Street = invoice.Receiver.AddressValueObject.Street,
                    FlatNumber = invoice.Receiver.AddressValueObject.FlatNumber,
                    PostCode = invoice.Receiver.AddressValueObject.PostCode,
                    StreetNumber = invoice.Receiver.AddressValueObject.StreetNumber
                },
                Name = invoice.Receiver.Name,
                TaxNumberDto = string.IsNullOrEmpty(invoice.Receiver.TaxNumber)
                    ? null 
                    : new TaxNumberDto()
                {
                    TaxNumber = invoice.Receiver.TaxNumber,
                    TaxPrefix = ""
                }
            },
            Items = invoice?.Items?.Select(x => new InvoiceDto.InvoicePositionItemDto()
            {
                Discount = x.Discount,
                Name = x.Name,
                Quantity = x.Quantity,
                Unit = x.Unit,
                GrossValue = x.GrossValue,
                NetValue = x.NetValue,
                TaxAmount = x.TaxAmount,
                TaxRate = x.TaxRate,
                UnitNetPrice = x.UnitNetPrice,
                
            }).ToList(),
            Approvals = invoice.Approvals.Select(x => new InvoiceDto.InvoiceCircuitDto()
            {
                StageLevel = x.StageLevel,
                GroupId = x.GroupId,
                UserId = x.UserId,
                ApproverName = x.ApproverName,
                AcceptationTime = x.AcceptationTime,
                Note = x.Note,
                Status = x.Status.ToString()
            }).ToList(),
            ApprovalHistories = invoice.ApprovalsHistories?.Select(x => new InvoiceDto.InvoiceApprovalHistoryDto()
            {
                Note = x.Note,
                Executor = x.Executor,
                CreationTime = x.CreationTime,
                ExecutionTime = x.ExecutionTime,
                IsAccepted = x.IsAccepted,
                StageName = x.StageName,
                InvoiceId = x.InvoiceId
            }).ToArray()
        };

        return dto;
    } 
}