using System;
using System.Data;
using System.Data.OleDb;

namespace Toys
{
    /// <summary>
    /// Сервис для работы с авторизацией
    /// </summary>
    public static class LoginService
    {
        /// <summary>
        /// Вход в систему
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <returns>1 в случае если пользователь есть</returns>
        public static bool Login(string login, string password)
        {
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