using MediatR;
using Postex.receipt.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Postex.receipt.Application.Behaviours
{
    public class TransactionPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ITransactionRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is ITransactionRequest<TResponse> || request is ITransactionRequest)
            {
                var transactionOption = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TransactionManager.MaximumTimeout
                };
                using (var tran = new TransactionScope(TransactionScopeOption.Required, transactionOption, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var resp = await next();
                    tran.Complete();
                    return resp;
                }
            }
            return await next();
        }
    }
}
