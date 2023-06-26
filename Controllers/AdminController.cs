using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Motte_IT.Models;
using MySql.Data.MySqlClient;
using Motte_IT.Data;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Motte_IT.Controllers
{
    public class AdminController : Controller
    {
         List<ClientComplaint> complist = new List<ClientComplaint>();
       public List<clientuser> clients = new List<clientuser>();
        // List<ClientComplaint> complist = new List<ClientComplaint>();
        // skapa en priorty queue 
        priorityqueue testlist = new priorityqueue(5);
        public IActionResult Admin()
        {
            return View();
        }

        public IActionResult Complaints()
        {

            using (MySqlConnection con = new MySqlConnection("server=localHost; user=root; database=mydbtest6; port=3306; password=Chidozie97@"))
            {

                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM mydbtest6.complaint", con);

                MySqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {



                    ClientComplaint complaint = new ClientComplaint();
                    complaint.ComplaintID = Convert.ToInt32(reader["ComplaintID"]);
                    complaint.StartDate = reader["startDate"].ToString();
                    complaint.ComputerNumber = Convert.ToInt32(reader["ComputerNumber"]);
                    complaint.CategorySubject = reader["Subject"].ToString();  // ge subjecten en värde
                    complaint.Description = reader["Description"].ToString();
                    complaint.ClientID = Convert.ToInt32(reader["ClientID"]);
                    complaint.Status = reader["Status"].ToString();
                    complaint.AdminID = Convert.ToInt32(reader["AdminID"]);
                    complaint.FinishedDate = reader["FinishedDate"].ToString();

                    // lägg i priority queue

                    testlist.enqueue(complaint);
                   

                }
                for (int i = 0; i < testlist.lastindex+2; i++) { 
                 complist.Add(testlist.dequeue());
                }
                reader.Close();
            }
            return View(complist);
        }
        [HttpGet]
        public IActionResult Users()
        {
            using (MySqlConnection con = new MySqlConnection("server=localHost; user=root; database=mydbtest6; port=3306; password=Chidozie97@"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM mydbtest6.clientuser", con);
                MySqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    clientuser client = new clientuser();
                    client.ClientID = Convert.ToInt32(reader["ClientID"]);
                    client.UserName = reader["UserName"].ToString();
                    client.PasswordHash = reader["PasswordHash"].ToString();
                    client.Email = reader["Email"].ToString();
                    clients.Add(client);
                }
                reader.Close();
            }
            return View(clients);
        }

        public IActionResult AdminLogin() {

            return View();
        }


        [Route("admin")]
        [HttpPost]
        public IActionResult AdminLogin2(UserAdmin adm )
        {
            List<clientuser> clients = new List<clientuser>();
            UserAdmin admInfo = new UserAdmin();
            using (MySqlConnection con = new MySqlConnection("server=localHost; user=root; database=mydbtest6; port=3306; password=Chidozie97@"))
            {

                String name = adm.Name;  // "chichi";
                String password = adm.Password;   //;"123"

                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT Name, AdminPassword FROM Admin WHERE Name =  '" + adm.Name + "' ", con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    admInfo.Name = reader["Name"].ToString();
                    admInfo.Password = reader["AdminPassword"].ToString();
                }
                reader.Close();

                if (name.Equals(admInfo.Name) && password.Equals(admInfo.Password))
                {
                    
                    return RedirectToAction("Admin", "Admin");

                }

                //               while (reader.Read())
                //   { reader.Close(); user.UserName == reader["UserName"].ToString() && user.PasswordHash == reader["PasswordHash"].ToString()}

                //  name != null && password != null && name.Equals("name") && password.Equals("password")

            }
            ViewBag.error = "Invalid Account";
            return View("AdminLogin");

        }


        public IActionResult UpdateStatus( )
        {


            return View();
        }

        [Route("UpdateStatus2")]
        [HttpPost]
        public IActionResult UpdateStatus2(ClientComplaint complaint )
        {
            
            using (MySqlConnection con = new MySqlConnection("server=localHost; user=root; database=mydbtest6; port=3306; password=Chidozie97@"))
            {
                con.Open();
                //---------

                if (complaint.Status.Equals("Completed")) {
                    DateTime now = DateTime.Now;
                    
                    MySqlCommand cmad = new MySqlCommand("update complaint SET FinishedDate = '" + (now.ToString("F")) + "' where ComplaintID = " + complaint.ComplaintID.ToString() + " ;", con);
                    cmad.ExecuteNonQuery();
                }



                //----------


                MySqlCommand cmd = new MySqlCommand("update complaint SET Status = '" + complaint.Status.ToString() + "' where ComplaintID = " + complaint.ComplaintID.ToString() + " ;", con);
                cmd.ExecuteNonQuery();
                con.Close();
            }


             return RedirectToAction("Admin", "Admin");
        }


    }



    }
       

    

    



