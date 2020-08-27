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
using System.Text.RegularExpressions;
using System.Data;
using POS;
using DataLayer;
using models;


namespace POS
{
    /// <summary>
    /// Interaction logic for Prod.xaml
    /// </summary>
    public partial class Prod : Page
    {
        public Prod()
        {
            InitializeComponent();
            ProductID.IsEnabled = false;
            ProductID.IsReadOnly = true;
        }
        DataTable table;
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (ProductID.Text == "" || ProductName.Text == "" || QuantityPerUnit.Text == "" || CategoryID.Text == "" ||
                UnitPrice.Text == "" || UnitInStock.Text == "" || ReorderLevel.Text == "" || SupplierID.Text == "" ||
                Discontinued.Text == "")
            {
                MessageBox.Show("All Fields are required to be filled");
            }
            else
            {
                dataAccess data = new dataAccess();
                products prod = new products();
                prod.BarCode = long.Parse(ProductID.Text);
                prod.ProductName = ProductName.Text;
                prod.QuantityPerUnit = int.Parse(QuantityPerUnit.Text);
                prod.ReorderLevel = int.Parse(ReorderLevel.Text);
                prod.UnitInStock = int.Parse(UnitInStock.Text);
                prod.UnitPrice = double.Parse(UnitPrice.Text);
                prod.Discontinued = Discontinued.Text;

                data.editProducts(prod);
                MessageBox.Show("Product " + prod.ProductName + " Modified Successfully");
            }
        }

