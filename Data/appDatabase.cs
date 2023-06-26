using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Motte_IT.Models;
using MySqlConnector;


namespace Motte_IT.Data {

    public class appDatabase
    {
        public string ConnectionString { get; set; }

        public appDatabase(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public void addClient(clientuser x) {
        


           /*conn.Open();
          MySqlCommand cmd = new MySqlCommand("select * from Album where id < 10", conn);
           /*/ 
        }

    }   
}