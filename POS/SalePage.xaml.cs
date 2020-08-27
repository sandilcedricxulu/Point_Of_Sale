using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents.DocumentStructures;
using System.Windows.Controls;
using System.Windows.Data;
using System.Configuration;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using POS;
using models;
using DataLayer;
using System.Net;
using System.Net.Mail;
using System.ComponentModel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ExcelLibrary.BinaryDrawingFormat;
using ExcelLibrary.BinaryFileFormat;
using System.Text.RegularExpressions;


namespace POS
{
    /// <summary>
    /// Interaction logic for SalePage.xaml
    /// </summary>
    public partial class SalePage : Page
    {
        dataAccess data = new dataAccess();
        DataTable tableRecords = new DataTable();
        DataTable table;
        int count = 0;
        double sum = 0;
        double incr;

        int inv = 1, dcr = -1, empnum, slipNumber = 0;
        long barcode;
        double price;
        DateTime tiime;

        bool sameProduct = false;
        public SalePage()
        {
            InitializeComponent();
        }

        List<string> prodName = new List<string>();
        List<string> slip = new List<string>();
        List<double> amt = new List<double>();
        List<DateTime> time = new List<DateTime>();
        List<int> code = new List<int>();
        List<int> empno = new List<int>();
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            SalePage sale = new SalePage();
            main.ShowDialog();
        }


        //Parallel list to store prices along with their names

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            slipNumber++;
            products prod = new products();
            DataTable tbl = new DataTable();
            double totalAmount = 0.0;

