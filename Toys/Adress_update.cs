using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Toys
{
    public partial class Adress_update : Form
    {
        public int id_u;
        public string first_name_u;
        public string last_name_u;
        public DateTime birthday_u;
        public string company_u;
        public string phone_number_u;

        public Adress_update(int Id, string first_name, string last_name, DateTime birthday, string company,
            string phone_number)
        {
            InitializeComponent();
            textBox1.Text = Id.ToString();
            textBox2.Text = first_name;
            textBox3.Text = last_name;
            dateTimePicker1.Value = birthday;
            textBox4.Text = company;
            textBox5.Text = phone_number;

            this.id_u = Id;
            this.first_name_u = first_name;
            this.last_name_u = last_name;
            this.birthday_u = birthday;
            this.company_u = company;
            this.phone_number_u = phone_number;
        }

        private void Adress_update_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" &&
                textBox5.Text != "")
            {
                using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.ConnectionString))
                {
                    String query =
                        "UPDATE Adressbook SET [First_name] = ?, [Second_name] = ?, [Birthday] = ?, [Company] = ?, [Phone_number] = ? WHERE [Id] = ?";

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@first_name", textBox2.Text);
                        command.Parameters.AddWithValue("@second_name", textBox3.Text);
                        command.Parameters.AddWithValue("@birthday",
                            Convert.ToDateTime(dateTimePicker1.Value.ToString()));
                        command.Parameters.AddWithValue("@company", textBox4.Text);
                        command.Parameters.AddWithValue("@phone_number", textBox5.Text);
                        command.Parameters.AddWithValue("@id_", int.Parse(textBox1.Text));

                        connection.Open();
                        int result = command.ExecuteNonQuery();

                        // Check Error
                        if (result < 0)
                        {
                            MessageBox.Show("Произошла ошибка при обновлении данных!");
                            this.Close();
                        }
                        else MessageBox.Show("Обновление прошло успешно!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Есть отсуствующие данные");
            }
        }
    }
}