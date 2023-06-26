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
    public class ClientController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login2(clientuser user)
        {
            List<clientuser> clients = new List<clientuser>();
            clientuser Infoclient = new clientuser();
            using (MySqlConnection con = new MySqlConnection("server=localHost; user=root; database=mydbtest6; port=3306; password=Chidozie97@"))
            {

                String name = user.UserName;  // "chichi";
                String password = user.PasswordHash;   //;"123"
                
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT UserName, PasswordHash FROM clientuser WHERE UserName =  '"  + user.UserName + "' ", con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read()) {                 
                    Infoclient.UserName = reader["UserName"].ToString();
                    Infoclient.PasswordHash = reader["PasswordHash"].ToString();
                    }
                reader.Close();
                  if ( name.Equals(Infoclient.UserName) && password.Equals(Infoclient.PasswordHash))
                    {
                    HttpContext.Session.SetString("username", name);
                    return RedirectToAction("Index", "Home");
                     }           
                //               while (reader.Read())
                //   { reader.Close(); user.UserName == reader["UserName"].ToString() && user.PasswordHash == reader["PasswordHash"].ToString()}

                //  name != null && password != null && name.Equals("name") && password.Equals("password")
            }
                    ViewBag.error = "Invalid Account";
                    return View("Login");
        }

        [Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Login");
        }

        public IActionResult Register()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Register(clientuser client)
        {
            using (MySqlConnection con = new MySqlConnection("server=localHost; user=root; database=mydbtest6; port=3306; password=Chidozie97@"))
            {
                clientuser newClient = new clientuser();
                newClient = client;
                con.Open();
                //------------
                int count = 0;
                MySqlCommand commd = new MySqlCommand("SELECT * FROM mydbtest6.clientuser", con);
                MySqlDataReader reader = commd.ExecuteReader();


                while (reader.Read())
                {
                    if (newClient.Email == reader["Email"].ToString()) {
                        ViewBag.error = "Account with same Email Already exist";
                        return View("Login");
                    }
                    count++;
                }
                reader.Close();



                client.ClientID = count+1;

                //-------------
                MySqlCommand cmd = new MySqlCommand("INSERT INTO clientuser(ClientID, UserName, PasswordHash, Email)" +
                " VALUES(" + client.ClientID.ToString() + ", '" + client.UserName.ToString() + "', '" + client.PasswordHash.ToString() + "', '" + client.Email.ToString() +"');", con);     
                cmd.ExecuteNonQuery();
                con.Close();
            } 
            return View("Login");
        }

       

        public ActionResult NewRegisterClient()
        {
            
            return View();
        }

        public ActionResult AdminLogin()
        {

            return View();
        }

    }
}
