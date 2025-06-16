using System.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

public class Northwinddb
{
    public static DataTable Customers()
    {
        // Create a new connection (SqliteConnection) pointing to Northwind
        SqliteConnection connection = new("Data Source=Northwind.db");

        // Open the connection
        connection.Open();

        // Create a command object to execute the query
        var command = connection.CreateCommand();

        // Populate the command with select query
        command.CommandText = "SELECT * FROM Customers";

        // Create a DataTable to hold customer data query results
        DataTable table = new();

        // Execute the command and get a reader to read the results
        var reader = command.ExecuteReader();

        // Fill the DataTable with the results of the query
        table.Load(reader);
/*
        // Test dataset returned by printing the results
        foreach (DataRow row in table.Rows) {
            Console.WriteLine($"CustomerID: {row["CustomerID"]}, CompanyName: {row["CompanyName"]}, ContactName: {row["ContactName"]}");
        }
*/
        // Close the database connection to release its resources 
        connection.Close();

        // Return the DataTable containing the results
        return table;

    }


}