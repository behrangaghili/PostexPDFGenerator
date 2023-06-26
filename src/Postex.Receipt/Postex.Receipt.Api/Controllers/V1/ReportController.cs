using MediatR;
using Microsoft.AspNetCore.Mvc;
using Postex.receipt.Application.Features.Cities.Commands.Create;
using Postex.receipt.Application.Features.Cities.Queries.GetById;
using Postex.Receipt.Application;
using Postex.Receipt.Domain;
using Postex.Receipt.Domain.Models;


namespace Postex.receipt.Api.Controllers.V1
    {
        [Route("api/[controller]")]
        [ApiController]
        public class ReportController : Controller
        {
           
            private readonly IInvoiceService _service;

            public ReportController(  IInvoiceService service)
            {
                
                _service = service;

            }


        /*
            [HttpPost("invoices/pdf")]
            public async Task<IActionResult> GenerateInvoicesPdf([FromBody] List<Invoice> invoices)
            {
                var invoiceService = new InvoiceService();

                // Generate the invoices PDF using the InvoiceService
                byte[] pdfBytes = await invoiceService.GenerateInvoicesPdfAsync(invoices);

                // Return the PDF file as a response
                return File(pdfBytes, "application/pdf");
            }

        */
        [HttpPost("invoices/pdf")]
        public async Task<IActionResult> GenerateInvoicesPdf([FromBody] List<Invoice> invoices)
        {
            var invoiceService = new InvoiceService();

            // Generate the invoices PDF using the InvoiceService
            byte[] pdfBytes = await invoiceService.GenerateInvoicesPdfAsync(invoices);

            // Generate a unique file name for the PDF
            string fileName = Guid.NewGuid().ToString() + ".pdf";

            // Get the server path to save the PDF file
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "PDFs", fileName);

            // Save the PDF file to the server
            System.IO.File.WriteAllBytes(filePath, pdfBytes);

            // Create a downloadable URL path for the saved PDF file
            string fileUrl = Url.Action("DownloadPdf", "Report", new { fileName = fileName }, Request.Scheme);

            // Return the URL path as a response
            return Ok(new { Url = fileUrl });
        }

        [HttpGet("invoices/pdf/{fileName}")]
        public IActionResult DownloadPdf(string fileName)
        {
            // Get the server path to the PDF file
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "PDFs", fileName);

            // Check if the PDF file exists
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            // Read the PDF file as bytes
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            // Return the PDF file as a downloadable response
            return File(fileBytes, "application/pdf", fileName);
        }



    }
}




