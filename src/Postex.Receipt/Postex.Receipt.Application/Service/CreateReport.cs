using System.Reflection;
using System.IO;

namespace Postex.Receipt.Application
{
    public class CreateReport
    {
        private readonly UnityContainerResolver _resolver;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IReportService _service;

        public CreateReport(UnityContainerResolver resolver, IHttpClientFactory httpClientFactory, IReportService service)
        {
            _resolver = resolver;
            _httpClientFactory = httpClientFactory;
            _service = service;
        }

        public async Task Create()
        {
            EmployeeViewModel model = new EmployeeViewModel();

            // Set model values
            model.Employee.EmpID = 100005;
            model.Employee.EmpName = "John Doe";
            model.Employee.EmpStatus = "Married";
            model.DependentsList.Add(new Dependent()
            {
                DepID = 100001,
                DepAge = 15,
                DepName = "Mark Hanson"
            });

            model.DependentsList.Add(new Dependent()
            {
                DepID = 100002,
                DepAge = 25,
                DepName = "Janet Mills"
            });

            // Get template source
            string templatePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "ReportTemplate.html");
            HttpClient httpClient = _httpClientFactory.CreateClient();
            //string template = await httpClient.GetStringAsync(templatePath);
            string template = File.ReadAllText(templatePath);

            // Replace template tokens with values from model
            if (!string.IsNullOrEmpty(template))
            {
                template = template.Replace("[EmpID]", model.Employee.EmpID.ToString());
                template = template.Replace("[EmpName]", model.Employee.EmpName);
                template = template.Replace("[EmpStatus]", model.Employee.EmpStatus);

                for (int i = 0; i < 2; i++)
                {
                    template = template.Replace($"[DepID{i + 1}]", model.DependentsList[i].DepID.ToString());
                    template = template.Replace($"[DepName{i + 1}]", model.DependentsList[i].DepName);
                    template = template.Replace($"[DepAge{i + 1}]", model.DependentsList[i].DepAge.ToString());
                }
            }

            // Generate the PDF report
            byte[] rptBytes = await _service.DocumentService.GeneratePdfReport(template);

            // Save the report
            string reportPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "EmployeeReport.pdf");
            File.WriteAllBytes(reportPath, rptBytes);

        }

        public byte[] GetReportBytes()
        {
            // Load the generated PDF report bytes from the saved file
            string reportPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "EmployeeReport.pdf");
            return File.ReadAllBytes(reportPath);
        }
    }
}
