using AspNetCore.Reporting;
using AspNetCore.Reporting.ReportExecutionService;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Models.Repository;
using WebApp.Security;

namespace WebApp.Controllers
{
    public class ReceiptController : Controller
    {
        private static List<Stream> m_streams;
        private static int m_currentPageIndex = 0;

        private readonly ISettingRepository settings;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IDataProtector protector;
        public ReceiptController(ISettingRepository _settings,
                                    UserManager<ApplicationUser> _userManager,
                                  IWebHostEnvironment _hostingEnvironment,
                                 IDataProtectionProvider dataProtectionProvider,
                                 DataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            settings = _settings;
            userManager = _userManager;
            hostingEnvironment = _hostingEnvironment;
            protector = dataProtectionProvider.CreateProtector(dataProtectionPurposeStrings.SiteSecurity);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PrintSettings()
        {
            try
            {
                int id = 1;
                string mimtype = "";
                int extension = 1;
                var path = $"{hostingEnvironment.ContentRootPath}\\Reports\\rpSettings.rdlc";
                LocalReport localReport = new LocalReport(path);
                localReport.AddDataSource("DsSetting", settings.Find(id));
                var result = localReport.Execute(RenderType.Pdf, extension, null, mimtype);

                return File(result.MainStream, "application/pdf");

            }
            catch (Exception ex)
            {
                //AddErrors(ex.Message);
            }
            return null;
        }
        public IActionResult PrintReceipt()
        {
            //string FilePathReturn = @"TempFiles/invoice.pdf" ;
            string FilePathReturn = @"invoice.pdf";

            return Content(FilePathReturn);
        }

        public static void PrintToPrinter(LocalReport report)
        {
            Export(report,true);

        }

        public static void Export(LocalReport report, bool print = true)
        {
            string deviceInfo =
             @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>3in</PageWidth>
                <PageHeight>8.3in</PageHeight>
                <MarginTop>0in</MarginTop>
                <MarginLeft>0.1in</MarginLeft>
                <MarginRight>0.1in</MarginRight>
                <MarginBottom>0in</MarginBottom>
            </DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            //report.Render()
            //report.Render("Image", deviceInfo, CreateStream, out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;

            if (print)
            {
                Print();
            }
        }


        public static void Print()
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }

        public static Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        public static void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);

            // Adjust rectangular area with printer margins.
            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        public static void DisposePrint()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }
    }
}
