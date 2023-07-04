using MediatR;
using Microsoft.AspNetCore.Mvc;
using Postex.receipt.Application;
using Postex.Receipt.Domain.Models;
using Postex.Receipt.Infrastrucre;

namespace Postex.Receipt.Api.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]
    public class PDFController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _env;

        public PDFController(IMediator mediator, IWebHostEnvironment env)
        {
            _mediator = mediator;
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateBarcodePdf([FromBody] List<CreateReceiptModel> models)
        {
            var command = new CreateReceiptCommand(models);
            var pdfFile = await _mediator.Send(command);
            var content=System.IO.File.ReadAllBytes(FileUtilities.GetPhysicalPath(pdfFile));
            return File(content, "application/pdf", "post-receipt.pdf");
            //return PhysicalFile(Path.Combine(_env.ContentRootPath,pdfFile), "application/pdf","post-receipt.pdf");
        }
    }
}
