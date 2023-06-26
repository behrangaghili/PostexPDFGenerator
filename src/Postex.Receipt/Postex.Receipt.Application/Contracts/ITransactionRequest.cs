using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postex.receipt.Application.Contracts
{
    public class ITransactionRequest<TRequest>:IRequest<TRequest>
    {
    }
    public interface ITransactionRequest : IRequest
    { 
    }
}
