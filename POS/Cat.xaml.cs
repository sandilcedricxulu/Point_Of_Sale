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
using System.Text.RegularExpressions;
using System.Data;

namespace POS
{
    /// <summary>
    /// Interaction logic for Cat.xaml
    /// </summary>
    public partial class Cat : Page
    {
        public Cat()
        {
            InitializeComponent();
            CategoryNumber.IsEnabled = false;
            CategoryNumber.IsReadOnly = true;
        }

        DataTable table;
        private void CatAdd_Click(object sender, RoutedEventArgs e)
        {
            if (CategoryName.Text == "" || CategoryNumber.Text =="" || CategoryDescription.Text == "")
            {
                MessageBox.Show("All fields are required");
            }
            else
            {
                dataAccess data = new dataAccess();
                if (MessageBox.Show("Continue to add a new category ??", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    //do no stuff
                }
                else
                {
                    if (CategoryNumber.IsEnabled == false && CategoryNumber.IsReadOnly == true)
                    {
                        MessageBox.Show("Category Number Field is Enabled now Enter the Category Number of a new Category");
                        CategoryNumber.IsEnabled = true;
                        CategoryNumber.IsReadOnly = false;

                        CategoryName.IsEnabled = false;
                        CategoryName.IsReadOnly = true;
                        CategoryDescription.IsEnabled = false;
                        CategoryDescription.IsReadOnly = true;

                        btnSearch.IsEnabled = false;
                        CatDelete.IsEnabled = false;
                        CatEdit.IsEnabled = false;
                    }
                    else if (CategoryNumber.IsEnabled == true && CategoryNumber.IsReadOnly == false)
                    {
                        string status = data.insertCategories(int.Parse(CategoryNumber.Text), CategoryName.Text, CategoryDescription.Text);

                        if (status.Equals("Unsuccessful"))
                        {
                            MessageBox.Show("This category already exists, Try Inserting a new one");
                            CategoryNumber.IsEnabled = true;
                            CategoryNumber.IsReadOnly = false;
                        }
                        else if (status.Equals("Successful"))
                        {
                            MessageBox.Show("New Category Inserted");
                            CategoryNumber.IsEnabled = false;
                            CategoryNumber.IsReadOnly = true;

                            CategoryName.IsReadOnly = false;
                            CategoryName.IsEnabled = true;
                            CategoryDescription.IsEnabled = true;
                            CategoryDescription.IsReadOnly = false;

                            btnSearch.IsEnabled = true;
                            CatDelete.IsEnabled = true;
                            CatEdit.IsEnabled = true;

                            CategoryNumber.Text = "";
                            CategoryName.Text = "";
                            CategoryDescription.Text = "";

                        }
                        else
                        {
                            MessageBox.Show("Exception Error");
                        }
                    }
                }
            }
        }

        private void CatEdit_Click(object sender, RoutedEventArgs e)
        {
            dataAccess data = new dataAccess();
            categories cat = new categories();

            if (CategoryName.Text == null || CategoryName.Text =="")
            {
                MessageBox.Show("Enter the category name");
            }
            else if (CategoryDescription.Text == null || CategoryDescription.Text == "")
            {
                MessageBox.Show("Enter the category description");
            }
            else
            {
                cat.CategoryID = int.Parse(CategoryNumber.Text);
                cat.CategoryName = CategoryName.Text;
                cat.Description = CategoryDescription.Text;
                data.EditCategory(cat);
                MessageBox.Show("Category Edited " + cat.CategoryName);
            }

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            dataAccess data = new dataAccess();
            categories cat = new categories();
            DataTable table = new DataTable();

            if (Search.Text == null || Search.Text == "")
            {
                MessageBox.Show("Category Number required");
            }
            else
            {
                cat.CategoryID = int.Parse(Search.Text);
                table = data.getCategory(cat);

                if (table.Rows.Count < 0)
                {
                    MessageBox.Show("Your Searched Category type does not exist !!");
                }
                else
                {
                    foreach (DataRow row in table.Rows)
                    {
                        CategoryNumber.Text = row["CategoryID"].ToString();
                        CategoryName.Text = row["CategoryName"].ToString();
                        CategoryDescription.Text = row["Description"].ToString();
                    }
                }
            }

        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            CategoryNumber.Text = "";
            CategoryName.Text = "";
            CategoryDescription.Text = "";
        }

        private void CatDelete_Click(object sender, RoutedEventArgs e)
        {
            dataAccess data = new dataAccess();
            categories cat = new categories();
            if (CategoryNumber.Text =="")
            {
                MessageBox.Show("Category ID field is Empty ,Enter The ID");
            }
            else
            {
                cat.CategoryID = int.Parse(CategoryNumber.Text);
                cat.CategoryName = CategoryName.Text;
                data.deleteCat(cat);
                MessageBox.Show("Category " + cat.CategoryName + " Has been removed successfully");
            }
        }


        private void CategoryNumber_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
        }
    }
}
