﻿using System;
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
using System.Windows.Shapes;

namespace Pigalev_Sessia1
{
    /// <summary>
    /// Логика взаимодействия для ChangeStatusOrder.xaml
    /// </summary>
    public partial class ChangeStatusOrder : Window
    {
        Order order;
        public ChangeStatusOrder(Order order)
        {
            InitializeComponent();
            this.order = order;
            tbHeader.Text = tbHeader.Text + order.OrderNomer;
            cbStatus.ItemsSource = Base.baseDate.OrderStatus.ToList();
            cbStatus.SelectedValuePath = "OrderStatusID";
            cbStatus.DisplayMemberPath = "OrderStatus1";
            cbStatus.SelectedValue = order.OrderStatusID;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnBasket_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                order.OrderStatusID = cbStatus.SelectedIndex + 1;
                Base.baseDate.SaveChanges();
                this.Close();
            }
            catch
            {
                MessageBox.Show("При изменение даты доставки возникла ошибка!");
            }
        }
    }
}
