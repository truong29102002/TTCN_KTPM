using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Xps.Packaging;


namespace QLCHXE.Report
{
    /// <summary>
    /// Interaction logic for ReportReview.xaml
    /// </summary>
    public partial class ReportReview : Window
    {
        public ReportReview()
        {
            InitializeComponent();
            
        }



        public void PrintToPdf(string fileName, FixedDocument document)
        {
            var xpsDocument = new XpsDocument(fileName, FileAccess.Write);
            var writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
            writer.Write(document);
            xpsDocument.Close();

            var printServer = new LocalPrintServer();
            var printQueue = printServer.GetPrintQueue("Microsoft Print to PDF");
            var printTicket = printQueue.DefaultPrintTicket.Clone();
            printQueue.AddJob(fileName, fileName, false, printTicket);
        }

    }
}
