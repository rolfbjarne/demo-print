using System;
using System.Drawing.Printing;
using System.Runtime;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using test.iOS;
using test.Services;
using UIKit;
using Vision;
using WebKit;
using Xamarin.Forms;
using test.iOS;
using test.Template;

[assembly: Xamarin.Forms.Dependency(typeof(PrintJob))]
namespace test.iOS
{
	public class PrintJob: IPrinter
    {
        public PrintJob()
        {

        }
       public  void SelectPrinter()
       {
            UIPrintInteractionController printController = UIPrintInteractionController.SharedPrintController;
            var template = new htm_printer() { };
            var htmlString = template.GenerateString();
            Console.WriteLine (htmlString);

            var controller = UIPrintInteractionController.SharedPrintController;

            var printInfo = UIPrintInfo.PrintInfo;
            printInfo.JobName = "Print";
            printInfo.OutputType = UIPrintInfoOutputType.General;

            controller.PrintInfo = printInfo;
            var formatter = new UIMarkupTextPrintFormatter (htmlString);
            formatter.PerPageContentInsets = UIEdgeInsets.Zero;

            var renderer = new UIPrintPageRenderer ();
            renderer.AddPrintFormatter (formatter, 0);
            renderer.DrawPage (0, new CGRect (0, 0, 172, 172));

            controller.PrintPageRenderer = renderer;
            controller.ShowsPaperSelectionForLoadedPapers = true;
            controller.ShowsPageRange = true;

            try
            {
                controller.Present(true, PrintingCompleted);
            }
            catch (Exception ex)
            {

            }
        }

        public void Printer()
        {
            var uiPrinter = UIPrinter.FromUrl(new NSUrl("ipp://BRW8CC84B1C9863.local.:631/ipp/print"));
            var template = new htm_printer() { };
            var page = template.GenerateString();
            var formatter = new UIMarkupTextPrintFormatter(page);
            formatter.PerPageContentInsets = UIEdgeInsets.Zero;

            var renderer = new UIPrintPageRenderer();
            renderer.AddPrintFormatter(formatter, 0);

            var controller = UIPrintInteractionController.SharedPrintController;
            var printInfo = UIPrintInfo.PrintInfo;
            printInfo.JobName = "Print";
            printInfo.OutputType = UIPrintInfoOutputType.General;

            controller.PrintInfo = printInfo;
            controller.PrintPageRenderer = renderer;
            controller.ShowsPaperSelectionForLoadedPapers = true;

          
            controller.PrintToPrinter(uiPrinter, (printInteractionController, completed, error) =>
                    {
                        printInfo?.Dispose();
                        uiPrinter?.Dispose();
                    });

        }

        void PrintingCompleted(UIPrintInteractionController controller, bool completed, NSError error)
        {

        }
    }
}

