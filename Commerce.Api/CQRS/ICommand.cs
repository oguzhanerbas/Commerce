using MediatR;

namespace Commerce.Api.CQRS;

public interface ICommand<out TResult> : IRequest<TResult>
{

}

public interface ICommand : ICommand<Unit>
{

}