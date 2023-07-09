using DinkToPdf;
using DinkToPdf.Contracts;
namespace Postex.Receipt.Application
{
    public class DocumentService : IDocumentService
    {
        private GlobalSettings globalSettings;
        private ObjectSettings objectSettings;
        private WebSettings webSettings;
        private HeaderSettings headerSettings;
        private FooterSettings footerSettings;
        private readonly IConverter _converter;

        public DocumentService(IConverter converter)
        {
            objectSettings = new ObjectSettings();
            webSettings = new WebSettings();
            headerSettings = new HeaderSettings();
            footerSettings = new FooterSettings();
            globalSettings = new GlobalSettings();

            _converter = converter;
        }

        public async Task<byte[]> GeneratePdfReport(string template)
        {
            byte[] result;
            HtmlToPdfDocument htmlToPdfDocument;

            htmlToPdfDocument = new HtmlToPdfDocument()
            {
                GlobalSettings = GetGlobalSettings(),
                Objects = { GetObjectSettings(template) }
            };

            result = await Task.FromResult(_converter.Convert(htmlToPdfDocument));

            return result;
        }

        private GlobalSettings GetGlobalSettings()
        {
            globalSettings.ColorMode = ColorMode.Color;
          
            globalSettings.Orientation = Orientation.Portrait;
            globalSettings.PaperSize = PaperKind.A5;
            globalSettings.Margins = new MarginSettings { Top = 0.1, Bottom = 0.1, Left = 0.1, Right = 0.1, Unit = Unit.Inches };

            return globalSettings;
        }

        private WebSettings WebSettings()
        {
            webSettings.DefaultEncoding = "UTF-8";
            return webSettings;
        }

        private ObjectSettings GetObjectSettings(string template)
        {
            objectSettings.PagesCount = true;
            objectSettings.WebSettings = WebSettings();
            objectSettings.HtmlContent = template;
            objectSettings.HeaderSettings = HeaderSettings();
            objectSettings.FooterSettings = FooterSettings();

            return objectSettings;
        }

        private HeaderSettings HeaderSettings()
        {
/*            headerSettings.FontSize = 6;
            headerSettings.FontName = "Times New Roman";
            headerSettings.Right = "Page [page] of [toPage]";
            headerSettings.Left = "پستکس";*/
            /*headerSettings.Line = false;*/
           // headerSettings.HtmUrl = "postex.ir";

            return headerSettings;
        }

        private FooterSettings FooterSettings()
        {
/*            footerSettings.FontSize = 5;
            footerSettings.FontName = "Times New Roman";
            footerSettings.Center = "نسخه بهار 1402";*/
            // footerSettings.HtmUrl = "postex.ir";
/*            footerSettings.Line = false;
*/
            return footerSettings;
        }
    }
}