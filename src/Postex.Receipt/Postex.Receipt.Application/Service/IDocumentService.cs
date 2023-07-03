


public interface IDocumentService
{
    Task<byte[]> GeneratePdfReport(string template);
}