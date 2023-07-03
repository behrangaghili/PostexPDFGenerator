using MediatR;

namespace Postex.receipt.Application.Contracts
{
    public class ITransactionRequest<TRequest> : IRequest<TRequest>
    {
    }
    public interface ITransactionRequest : IRequest
    {
    }
}
