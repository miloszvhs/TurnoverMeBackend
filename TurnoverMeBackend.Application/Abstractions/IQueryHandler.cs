﻿namespace TurnoverMeBackend.Application.Abstractions;

public interface IQueryHandler<in TQuery, out TResult> where TQuery: class, IQuery<TResult>
{
    TResult Handle(TQuery query);
}