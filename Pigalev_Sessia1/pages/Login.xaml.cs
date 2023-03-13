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
using System.Windows.Threading;

namespace Pigalev_Sessia1
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        int kolError;
        public static bool correctValue;
        int countTime; // Время для повторного получения кода
        DispatcherTimer disTimer = new DispatcherTimer();
        public Login()
        {
            InitializeComponent();
            kolError = 0; // кол-во неудачных входов
            correctValue = false; // корректность ввода капчи
            disTimer.Interval = new TimeSpan(0, 0, 1); // интервал времени для таймера
            disTimer.Tick += new EventHandler(DisTimer_Tick);
        }

        private void BtnAutorization_Click(object sender, RoutedEventArgs e)
        {
            User user = Base.baseDate.User.FirstOrDefault(x => x.UserLogin == tbLogin.Text && x.UserPassword == pbPassword.Password);
            if(user != null)
            {
                FrameClass.frame.Navigate(new ListProduct(user));
            }
            else
            {
                if (kolError == 0)
                {
                    MessageBox.Show("Пользователь с таким логиным и паролем не найден!");
                    kolError++;
                }
                else
                {
                    CAPTCHA captcha = new CAPTCHA();
                    captcha.ShowDialog();
                    kolError++;
                    if (!correctValue) // Если капча не пройдена
                    {
                        BtnAutorization.IsEnabled = false;
                        countTime = 10;
                        tbNewCode.Text = "Получить новый код можно будет через " + countTime + " секунд";
                        tbNewCode.Visibility = Visibility.Visible;
                        disTimer.Start();
                    }
                }
            }
        }

        private void BtnGuest_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.frame.Navigate(new ListProduct());
        }
        private void DisTimer_Tick(object sender, EventArgs e)
        {
            if (countTime == 0) // Если 10 секунд закончились
            {
                BtnAutorization.IsEnabled = true;
                disTimer.Stop();
                tbNewCode.Visibility = Visibility.Collapsed;
            }
            else
            {
                tbNewCode.Text = "Получить новый код можно будет через " + countTime + " секунд";
            }
            countTime--;
        }
    }
}
