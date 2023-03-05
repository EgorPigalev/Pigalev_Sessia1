using Pigalev_Sessia1.classes;
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
        public ListProduct(User user)
        {
            InitializeComponent();
            CreatingFields();
            tbFIO.Text = "" + user.UserSurname + " " + user.UserName + " " + user.UserPatronymic;
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
    }
}
