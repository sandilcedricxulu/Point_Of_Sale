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
using System.Data;
using POS;
using DataLayer;
using models;
using System.Text.RegularExpressions;

namespace POS
{
    /// <summary>
    /// Interaction logic for history.xaml
    /// </summary>
    public partial class history : Page
    {
        public history()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dataAccess data = new dataAccess();
            DataTable table = new DataTable();
            table = data.getInvoices();
            if(table.Rows.Count < 0)
            {
                MessageBox.Show("History");
            }
            else
            {
                historyGrid.ItemsSource = table.DefaultView;
            }


        }
    }
}
