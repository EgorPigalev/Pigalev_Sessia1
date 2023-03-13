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
    }
}
