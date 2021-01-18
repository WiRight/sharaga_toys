using System;
using System.Data;
using System.Data.OleDb;

namespace Toys
{
    public static class LoginService
    {
        public static bool Login(String login, String password)
        {
            // return login == "Admin" && password == "1234";

            var myConn = new OleDbConnection(Properties.Settings.Default.ConnectionString);
            myConn.Open();

            var query = "Select * From Users Where username = '" + login + "' and password = '" + password + "'";

            var sda = new OleDbDataAdapter(query, myConn);

            var dt = new DataTable();
            sda.Fill(dt);

            return dt.Rows.Count == 1;
        }
    }
}