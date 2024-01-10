using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Inventory_management_system.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo =new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }
        public void OnPost() 
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.lastname = Request.Form["lastname"];
            clientInfo.item = Request.Form["item"];
            clientInfo.address = Request.Form["address"];


            if (clientInfo.name.Length==0 || clientInfo.lastname.Length == 0 ||
                clientInfo.item.Length == 0|| clientInfo.address.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }
            //save new client into to db
            try
            {
                String connectionString = "Data Source=ENGIN;Initial Catalog=my_db;Integrated Security=True";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO clients" + "(name,lastname,item,address) VALUES" + "(@name,@lastname,@item,@address) "; 

                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@lastname", clientInfo.lastname);
                        command.Parameters.AddWithValue("@item", clientInfo.item);
                        command.Parameters.AddWithValue("@address", clientInfo.address);

                        command.ExecuteNonQuery();



                    }
                }
            }
            catch (Exception ex)
            {

                errorMessage=ex.Message;
                return;
            }



            
            clientInfo.name = ""; clientInfo.lastname = ""; clientInfo.item = ""; clientInfo.address = "";
            successMessage = "New client added correctly";



            Response.Redirect("/Clients/Index");

        }
    }
}
