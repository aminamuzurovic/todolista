using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace ToDoApp.Controllers
{
    public class TaskTypeController : Controller
    {
        string connectionString = @"Data Source=DESKTOP-SHHT07O\SQLEXPRESS;Initial Catalog=ToDo;Integrated Security=True";
       
        public ActionResult Index()
        {
            List<TaskTypeModel> Types = new List<TaskTypeModel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "SELECT * FROM TaskType";
                connection.Open();
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    try
                    {
                        SqlDataReader reader = command .ExecuteReader();
                        while (reader.Read())
                        {
                            TaskTypeModel _model = new TaskTypeModel();
                            _model.TypeID = (int)reader[0];
                            _model.Type = (string)reader[1];
                            Types.Add(_model);
                        }
                        reader.Close();
                    }
                    catch (Exception)
                    {
                        throw new Exception("Error");
                    }
                }
                return View(Types);
            }
        }
    }
}