using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
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

namespace Pigalev_Sessia1
{
    /// <summary>
    /// Логика взаимодействия для Basket.xaml
    /// </summary>
    public partial class Basket : Window
    {
        User user;
        List<ProductBasket> bascet;
        public Basket(List<ProductBasket> bascet, User user)
        {
            InitializeComponent();
            if(user != null)
            {
                tbFIO.Text = "" + user.UserSurname + " " + user.UserName + " " + user.UserPatronymic;
            }
            this.bascet = bascet;
            this.user = user;
            lvProduct.ItemsSource = bascet;
            calculateSummaAndDiscount();
            cmbPickupPoint.ItemsSource = Base.baseDate.PickupPoint.ToList();
            cmbPickupPoint.SelectedValuePath = "OrderPickupPointID";
            cmbPickupPoint.DisplayMemberPath = "OrderPickupPoint";
            cmbPickupPoint.SelectedIndex = 0;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnBasket_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Order order = new Order();
                List<Order> orderLast = Base.baseDate.Order.OrderBy(x => x.OrderNomer).ToList();
                order.OrderNomer = orderLast[orderLast.Count - 1].OrderNomer + 1;
                order.OrderStatusID = Base.baseDate.OrderStatus.FirstOrDefault(x => x.OrderStatus1 == "Новый").OrderStatusID;
                order.OrderDate = DateTime.Now;
                if (getDeliveryTime())
                {
                    order.OrderDeliveryDate = order.OrderDate.AddDays(6);
                }
                else
                {
                    order.OrderDeliveryDate = order.OrderDate.AddDays(3);
                }
                order.OrderPickupPointID = (int)((Pigalev_Sessia1.PickupPoint)cmbPickupPoint.SelectedItem).OrderPickupPointID;
                if (user != null)
                {
                    order.UserID = user.UserID;
                }
                Random rand = new Random();
                string textCode = "";
                for (int i = 0; i < 3; i++)
                {
                    textCode = textCode + rand.Next(10).ToString();
                }
                order.OrderCode = Convert.ToInt32(textCode);
                Base.baseDate.Order.Add(order);
                Base.baseDate.SaveChanges();
                foreach (ProductBasket productBasket in bascet)
                {
                    OrderProduct orderProduct = new OrderProduct();
                    orderProduct.OrderID = order.OrderID;
                    orderProduct.ProductID = productBasket.product.ProductID;
                    orderProduct.Count = productBasket.count;
                    Base.baseDate.OrderProduct.Add(orderProduct);
                }
                Base.baseDate.SaveChanges();
                MessageBox.Show("Заказ успешно создан");
                bascet.Clear();
                this.Close();
            }
            catch
            {
                MessageBox.Show("При создание заказа возникла ошибка!");
            }
        }

        private bool getDeliveryTime()
        {
            foreach(ProductBasket productBasket in bascet)
            {
                if(productBasket.count < 3 || productBasket.product.ProductQuantityInStock < productBasket.count)
                {
                    return true;
                }
            }
            return false;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0)))
            {
                e.Handled = true;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);
            ProductBasket productBasket = bascet.FirstOrDefault(x => x.product.ProductID == index);
            bascet.Remove(productBasket);
            if(bascet.Count == 0)
            {
                this.Close();
            }
            lvProduct.Items.Refresh();
            calculateSummaAndDiscount();
        }

        private void calculateSummaAndDiscount()
        {
            double summa = 0;
            double summaDiscount = 0;
            foreach(ProductBasket productBasket in bascet)
            {
                summa += productBasket.count * productBasket.product.costWithDiscount;
                summaDiscount += productBasket.count * ((double)productBasket.product.ProductCost - productBasket.product.costWithDiscount);
            }
            tbSumma.Text = "Сумма заказа: " + summa.ToString("0.00") + " руб.";
            tbSummaDiscount.Text = "Сумма скидки: " + summaDiscount.ToString("0.00") + " руб.";
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            int index = Convert.ToInt32(tb.Uid);
            ProductBasket productBasket = bascet.FirstOrDefault(x => x.product.ProductID == index);
            if(tb.Text.Replace(" ", "") == "")
            {
                productBasket.count = 0;
            }
            else
            {
                productBasket.count = Convert.ToInt32(tb.Text);
            }
            if (productBasket.count == 0)
            {
                bascet.Remove(productBasket);
            }
            if (bascet.Count == 0)
            {
                this.Close();
            }
            lvProduct.Items.Refresh();
            calculateSummaAndDiscount();
        }
    }
}
