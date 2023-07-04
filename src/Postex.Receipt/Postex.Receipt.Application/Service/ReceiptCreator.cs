using Postex.Receipt.Domain.Models;
using Postex.Receipt.Infrastrucre;
using Postex.SharedKernel.Exceptions;
using System.Text;

namespace Postex.Receipt.Application
{
    public class ReceiptCreator : IReceiptCreator
    {
        private const string TemplateDirectoryName = "Templates";
        private const string HeaderFileName = "_Header";
        private const string FooterFileName = "_Footer";
        private const string OutputDirectoryName = "Output";
        private const string TemplateExtension = "html";
        private IDocumentService documentService;

        public ReceiptCreator(IDocumentService documentService)
        {
            this.documentService = documentService;
        }

        private string GetTemplatePath(string templateName)
        {
            var path = FileUtilities.GetPhysicalPath($"{TemplateDirectoryName}/{templateName}.{TemplateExtension}");

            if (!File.Exists(path))
                throw new AppException("template file not found");

            return path;
        }

        /// <summary>
        /// Save pdf file on the disk
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private string SavePdfFile(byte[] content)
        {
            var now = DateTime.Now;
            var dir = $"{OutputDirectoryName}/{now.Year}/{now.Month}/{now.Day}";
            var path = FileUtilities.GetPhysicalPath(dir);
            var fileName = Guid.NewGuid().ToString().Replace("-", string.Empty) + ".pdf";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            File.WriteAllBytes(Path.Combine(path, fileName), content);

            return Path.Combine(dir, fileName);
        }


        public async Task<string> CreatePdfFile(List<CreateReceiptModel> model)
        {
            var header = File.ReadAllText(GetTemplatePath(HeaderFileName));
            var footer = File.ReadAllText(GetTemplatePath(FooterFileName));
            var sb = new StringBuilder(header);

            foreach (var item in model)
            {
                var templatePath = GetTemplatePath(item.TemplateName);
                var templateContent = new StringBuilder(File.ReadAllText(templatePath));

                foreach (var value in item.Values)
                {
                    templateContent = templateContent.Replace($"[{value.Key}]", value.Value);
                }

                sb.Append(templateContent.ToString());
                sb.Append("<p class='break'></p>");
            }

            sb.Append(footer);

            var pdfContent = await documentService.GeneratePdfReport(sb.ToString());

            return SavePdfFile(pdfContent);
        }
    }
}
