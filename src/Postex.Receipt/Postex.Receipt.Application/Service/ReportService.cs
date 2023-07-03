
namespace Postex.Receipt.Application
{
    public class ReportService : IReportService
    {
        public IDocumentService DocumentService { get; private set; }

        public ReportService(IDocumentService documentService)
        {
            DocumentService = documentService;
        }
    }
}