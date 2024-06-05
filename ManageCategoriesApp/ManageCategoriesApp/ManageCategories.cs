using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageCategoriesApp
{
    public record Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
    public class ManageCategories
    {
        SqlConnection connection;
        SqlCommand command;
        //string connectionString = "Data Source=(local);Initial Catalog=MyStore;User ID=sa;Password=123;Trusted_Connection=True";
        //string connectionString = "Data Source=(local);Initial Catalog=MyStore;User ID=sa;Password=123;Trusted_Connection=True";
        string connectionString = "Server=(local);uid=sa;pwd=123;database=Mystore;Trust Server Certificate=True";
        public List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();
            connection = new SqlConnection(connectionString);
            string SQl = "SELECT  [CategoryID]\r\n      ,[CategoryName]\r\n  FROM [MyStore].[dbo].[Categories]\r\n";
            command = new SqlCommand(SQl, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        categories.Add(new Category
                        {
                            CategoryID = reader.GetInt32("CategoryID"),
                            CategoryName = reader.GetString("CategoryName")
                        });
                    }// end while
                }// en if



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally { connection.Close(); }
            return categories;
        }


        public void InsertCategory(Category category)
        {
            connection = new SqlConnection(connectionString);

            command = new SqlCommand("Insert Categories values(@CategoryName)", connection);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally { connection.Close(); }
        }


        public void UpdateCategory(Category category)
        {
            connection = new SqlConnection(connectionString);
            string SQL = "Update Categories set CategoryName=@CategoryName where CategoryID=@CategoryID";
            command = new SqlCommand(SQL, connection);
            command.Parameters.AddWithValue("@CategoryID", category.CategoryID);
            command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }//end UpdateCategory

        public void DeleteCategory(Category category)
        {
            connection = new SqlConnection(connectionString);
            string SQL = "Delete Categories where CategoryID=@CategoryID";
            command = new SqlCommand(SQL, connection);
            command.Parameters.AddWithValue("@CategoryID", category.CategoryID);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }


    }
}

