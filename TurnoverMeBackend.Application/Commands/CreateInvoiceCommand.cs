﻿using TurnoverMeBackend.Application.Abstractions;
using TurnoverMeBackend.Application.DTO;

namespace TurnoverMeBackend.Application.Commands;

public record CreateInvoiceCommand(SaveInvoiceRequest InvoiceRequest) : ICommand;