using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pigalev_Sessia1
{
    /// <summary>
    /// Логика взаимодействия для ListProduct.xaml
    /// </summary>
    public partial class ListProduct : Page
    {
        List<ProductBasket> basket = new List<ProductBasket>();
        User user;
        public ListProduct(User user)
        {
            InitializeComponent();
            this.user = user;
            CreatingFields();
            tbFIO.Text = "" + user.UserSurname + " " + user.UserName + " " + user.UserPatronymic;
            if(user.Role.RoleName == "Менеджер" || user.Role.RoleName == "Администратор")
            {
                btnOrders.Visibility = Visibility.Visible;
            }
        }
        public ListProduct()
        {
            InitializeComponent();
            CreatingFields();
        }

        public void CreatingFields()
        {
            lvListProducts.ItemsSource = Base.baseDate.Product.ToList();
            cbFilt.SelectedIndex = 0;
            cbSort.SelectedIndex = 0;
            tbCountProduct.Text = "" + Base.baseDate.Product.ToList().Count() + " из " + Base.baseDate.Product.ToList().Count();
        }

        public void Filter()
        {
            List<Product> products = Base.baseDate.Product.ToList();
            if(tbSearch.Text.Length > 0)
            {
                products = products.Where(x => x.ProductName.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
            }
            if(cbFilt.SelectedIndex > 0)
            {
                switch(cbFilt.SelectedIndex)
                {
                    case 1:
                        products = products.Where(x => x.ProductDiscountAmount > 0 && x.ProductDiscountAmount < 9.99).ToList();
                        break;
                    case 2:
                        products = products.Where(x => x.ProductDiscountAmount > 10 && x.ProductDiscountAmount < 14.99).ToList();
                        break;
                    case 3:
                        products = products.Where(x => x.ProductDiscountAmount > 15).ToList();
                        break;
                }
            }
            if(cbSort.SelectedIndex > 0)
            {
                switch (cbSort.SelectedIndex)
                {
                    case 1:
                        products = products.OrderBy(x => x.costWithDiscount).ToList();
                        break;
                    case 2:
                        products = products.OrderByDescending(x => x.costWithDiscount).ToList();
                        break;
                }
            }
            lvListProducts.ItemsSource = products;
            if (products.Count == 0)
            {
                MessageBox.Show("Данные не найдены");
            }
            tbCountProduct.Text = "" + products.Count() + " из " + Base.baseDate.Product.ToList().Count();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.frame.Navigate(new Login());
        }

        private void miAddBasket_Click(object sender, RoutedEventArgs e)
        {
            Product x = (Product)lvListProducts.SelectedItem;
            bool stock = false;
            foreach(ProductBasket productBasket in basket)
            {
                if(productBasket.product == x)
                {
                    productBasket.count = productBasket.count+=1;
                    stock = true;
                }
            }
            if(!stock)
            {
                ProductBasket product = new ProductBasket();
                product.product = x;
                product.count = 1;
                basket.Add(product);
            }
            btnBasket.Visibility = Visibility.Visible;
        }

        private void btnBasket_Click(object sender, RoutedEventArgs e)
        {
            Basket basketWindow = new Basket(basket, user);
            basketWindow.ShowDialog();
            if(basket.Count == 0)
            {
                btnBasket.Visibility = Visibility.Collapsed;
            }
        }

        private void btnOrders_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.frame.Navigate(new ListOrders());
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                int index = Convert.ToInt32(btn.Uid);
                Product product = Base.baseDate.Product.FirstOrDefault(x => x.ProductID == index);
                List<OrderProduct> orderProducts = Base.baseDate.OrderProduct.Where(x => x.ProductID == index).ToList();
                if(orderProducts.Count == 0)
                {
                    foreach(OrderProduct orderProduct in orderProducts)
                    {
                        Base.baseDate.OrderProduct.Remove(orderProduct);
                    }
                    Base.baseDate.Product.Remove(product);
                    Base.baseDate.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Товар нельзя удалить так как он указан в заказе!");
                }
                
            }
            catch
            {
                MessageBox.Show("При удаление товара возникла ошибка");
            }
        }

        private void btnDelete_Loaded(object sender, RoutedEventArgs e)
        {
            if(user == null)
            {
                return;
            }
            Button btnDelete = sender as Button;
            if (user.Role.RoleName == "Менеджер" || user.Role.RoleName == "Администратор")
            {
                btnDelete.Visibility = Visibility.Visible;
            }
            else
            {
                btnDelete.Visibility = Visibility.Collapsed;
            }
        }
    }
}
