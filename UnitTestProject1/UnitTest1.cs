using System;
using System.Data.OleDb;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Toys;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private TestContext TestContext { get; set; }

        [TestMethod]
        public void TestLogin()
        {
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

            const int id = 99;

            var result = AddressService.Add(new AddressAddData
            {
                Id = id,
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
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();

                    result = command.ExecuteNonQuery();

                    Assert.AreEqual(1, result);
                }
            }
        }

        private string ConnectionString()
        {
            return (string) TestContext.Properties["connectionString"];
        }
    }
}