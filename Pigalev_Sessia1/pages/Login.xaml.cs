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
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void BtnAutorization_Click(object sender, RoutedEventArgs e)
        {
            User user = Base.baseDate.User.FirstOrDefault(x => x.UserLogin == tbLogin.Text && x.UserPassword == pbPassword.Password);
            if(user == null)
            {
                CAPTCHA captcha = new CAPTCHA(user);
                captcha.ShowDialog();
            }
            else
            {
                MessageBox.Show("Пользователь с таким логиным и паролем не найден!");
            }
        }
    }
}
