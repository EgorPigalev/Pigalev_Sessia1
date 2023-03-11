using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Pigalev_Sessia1
{
    public partial class Order
    {
        public string OrderList
        {
            get
            {
                List<OrderProduct> products = Base.baseDate.OrderProduct.Where(x => x.OrderID == OrderID).ToList();
                string orderList = "";
                for(int i = 0; i < products.Count; i++)
                {
                    if(i == products.Count - 1)
                    {
                        orderList = orderList + products[i].Product.ProductName + " Количество: " + products[i].Count;
                    }
                    else
                    {
                        orderList = orderList + products[i].Product.ProductName + " Количество: " + products[i].Count + "\n";
                    }
                }
                return orderList;
            }
        }
        public string Summa
        {
            get
            {
                List<OrderProduct> products = Base.baseDate.OrderProduct.Where(x => x.OrderID == OrderID).ToList();
                double summa = 0;
                foreach(OrderProduct product in products)
                {
                    summa = summa + ((double)product.Product.ProductCost * product.Product.costWithDiscount / 100) * product.Count;
                }
                return "" + summa.ToString("0.00");
            }
        }
        public string Discount
        {
            get
            {
                List<OrderProduct> products = Base.baseDate.OrderProduct.Where(x => x.OrderID == OrderID).ToList();
                double summaDiscount = 0;
                foreach (OrderProduct product in products)
                {
                    summaDiscount = summaDiscount + (product.Product.costWithDiscount * product.Count);
                }
                double summa = 0;
                foreach (OrderProduct product in products)
                {
                    summa = summa + ((double)product.Product.ProductCost * product.Count);
                }
                double procent = (summa - summaDiscount) / summa * 100;
                return "" + procent.ToString("0.00");
            }
        }
        public SolidColorBrush colorBackground
        {
            get
            {
                if (10 > 15)
                {
                    SolidColorBrush color = (SolidColorBrush)new BrushConverter().ConvertFromString("#7fff00");
                    return color;
                }
                return Brushes.White;
            }
        }
    }
}
