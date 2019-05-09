using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace ToDoApp.Controllers
{
    public class ToDoAppController : Controller
    {        
        public ActionResult Index()
        {
            string connectionString = @"Data Source=DESKTOP-SHHT07O\SQLEXPRESS;Initial Catalog=ToDo;Integrated Security=True";            
            string queryString = "SELECT * FROM Task";
            List<string> listTask = new List<string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command1 = new SqlCommand(queryString, connection);                
                try
                {
                    connection.Open();
                    SqlDataReader reader = command1.ExecuteReader();
                    while (reader.Read())
                    {
                        listTask.Add(String.Format("\t{0}\t{1}\t{2}",
                            reader[0], reader[1], reader[2]));
                    }
                    reader.Close();
                }
                catch (Exception)
                {
                    throw new Exception("Error");
                }
                return Json(listTask, JsonRequestBehavior.AllowGet);
            }
        }
    }
}