using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Motte_IT.Models;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace Motte_IT.Controllers
{
    public class HomeController : Controller
    {
        List<clientuser> clients = new List<clientuser>();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

 

            [HttpGet]
            public IActionResult Index()
            {
            List<ClientComplaint> complist = new List<ClientComplaint>();

            using (MySqlConnection con = new MySqlConnection("server=localHost; user=root; database=mydbtest6; port=3306; password=Chidozie97@"))
            {

                con.Open();

                String s = "";
                MySqlCommand comand = new MySqlCommand("SELECT ClientID FROM clientuser WHERE UserName = '" + HttpContext.Session.GetString("username") + "' ", con);
                MySqlDataReader read = comand.ExecuteReader();
                while (read.Read())
                {
                    s = read["ClientID"].ToString();
                }
                read.Close();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM complaint WHERE ClientID = " + s + "; ", con);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ClientComplaint complaint = new ClientComplaint();
                    complaint.ComplaintID = Convert.ToInt32(reader["ComplaintID"]);
                    complaint.StartDate = reader["startDate"].ToString();
                    complaint.ComputerNumber = Convert.ToInt32(reader["ComputerNumber"]);
                    complaint.CategorySubject = reader["Subject"].ToString();
                    complaint.Description = reader["Description"].ToString();
                    complaint.ClientID = Convert.ToInt32(reader["ClientID"]);
                    complaint.Status = reader["Status"].ToString();
                    complaint.AdminID = Convert.ToInt32(reader["AdminID"]);
                    complaint.FinishedDate = reader["FinishedDate"].ToString();

                    complist.Add(complaint);

                }
                reader.Close();
            }

            return View(complist);
            }

            public IActionResult Complaint()
            {
                return View();
            }

            [HttpPost]
            public IActionResult Complaint(ClientComplaint complaint)
            {
           

                using (MySqlConnection con = new MySqlConnection("server=localHost; user=root; database=mydbtest6; port=3306; password=Chidozie97@"))
                {

                DateTime now = DateTime.Now;
               


                int count = 0;
                String s ="";
                    con.Open();
                //-------------
                
                MySqlCommand commd = new MySqlCommand("SELECT * FROM mydbtest6.complaint", con);
                MySqlDataReader readeR = commd.ExecuteReader();


                while (readeR.Read())
                {
                    count++;
                }
                readeR.Close();

                complaint.ComplaintID = count + 1;

                //-------------

                MySqlCommand comand = new MySqlCommand("SELECT ClientID FROM clientuser WHERE UserName = '" + HttpContext.Session.GetString("username") + "' ", con);
                MySqlDataReader reader = comand.ExecuteReader();
                while (reader.Read())
                {

                     s = reader["ClientID"].ToString();

                
                 }
                    reader.Close();
                 
                 complaint.StartDate = (now.ToString("F"));


                MySqlCommand cmd = new MySqlCommand("INSERT INTO complaint(ComplaintID, ComputerNumber, Subject, " +
                    "Description, ClientID, Status, AdminID, startDate ) VALUES( " + complaint.ComplaintID.ToString() + ", " + complaint.ComputerNumber.ToString() + ", '" + complaint.CategorySubject.ToString() + "', '" + complaint.Description.ToString() + "', " + s + ", 'Waiting for confirmation' , 1,'"  + complaint.StartDate + "' );", con);
                 
                    cmd.ExecuteNonQuery();
                    con.Close();
                }


                /*
                int personId = person.PersonId;
                string name = person.Name;
                string gender = person.Gender;
                  string city = person.City;
                /*/
                return View("~/Views/Home/Complaint.cshtml");

            }


            public IActionResult Profile()
            {
            clientuser client = new clientuser();
            List<clientuser> list = new List<clientuser>();
            using (MySqlConnection con = new MySqlConnection("server=localHost; user=root; database=mydbtest6; port=3306; password=Chidozie97@"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT ClientID, UserName, PasswordHash, " +
                    "Email FROM clientuser" +
                    " WHERE UserName =  '" + HttpContext.Session.GetString("username") + "' ", con);
                MySqlDataReader reader = cmd.ExecuteReader();
               

                while (reader.Read())
                {
                    
                    client.ClientID = Convert.ToInt32(reader["ClientID"]);
                    client.UserName = reader["UserName"].ToString();
                    client.PasswordHash = reader["PasswordHash"].ToString();
                    client.Email = reader["Email"].ToString();
                    list.Add(client);
                }
                reader.Close();
            }
            return View(client);

            }

        
        public IActionResult edit() {

            return View();
        }


        [Route("editName")]
        [HttpPost]
        public IActionResult editName(clientuser client) {

            using (MySqlConnection con = new MySqlConnection("server=localHost; user=root; database=mydbtest6; port=3306; password=Chidozie97@"))
            {
                con.Open();
                String s = "";
                MySqlCommand comand = new MySqlCommand("SELECT ClientID FROM clientuser" +
                    " WHERE UserName = '" + HttpContext.Session.GetString("username") + "' ", con);
                MySqlDataReader read = comand.ExecuteReader();
                while (read.Read())
                {
                    s = read["ClientID"].ToString();
                }
                read.Close();

                client.UserName.ToString();
                MySqlCommand cmd = new MySqlCommand("update clientuser SET UserName = '" + client.UserName.ToString() + "'" +
                    " where ClientID = " + s + " ;", con);
                cmd.ExecuteNonQuery();
                HttpContext.Session.SetString("username", client.UserName.ToString());
                con.Close();
            }

            return RedirectToAction("Profile");
        }

        [Route("editPassword")]
        [HttpPost]
        public IActionResult editPassword(clientuser client)
        {
            using (MySqlConnection con = new MySqlConnection("server=localHost; user=root; database=mydbtest6; port=3306; password=Chidozie97@"))
            {
                con.Open();
                String s = "";
                MySqlCommand comand = new MySqlCommand("SELECT ClientID FROM clientuser WHERE UserName = '" + HttpContext.Session.GetString("username") + "' ", con);
                MySqlDataReader read = comand.ExecuteReader();
                while (read.Read())
                {
                    s = read["ClientID"].ToString();
                }
                read.Close();

               
                MySqlCommand cmd = new MySqlCommand("update clientuser SET PasswordHash = '" + client.PasswordHash.ToString() + "' where ClientID = " + s + " ;", con);
                cmd.ExecuteNonQuery();
                con.Close();
            }

            return RedirectToAction("Profile");

            
        }

        





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

        

    }
}   
