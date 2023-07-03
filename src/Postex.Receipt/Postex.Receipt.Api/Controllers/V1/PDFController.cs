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
        //use: dotnet dev-certs https --trust   to not get SSl Certificate error 
        [HttpPost]
        public async Task<IActionResult> GenerateBarcodePdf()
        {
            var pdfResponse = await _mediator.Send(new CreateReceiptCommand());

            return File(pdfResponse.FileContent, pdfResponse.ContentType, pdfResponse.FileName);
        }


    }
}
