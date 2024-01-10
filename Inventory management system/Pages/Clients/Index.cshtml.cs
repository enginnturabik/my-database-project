using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient; //buraya dikkat 

namespace Inventory_management_system.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClient = new List<ClientInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=ENGIN;Initial Catalog=my_db;Integrated Security=True ";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ClientInfo clientInfo = new ClientInfo();
                            clientInfo.id = "" + reader.GetInt32(0);
                            clientInfo.name = reader.GetString(1);
                            clientInfo.lastname =  reader.GetString(2);
                            clientInfo.item = reader.GetString(3);
                            clientInfo.address = reader.GetString(4);

                            listClient.Add(clientInfo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exeption:" + ex.ToString());
            }
        }
    }
    

    public class ClientInfo
    {
        public String id;
        public String name;
        public String lastname;
        public String item;
        public String address;
        public String created_at;

    }

}
