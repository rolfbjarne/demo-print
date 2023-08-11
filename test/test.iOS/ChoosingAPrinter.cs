using System;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;

namespace test.iOS
{
    public partial class ChoosingAPrinter : UITableViewController
    {
        UIBarButtonItem printButtonItem;
        public ChoosingAPrinter(UITableViewStyle style) : base(style)
        {
            printButtonItem = new UIBarButtonItem("Print1", UIBarButtonItemStyle.Bordered, SelectAPrinter);
            NavigationItem.RightBarButtonItem = printButtonItem;
        }

        void SelectAPrinter(object sender, EventArgs args)
        {
            CGRect rect = new CGRect(10, 10, 200, 300);
            //var printButtonItem1 = new UIBarButtonItem("Print1", UIBarButtonItemStyle.Bordered, SelectAPrinter);
            //NavigationItem.LeftBarButtonItem = printButtonItem;
            string printerUrl = string.Empty;
            UIPrinter uiPrinter = UIPrinter.FromUrl(new NSUrl(printerUrl));
            if (printerUrl == string.Empty)
            {
                ///UIPrinter uiPrinter = UIPrinter.FromUrl(new NSUrl(printerUrl));//printerUrl != null ? null as UIPrinter : UIPrinter.FromUrl(new NSUrl(printerUrl));
                var uiPrinterPickerController = UIPrinterPickerController.FromPrinter(null);
                //uiPrinterPickerController.PresentFromBarButtonItem()
                uiPrinterPickerController.PresentFromRect(rect, this.View, true, (printerPickerController, userDidSelect, error) =>//PresentFromBarButtonItem(printButtonItem1, false, (printerPickerController, userDidSelect, error) =>//Present(false, (printerPickerController, userDidSelect, error) =>
                {
                    if (userDidSelect)
                    {
                        uiPrinter = uiPrinterPickerController?.SelectedPrinter;
                        printerUrl = uiPrinter.Url.AbsoluteUrl.ToString();
                        //Console.WriteLine($"Save this UIPrinter's Url string for later use: {printerUrl}");
                        PrintFunc(uiPrinter);
                    }
                });
            }

            //this.DismissViewController(false, null);

        }

        void PrintFunc(UIPrinter uiPrinter)
        {
            var printInfo = UIPrintInfo.PrintInfo;
            printInfo.OutputType = UIPrintInfoOutputType.General;
            printInfo.JobName = "My first Print Job";
            printInfo.Orientation = UIPrintInfoOrientation.Landscape;
            var MarkupText1 = HtmlRepresentation(string.Empty);

            //UIWebView webView = new UIWebView()
            //{

            //};

            UIViewPrintFormatter vvv = new UIViewPrintFormatter()
            {

            };
            var textFormatter = new UIMarkupTextPrintFormatter(MarkupText1)
            {
                StartPage = 0,
                //ContentInsets = new UIEdgeInsets(0, 0, 0, 0),
                MaximumContentWidth = 1800,
                MaximumContentHeight = 800,
            };

            /*var textFormatter = new UISimpleTextPrintFormatter("Jack Jafari \n \n\nHost Name: Matt Pope \n\n Visitor Code: 220")
            {
                StartPage = 0,
                ContentInsets = new UIEdgeInsets(0, 0, 0, 0),
                MaximumContentWidth = 800,
                MaximumContentHeight = 160,
                //AttributedText = new NSAttributedString()
            };*/

            var printer = UIPrintInteractionController.SharedPrintController;
            printer.PrintInfo = printInfo;
            //printer.ChoosePaper += 
            //printer.PrintingItem = 
            printer.PrintFormatter = textFormatter;
            printer.ShowsPageRange = true;


            printer.PrintToPrinter(uiPrinter, (printInteractionController, completed, error) =>
            {
                /*if ((completed && error != null))
                {
                    Console.WriteLine($"Print Error: {error.Code}:{error.Description}");
                    PresentViewController(
                        UIAlertController.Create("Print Error", "Code: {error.Code} Description: {error.Description}", UIAlertControllerStyle.ActionSheet),
                        true, () => { });
                }*/
                printInfo?.Dispose();
                uiPrinter?.Dispose();
            });
            //printer.Dismiss(true);
            /*printer.Present(true, (handler, completed, err) => {
                if (!completed && err != null)
                {
                    Console.WriteLine("error");
                }
            });*/
        }

        public string HtmlRepresentation(string imageFilePath)
        {

            StringBuilder body = new StringBuilder("<!DOCTYPE html><html><body>");


            body.Append("<h2>Visitor Name</h2>");
            body.Append("<ul>");
            //foreach (var ingredient in Ingredients)
            body.AppendFormat("<li>{0}</li>", imageFilePath);
            body.Append("</ul>");



            body.Append("<h2>HostName</h2>");
            body.AppendFormat("<p>{0}</p>", "Matt Pope");


            //body.Append("</body></html>");
            //"/var/mobile/Containers/Data/Application/D1A14AF8-7491-4868-906E-CA43DBE050FF/Documents/Test/IMG_20180423_134201.jpg"
            //return body.ToString();
            body.Append("<p><img src=\"file:///var/mobile/Containers/Data/Application/D1A14AF8-7491-4868-906E-CA43DBE050FF/Documents/Test/IMG_20180423_134201.jpg\" alt=\"\" height=\"100\"/></p>");
            body.Append("</body></html>");
            return body.ToString();
        }

    }
}


