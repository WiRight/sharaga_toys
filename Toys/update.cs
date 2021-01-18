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
    public partial class update : Form
    {
        public int Price;
        public int Count;
        public int Age_from;
        public int Age_to;

        public update(string name, int price, int count, int age_from, int age_to)
        {
            InitializeComponent();
            Price = price;
            Count = count;
            Age_from = age_from;
            Age_to = age_to;

            textBox1.Text = name;
            textBox2.Text = Price.ToString();
            textBox3.Text = Count.ToString();
            textBox4.Text = Age_from.ToString();
            textBox5.Text = Age_to.ToString();
        }

        private void update_Load(object sender, EventArgs e)
        {
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            int parsedValue;
            if (!int.TryParse(textBox2.Text, out parsedValue) & textBox2.Text != "")
            {
                MessageBox.Show("This is a number only field");
                textBox2.Text = this.Price.ToString();
                return;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            int parsedValue;
            if (!int.TryParse(textBox3.Text, out parsedValue) & textBox3.Text != "")
            {
                MessageBox.Show("This is a number only field");
                textBox3.Text = this.Count.ToString();
                return;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            int parsedValue;
            if (!int.TryParse(textBox4.Text, out parsedValue) & textBox4.Text != "")
            {
                MessageBox.Show("This is a number only field");
                textBox4.Text = this.Age_from.ToString();
                return;
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            int parsedValue;

            if (!int.TryParse(textBox5.Text, out parsedValue) & textBox5.Text != "")
            {
                MessageBox.Show("This is a number only field");
                textBox5.Text = this.Age_to.ToString();
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" &&
                textBox5.Text != "")
            {
                using (var connection = new OleDbConnection(Properties.Settings.Default.ConnectionString))
                {
                    const string query =
                        "UPDATE Toys SET [Name] = ?, [Price] = ?, [Count] = ?, [Age_from] = ?, [Age_to] = ? WHERE [Name] = ?";

                    using (var command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", textBox1.Text);
                        command.Parameters.AddWithValue("@price", int.Parse(textBox2.Text));
                        command.Parameters.AddWithValue("@count", int.Parse(textBox3.Text));
                        command.Parameters.AddWithValue("@age_from", int.Parse(textBox4.Text));
                        command.Parameters.AddWithValue("@age_to", int.Parse(textBox5.Text));
                        command.Parameters.AddWithValue("@this_name", textBox1.Text);

                        connection.Open();

                        var result = command.ExecuteNonQuery();

                        // Check Error
                        if (result < 0)
                        {
                            MessageBox.Show("Произошла ошибка при обновлении данных!");
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Обновление прошло успешно!");
                        }
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