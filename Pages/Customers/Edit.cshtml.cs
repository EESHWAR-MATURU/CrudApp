using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace CRUD.Pages.Customers
{
    public class Edit : PageModel
    {
        [BindProperty]
        public int ID { get; set; }
        [BindProperty, Required(ErrorMessage = "The Name is required")]
        public string Name { get; set; } = "";

        [BindProperty, Phone, Required(ErrorMessage = "The Phone Number is required")]
        public string PhoneNumber { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "The Email is required"), EmailAddress]
        public string Email { get; set; } = "";

        public string ErrorMessage { get; set; } = "";
        public void OnGet(int id)
        {
            try {
                string connectionString = "Server=DESKTOP-1C4DOGP\\SQLEXPRESS;Database=CrudApp;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();

                    string sql = "SELECT * FROM customers WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader()) {
                            if (reader.Read()) {
                                ID = reader.GetInt32(0);
                                Name = reader.GetString(1);
                                PhoneNumber = reader.GetString(2);
                                Email = reader.GetString(3);
                            }
                            else {
                                Response.Redirect("/Customers/Index");
                            }
                        }
                    }
                }

            }
            catch(Exception ex) {
                ErrorMessage = ex.Message;
            }
        }


        public void OnPost() {
    if (!ModelState.IsValid) {
        return;
    }
    if (PhoneNumber == null) PhoneNumber = "";
    if (Email == null) Email = "";

    try {
        string connectionString = "Server=DESKTOP-1C4DOGP\\SQLEXPRESS;Database=CrudApp;Trusted_Connection=True;TrustServerCertificate=True;";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string sql = "UPDATE customers SET Name=@Name, PhoneNumber=@PhoneNumber, Email=@Email WHERE id=@id;";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Name", Name);
                command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                command.Parameters.AddWithValue("@Email", Email);
                command.Parameters.AddWithValue("@id", ID); // Add @id parameter here
                command.ExecuteNonQuery();
            }
        }

        Response.Redirect("/Customers/Index");
    }
    catch (Exception ex)
    {
        ErrorMessage = ex.Message;
        return;
    }
}

        }
    }