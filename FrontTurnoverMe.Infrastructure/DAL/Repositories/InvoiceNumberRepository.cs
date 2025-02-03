﻿using FrontTurnoverMe.Application.Interfaces;
using FrontTurnoverMe.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FrontTurnoverMe.Infrastructure.DAL.Repositories;

public class InvoiceNumberRepository(InvoicesDbContext dbContext) : IInvoiceNumberRepository
{
    private DbSet<InvoiceNumber> _invoiceNumbers = dbContext.InvoiceNumbers;

    public InvoiceNumber GetLastInvoiceNumber(string type)
    {
        return _invoiceNumbers
            .Where(x => x.Type == type)
            .FirstOrDefault();
    }

    public void SaveOrUpdate(InvoiceNumber invoiceNumber)
    {
        var existingInvoiceNumber = _invoiceNumbers
            .Where(x => x.Type == invoiceNumber.Type)
            .FirstOrDefault();

        if (existingInvoiceNumber == null)
            _invoiceNumbers.Add(invoiceNumber);
        else
            existingInvoiceNumber.Number = invoiceNumber.Number;

        dbContext.SaveChanges();
    }
}