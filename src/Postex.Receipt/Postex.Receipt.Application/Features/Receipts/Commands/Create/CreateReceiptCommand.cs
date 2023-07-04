using MediatR;
using Postex.Receipt.Application;
using Postex.Receipt.Domain.Models;

namespace Postex.receipt.Application
{
    public class CreateReceiptCommand : IRequest<string>
    {
        public List<CreateReceiptModel> ReceiptModels;

        public CreateReceiptCommand(List<CreateReceiptModel> receiptModels)
        {
            ReceiptModels = receiptModels;
        }
    }
}
