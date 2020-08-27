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
using System.Data;
using POS;
using DataLayer;
using models;
using System.Text.RegularExpressions;

namespace POS
{
    /// <summary>
    /// Interaction logic for Supp.xaml
    /// </summary>
    public partial class Supp : Page
    {
        public Supp()
        {
            InitializeComponent();
            SupplierID.IsEnabled = false;
            SupplierID.IsReadOnly = true;
        }

        DataTable table;
        private void SuppAdd_Click(object sender, RoutedEventArgs e)
        {
            if (SupplierID.Text == "" || CompanyName.Text == "" || Address.Text == "" || City.Text == "" || Region.Text == "" ||
                Postal.Text == "" || Country.Text == "" || Email.Text == "" || Phone.Text == "")
            {
                MessageBox.Show("All Fields are required");
            }
            else
            {
                dataAccess data = new dataAccess();
                if (MessageBox.Show("Continue to add a new supplier ??", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    //do no stuff
                }
                else
                {
                    //if Yes
                    if (SupplierID.IsEnabled == false && SupplierID.IsReadOnly == true)
                    {
                        MessageBox.Show("Supplier Identity Number Field is Enabled now Enter the Employee Number of a new Employee");
                        SupplierID.IsEnabled = true;
                        SupplierID.IsReadOnly = false;

                        CompanyName.IsEnabled = false;
                        CompanyName.IsReadOnly = true;
                        Address.IsEnabled = false;
                        Address.IsReadOnly = true;
                        City.IsEnabled = false;
                        City.IsReadOnly = true;
                        Region.IsEnabled = false;
                        Region.IsReadOnly = true;
                        Postal.IsEnabled = false;
                        Postal.IsReadOnly = true;
                        Country.IsEnabled = false;
                        Country.IsReadOnly = true;
                        Email.IsEnabled = false;
                        Email.IsReadOnly = true;
                        Phone.IsEnabled = false;
                        Phone.IsReadOnly = true;

                        SuppEdit.IsEnabled = false;
                        SuppDelete.IsEnabled = false;
                        btnClear.IsEnabled = false;

                    }
                    else if (SupplierID.IsEnabled == true && SupplierID.IsReadOnly == false)
                    {

                        if (Email.Text.Length == 0)
                        {
                            MessageBox.Show("Enter an email.");
                            Email.Focus();
                        }
                        else if (!Regex.IsMatch(Email.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
                        {
                            MessageBox.Show("Enter a valid email.");
                            Email.Select(0, Email.Text.Length);
                            Email.Focus();

                            Email.IsEnabled = true;
                            Email.IsReadOnly = false;
                        }
                        else
                        {

                            string status = data.insertSupplier(int.Parse(SupplierID.Text), CompanyName.Text, Address.Text, City.Text, Region.Text,
                            int.Parse(Postal.Text), Country.Text, Int64.Parse(Phone.Text), Email.Text);

                            if (status.Equals("Successful"))
                            {
                                MessageBox.Show("New Supplier Inserted");
                                SupplierID.IsEnabled = false;
                                SupplierID.IsReadOnly = true;

                                CompanyName.IsEnabled = true;
                                CompanyName.IsReadOnly = false;
                                Address.IsEnabled = true;
                                Address.IsReadOnly = false;
                                City.IsEnabled = true;
                                City.IsReadOnly = false;
                                Region.IsEnabled = true;
                                Region.IsReadOnly = false;
                                Postal.IsEnabled = true;
                                Postal.IsReadOnly = false;
                                Country.IsEnabled = true;
                                Country.IsReadOnly = false;
                                Email.IsEnabled = true;
                                Email.IsReadOnly = false;
                                Phone.IsEnabled = true;
                                Phone.IsReadOnly = false;

                                SuppEdit.IsEnabled = true;
                                SuppDelete.IsEnabled = true;
                                btnClear.IsEnabled = true;

                                SupplierID.Text = "";
                                CompanyName.Text = "";
                                Address.Text = "";
                                City.Text = "";
                                Region.Text = "";
                                Postal.Text = "";
                                Country.Text = "";
                                Email.Text = "";
                                Phone.Text = "";
                            }
                            else if (status.Equals("Unsuccessful"))
                            {
                                MessageBox.Show("The supplier already exist ");
                            }
                        }

                    }

                }
            }
        }

        private void SuppEdit_Click(object sender, RoutedEventArgs e)
        {
            dataAccess data = new dataAccess();
            suppliers supp = new suppliers();

            if (SupplierID.Text == "" || CompanyName.Text =="" || Address.Text =="" || City.Text == "" || Region.Text =="" ||
                Postal.Text =="" || Country.Text =="" || Email.Text =="" || Phone.Text =="")
            {
                MessageBox.Show("All Fields are required");
            }
            else
            {
                supp.Address = Address.Text;
                supp.City = City.Text;
                supp.CompanyName = CompanyName.Text;
                supp.Country = Country.Text;
                supp.Email = Email.Text;
                supp.Phone = long.Parse(Phone.Text);
                supp.PostalCode = int.Parse(Postal.Text);
                supp.Region = Region.Text;
                supp.SupplierID = int.Parse(SupplierID.Text);

                data.editSuppliers(supp);
                MessageBox.Show("Suppliers" + supp.CompanyName + " Modified Successfully");
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            SupplierID.Text = "";
            CompanyName.Text = "";
            Address.Text = "";
            City.Text = "";
            Region.Text = "";
            Postal.Text = "";
            Country.Text = "";
            Email.Text = "";
            Phone.Text = "";

            SupplierID.IsEnabled = false;
            SupplierID.IsReadOnly = true;

            CompanyName.IsEnabled = true;
            CompanyName.IsReadOnly = false;
            Address.IsEnabled = true;
            Address.IsReadOnly = false;
            City.IsEnabled = true;
            City.IsReadOnly = false;
            Region.IsEnabled = true;
            Region.IsReadOnly = false;
            Postal.IsEnabled = true;
            Postal.IsReadOnly = false;
            Country.IsEnabled = true;
            Country.IsReadOnly = false;
            Email.IsEnabled = true;
            Email.IsReadOnly = false;
            Phone.IsEnabled = true;
            Phone.IsReadOnly = false;

            SuppEdit.IsEnabled = true;
            SuppDelete.IsEnabled = true;
            btnClear.IsEnabled = true;
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            //Search Supplier
            dataAccess data = new dataAccess();
            table = new DataTable();

            if (Search.Text.Equals("") || Search.Text.Equals(null))
            {
                MessageBox.Show("Search Field is Empty , Enter the Search Field Value");
            }
            else
            {
                table = data.getSupplier(int.Parse(Search.Text));
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        SupplierID.Text = row["SupplierID"].ToString();
                        CompanyName.Text = row["CompanyName"].ToString();
                        Address.Text = row["Address"].ToString();
                        City.Text = row["City"].ToString();
                        Region.Text = row["Region"].ToString();
                        Postal.Text = row["PostalCode"].ToString();
                        Country.Text = row["Country"].ToString();
                        Email.Text = row["Email"].ToString();
                        Phone.Text = row["Phone"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Employee Not Found !!");
                }
            }
        }

        private void SuppDelete_Click(object sender, RoutedEventArgs e)
        {
            if (SupplierID.Text == "")
            {
                MessageBox.Show("Supplier ID field is Empty");
            }
            else
            {
                dataAccess data = new dataAccess();
                suppliers supp = new suppliers();
                supp.SupplierID = int.Parse(SupplierID.Text);
                supp.CompanyName = CompanyName.Text;

                data.deleteSuppliers(supp);
                MessageBox.Show("Supplier " + supp.CompanyName + " Removed Successfully");
            }

        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
        }
    }
}
