using System;
using UIKit;
using Foundation;

namespace PrinterSelectionApp
{
    public partial class PrinterSelectionViewController : UIViewController
    {
        private UIPrinterPickerController printerPickerController;

        public PrinterSelectionViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //printerPickerController = new UIPrinterPickerController(UIPrinterPickerControllerMode.SelectPrinter);
            printerPickerController.Present(true, (UIPrinterPickerController p, bool userDidSelect, NSError error) =>
            {
                if (userDidSelect)
                {
                    Console.WriteLine("Selected Printer: " + p.SelectedPrinter.DisplayName);
                }
                else
                {
                    Console.WriteLine("No Printer Selected");
                }
            });
        }
    }
}
