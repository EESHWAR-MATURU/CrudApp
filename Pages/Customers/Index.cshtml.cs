using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace CRUD.Pages.Customers
{
    public class Index : PageModel
    {
        public List<CustomersInfo> CustomersList { get; set; } = new List<CustomersInfo>();
        
        public void OnGet()
        {
            try 
            {
                string connectionString = "Server=DESKTOP-1C4DOGP\\SQLEXPRESS;Database=CrudApp;Trusted_Connection=True;TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM customers ORDER BY id DESC";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CustomersInfo customersInfo = new CustomersInfo
                                {
                                    ID = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    PhoneNumber = reader.GetString(2),
                                    Email = reader.GetString(3),
                                    CreatedAt = reader.GetDateTime(4).ToString("MM/dd/yyyy")
                                };

                                CustomersList.Add(customersInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("We have an error: " + ex.Message);
            }
        }
    }

    public class CustomersInfo
    {
        public int ID { get; set; }
        public string Name { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Email { get; set; } = "";
        public string CreatedAt { get; set; } = "";
    }
}