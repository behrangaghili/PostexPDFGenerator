using DinkToPdf;
using DinkToPdf.Contracts;
using Postex.Receipt.Domain.Models;
using System.Text;

namespace Postex.Receipt.Application
{
    public interface IInvoiceService
    {
        Task<byte[]> GenerateInvoicesPdfAsync(List<Invoice> invoices);
    }

    public class InvoiceService : IInvoiceService
    {
        private readonly IConverter _converter;

        public InvoiceService(IConverter converter)
        {
            _converter = converter;
        }

        public async Task<byte[]> GenerateInvoicesPdfAsync(List<Invoice> invoices)
        {
            var converter = new BasicConverter(new PdfTools());
            var htmlContent = GenerateHtmlContent(invoices);

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings =
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                },
                Objects =
                {
                    new ObjectSettings
                    {
                        HtmlContent = htmlContent,
                    }
                }
            };

            var pdfBytes = _converter.Convert(doc);

            return pdfBytes;
        }

        private string GenerateHtmlContent(List<Invoice> invoices)
        {
            // Start building the HTML content
            var htmlBuilder = new StringBuilder();

            // Add HTML header
            htmlBuilder.AppendLine("<html>");
            htmlBuilder.AppendLine("<head>");
            htmlBuilder.AppendLine("<style>");
            htmlBuilder.AppendLine("table { width: 100%; border-collapse: collapse; }");
            htmlBuilder.AppendLine("th, td { border: 1px solid black; padding: 8px; }");
            htmlBuilder.AppendLine("</style>");
            htmlBuilder.AppendLine("</head>");
            htmlBuilder.AppendLine("<body>");

            // Add a table to hold the invoice details
            htmlBuilder.AppendLine("<table>");

            // Add table header row
            htmlBuilder.AppendLine("<tr>");
            htmlBuilder.AppendLine("<th>Invoice ID</th>");
            htmlBuilder.AppendLine("<th>Product</th>");
            htmlBuilder.AppendLine("<th>Price</th>");
            htmlBuilder.AppendLine("<th>Recipient</th>");
            htmlBuilder.AppendLine("</tr>");

            // Add table rows for each invoice
            foreach (var invoice in invoices)
            {
                htmlBuilder.AppendLine("<tr>");
                htmlBuilder.AppendLine($"<td>{invoice.OrderId}</td>");
                htmlBuilder.AppendLine($"<td>{invoice.ProductName}</td>");
                htmlBuilder.AppendLine($"<td>{invoice.ProductPrice}</td>");
                htmlBuilder.AppendLine($"<td>{invoice.ReciverFullName}</td>");
                htmlBuilder.AppendLine("</tr>");
            }

            // Close the table and body
            htmlBuilder.AppendLine("</table>");
            htmlBuilder.AppendLine("</body>");
            htmlBuilder.AppendLine("</html>");

            // Return the generated HTML content
            return htmlBuilder.ToString();
        }
    }
}
