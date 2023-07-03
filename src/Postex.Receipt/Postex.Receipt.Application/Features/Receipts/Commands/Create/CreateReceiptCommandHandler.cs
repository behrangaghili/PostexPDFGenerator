using MediatR;
using Microsoft.AspNetCore.Hosting;
using Postex.receipt.Application;

namespace Postex.Receipt.Application
{
    public class CreateReceiptCommandHandler : IRequestHandler<CreateReceiptCommand, PdfFileResponse>
    {
        private readonly CreateReport _createReport;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateReceiptCommandHandler(CreateReport createReport, IWebHostEnvironment webHostEnvironment)
        {
            _createReport = createReport;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<PdfFileResponse> Handle(CreateReceiptCommand request, CancellationToken cancellationToken)
        {
            await _createReport.Create();

            // Get the byte array of the generated PDF report
            byte[] reportBytes = _createReport.GetReportBytes();

            var fileName = "BarcodeReceipt.pdf";
            var contentType = "application/pdf";

            var folderPath = Path.Combine(_webHostEnvironment.ContentRootPath, "PDFs");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var filePath = Path.Combine(folderPath, fileName);
            File.WriteAllBytes(filePath, reportBytes);

            var fileUrl = Path.Combine("/PDFs", fileName);

            return new PdfFileResponse(fileUrl, fileName, contentType, reportBytes);
        }
    }
}
