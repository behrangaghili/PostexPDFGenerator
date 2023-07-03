using MediatR;
using Microsoft.AspNetCore.Mvc;
using Postex.receipt.Application;

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
        public async Task<ActionResult<byte[]>> GenerateBarcodePdf()
        {
            var pdfBytes = await _mediator.Send(new CreateReceiptCommand());

            return File(pdfBytes, "application/pdf");
        }
    }
}