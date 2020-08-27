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

namespace POS
{
    /// <summary>
    /// Interaction logic for AdminOptions.xaml
    /// </summary>
    public partial class AdminOptions : Page
    {
        public AdminOptions()
        {
            InitializeComponent();
            hrs.Content = DateTime.Now.TimeOfDay.Hours+" :";
            min.Content = DateTime.Now.TimeOfDay.Minutes;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            pageNav.Content = new AdminHome();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            pageNav.Content = new Cat();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            pageNav.Content = new Supp();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //adminOption product button
            pageNav.Content = new Prod();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            pageNav.Content = new emp();
        }
    }
}
