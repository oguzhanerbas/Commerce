﻿using MediatR;

namespace Commerce.Api.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull
{
    
}