using MediatR;
using Microsoft.AspNetCore.Mvc;
using Postex.receipt.Application;
using Postex.Receipt.Domain.Models;

namespace Postex.receipt.Api.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDFController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PDFController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/BarcodeReceipt
        [HttpPost]
        public async Task<ActionResult<byte[]>> GenerateBarcodePdf( )
        {
            var pdfBytes = await _mediator.Send(new CreateReceiptCommand());

            return File(pdfBytes, "application/pdf");
        }



    //    // POST: api/InvoiceReceipt
    //    [HttpPost]
    //    public async Task<ActionResult<byte[]>> GenerateInvoicePdf(PostCompanyModel _postCompanyModel)
    //    {
    //        var pdfBytes = await _mediator.Send(new BarcodePdfGenerationQuery { postCompanyModel = _postCompanyModel });

    //        return File(pdfBytes, "application/pdf");
    //    }

    //    // Additional methods for other barcode-related operations
    }
}