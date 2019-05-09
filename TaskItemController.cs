using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace ToDoApp.Controllers
{
    public class TaskItemController : Controller
    {
        string connectionString = @"Data Source=DESKTOP-SHHT07O\SQLEXPRESS;Initial Catalog=ToDo;Integrated Security=True";
        List<TaskModel> Tasks = new List<TaskModel>();
        List<string> listTask = new List<string>();

        public ActionResult Index()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "SELECT * FROM Task";
                connection.Open();
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            TaskModel model = new TaskModel();
                            model.TaskID = (int)reader[0];
                            model.TaskName = (string)reader[1];
                            model.TaskDescription = (string)reader[2];
                            Tasks.Add(model);
                        }
                        reader.Close();
                    }
                    catch (Exception)
                    {
                        throw new Exception("Error");
                    }
                }         
                
                return View(Tasks);
            }
        }

        public ActionResult Edit(int? id)
        {
            TaskModel model = new TaskModel();
            if (id != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string queryString = "SELECT * FROM Task WHERE TaskID = " +id;
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        try
                        {
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {

                                 model.TaskID = (int)reader[0];
                                 model.TaskName = (string)reader[1];
                                 model.TaskDescription = (string)reader[2];

                            }
                            reader.Close();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error");
                        }
                    }
                }                
            }
            TempData["message"] = "Edited";              
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(TaskModel model)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string editString = @"UPDATE Task SET Name =  @TaskName , Description =  @TaskDescription WHERE TaskID =  @TaskID";
                connection.Open();
                using (SqlCommand command = new SqlCommand(editString, connection))
                {
                    command.Parameters.AddWithValue("@TaskID", model.TaskID);
                    command.Parameters.AddWithValue("@TaskName", model.TaskName);
                    command.Parameters.AddWithValue("@TaskDescription", model.TaskDescription);                                       
                    try
                    {
                       command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error");
                    }
                }
            }
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public ActionResult Add(TaskModel model)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string insertString = @"INSERT INTO Task(Name, Description) VALUES (@TaskName, @TaskDescription)";                
                connection.Open();
                using (SqlCommand command = new SqlCommand(insertString, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@TaskName", model.TaskName);
                        command.Parameters.AddWithValue("@TaskDescription", model.TaskDescription);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error");
                    }
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Add()
        {
            TempData["message"] = "Added";
            return View();
        }
        
        [HttpGet]
        public ActionResult Delete()
        {
            TempData["message"] = "Deleted";
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string deleteString = @"DELETE FROM Task WHERE TaskID = @id";
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(deleteString, connection))
                    {
                        try
                        {
                            command.Parameters.AddWithValue("@id", id);
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                TaskModel model = new TaskModel();
                                model.TaskID = (int)reader[0];
                                model.TaskName = (string)reader[1];
                                model.TaskDescription = (string)reader[2];
                                Tasks.Remove(model);
                            }
                            reader.Close();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error");
                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }        
    }

}

