using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ToDoApp
{
    public class TaskItem : Controller
    {
        string connectionString = @"Data Source=DESKTOP-SHHT07O\SQLEXPRESS;Initial Catalog=ToDoApp;Integrated Security=True";
        List<TaskModel> Tasks = new List<TaskModel>();
        List<string> listTask = new List<string>();       
        public ActionResult Index()
        {   
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "SELECT * FROM Task";
                connection.Open();                
                using (SqlCommand command1 = new SqlCommand(queryString, connection))
                {                    
                    try
                    {                        
                        SqlDataReader reader = command1.ExecuteReader();
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
                    catch (Exception ex)
                    {
                        throw new Exception("Error");
                    }                    
                }
                return View();
            }
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskModel todos = Tasks.FirstOrDefault(x=> x.TaskID == id);
            if (todos == null)
            {
                return HttpNotFound();
            }
            return View(Tasks);
        }
        public ActionResult Insert(Task t)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string insertString = "INSERT INTO Task(ID, Name, Description) VALUES (TaskID, TaskName, TaskDescription)";
                connection.Open();
                using (SqlCommand command = new SqlCommand(insertString, connection))
                {
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
            return View(t);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Add()
        {
            listTask.Add(String.Format("\t{0}\t{1}\t{2}",
                            listTask[0], listTask[1], listTask[2]));
            return Json(listTask, JsonRequestBehavior.AllowGet);
        }
    }
}