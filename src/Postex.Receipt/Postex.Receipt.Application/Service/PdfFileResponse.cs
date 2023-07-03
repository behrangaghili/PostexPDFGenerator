using System;

namespace Postex.Receipt.Application
{
    public class PdfFileResponse
    {
        public string FileUrl { get; }
        public string FileName { get; }
        public string ContentType { get; }
        public byte[] FileContent { get; }

        public PdfFileResponse(string fileUrl, string fileName, string contentType, byte[] fileContent)
        {
            FileUrl = fileUrl;
            FileName = fileName;
            ContentType = contentType;
            FileContent = fileContent;
        }
    }
}
