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
    public partial class add_data : Form
    {
        public add_data()
        {
            InitializeComponent();
        }

        private void add_data_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            int parsedValue;
            if (!int.TryParse(textBox2.Text, out parsedValue) & textBox2.Text != "")
            {

                MessageBox.Show("This is a number only field");
                textBox2.Text = "";
                return;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            int parsedValue;
            if (!int.TryParse(textBox3.Text, out parsedValue) & textBox3.Text != "")
            {

                MessageBox.Show("This is a number only field");
                textBox3.Text = "";
                return;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            int parsedValue;
            if (!int.TryParse(textBox4.Text, out parsedValue) & textBox4.Text != "")
            {

                MessageBox.Show("This is a number only field");
                textBox4.Text = "";
                return;
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            int parsedValue;
            if (!int.TryParse(textBox5.Text, out parsedValue) & textBox5.Text != "")
            {

                MessageBox.Show("This is a number only field");
                textBox5.Text = "";
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "")
            {
                using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.ConnectionString))
                {
                    String query = "INSERT INTO Toys([Name], [Price], [Count], [Age_from], [Age_to]) VALUES (?,?,?,?,?)";

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", textBox1.Text);
                        command.Parameters.AddWithValue("@price", int.Parse(textBox2.Text));
                        command.Parameters.AddWithValue("@count", int.Parse(textBox3.Text));
                        command.Parameters.AddWithValue("@age_from", int.Parse(textBox4.Text));
                        command.Parameters.AddWithValue("@age_to", int.Parse(textBox5.Text));

                        connection.Open();
                        int result = command.ExecuteNonQuery();

                        // Check Error
                        if (result < 0)
                            MessageBox.Show("Произошла ошибка при добавлении данных!");
                        else MessageBox.Show("Добавление прошло успешно!");
                    }
                }
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";

            }
            else 
            {
                MessageBox.Show("Есть отсуствующие данные");
            }
        }
    }
}