        private void ProdAdd_Click(object sender, RoutedEventArgs e)
        {
            if (ProductID.Text == "" || ProductName.Text =="" || QuantityPerUnit.Text =="" || CategoryID.Text == "" || 
                UnitPrice.Text =="" || UnitInStock.Text =="" || ReorderLevel.Text =="" || SupplierID.Text =="" || 
                Discontinued.Text =="")
            {
                MessageBox.Show("All Fields are required to be filled");
            }
            else
            {
                dataAccess data = new dataAccess();
                if (MessageBox.Show("Continue to add a new Product ??", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    //do no stuff
                }
                else
                {
                    if (ProductID.IsEnabled == false && ProductID.IsReadOnly == true)
                    {
                        MessageBox.Show("Product Barcode Number Field is Enabled now Enter the Barcode of a new Product");
                        ProductID.IsEnabled = true;
                        ProductID.IsReadOnly = false;

                        ProductName.IsEnabled = false;
                        ProductName.IsReadOnly = true;
                        SupplierID.IsEnabled = false;
                        SupplierID.IsReadOnly = true;
                        CategoryID.IsEnabled = false;
                        CategoryID.IsReadOnly = true;
                        QuantityPerUnit.IsEnabled = false;
                        QuantityPerUnit.IsReadOnly = true;
                        UnitPrice.IsEnabled = false;
                        UnitPrice.IsReadOnly = true;
                        UnitInStock.IsEnabled = false;
                        UnitInStock.IsReadOnly = true;
                        ReorderLevel.IsEnabled = false;
                        ReorderLevel.IsReadOnly = true;
                        Discontinued.IsEnabled = false;
                        Discontinued.IsReadOnly = true;

                        ProdEdit.IsEnabled = false;
                        ProdDelete.IsEnabled = false;
                    }
                    else if (ProductID.IsEnabled == true && ProductID.IsReadOnly == false)
                    {

                        table = new DataTable();
                        categories cat = new categories();
                        cat.CategoryID = int.Parse(CategoryID.Text);
                        table = data.getCategory(cat);

                        for (int a = 0; a < table.Rows.Count; a++)
                        {
                            if (table.Rows[a]["CategoryID"].ToString().Equals(CategoryID.Text))
                            {
                                //Exist good

                                table = new DataTable();
                                table = data.getSupplier(int.Parse(SupplierID.Text));

                                for (int b = 0; b < table.Rows.Count; b++)
                                {
                                    if (table.Rows[b]["SupplierID"].ToString().Equals(SupplierID.Text))
                                    {
                                        // exist good
                                        string status = data.insertProduct(long.Parse(ProductID.Text), ProductName.Text, int.Parse(SupplierID.Text), Int32.Parse(CategoryID.Text),
                                        Int32.Parse(QuantityPerUnit.Text), double.Parse(UnitPrice.Text), Int32.Parse(UnitInStock.Text), Int32.Parse(ReorderLevel.Text),
                                        Discontinued.Text);

                                        if (status.Equals("Successful"))
                                        {
                                            MessageBox.Show("New Product Inserted");
                                            ProductID.IsEnabled = false;
                                            ProductID.IsReadOnly = true;

                                            ProductName.IsEnabled = true;
                                            ProductName.IsReadOnly = false;
                                            SupplierID.IsEnabled = true;
                                            SupplierID.IsReadOnly = false;
                                            CategoryID.IsEnabled = true;
                                            CategoryID.IsReadOnly = false;
                                            QuantityPerUnit.IsEnabled = true;
                                            QuantityPerUnit.IsReadOnly = false;
                                            UnitPrice.IsEnabled = true;
                                            UnitPrice.IsReadOnly = false;
                                            UnitInStock.IsEnabled = true;
                                            UnitInStock.IsReadOnly = false;
                                            ReorderLevel.IsEnabled = true;
                                            ReorderLevel.IsReadOnly = false;
                                            Discontinued.IsEnabled = true;
                                            Discontinued.IsReadOnly = false;

                                            ProdEdit.IsEnabled = true;
                                            ProdDelete.IsEnabled = true;

                                        }
                                        else if (status.Equals("Unsuccessful"))
                                        {
                                            MessageBox.Show("The Product Already Exist");
                                        }

                                    }
                                    else if (table.Rows[b]["SupplierID"].ToString() != (SupplierID.Text))
                                    {
                                        MessageBox.Show("Entered Supplier ID does not Exist");
                                    }
                                }
                            }
                            else if (table.Rows[a]["CategoryID"].ToString() != (CategoryID.Text))
                            {
                                MessageBox.Show("Entered Category ID does not exist");
                            }
                        }


                    }
                }
            }

            //data.insertProduct(Int32.Parse(ProductID.Text), ProductName.Text, int.Parse(SupplierID.Text), Int32.Parse(CategoryID.Text),
            //    Int32.Parse(QuantityPerUnit.Text), double.Parse(UnitPrice.Text), Int32.Parse(UnitInStock.Text), Int32.Parse(ReorderLevel.Text),
            //    Discontinued.Text);
            //MessageBox.Show("Products Inserted");
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {

            dataAccess data = new dataAccess();
            table = new DataTable();

            if (Search.Text == "" || Search.Text == null)
            {
                MessageBox.Show("Search Field required");
            }
            else
            {
                table = data.getThatProduct(long.Parse(Search.Text));
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        ProductID.Text = row["BarCode"].ToString();
                        ProductName.Text = row["ProductName"].ToString();
                        QuantityPerUnit.Text = row["QuantityPerUnit"].ToString();
                        CategoryID.Text = row["CategoryID"].ToString();
                        UnitPrice.Text = row["UnitPrice"].ToString();
                        UnitInStock.Text = row["UnitInStock"].ToString();
                        ReorderLevel.Text = row["ReorderLevel"].ToString();
                        SupplierID.Text = row["SupplierID"].ToString();
                        Discontinued.Text = row["Discontinued"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Product Not Found !!");
                }
            }
        }

        private void ProdDelete_Click(object sender, RoutedEventArgs e)
        {
            dataAccess data = new dataAccess();
            products prod = new products();

            if (ProductID.Text == "")
            {
                MessageBox.Show("Product ID field is Empty , Enter Product ID");
            }
            else
            {
                prod.BarCode = int.Parse(ProductID.Text);
                prod.ProductName = ProductName.Text;
                data.deleteProduct(prod);
                MessageBox.Show("Product " + prod.ProductName + " Removed Successfully");
            }

        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ProductID.Text = "";
            ProductName.Text = "";
            QuantityPerUnit.Text = "";
            CategoryID.Text = "";
            UnitPrice.Text = "";
            UnitInStock.Text = "";
            ReorderLevel.Text = "";
            SupplierID.Text = "";
            Discontinued.Text = "";

            ProductID.IsEnabled = false;
            ProductID.IsReadOnly = true;

            ProductName.IsEnabled = true;
            ProductName.IsReadOnly = false;
            SupplierID.IsEnabled = true;
            SupplierID.IsReadOnly = false;
            CategoryID.IsEnabled = true;
            CategoryID.IsReadOnly = false;
            QuantityPerUnit.IsEnabled = true;
            QuantityPerUnit.IsReadOnly = false;
            UnitPrice.IsEnabled = true;
            UnitPrice.IsReadOnly = false;
            UnitInStock.IsEnabled = true;
            UnitInStock.IsReadOnly = false;
            ReorderLevel.IsEnabled = true;
            ReorderLevel.IsReadOnly = false;
            Discontinued.IsEnabled = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
        }
    }
}
