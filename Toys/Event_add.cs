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
    public partial class Event_add : Form
    {
        public Event_add()
        {
            InitializeComponent();
            for (int i = 0; i < 24; i++)
            {
                comboBox1.Items.Add(i);
            }

            for (int j = 0; j < 61; j++)
            {
                comboBox2.Items.Add(j);
            }

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.ConnectionString))
            {
                String query = "Select Id From Events";
                connection.Open();
                OleDbDataAdapter sda = new OleDbDataAdapter(query, connection);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                int id = dt.Rows.Count + 1;
                textBox1.Text = id.ToString();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void Event_add_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.ConnectionString))
                {
                    String query = "INSERT INTO Events([Id], [Date], [Time], [Event_name]) VALUES (?,?,?,?)";

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", int.Parse(textBox1.Text));
                        command.Parameters.AddWithValue("@date", dateTimePicker1.Value.Date);
                        string time = comboBox1.Text + ":" + comboBox2.Text;
                        command.Parameters.AddWithValue("@time", time);
                        command.Parameters.AddWithValue("@event_name", textBox2.Text);

                        connection.Open();
                        int result = command.ExecuteNonQuery();

                        // Check Error
                        if (result < 0)
                            MessageBox.Show("Произошла ошибка при добавлении данных!");
                        else MessageBox.Show("Добавление прошло успешно!");
                    }
                }

                this.Close();
            }
        }
    }
}