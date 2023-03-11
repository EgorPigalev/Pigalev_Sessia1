using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace Pigalev_Sessia1
{
    /// <summary>
    /// Логика взаимодействия для Ticket.xaml
    /// </summary>
    public partial class Ticket : Window
    {
        Order order;
        List<ProductBasket> basket;
        public Ticket(Order order, List<ProductBasket> basket, double summa, double summaDiscount)
        {
            InitializeComponent();
            this.order = order;
            this.basket = basket;
            tbOrderNomer.Text = tbOrderNomer.Text + order.OrderNomer.ToString();
            tbDateOrder.Text = tbDateOrder.Text + order.OrderDate.ToString("d");
            for(int i = 0; i < basket.Count; i++)
            {
                if(i == basket.Count - 1)
                {
                    tbOrders.Text = tbOrders.Text + basket[i].product.ProductName + " Количество: " + basket[i].count;
                }
                else
                {
                    tbOrders.Text = tbOrders.Text + basket[i].product.ProductName + " Количество: " + basket[i].count + "\n";
                }
            }
            tbSumma.Text = tbSumma.Text + summa.ToString("0.00") + " руб.";
            tbSummaDiscount.Text = tbSummaDiscount.Text + summaDiscount.ToString("0.00") + " руб.";
            tbOrderPickupPoint.Text = tbOrderPickupPoint.Text + order.PickupPoint.OrderPickupPoint;
            tbCode.Text = tbCode.Text + order.OrderCode.ToString();
        }

        private void btnBasket_Click(object sender, RoutedEventArgs e)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont fontHeader = new XFont("Comic Sans MS", 22, XFontStyle.Bold);
            gfx.DrawString("Талон для получения заказа", fontHeader, XBrushes.Black,
                new XRect(0, 0, page.Width, page.Height),
                XStringFormats.TopCenter);
            XRect rect = new XRect(40, 100, 250, 220);
            gfx.DrawRectangle(XBrushes.SeaShell, rect);
            XFont font = new XFont("Comic Sans MS", 18);
            gfx.DrawString("Номер заказа: " + order.OrderNomer, font, XBrushes.Black,
                new XRect(0, 0, page.Width, page.Height),
                XStringFormats.BaseLineLeft);
            string filename = "TicketPDF.pdf";
            document.Save(filename);
            Process.Start(filename);

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
