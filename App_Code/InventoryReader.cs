using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace YourNamespace
{
    public class InventoryReader
    {
        // Private field to store the database connection string
        private string conString;

        // Constructor to initialize the connection string
        public InventoryReader()
        {
            // Retrieve the connection string from the web.config file
            conString = ConfigurationManager.ConnectionStrings["BISInventoryCOMP235-ConnectionString"].ConnectionString;
        }

        // Method to retrieve inventory items based on a condition ID
        public SqlDataReader getInventory(int condition)
        {
            // Create a new SQL connection using the connection string
            SqlConnection con = new SqlConnection(conString);

            // Create a SQL command to select inventory items with a specific condition
            SqlCommand cmd = new SqlCommand("SELECT * FROM Inventory WHERE condition = @condition", con);

            // Add the condition parameter to the SQL command
            cmd.Parameters.AddWithValue("@condition", condition);

            // Open the database connection
            con.Open();

            // Execute the SQL command and return a SqlDataReader to read the results
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        // Method to update an inventory item
        public void updateInventory(int id, string serialNo, string itemName, DateTime purchased, string imagePath, int condition)
        {
            // Use a using statement to ensure the connection is properly disposed of
            using (SqlConnection con = new SqlConnection(conString))
            {
                // Create a SQL command to update an inventory item
                SqlCommand cmd = new SqlCommand("UPDATE Inventory SET SerialNo = @SerialNo, ItemName = @ItemName, Purchased = @Purchased, ImagePath = @ImagePath, condition = @condition WHERE Id = @Id", con);

                // Add parameters to the SQL command
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@SerialNo", serialNo);
                cmd.Parameters.AddWithValue("@ItemName", itemName);
                cmd.Parameters.AddWithValue("@Purchased", purchased);
                cmd.Parameters.AddWithValue("@ImagePath", imagePath);
                cmd.Parameters.AddWithValue("@condition", condition);

                // Open the database connection
                con.Open();

                // Execute the SQL command to update the inventory item
                cmd.ExecuteNonQuery();
            }
        }

        // Method to retrieve all conditions from the Condition table
        public SqlDataReader getCondition()
        {
            // Create a new SQL connection using the connection string
            SqlConnection con = new SqlConnection(conString);

            // Create a SQL command to select all conditions
            SqlCommand cmd = new SqlCommand("SELECT Id, Description FROM Condition", con);

            // Open the database connection
            con.Open();

            // Execute the SQL command and return a SqlDataReader to read the results
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        // Method to add a new inventory item
        public void addInventory(string serialNo, string itemName, DateTime purchased, string imagePath, int condition)
        {
            // Use a using statement to ensure the connection is properly disposed of
            using (SqlConnection con = new SqlConnection(conString))
            {
                // Create a SQL command to insert a new inventory item
                SqlCommand cmd = new SqlCommand("INSERT INTO Inventory (SerialNo, ItemName, Purchased, ImagePath, condition) VALUES (@SerialNo, @ItemName, @Purchased, @ImagePath, @condition)", con);

                // Add parameters to the SQL command
                cmd.Parameters.AddWithValue("@SerialNo", serialNo);
                cmd.Parameters.AddWithValue("@ItemName", itemName);
                cmd.Parameters.AddWithValue("@Purchased", purchased);
                cmd.Parameters.AddWithValue("@ImagePath", imagePath);
                cmd.Parameters.AddWithValue("@condition", condition);

                // Open the database connection
                con.Open();

                // Execute the SQL command to add the new inventory item
                cmd.ExecuteNonQuery();
            }
        }
    }
}