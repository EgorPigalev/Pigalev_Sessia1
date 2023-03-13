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
        Order order; // 
        List<ProductBasket> basket;
        double summa;
        double summaDiscount;
        int countDay;
        public Ticket(Order order, List<ProductBasket> basket, double summa, double summaDiscount, int countDay)
        {
            InitializeComponent();
            this.order = order;
            this.basket = basket;
            this.summa = summa;
            this.summaDiscount = summaDiscount;
            this.countDay = countDay;
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
            if(countDay == 3)
            {
                tbDeliveryDate.Text = tbDeliveryDate.Text + "3 дня";
            }
            else
            {
                tbDeliveryDate.Text = tbDeliveryDate.Text + "6 дней";
            }
        }

        private void btnBasket_Click(object sender, RoutedEventArgs e)
        {
            PdfDocument document = new PdfDocument();
            int height = 0;
            document.Info.Title = "Талон для получения заказа";
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont fontHeader = new XFont("Comic Sans MS", 18, XFontStyle.Bold);
            gfx.DrawString("Талон для получения заказа", fontHeader, XBrushes.Black,
                new XRect(10, height, page.Width, page.Height),
                XStringFormats.TopCenter);
            XFont font = new XFont("Comic Sans MS", 14);
            height += 30;
            gfx.DrawString("Номер: " + order.OrderNomer, font, XBrushes.Black,
                new XRect(10, height, page.Width, page.Height),
                XStringFormats.TopLeft);
            height += 30;
            gfx.DrawString("Дата заказа: " + order.OrderDate.ToString("D"), font, XBrushes.Black,
                new XRect(10, height, page.Width, page.Height),
                XStringFormats.TopLeft);
            height += 30;
            if(countDay == 3)
            {
                gfx.DrawString("Заказ будет готов через 3 дня", font, XBrushes.Black,
                    new XRect(10, height, page.Width, page.Height),
                    XStringFormats.TopLeft);
            }
            else
            {
                gfx.DrawString("Заказ будет готов через 6 дней", font, XBrushes.Black,
                    new XRect(10, height, page.Width, page.Height),
                    XStringFormats.TopLeft);
            }
            height += 30;
            gfx.DrawString("Дата получения заказа: " + order.OrderDeliveryDate.ToString("D"), font, XBrushes.Black,
                new XRect(10, height, page.Width, page.Height),
                XStringFormats.TopLeft);
            height += 30;
            gfx.DrawString("Состав заказа: ", font, XBrushes.Black,
                new XRect(10, height, page.Width, page.Height),
                XStringFormats.TopLeft);
            for (int i = 0; i < basket.Count; i++)
            {
                height += 30;
                if (i != basket.Count - 1)
                {
                    gfx.DrawString("" + basket[i].product.ProductName + " Колличество: " + basket[i].count + ";", font, XBrushes.Black,
                        new XRect(30, height, page.Width, page.Height),
                        XStringFormats.TopLeft);
                }
                else
                {
                    gfx.DrawString("" + basket[i].product.ProductName + " Колличество: " + basket[i].count + ".", font, XBrushes.Black,
                        new XRect(30, height, page.Width, page.Height),
                        XStringFormats.TopLeft);
                }
            }
            height += 30;
            gfx.DrawString("Сумма заказа: " + summa.ToString("0.00") + " руб.", font, XBrushes.Black,
                new XRect(10, height, page.Width, page.Height),
                XStringFormats.TopLeft);
            height += 30;
            gfx.DrawString("Сумма скидки: " + summaDiscount.ToString("0.00") + " руб.", font, XBrushes.Black,
                new XRect(10, height, page.Width, page.Height),
                XStringFormats.TopLeft);
            height += 30;
            gfx.DrawString("Пункт выдачи: " + order.PickupPoint.OrderPickupPoint, font, XBrushes.Black,
                new XRect(10, height, page.Width, page.Height),
                XStringFormats.TopLeft);
            height += 30;
            gfx.DrawString("Код для получения: " + order.OrderCode, fontHeader, XBrushes.Black,
                new XRect(10, height, page.Width, page.Height),
                XStringFormats.TopLeft);
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
