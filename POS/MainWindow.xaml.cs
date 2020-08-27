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
using System.Configuration;
using POS;
using DataLayer;
using models;

namespace POS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public static string user = "";
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dataAccess data = new dataAccess();

            if (username.Text == "" || password.Password.ToString() == "")
            {
                MessageBox.Show("Enter Fields");
            }
            else
            {
                string status = data.GetLogin(username.Text, password.Password.ToString());
                user = username.Text;

                if (status.Equals("Supervisor"))
                {
                    supervisor spv = new supervisor();
                    spv.ShowDialog();
                   Window.GetWindow(this).Close();
                }
                else if (status.Equals("Admin"))
                {
                    Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().Content = new AdminOptions();
                }
                else if (status.Equals("Cashier"))
                {
                    Application.Current.Windows.OfType<MainWindow>().FirstOrDefault().Content = new SalePage();
                }
                else if (status.Equals("NotFound"))
                {
                    MessageBox.Show("Unrecorgnized Username or Password !! , Contact your administrator");
                }
            }
        }

        public static string userN
        {
            get { return user; }
            set { user = value; }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
