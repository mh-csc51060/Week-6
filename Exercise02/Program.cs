using static System.Console; 
using Microsoft.EntityFrameworkCore; 
using System.Linq;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Data.Common;
using System.Text;

main();

// This is the main entry point for the program
static void main()
{

    for (int i = 0; i < 7; i++) { String s = new String(' ', i); Console.WriteLine(s); }

    AssignmentPart1();

    Console.WriteLine("\n");
    Console.WriteLine("Now lets run the second assignment.");
    Console.WriteLine("Press the enter key to continue...");
    Console.ReadLine();
    for (int i = 0; i < 7; i++) { String s = new String(' ', i); Console.WriteLine(s); }

    AssignmentPart2();

    Console.WriteLine("\n");
}

static void AssignmentPart1()
{
    Console.Write("Enter a city: ");
    var userinput = Console.ReadLine();

    // Trim the input to remove leading and trailing whitespace
    userinput = userinput?.Trim() ?? string.Empty; // Ensure userinput is not null

    // Check for empty input. If the input is null or empty, stop.
    if (userinput.Length < 1) { return; } // user input is empty do nothing more to do

    // Get the customers from the Northwind database
    DataTable Customers = Northwinddb.Customers();

    // Use LINQ query to find customers by city and then sort them by company name
    var query = Customers.AsEnumerable()
        .Where(row => row.Field<string>("City").ToLower() == userinput.ToLower())
        .OrderBy(row => row.Field<string>("CompanyName"));

    // If query has results print number of customers then print each company name
    if (query.Any())
    {

        // Get the first row to display the city to use in the output for number of customers
        DataRow FirstRow = query.First<DataRow>();
        Console.WriteLine("There are " + query.Count() + " customers in " + FirstRow["City"] + ":");

        // Now, print each company name on a new line
        foreach (DataRow row in query)
        {
            Console.WriteLine("Company: " + row["CompanyName"]);
        }
    }
    else
    {
        // If we made it here, there were no customers in the city entered
        Console.WriteLine("No customers found in " + userinput);
        return;
    }

}

static void AssignmentPart2() {

    // Get the customers from the Northwind database
    DataTable Customers = Northwinddb.Customers();

    // Get a list of cities from customers by filtering out deuplicates and then sorting them
    var UniqueCities = Customers.AsEnumerable()
        .Select(row => row.Field<string>("City")).Distinct().OrderBy(city => city);
    
    // Begin showing the user the list of unique cities
    Console.WriteLine("Here is a list of unique cities to pick from:");

    // Initialize a counter to keep track of the index to know when to print a comma
    int i = 0;

    // Print the list of unique cities
    foreach (var City in UniqueCities) {
        Console.Write(City);
        if (i < UniqueCities.Count() - 1) { Console.Write(", "); }
        i++;
    }
    // End the list of cities so we an prompt the user for input on a new line
    Console.WriteLine();

    // Realized that I can just call AssignmentPart1() to do the rest of the work
    AssignmentPart1();       
    
}

