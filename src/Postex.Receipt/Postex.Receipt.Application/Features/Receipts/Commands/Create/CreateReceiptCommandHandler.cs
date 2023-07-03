using MediatR;
using Postex.Receipt.Application;

namespace Postex.receipt.Application
{
    public class CreateReceiptCommandHandler : IRequestHandler<CreateReceiptCommand, byte[]>
    {
        private readonly CreateReport _createReport;

        public CreateReceiptCommandHandler(CreateReport createReport)
        {
            _createReport = createReport;
        }

        public async Task<byte[]> Handle(CreateReceiptCommand request, CancellationToken cancellationToken)
        {
            await _createReport.Create();

            // Get the byte array of the generated PDF report
            byte[] reportBytes = _createReport.GetReportBytes();

            return reportBytes;
        }
    }

}