            if (BarCode.Text.Equals("") || BarCode.Text == null)
            {
                MessageBox.Show("Enter the barcode you want to Scan");
            }
            else
            {
                tbl = data.getThatProduct(long.Parse(BarCode.Text));
                if (tbl.Rows.Count > 0)
                {
                    DataColumn c_column = new DataColumn("Quantity");
                    if (count < 1)
                    {
                        table = data.getDt();
                        count++;
                    }

                    DataRow row = data.getProduct(long.Parse(BarCode.Text));
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        if (long.Parse(BarCode.Text) == long.Parse(table.Rows[i]["Barcode"].ToString()))
                        {
                            table.Rows[i]["Quantity"] = Int32.Parse(table.Rows[i]["Quantity"].ToString()) + 1;
                            //dcr += int.Parse(table.Rows[i]["Quantity"].ToString());
                            incr = Double.Parse(table.Rows[i]["Price"].ToString()); 
                            table.Rows[i]["Price"] = Double.Parse(table.Rows[i]["Price"].ToString()) + Double.Parse(row["Price"].ToString());

                            for (int a = 0; a < prodName.Count; a++)
                            {
                                if (prodName[a].ToString().Equals(row["ProductName"].ToString()))
                                {
                                    amt[a] = Double.Parse(table.Rows[i]["Price"].ToString());
                                }
                            }

                            amt.Add(Double.Parse(table.Rows[i]["Price"].ToString()));
                            prodName.Add(String.Format(row["ProductName"].ToString()));
                            time.Add(DateTime.Now);
                            slip.Add(
                                       "----------------------------------- \n" +
                                        "Product Name : " + String.Format(row["ProductName"].ToString()) + " \n " +
                                        "Day : " + tiime + " \n" +
                                        "Price : " + Double.Parse(table.Rows[i]["Price"].ToString())
                                    );

                            sameProduct = true;
                        }
                    }
                    if (!sameProduct)
                    {
                        table.Rows.Add(row);
                        amt.Add(Double.Parse(row["Price"].ToString()));
                        prodName.Add(String.Format(row["ProductName"].ToString()));
                        time.Add(DateTime.Now);

                        slip.Add(
                                    "----------------------------------- \n" +
                                    "Product Name : " + String.Format(row["ProductName"].ToString()) + " \n " +
                                    "Day : " + tiime + " \n" +
                                    "Price : " + Double.Parse(row["Price"].ToString())
                                );
                    }

                    //totalLabel.Content = "R "+totalAmount;
                    invoiceGrid.ItemsSource = table.DefaultView;
                    sameProduct = false;

                }
                else if (tbl.Rows.Count < 0)
                {
                    MessageBox.Show("Entered Bar Code does not exist");
                }
            }
        }

        private void BtnDone_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = new DataTable();
            dataAccess data = new dataAccess();

            table = data.getAllProduct();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                for (int a = 0; a < prodName.Count; a++)
                {
                    if (table.Rows[i]["ProductName"].ToString().Equals(prodName[a].ToString()))
                    {
                        sum = sum + amt[a];
                        a = prodName.Count + 1000;
                    }
                }
            }

            totalLabel.Content = "R " + (sum + (sum * 0.15));

            //tableRecords = ((DataView)invoiceGrid.ItemsSource).ToTable();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            tenderedAmount.Text = "";
            changeLabel.Content = "r...";
            totalLabel.Content = "r...";
            invoiceGrid.ItemsSource = null;
            SalePage salePage = new SalePage();
            salePage._contentLoaded = false;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            supervisor super = new supervisor();
            super.Show();
        }

        private void Purchase_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(
                ConfigurationManager.ConnectionStrings["POS.Properties.Settings.Setting"].ConnectionString
                );
            DataSet dataSet = new DataSet();
            DataTable table = new DataTable();
            dataAccess data = new dataAccess();

            SqlCommand cmd = new SqlCommand();
            connection.Open();

            double change = 0;
            string usernaam = MainWindow.userN;

            if (tenderedAmount.Text.Equals("") || tenderedAmount.Text == null || BarCode.Text.Equals("") || BarCode.Text == null)
            {
                MessageBox.Show("BarCode/Tendered Amount are empty of not of correct type");
            }
            else
            {
                if (Double.Parse(tenderedAmount.Text) < sum)
                {
                    MessageBox.Show("Insufficient cash provided");
                }
                else if (Double.Parse(tenderedAmount.Text) > sum)
                {
                    change = Double.Parse(tenderedAmount.Text) - (sum + (sum * 0.15));
                    changeLabel.Content = "R " + change;


                    //Obtain values of the invoice table
                    string query = "SELECT * FROM Employee WHERE Username = '" + usernaam + "' ";
                    string secQuery = "";

                    for (int i = 0; i < prodName.Count; i++)
                    {
                        secQuery = "SELECT * FROM Product WHERE ProductName = '" + prodName[i].ToString() + "' ";
                        using (SqlDataAdapter adapter = new SqlDataAdapter())
                        {
                            cmd = new SqlCommand(secQuery, connection);
                            adapter.SelectCommand = cmd;
                            adapter.Fill(dataSet, "product");
                            table = dataSet.Tables["product"];

                            cmd = new SqlCommand(query, connection);
                            adapter.SelectCommand = cmd;
                            adapter.Fill(dataSet, "emp");
                            empnum = int.Parse(dataSet.Tables["emp"].Rows[0]["EmployeeNumber"].ToString());

                            foreach (DataRow row in table.Rows)
                            {
                                barcode = long.Parse(row["BarCode"].ToString());
                                price = Double.Parse(row["UnitPrice"].ToString());
                                tiime = time[i];

                                data.recordHistory(barcode, price, empnum, tiime);

                                inv++;
                            }
                        }

                    }
                    for (int i = 0; i < prodName.Count; i++)
                    {
                        secQuery = "SELECT * FROM Product WHERE ProductName = '" + prodName[i].ToString() + "' ";
                        int leftStock = 0, reorder;

                        using (SqlDataAdapter adapter = new SqlDataAdapter())
                        {
                            cmd = new SqlCommand(secQuery, connection);
                            adapter.SelectCommand = cmd;
                            adapter.Fill(dataSet, "product");
                            table = dataSet.Tables["product"];

                            foreach (DataRow row in table.Rows)
                            {
                                leftStock = Int32.Parse(row["UnitInStock"].ToString());
                                reorder = Int32.Parse(row["ReorderLevel"].ToString());
                                leftStock += dcr;
                                //string email = data.isAvailable(int.Parse(table.Rows[0]["SupplierID"].ToString()));

                                if (leftStock < reorder)
                                {
                                    //send an email
                                    NetworkCredential network;
                                    SmtpClient client;
                                    MailMessage message;

                                    network = new NetworkCredential("sm.ced.xulu@gmail.com", "Sx082@0797479084");
                                    client = new SmtpClient("smtp.gmail.com");//txtSmpt.Text
                                    client.Port = Int32.Parse("587");//
                                    client.EnableSsl = true;
                                    client.Credentials = network;
                                    message = new MailMessage { From = new MailAddress("sm.ced.xulu" + "smtp.gmail.com".Replace("smtp.", "@"), "Lucy", Encoding.UTF8) };
                                    message.To.Add(new MailAddress("sm.ced.xulu@gmail.com"));
                                    if (!string.IsNullOrEmpty(""))
                                        message.To.Add(new MailAddress(""));
                                    message.Subject = "Product Shortage";
                                    message.Body = "Dear " + prodName[i].ToString() + " \n" +
                                        "We Are in need of your stock so please get it for us Immediatly\n" +
                                        "Thank you";
                                    message.BodyEncoding = Encoding.UTF8;
                                    message.IsBodyHtml = true;
                                    message.Priority = MailPriority.Normal;
                                    message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                                    client.SendCompleted += new SendCompletedEventHandler(SendCompleteCallBack);
                                    string userstate = "Sending...";
                                    client.SendAsync(message, userstate);
                                }
                            }

                            string instockQuery = "UPDATE Product " +
                                "SET UnitInStock = " + leftStock + " " +
                                "WHERE  ProductName = '" + prodName[i].ToString() + "' ";

                            cmd = new SqlCommand(instockQuery, connection);
                            cmd.ExecuteNonQuery();
                        }

                    }
                }
            }

            tableRecords = ((DataView)invoiceGrid.ItemsSource).ToTable();
            for (int er = 0; er < tableRecords.Rows.Count; er++)
            {
                data.invoice(long.Parse(tableRecords.Rows[er]["BarCode"].ToString()), int.Parse(tableRecords.Rows[er]["Quantity"].ToString()),
                double.Parse(tableRecords.Rows[er]["Price"].ToString()), empnum);


                data = new dataAccess();
            }
            //Printi out A SLIIP
            DataTable prodFinder = new DataTable();
            DataTable empFinder = new DataTable();
            double totalSlipPrice = 0;
            List<string> listedItems = new List<string>();

            empFinder = data.getEmployee(empnum);
            string cashierName = empFinder.Rows[0]["EmployeeName"].ToString();
      
            string formalSlip = "";
            foreach (DataRow rw in tableRecords.Rows)
            {
                totalSlipPrice += double.Parse(rw["Price"].ToString());
            }
            foreach (DataRow rw in tableRecords.Rows)
            {
                formalSlip = "Bar Code: " + rw["BarCode"].ToString() + "\n" +
                    "Product Name: " + rw["ProductName"].ToString() + "\n" +
                    "Quantity: " + rw["Quantity"].ToString() + "\n" +
                    "Product(s) Price:" + rw["Price"] + "\n" +
                    "---------------------------------------\n";
                listedItems.Add(formalSlip);
            }

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream("C:/Users/smced/Music/Slip.pdf", FileMode.Create));
            doc.Open();
            totalSlipPrice += (totalSlipPrice * 0.15);
            listedItems.Add("Total Price :" + totalSlipPrice +"\n"+
                "Cashier Name: " + cashierName);
            
            Paragraph elements;

            for (int c = 0; c < listedItems.Count; c++)
            {
                elements = new Paragraph(listedItems[c]);
                doc.Add(elements);
            }

            doc.Close();
            MessageBox.Show("Slip Printed");


            connection.Close();
        }


        private static void SendCompleteCallBack(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show(string.Format("{0} send cancelled", e.UserState), "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if (e.Error != null)
            {
                MessageBox.Show(string.Format("{0} {1}", e.UserState, e.Error), "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                //MessageBox.Show("Your Message was successfully Sent", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
