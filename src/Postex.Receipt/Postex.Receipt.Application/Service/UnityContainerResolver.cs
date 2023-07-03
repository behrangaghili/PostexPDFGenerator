using DinkToPdf;
using DinkToPdf.Contracts;
using Unity;

namespace Postex.Receipt.Application
{
    public class UnityContainerResolver
    {
        private UnityContainer container;

        public UnityContainerResolver()
        {
            container = new UnityContainer();
            RegisterTypes();
        }

        public void RegisterTypes()
        {
            container.RegisterType<IDocumentService, DocumentService>();
            container.RegisterInstance(typeof(IConverter), new STASynchronizedConverter(new PdfTools()), InstanceLifetime.Singleton);
            container.RegisterType<IReportService, ReportService>();
        }

        public ReportService Resolver()
        {
            return container.Resolve<ReportService>();
        }
    }
}