using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Postex.Receipt.Domain.Models;

namespace Postex.Receipt.Application
{
    public interface IInvoiceService
    {
        Task<byte[]> GenerateInvoicesPdfAsync(List<Invoice> invoices);
    }

    public class InvoiceService : IInvoiceService
    {
        public async Task<byte[]> GenerateInvoicesPdfAsync(List<Invoice> invoices)
        {
            using (var document = new Document())
            {
                using (var stream = new MemoryStream())
                {
                    using (var writer = PdfWriter.GetInstance(document, stream))
                    {
                        document.Open();

                        foreach (var invoice in invoices)
                        {
                            AddInvoiceContent(document, invoice);
                        }

                        document.Close();
                    }

                    return stream.ToArray();
                }
            }
        }

        private void AddInvoiceContent(Document document, Invoice invoice)
        {
            // Create a table to hold the invoice details
            PdfPTable table = new PdfPTable(2); // 2 columns
            table.WidthPercentage = 100;

            // Add the invoice details to the table
            table.AddCell("Invoice ID:");
            table.AddCell(invoice.OrderId.ToString());

            table.AddCell("Product:");
            table.AddCell(invoice.ProductName);

            table.AddCell("Price:");
            table.AddCell(invoice.ProductPrice.ToString());

            table.AddCell("Recipient:");
            table.AddCell(invoice.ReciverFullName);

            // Add the table to the document
            document.Add(table);
        }

    }
}
