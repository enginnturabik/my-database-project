using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Inventory_management_system.Pages.Clients
{
    public class EditModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"]; // id which get from request
            try
            {
                String connectionString = "Data Source=ENGIN;Initial Catalog=my_db;Integrated Security=True ";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients WHERE id = @id";
                    using (SqlCommand command = new SqlCommand(sql, connection)) //generate sql query
                    {
                        command.Parameters.AddWithValue("@id", id); // this correspoind id get what we get from user
                        using (SqlDataReader reader = command.ExecuteReader())
                            if (reader.Read())
                            {
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.lastname = reader.GetString(2);
                                clientInfo.item = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);

                            }
                    }
                }


            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;


            }
        }
        public void OnPost()
        {
            clientInfo.id = Request.Form["id"];
            clientInfo.name = Request.Form["name"];
            clientInfo.lastname = Request.Form["lastname"];
            clientInfo.item = Request.Form["item"];
            clientInfo.address = Request.Form["address"];

            if (clientInfo.id.Length == 0 || clientInfo.name.Length == 0 || clientInfo.lastname.Length == 0 ||
              clientInfo.item.Length == 0 || clientInfo.address.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }
            try
            {
                String connectionString = "Data Source=ENGIN;Initial Catalog=my_db;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE clients SET name=@name, lastname=@lastname, item=@item, address=@address WHERE id = @id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@lastname", clientInfo.lastname);
                        command.Parameters.AddWithValue("@item", clientInfo.item);
                        command.Parameters.AddWithValue("@address", clientInfo.address);
                        command.Parameters.AddWithValue("@id", clientInfo.id);


                        command.ExecuteNonQuery();


                    } 
                }
            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Clients/Index");
        }
    }
}
