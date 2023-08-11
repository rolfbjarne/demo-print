using System;
using Foundation;
using MonoTouch.Dialog;
using test.Template;
using UIKit;

namespace test.iOS
{
    public class GuestPrintPageRenderer : UIPrintPageRenderer
    {
        public override nint NumberOfPages => 1;

        public GuestPrintPageRenderer()
        {
            var template = new htm_printer() { };
            var page = template.GenerateString();
            var formatter = new UIMarkupTextPrintFormatter(page);
            formatter.PerPageContentInsets = UIEdgeInsets.Zero;

            AddPrintFormatter(formatter, 0);
        }
    }
}
