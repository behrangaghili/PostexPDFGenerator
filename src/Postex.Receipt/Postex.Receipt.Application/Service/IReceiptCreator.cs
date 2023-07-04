using Postex.Receipt.Domain.Models;

namespace Postex.Receipt.Application
{
    public interface IReceiptCreator
    {
        Task<string> CreatePdfFile(List<CreateReceiptModel> model);
    }
}