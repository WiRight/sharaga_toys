using System;
using System.Data.OleDb;

namespace Toys
{
    /// <summary>
    /// Данные для добавления/обновления адреса
    /// </summary>
    public class AddressData
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public DateTime Birthday { get; set; }

        public string Company { get; set; }

        public string PhoneNumber { get; set; }
    }

    public static class AddressService
    {
        /// <summary>
        /// Добавление нового адреса в базу
        /// </summary>
        /// <param name="data">Данные для добавления</param>
        /// <returns>Количество вставленных элементов</returns>
        public static int Add(AddressData data)
        {
            int result;

            using (var connection = new OleDbConnection(Properties.Settings.Default.ConnectionString))
            {
                const string query =
                    "INSERT INTO Adressbook([Id], [First_name], [Second_name], [Birthday], [Company], [Phone_number]) VALUES (?,?,?,?,?,?)";

                using (var command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", data.Id);
                    command.Parameters.AddWithValue("@first_name", data.FirstName);
                    command.Parameters.AddWithValue("@csecond_name", data.SecondName);
                    command.Parameters.AddWithValue("@birthday", data.Birthday);
                    command.Parameters.AddWithValue("@company", data.Company);
                    command.Parameters.AddWithValue("@phone_number", data.PhoneNumber);

                    connection.Open();

                    result = command.ExecuteNonQuery();
                }
            }

            return result;
        }

        /// <summary>
        /// Обновление адреса
        /// </summary>
        /// <param name="data">Данные для обновления</param>
        /// <returns></returns>
        public static int Update(AddressData data)
        {
            int result;

            using (var connection = new OleDbConnection(Properties.Settings.Default.ConnectionString))
            {
                const string query =
                    "UPDATE Adressbook SET [First_name] = ?, [Second_name] = ?, [Birthday] = ?, [Company] = ?, [Phone_number] = ? WHERE [Id] = ?";

                using (var command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@first_name", data.FirstName);
                    command.Parameters.AddWithValue("@second_name", data.SecondName);
                    command.Parameters.AddWithValue("@birthday", data.Birthday);
                    command.Parameters.AddWithValue("@company", data.Company);
                    command.Parameters.AddWithValue("@phone_number", data.PhoneNumber);
                    command.Parameters.AddWithValue("@id_", data.Id);

                    connection.Open();
                    result = command.ExecuteNonQuery();
                }
            }
            
            return result;
        }
    }
}