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
    /// Interaction logic for emp.xaml
    /// </summary>
    public partial class emp : Page
    {
        public emp()
        {
            InitializeComponent();
            EmployeeNumber.IsEnabled = false;
            EmployeeNumber.IsReadOnly = true;
        }

        DataTable table;
        private void EmpAdd_Click(object sender, RoutedEventArgs e)
        {
            dataAccess data = new dataAccess();
            // MessageBox.Show("Continue to add a new employee ?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (EmployeeID.Text == "" || EmployeeName.Text == "" || Email.Text == "" || 
                Address.Text == "" || ReportTo.Text == "" || Username.Text == "" || PositionID.Text == "" ||
                Password.Password.ToString() == "")
            {
                MessageBox.Show("All Fields are required to be filled");
            }
            else
            {
                if (MessageBox.Show("Continue to add a new employee ??", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    //do no stuff
                }
                else
                {
                    //do Yes stuff
                    if (EmployeeNumber.IsEnabled == false && EmployeeNumber.IsReadOnly == true)
                    {
                        MessageBox.Show("Employee Number Field is Enabled now Enter the Employee Number of a new Employee");
                        EmployeeNumber.IsEnabled = true;
                        EmployeeNumber.IsReadOnly = false;

                        EmployeeName.IsEnabled = false;
                        EmployeeName.IsReadOnly = true;
                        EmployeeID.IsEnabled = false;
                        EmployeeID.IsReadOnly = true;
                        Address.IsEnabled = false;
                        Address.IsReadOnly = true;
                        Email.IsEnabled = false;
                        Email.IsReadOnly = true;
                        PositionID.IsEnabled = false;
                        PositionID.IsReadOnly = true;
                        Username.IsEnabled = false;
                        Username.IsReadOnly = true;
                        Password.IsEnabled = false;
                        ReportTo.IsEnabled = false;
                        ReportTo.IsReadOnly = true;

                        empSearch.IsEnabled = false;
                        empDelete.IsEnabled = false;
                        empEdit.IsEnabled = false;
                    }
                    else if (EmployeeNumber.IsEnabled == true && EmployeeNumber.IsReadOnly == false)
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
                            string status = data.insertEmployee(int.Parse(EmployeeNumber.Text), long.Parse(EmployeeID.Text), EmployeeName.Text,
                            Address.Text, Email.Text, Username.Text, Password.Password.ToString(), ReportTo.Text, int.Parse(PositionID.Text));

                            if (status.Equals("Successful"))
                            {
                                MessageBox.Show("New Employee Inserted");
                                EmployeeNumber.IsEnabled = false;
                                EmployeeNumber.IsReadOnly = true;

                                EmployeeName.IsEnabled = true;
                                EmployeeName.IsReadOnly = false;
                                EmployeeID.IsEnabled = true;
                                EmployeeID.IsReadOnly = false;
                                Address.IsEnabled = true;
                                Address.IsReadOnly = false;
                                Email.IsEnabled = true;
                                Email.IsReadOnly = false;
                                PositionID.IsEnabled = true;
                                PositionID.IsReadOnly = false;
                                Username.IsEnabled = true;
                                Username.IsReadOnly = false;
                                Password.IsEnabled = true;
                                ReportTo.IsEnabled = true;
                                ReportTo.IsReadOnly = false;

                                empSearch.IsEnabled = true;
                                empDelete.IsEnabled = true;
                                empEdit.IsEnabled = true;


                                EmployeeNumber.Text = "";
                                EmployeeName.Text = "";
                                EmployeeID.Text = "";
                                Address.Text = "";
                                Email.Text = "";
                                PositionID.Text = "";
                                Username.Text = "";
                                Password.Password = "";
                                ReportTo.Text = "";
                            }
                            else if (status.Equals("Unsuccessful"))
                            {
                                MessageBox.Show("Employee Number/Username Already Exist !! \n Try to use other values in these fields");
                                Username.IsEnabled = true;
                                Username.IsReadOnly = false;
                            }
                            else
                            {
                                MessageBox.Show("Exception Error");
                            }

                        }

                    }

                }
            }


        }

        private void EmpEdit_Click(object sender, RoutedEventArgs e)
        {
            dataAccess data = new dataAccess();
            employees emp = new employees();

            if (EmployeeNumber.Text == "")
            {
                MessageBox.Show("Cannot Edit without employee number");
            }
            else if (EmployeeNumber.Text == "" || EmployeeID.Text == "" || EmployeeName.Text == "" || Email.Text == "" ||
                Address.Text == "" || ReportTo.Text == "" || Username.Text == "" || PositionID.Text == "" || 
                Password.Password.ToString() == "")
            {
                MessageBox.Show("All Fields are required to be filled");
            }
            else
            {
                emp.EmployeeID = long.Parse(EmployeeID.Text);
                emp.EmployeeNumber = int.Parse(EmployeeNumber.Text);
                emp.EmployeeName = EmployeeName.Text;
                emp.Address = Address.Text;
                emp.Email = Email.Text;
                emp.Username = Username.Text;
                emp.Password = Password.Password.ToString();
                emp.PositionID = int.Parse(PositionID.Text);
                emp.ReportTo = ReportTo.Text;

                data.editEmployee(emp);
                MessageBox.Show("Employee Editted");
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //empSearch
            dataAccess data = new dataAccess();
            table = new DataTable();

            if (Search.Text == "" || Search.Text == null)
            {
                MessageBox.Show("Search Field required");
            }
            else
            {
                table = data.getEmployee(int.Parse(Search.Text));
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        EmployeeNumber.Text = row["EmployeeNumber"].ToString();
                        EmployeeName.Text = row["EmployeeName"].ToString();
                        EmployeeID.Text = row["EmployeeID"].ToString();
                        Address.Text = row["Address"].ToString();
                        Email.Text = row["Email"].ToString();
                        PositionID.Text = row["PositionID"].ToString();
                        Username.Text = row["Username"].ToString();
                        Password.Password = row["Password"].ToString();
                        ReportTo.Text = row["ReportTo"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Employee Not Found !!");
                }
            }
        }

        
        //only numerics in a field
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            EmployeeNumber.IsEnabled = false;
            EmployeeNumber.IsReadOnly = true;

            EmployeeName.IsEnabled = true;
            EmployeeName.IsReadOnly = false;
            EmployeeID.IsEnabled = true;
            EmployeeID.IsReadOnly = false;
            Address.IsEnabled = true;
            Address.IsReadOnly = false;
            Email.IsEnabled = true;
            Email.IsReadOnly = false;
            PositionID.IsEnabled = true;
            PositionID.IsReadOnly = false;
            Username.IsEnabled = true;
            Username.IsReadOnly = false;
            Password.IsEnabled = true;
            ReportTo.IsEnabled = true;
            ReportTo.IsReadOnly = false;

            empSearch.IsEnabled = true;
            empDelete.IsEnabled = true;
            empEdit.IsEnabled = true;


            EmployeeNumber.Text = "";
            EmployeeName.Text = "";
            EmployeeID.Text = "";
            Address.Text = "";
            Email.Text = "";
            PositionID.Text = "";
            Username.Text = "";
            Password.Password = "";
            ReportTo.Text = "";
        }

        private void EmpDelete_Click(object sender, RoutedEventArgs e)
        {
            dataAccess data = new dataAccess();
            employees emp = new employees();

            if (EmployeeNumber.Text == "" || EmployeeNumber.Text.Equals(null))
            {
                MessageBox.Show("Employee Number Field is empty");
            }
            else
            {
                emp.EmployeeNumber = int.Parse(EmployeeNumber.Text);
                emp.EmployeeName = EmployeeName.Text;
                emp.Address = Address.Text;
                emp.Email = Email.Text;
                emp.Username = Username.Text;
                emp.Password = Password.Password.ToString();
                emp.PositionID = int.Parse(PositionID.Text);
                emp.ReportTo = ReportTo.Text;

                data.deleteEmployee(emp);
                MessageBox.Show("Employee " + emp.EmployeeName + " Removed Successfully");
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
        }
    }
}
