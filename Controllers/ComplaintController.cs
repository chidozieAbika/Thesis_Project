using Microsoft.AspNetCore.Mvc;

using Motte_IT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;


namespace Motte_IT.Controllers
{
    public class ComplaintController : Controller
    {
       
        List<ClientComplaint> complist = new List<ClientComplaint>();
            public IActionResult Index1()
            {
               

                using (MySqlConnection con = new MySqlConnection("server=localHost; user=root; database=mydbtest6; port=3306; password=Chidozie97@"))
                {

                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM mydbtest6.clientuser", con);
                
                    MySqlDataReader reader = cmd.ExecuteReader();


                    while (reader.Read())
                    {



                        ClientComplaint client = new ClientComplaint();
                         client.ComplaintID = Convert.ToInt32(reader["ClientID"]);
                         client.StartDate = reader["UserName"].ToString();
                        client.ComputerNumber = Convert.ToInt32(reader["ComputerNumber"]);
                        client.CategorySubject = reader["Subject"].ToString();
                         client.Description = reader["Description"].ToString();
                         client.ClientID = Convert.ToInt32(reader["ClientID"]);
                        client.Status = reader["Status"].ToString();
                        client.AdminID = Convert.ToInt32(reader["AdminID"]);
                    client.FinishedDate = reader["FinishedDate"].ToString();

                    complist.Add(client);

                    }
                    reader.Close();
                }

                return View(complist);
            }


        public IActionResult Privacy()
        {
            return View();
        }

        /*
        IEnumerable<ClientComplaint> compList = _db.ClientComplaint; 
        /*/



    }
    }

