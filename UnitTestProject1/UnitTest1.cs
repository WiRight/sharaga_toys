using System;
using System.Data.OleDb;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Toys;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        public TestContext TestContext { get; set; }

        private const int Id = 99;

        [TestMethod]
        public void TestLogin()
        {
            // Тест авторизации - Поиск
            Assert.IsTrue(LoginService.Login("Admin", "1234"));

            Assert.IsFalse(LoginService.Login("Admin1", "1234"));
            Assert.IsFalse(LoginService.Login("Admin", "12341"));
            Assert.IsFalse(LoginService.Login("", ""));
            Assert.IsFalse(LoginService.Login("неверно", "неверно"));
            Assert.IsFalse(LoginService.Login("*(@HFSJ", "434e2@"));
        }

        [TestMethod]
        public void TestInsert()
        {
            // Тест добавления адреса - вставки в БД

            var result = AddressService.Add(new AddressData
            {
                Id = Id,
                FirstName = "Tester",
                Birthday = new DateTime(1996, 3, 10),
                Company = "Dizoft",
                PhoneNumber = "+79094567201",
                SecondName = "WiRight"
            });

            Assert.AreEqual(1, result);

            using (var connection = new OleDbConnection(ConnectionString()))
            {
                const string query = "select * from Adressbook WHERE Id = @id";

                using (var command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);

                    connection.Open();

                    result = command.ExecuteNonQuery();

                    Assert.AreEqual(1, result);
                }
            }
        }

        [TestMethod]
        public void TestUpdate()
        {
            // Тест обновления адреса - обновление в БД

            var result = AddressService.Update(new AddressData
            {
                Id = Id,
                Birthday = new DateTime(2000, 1, 10),
                Company = "Diz",
                FirstName = "Tes",
                SecondName = "WiR",
                PhoneNumber = "+7999333221"
            });

            Assert.AreEqual(1, result);
        }

        private static string ConnectionString()
        {
            return "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\Toys.accdb";
        }
    }
}