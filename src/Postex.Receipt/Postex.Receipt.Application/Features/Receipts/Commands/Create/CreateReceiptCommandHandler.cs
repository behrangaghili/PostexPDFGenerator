using MediatR;
using Microsoft.AspNetCore.Mvc;
using Postex.Receipt.Application;

namespace Postex.receipt.Application
{
    public class CreateReceiptCommandHandler : IRequestHandler<CreateReceiptCommand, string>
    {
        private readonly IReceiptCreator _createReport;

        public CreateReceiptCommandHandler(IReceiptCreator createReport)
        {
            _createReport = createReport;
        }

        public Task<string> Handle(CreateReceiptCommand request, CancellationToken cancellationToken)
        {
            return _createReport.CreatePdfFile(request.ReceiptModels);
        }
    }
}
