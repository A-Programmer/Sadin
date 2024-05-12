namespace Sadin.Common.MediatRCommon.Commands;
public interface ICommand<out TResult> : IRequest<TResult>
{
}

public interface ICommand : IRequest
{
}
