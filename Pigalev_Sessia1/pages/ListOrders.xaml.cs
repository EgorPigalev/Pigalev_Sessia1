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
    /// Логика взаимодействия для ListOrders.xaml
    /// </summary>
    public partial class ListOrders : Page
    {
        User user;
        public ListOrders()
        {
            InitializeComponent();
            getData();
        }

        public ListOrders(User user)
        {
            InitializeComponent();
            this.user = user;
            getData();
        }

        /// <summary>
        /// Заполнение начальных данных на странице
        /// </summary>
        private void getData()
        {
            lvListOrders.ItemsSource = Base.baseDate.Order.ToList();
            cbSort.SelectedIndex = 0;
            cbFilt.SelectedIndex = 0;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if(user != null)
            {
                FrameClass.frame.Navigate(new ListProduct(user));
            }
            else
            {
                FrameClass.frame.Navigate(new ListProduct());
            }
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        /// <summary>
        /// Фильтрация и сортировка списка заказов
        /// </summary>
        private void Filter()
        {
            List<Order> orders = Base.baseDate.Order.ToList();
            if (cbFilt.SelectedIndex > 0) // Если фильрация выбрана
            {
                switch (cbFilt.SelectedIndex)
                {
                    case 1:
                        orders = orders.Where(x => x.DiscountProcent > 0 && x.DiscountProcent < 10).ToList();
                        break;
                    case 2:
                        orders = orders.Where(x => x.DiscountProcent >= 10 && x.DiscountProcent < 15).ToList();
                        break;
                    case 3:
                        orders = orders.Where(x => x.DiscountProcent >= 15).ToList();
                        break;
                }
            }
            if (cbSort.SelectedIndex > 0) // Если выбрана сортировка
            {
                switch (cbSort.SelectedIndex)
                {
                    case 1:
                        orders = orders.OrderBy(x => x.Summa).ToList();
                        break;
                    case 2:
                        orders = orders.OrderByDescending(x => x.Summa).ToList();
                        break;
                }
            }
            lvListOrders.ItemsSource = orders;
            if (orders.Count == 0)
            {
                MessageBox.Show("Данные не найдены");
            }
        }

        private void btnChangeStatus_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int index = Convert.ToInt32(button.Uid);
            Order order = Base.baseDate.Order.FirstOrDefault(x => x.OrderID == index);
            ChangeStatusOrder changeStatus = new ChangeStatusOrder(order);
            changeStatus.ShowDialog();
        }

        private void btnChangeDeliveryDate_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int index = Convert.ToInt32(button.Uid);
            Order order = Base.baseDate.Order.FirstOrDefault(x => x.OrderID == index);
            ChangeOrderDeliveryDate changeOrderDeliveryDate = new ChangeOrderDeliveryDate(order);
            changeOrderDeliveryDate.ShowDialog();
        }
    }
}
