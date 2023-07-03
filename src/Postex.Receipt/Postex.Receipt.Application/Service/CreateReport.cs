
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Postex.Receipt.Application.Service
{
   
    public class CreateReport
    {
       public async  void Create()
        {
            UnityContainerResolver resolver;
            EmployeeViewModel model;
            string template;
            string reportPath;
            string templatePath;
            WebClient webClient;
            IReportService service;
            byte[] rptBytes;

            webClient = new WebClient();
            model = new EmployeeViewModel();
            resolver = new UnityContainerResolver();

            //set model values
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

            //get template source
            templatePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "ReportTemplate.html");
            template = webClient.DownloadString(templatePath);

            //replace template tokens with values from model
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

            //generate the pdf report
            service = resolver.Resolver();
            rptBytes = await service.DocumentService.GeneratePdfReport(template);
            reportPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "EmployeeReport.pdf");

            //save report
            File.WriteAllBytes(reportPath, rptBytes);
           
        }
    }
}
