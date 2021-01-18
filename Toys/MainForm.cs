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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "toysDataSet5.Toys". При необходимости она может быть перемещена или удалена.
            this.toysTableAdapter3.Fill(this.toysDataSet5.Toys);

            comboBox1.Items.Add("Название игрушки");
            comboBox1.Items.Add("Цена");
            comboBox1.Items.Add("Количество");
            comboBox1.Items.Add("Возраст от");
            comboBox1.Items.Add("Возраст до");
            comboBox1.SelectedIndex = 0;
            button8.Text = "\u221A";
            Max_price();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void Max_price()
        {
            using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.ConnectionString))
            {
                String query = "SELECT TOP 1 Name, Price FROM Toys WHERE Price = (SELECT max(Price) FROM Toys)";

                using (OleDbCommand command = new OleDbCommand(query, connection))
                {

                    connection.Open();
                    using (OleDbDataReader oReader = command.ExecuteReader())
                    {
                        while (oReader.Read())
                        {
                            label5.Text = oReader["Name"].ToString();
                            label7.Text = oReader["Price"].ToString();
                        }

                        connection.Close();
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.ConnectionString))
            {
                String query = "SELECT * FROM Toys";

                using (OleDbCommand command = new OleDbCommand(query, connection))
                {

                    connection.Open();

                    OleDbDataAdapter sqlDataAdap = new OleDbDataAdapter(command);

                    DataTable dtRecord = new DataTable();
                    sqlDataAdap.Fill(dtRecord);
                    dataGridView1.DataSource = dtRecord;
                }
            }


            this.toysTableAdapter3.Fill(this.toysDataSet5.Toys);
            Max_price();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            add_data add_ = new add_data();
            add_.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1) 
            {
                using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.ConnectionString))
                {
                    String query = "DELETE FROM Toys WHERE Name = @name";

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", dataGridView1.SelectedRows[0].Cells[0].Value);

                        connection.Open();
                        int result = command.ExecuteNonQuery();

                        // Check Error
                        if (result < 0)
                            MessageBox.Show("Произошла ошибка при удалении данных!");
                        else
                        {
                            this.toysTableAdapter1.Fill(this.toysDataSet1.Toys);
                            MessageBox.Show("Удаление прошло успешно!");
                        }
                    }
                }

            }
            else
            {
                MessageBox.Show("Выберите пожалуйста одну полную строку");
            }
            button1_Click(sender,e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                string Name = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                int Price = int.Parse(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                int Count = int.Parse(dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
                int Age_from = int.Parse(dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
                int Age_to = int.Parse(dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
                update apd = new update(Name, Price,Count,Age_from,Age_to);
                apd.ShowDialog();
            }

                
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            int parsedValue;
            if (!int.TryParse(textBox1.Text, out parsedValue) & textBox1.Text != "")
            {
                MessageBox.Show("This is a number only field");
                textBox1.Text = "";
                return;
            }
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
                textBox2.Text = "";
                return;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.ConnectionString))
                {
                    String query = "SELECT * FROM Toys WHERE Price <= @price AND Age_from <= @age_from AND Age_to >= @age_to;";

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@price", int.Parse(textBox1.Text));
                        command.Parameters.AddWithValue("@age_from", int.Parse(textBox2.Text));
                        command.Parameters.AddWithValue("@age_to", int.Parse(textBox3.Text));


                        connection.Open();

                        OleDbDataAdapter sqlDataAdap = new OleDbDataAdapter(command);

                        DataTable dtRecord = new DataTable();
                        sqlDataAdap.Fill(dtRecord);
                        Results rs = new Results(sqlDataAdap, dtRecord);

                        rs.ShowDialog();
                        connection.Close();
                    }
                }

            }
            else
            {
                MessageBox.Show("Не все строки заполнены!");
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

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "" && textBox5.Text != "")
            {
                using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.ConnectionString))
                {
                    String query = "SELECT * FROM Toys WHERE Age_from <= @age_from AND Age_to >= @age_to;";

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@age_from", int.Parse(textBox5.Text));
                        command.Parameters.AddWithValue("@age_to", int.Parse(textBox4.Text));


                        connection.Open();

                        OleDbDataAdapter sqlDataAdap = new OleDbDataAdapter(command);

                        DataTable dtRecord = new DataTable();
                        sqlDataAdap.Fill(dtRecord);
                        Results rs = new Results(sqlDataAdap, dtRecord);

                        rs.ShowDialog();
                        connection.Close();
                    }
                }

            }
            else
            {
                MessageBox.Show("Не все строки заполнены!");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = int.Parse(comboBox1.SelectedIndex.ToString());
            if(asc == 1)
                dataGridView1.Sort(dataGridView1.Columns[index],ListSortDirection.Ascending);
            else
                dataGridView1.Sort(dataGridView1.Columns[index], ListSortDirection.Descending);
        }

        public int asc = 1;
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                asc = 1;
            }
            else
            {
                asc = 0;
            }
            comboBox1_SelectedIndexChanged(sender,e);
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            int parsedValue;
            if (!int.TryParse(textBox6.Text, out parsedValue) & textBox6.Text != "")
            {
                MessageBox.Show("This is a number only field");
                textBox6.Text = "";
                return;
            }
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            int parsedValue;
            if (!int.TryParse(textBox7.Text, out parsedValue) & textBox7.Text != "")
            {
                MessageBox.Show("This is a number only field");
                textBox7.Text = "";
                return;
            }
        }

        private void textBox8_Leave(object sender, EventArgs e)
        {
            int parsedValue;
            if (!int.TryParse(textBox8.Text, out parsedValue) & textBox8.Text != "")
            {
                MessageBox.Show("This is a number only field");
                textBox8.Text = "";
                return;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox6.Text != "" && textBox7.Text != "" && textBox8.Text != "" && int.Parse(textBox6.Text) <= int.Parse(textBox7.Text) )
            {
                using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.ConnectionString))
                {
                    String query = "UPDATE Toys SET Price = Price + Price * @price /100  WHERE Age_from <= @age_from AND Age_to >= @age_to;";

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        
                        command.Parameters.AddWithValue("@price", int.Parse(textBox8.Text));
                        command.Parameters.AddWithValue("@age_from", int.Parse(textBox6.Text));
                        command.Parameters.AddWithValue("@age_to", int.Parse(textBox7.Text));

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
            button1_Click(sender, e);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.ConnectionString))
            {
                String query = "DELETE FROM Toys WHERE Name = @name";

                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", comboBox2.Text);

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    // Check Error
                    if (result < 0)
                        MessageBox.Show("Произошла ошибка при удалении данных!");
                    else
                    {
                        this.toysTableAdapter1.Fill(this.toysDataSet1.Toys);
                        MessageBox.Show("Удаление прошло успешно!");
                    }
                }
            }
        }
    }
}
