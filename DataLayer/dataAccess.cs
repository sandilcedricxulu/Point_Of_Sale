using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using models;

namespace DataLayer
{
    public class dataAccess
    {
        SqlCommand cmd;
        DataTable table;
        
        //Loging In

        public string isAvailable(long barcode)
        {
            SqlConnection connection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["POS.Properties.Settings.Setting"].ConnectionString
            );

            DataSet dataSet = new DataSet();
            string suppEmail = "";
            string query =" SELECT Supplier.Email FROM Supplier,Product WHERE Supplier.SupplierID = "+barcode+" " +
                " Group by Supplier.Email ";

            connection.Open();
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                SqlCommand cmd = new SqlCommand(query,connection);
                adapter.SelectCommand = cmd;
                adapter.Fill(dataSet, "supp");
                table = new DataTable();
                table = dataSet.Tables["supp"];

                suppEmail = table.Rows[0]["Email"].ToString();
            }

            return suppEmail;
        }

        public string GetLogin(string username,string password)
        {
            SqlConnection connection = new SqlConnection(
                ConfigurationManager.ConnectionStrings["POS.Properties.Settings.Setting"].ConnectionString
                );

            DataSet dataSet = new DataSet();
            string query = "SELECT * FROM Login WHERE Username = @Username";
            
            try
            {
                connection.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dataSet, "Login");
                    table = new DataTable();
                    table = dataSet.Tables["Login"];
                    string status="";
                    foreach(DataRow row in table.Rows)
                    {
                        if (row["Username"].ToString().Equals(username) && row["Password"].ToString().Equals(password))
                        {
                            if (row["Position"].ToString().Equals("1"))
                            {
                                status = "Supervisor";
                            }
                            else if (row["Position"].ToString().Equals("2"))
                            {
                                status = "Admin";
                            }
                            else if (row["Position"].ToString().Equals("3"))
                            {
                                status = "Cashier";
                            }
                            else
                            {
                                status = "NotFound";
                            }
                        }
                        else if (row["Username"].ToString() != username || row["Password"].ToString() != password)
                        {
                            status = "NotFound";
                        }
                    }
                    return status;
                }
                connection.Close();
            }
            catch(Exception e)
            {
                return "There was an error";
            }
        }

        //Insert Product
        public string insertProduct(long BarCode, string ProductName, int SupplierID, int CategoryID, int QuantityPerUnit, double UnitPrice, int UnitInStock, int ReorderLevel,string Discontinued)
        {
                SqlConnection connection = new SqlConnection(
                ConfigurationManager.ConnectionStrings["POS.Properties.Settings.Setting"].ConnectionString
                );
            try
            {
                connection.Open();
                string status = "";
                bool found = false;
                DataSet dataSet = new DataSet();
                string validationQuery = "SELECT * FROM Product WHERE BarCode = @BarCode";
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    SqlCommand command = new SqlCommand(validationQuery, connection);
                    command.Parameters.AddWithValue("@BarCode", BarCode);
                    adapter.SelectCommand = command;
                    adapter.Fill(dataSet, "Product");

                    foreach (DataRow row in dataSet.Tables["Product"].Rows)
                    {
                        if (row["BarCode"].ToString().Equals(BarCode.ToString()))
                        {
                            found = true;
                        }
                    }
                }
                if (found == true)
                {
                    status = "Unsuccessful";
                }
                else if (found == false)
                {
                    string query = "INSERT INTO Product(BarCode,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitInStock,ReorderLevel,Discontinued)" +
                    "VALUES(@BarCode,@ProductName,@SupplierID,@CategoryID,@QuantityPerUnit,@UnitPrice,@UnitInStock,@ReorderLevel,@Discontinued)";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@BarCode", BarCode);
                        cmd.Parameters.AddWithValue("@ProductName", ProductName);
                        cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
                        cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                        cmd.Parameters.AddWithValue("@QuantityPerUnit", QuantityPerUnit);
                        cmd.Parameters.AddWithValue("@UnitPrice", UnitPrice);
                        cmd.Parameters.AddWithValue("@UnitInStock", UnitInStock);
                        cmd.Parameters.AddWithValue("@ReorderLevel", ReorderLevel);
                        cmd.Parameters.AddWithValue("@Discontinued", Discontinued);

                        cmd.ExecuteNonQuery();
                        status = "Successful";
                    }
                }
                return status;
                connection.Close();
            }
            catch(Exception e)
            {
                e.ToString();
                return "Exception Error";
            }
        }

        //Adding Categories
        public string insertCategories(int CategoryID, string CategoryName,string Description)
        {
            SqlConnection connection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["POS.Properties.Settings.Setting"].ConnectionString
            );
            try
            {
                connection.Open();

                string status = "";
                bool found = false;
                DataSet dataSet = new DataSet();
                string validationQuery = "SELECT * FROM Category WHERE CategoryID = @CategoryID";
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    SqlCommand command = new SqlCommand(validationQuery, connection);
                    command.Parameters.AddWithValue("@CategoryID", CategoryID);
                    adapter.SelectCommand = command;
                    adapter.Fill(dataSet, "Category");

                    foreach (DataRow row in dataSet.Tables["Category"].Rows)
                    {
                        if (row["CategoryID"].ToString().Equals(CategoryID.ToString()))
                        {
                            found = true;
                        }
                    }
                }

                if(found == true)
                {
                    status = "Unsuccessful";
                }
                else if (found == false)
                {
                    string query = "INSERT INTO Category(CategoryID,CategoryName,Description)" +
                    "VALUES(@CategoryID,@CategoryName,@Description)";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                        cmd.Parameters.AddWithValue("@CategoryName", CategoryName);
                        cmd.Parameters.AddWithValue("@Description", Description);

                        cmd.ExecuteNonQuery();
                        status = "Successful";
                    }
                }

                connection.Close();
                return status;
            }
            catch (Exception e)
            {
                e.ToString();
                return "There was an Exceptional Error";
            }
        }

        //Adding Suppliers
        public string insertSupplier(int SupplierID, string CompanyName, string Address, string City, string Region, int PostalCode, string Country, Int64 Phone, string Email)
        {
            SqlConnection connection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["POS.Properties.Settings.Setting"].ConnectionString
            );
            try
            {
                connection.Open();
                string status = "";
                bool found = false;
                DataSet dataSet = new DataSet();
                string validationQuery = "SELECT * FROM Supplier WHERE SupplierID = @SupplierID";
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    SqlCommand command = new SqlCommand(validationQuery, connection);
                    command.Parameters.AddWithValue("@SupplierID", SupplierID);
                    adapter.SelectCommand = command;
                    adapter.Fill(dataSet, "Supp");

                    foreach (DataRow row in dataSet.Tables["Supp"].Rows)
                    {
                        if (row["SupplierID"].ToString().Equals(SupplierID.ToString()))
                        {
                            found = true;
                        }
                    }
                }

                if(found == true)
                {
                    status = "Unsuccessful";
                }
                else if (found == false)
                {
                    string query = "INSERT INTO Supplier(SupplierID,CompanyName,Address,City,Region,PostalCode,Country,Phone,Email)" +
                    "VALUES(@SupplierID,@CompanyName,@Address,@City,@Region,@PostalCode,@Country,@Phone,@Email)";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
                        cmd.Parameters.AddWithValue("@CompanyName", CompanyName);
                        cmd.Parameters.AddWithValue("@Address", Address);
                        cmd.Parameters.AddWithValue("@City", City);
                        cmd.Parameters.AddWithValue("@Region", Region);
                        cmd.Parameters.AddWithValue("@PostalCode", PostalCode);
                        cmd.Parameters.AddWithValue("@Country", Country);
                        cmd.Parameters.AddWithValue("@Phone", Phone);
                        cmd.Parameters.AddWithValue("@Email", Email);

                        cmd.ExecuteNonQuery();
                        status = "Successful";
                    }

                }
                return status;
                connection.Close();
            }
            catch (Exception e)
            {
                e.ToString();
                return "There was an exception Error";
            }
        }

        //insert employees
        //Adding Suppliers
        public string insertEmployee(int EmployeeNumber, long EmployeeID, string EmployeeName, string Address, string Email, string Username, string Password, string ReportTo, int PositionID)
        {
            SqlConnection connection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["POS.Properties.Settings.Setting"].ConnectionString
            );

            try
            {
                string status = "";
                bool found = false;
                connection.Open();
                DataSet dataSet = new DataSet();
                string validationQuery = "SELECT * FROM Employee WHERE EmployeeNumber = @ENUM";
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    SqlCommand command = new SqlCommand(validationQuery,connection);
                    command.Parameters.AddWithValue("@ENUM", EmployeeNumber);
                    adapter.SelectCommand = command;
                    adapter.Fill(dataSet,"Employee");

                    foreach (DataRow row in dataSet.Tables["Employee"].Rows)
                    {
                        if (row["EmployeeNumber"].ToString().Equals(EmployeeNumber.ToString()) || row["Username"].ToString().Equals(Username))
                        {
                            found = true;
                        }
                    }
                } 
                
                if(found == true)
                {
                    status = "Unsuccessful";
                }
                else if (found == false)
                {
                    string query = "INSERT INTO Employee(EmployeeNumber,EmployeeID,EmployeeName,Address,Email,Username,Password,ReportTo,PositionID)" +
                    "VALUES(@EmployeeNumber,@EmployeeID,@EmployeeName,@Address,@Email,@Username,@Password,@ReportTo,@PositionID)";

                    //Insert into login
                    string queryLogin = "INSERT INTO Login(Username,Password,Position) VALUES(@Username,@Password,@Position)";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeNumber", EmployeeNumber);
                        cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                        cmd.Parameters.AddWithValue("@EmployeeName", EmployeeName);
                        cmd.Parameters.AddWithValue("@Address", Address);
                        cmd.Parameters.AddWithValue("@Email", Email);
                        cmd.Parameters.AddWithValue("@Username", Username);
                        cmd.Parameters.AddWithValue("@Password", Password);
                        cmd.Parameters.AddWithValue("@ReportTo", ReportTo);
                        cmd.Parameters.AddWithValue("@PositionID", PositionID);

                        cmd.ExecuteNonQuery();
                        
                        status = "Successful";
                    }

                    using (SqlCommand cmd = new SqlCommand(queryLogin, connection)) 
                    {
                        cmd.Parameters.AddWithValue("@Username", Username);
                        cmd.Parameters.AddWithValue("@Password", Password);
                        cmd.Parameters.AddWithValue("@Position", PositionID);

                        cmd.ExecuteNonQuery();
                        status = "Successful";
                    }                   
                }
                return status;
                connection.Close();
            }
            catch (Exception e)
            {
                e.ToString();
                return "There was an exception Error";
            }
        }

        public void AddLogin(string Username, string Password, int PositionID)
        {
            SqlConnection connection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["POS.Properties.Settings.Setting"].ConnectionString
            );
            try
            {
                connection.Open();
                string query = "INSERT INTO Login(Username,Password,Position) " +
                                "VALUES(@Username,@Password,@Position)";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", Username);
                    cmd.Parameters.AddWithValue("@Password", Password);
                    cmd.Parameters.AddWithValue("@Position", PositionID);

                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        //Edit all cat
        public void EditCategory(categories categories)
        {
            SqlConnection connection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["POS.Properties.Settings.Setting"].ConnectionString
            );
            try
            {
                connection.Open();
                string query = "UPDATE Category SET CategoryName = '"+categories.CategoryName+"', " +
                    "Description = '"+categories.Description+"' " +
                    "WHERE CategoryID = "+categories.CategoryID+" ";

                using (cmd = new SqlCommand(query, connection))
                {
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        //Edit all employee
        public void editEmployee(employees employees)
        {
            SqlConnection connection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["POS.Properties.Settings.Setting"].ConnectionString
            );
            try
            {
                connection.Open();
                string query = "UPDATE Employee SET EmployeeID="+employees.EmployeeID+"," +
                    "EmployeeName ='"+employees.EmployeeName+"'," +
                    " Address = '"+employees.Address+"',Email = '"+employees.Email+"'," +
                    "Username='"+employees.Username+"',Password='"+employees.Password+"'," +
                    "ReportTo='"+employees.ReportTo+"',PositionID="+employees.PositionID+" " +
                    "WHERE EmployeeNumber="+employees.EmployeeNumber+" ";

                string query2 = "UPDATE Login SET Password='"+employees.Password+"', " +
                    "Position="+employees.PositionID+" " +
                    "WHERE Username='"+employees.Username+"'";

                using (cmd = new SqlCommand(query, connection))
                {
                    cmd.ExecuteNonQuery();
                }
                using (cmd = new SqlCommand(query2,connection))
                {
                    cmd.ExecuteNonQuery();
                }
                    connection.Close();
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        //Edit Suppliers
        public void editSuppliers(suppliers suppliers)
        {
            SqlConnection connection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["POS.Properties.Settings.Setting"].ConnectionString
            );
            try
            {
                connection.Open();
                string query = "UPDATE Supplier SET CompanyName='"+suppliers.CompanyName+"'," +
                    "Address='"+suppliers.Address+"',City='"+suppliers.City+"',Region='"+suppliers.Region+"'," +
                    "PostalCode="+suppliers.PostalCode+",Country='"+suppliers.Country+"'," +
                    "Phone="+suppliers.Phone+",Email='"+suppliers.Email+"' " +
                    "WHERE SupplierID="+suppliers.SupplierID+" ";

                using (cmd = new SqlCommand(query, connection))
                {
                    cmd.ExecuteNonQuery();
                }


                connection.Close();
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }


        //Edit products
        public void editProducts(products products)
        {
            SqlConnection connection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["POS.Properties.Settings.Setting"].ConnectionString
            );
            try
            {
                connection.Open();
                string query = "UPDATE Product SET ProductName='"+products.ProductName+ "'," +
                    "QuantityPerUnit="+products.QuantityPerUnit+ ",UnitPrice="+products.UnitPrice+ "," +
                    "UnitInStock="+products.UnitInStock+ ",ReorderLevel="+products.ReorderLevel+ " " +
                   " WHERE BarCode="+products.BarCode+"";

                using (cmd = new SqlCommand(query, connection))
                {
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }
        DataTable dt = new DataTable();

        public DataTable getDt()
        {
            return dt;
        }
        int count = 0;
        //Get product details
        public DataRow getProduct(long pk)
        {
            List<double> listTotal = new List<double>();
            SqlConnection connection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["POS.Properties.Settings.Setting"].ConnectionString
            );
            int countQ = 1;
            DataSet dataSet = new DataSet();
            DataRow row;
           
            DataColumn column;
            if(count < 1)
            {
                column = new DataColumn("Barcode");
                column.DataType = System.Type.GetType("System.Int64");
                dt.Columns.Add(column);

                column = new DataColumn("ProductName");
                column.DataType = System.Type.GetType("System.String");
                dt.Columns.Add(column);

                column = new DataColumn("Price");
                column.DataType = System.Type.GetType("System.Double");
                dt.Columns.Add(column);

                column = new DataColumn("Quantity");
                column.DataType = System.Type.GetType("System.Int16");
                dt.Columns.Add(column);

                //column = new DataColumn("TotalPrice");
                //column.DataType = System.Type.GetType("System.Double");
                //dt.Columns.Add(column);
                count++;
            }
            

            string query = "SELECT BarCode,ProductName,UnitPrice FROM Product WHERE BarCode=@BarCode";
            connection.Open();
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@BarCode", pk);
                adapter.SelectCommand = cmd;
                adapter.Fill(dataSet, "Product");
                table = new DataTable();
                table = dataSet.Tables["Product"];
                row = dt.NewRow();

                foreach(DataRow r in table.Rows)
                {
                    row["Barcode"] = long.Parse(r["BarCode"].ToString());
                    row["ProductName"] = r["ProductName"].ToString();
                    row["Price"] = Double.Parse(r["UnitPrice"].ToString());
                    row["Quantity"] = countQ;
                    //row["TotalPrice"] = (Double)(countQ) * (Double.Parse(r["UnitPrice"].ToString()));
                }
            }

            return row;
            cmd.Dispose();
            connection.Close();
        }

        
        //find product names for total calculations
        public DataTable getAllProduct()
        {
            SqlConnection connection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["POS.Properties.Settings.Setting"].ConnectionString
            );
            DataSet dataSet = new DataSet();
            table = new DataTable();
            string query = "SELECT * FROM Product";
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                cmd = new SqlCommand(query,connection);
                adapter.SelectCommand = cmd;
                adapter.Fill(dataSet, "product");
                table = dataSet.Tables["product"];
               
            }
            return table;
        }

        //INVOICE
        public void invoice(long BarCode,int Quantity,double totalPrice,int EmployeeNumber)
        {
            SqlConnection connection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["POS.Properties.Settings.Setting"].ConnectionString
            );

            try
            {
                connection.Open();
                string query = "INSERT INTO Invoices (BarCode,Quantity,TotalPrice,EmployeeNumber)" +
                    " VALUES(@BarCode,@Quantity,@TotalPrice,@EmployeeNumber)";

                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@BarCode",BarCode);
                    cmd.Parameters.AddWithValue("@Quantity", Quantity);
                    cmd.Parameters.AddWithValue("@TotalPrice", totalPrice);
                    cmd.Parameters.AddWithValue("@EmployeeNumber", EmployeeNumber);

                    cmd.ExecuteNonQuery();
                }

                 connection.Close();
            }
            catch (Exception e)
            {
                e.ToString();
                connection.Close();
            }

        }

        //History Methods
        public void recordHistory(long barcode,double price,int empnum,DateTime date)
        {
            SqlConnection connection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["POS.Properties.Settings.Setting"].ConnectionString
            );

            try
            {
                connection.Open();
                string query = "INSERT INTO History(BarCode,Price,EmployeeNumber,day) VALUES(@BarCode,@Price,@EmployeeNumber,@day)";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@BarCode", barcode);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@EmployeeNumber", empnum);
                    cmd.Parameters.AddWithValue("@day", date);

                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }


        public DataTable getEmployee(int empnum)
        {
            SqlConnection connection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["POS.Properties.Settings.Setting"].ConnectionString
            );
            DataSet dataSet = new DataSet();
            table = new DataTable();
            string query = "SELECT * FROM Employee WHERE EmployeeNumber = @EmployeeNumber";
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@EmployeeNumber", empnum);
                adapter.SelectCommand = cmd;
                adapter.Fill(dataSet, "emp");
                table = dataSet.Tables["emp"];
            }
            return table;
        }

        public DataTable getThatProduct(long prodnum)
        {
            SqlConnection connection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["POS.Properties.Settings.Setting"].ConnectionString
            );
            DataSet dataSet = new DataSet();
            table = new DataTable();
            string query = "SELECT * FROM Product WHERE BarCode = @BarCode";
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@BarCode", prodnum);
                adapter.SelectCommand = cmd;
                adapter.Fill(dataSet, "prod");
                table = dataSet.Tables["prod"];
            }
            return table;
        }
        public DataTable getCategory(categories cat)
        {
            SqlConnection connection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["POS.Properties.Settings.Setting"].ConnectionString
            );
            DataSet dataSet = new DataSet();
            table = new DataTable();
            string query = "SELECT * FROM Category WHERE CategoryID = @CategoryID";
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@CategoryID", cat.CategoryID);
                adapter.SelectCommand = cmd;
                adapter.Fill(dataSet, "cat");
                table = dataSet.Tables["cat"];
            }
            return table;
        }

        public DataTable getSupplier(int supp_id)
        {
            SqlConnection connection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["POS.Properties.Settings.Setting"].ConnectionString
            );
            DataSet dataSet = new DataSet();
            table = new DataTable();
            string query = "SELECT * FROM Supplier WHERE SupplierID = @SupplierID";
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@SupplierID", supp_id);
                adapter.SelectCommand = cmd;
                adapter.Fill(dataSet, "supp");
                table = dataSet.Tables["supp"];
            }
            return table;
        }


        public DataTable getInvoices()
        {
            SqlConnection connection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["POS.Properties.Settings.Setting"].ConnectionString
            );
            DataSet dataSet = new DataSet();
            table = new DataTable();
            string query = "SELECT * FROM History";
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                cmd = new SqlCommand(query, connection);
                adapter.SelectCommand = cmd;
                adapter.Fill(dataSet, "history");
                table = dataSet.Tables["history"];
            }
            return table;
        }

        public void deleteEmployee(employees emp)
        {
            SqlConnection connection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["POS.Properties.Settings.Setting"].ConnectionString
            );

            try
            {
                connection.Open();
                string query = "DELETE FROM Employee WHERE EmployeeNumber = @EmployeeNumber";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@EmployeeNumber", emp.EmployeeNumber);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        public void deleteSuppliers(suppliers supp)
        {
            SqlConnection connection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["POS.Properties.Settings.Setting"].ConnectionString
            );
            try
            {
                connection.Open();
                string query = "DELETE FROM Supplier WHERE SupplierID = @SupplierID";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@SupplierID", supp.SupplierID);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        public void deleteProduct(products prod)
        {
            SqlConnection connection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["POS.Properties.Settings.Setting"].ConnectionString
            );
            try
            {
                connection.Open();
                string query = "DELETE FROM Product WHERE BarCode = @BarCode";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@BarCode", prod.BarCode);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        public void deleteCat(categories cat)
        {
            SqlConnection connection = new SqlConnection(
            ConfigurationManager.ConnectionStrings["POS.Properties.Settings.Setting"].ConnectionString
            );

            try
            {
                connection.Open();
                string query = "DELETE FROM Category WHERE CategoryID = @CategoryID";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CategoryID", cat.CategoryID);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }
    }
}