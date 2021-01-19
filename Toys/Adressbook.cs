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
    public partial class Adressbook : Form
    {
        public Adressbook()
        {
            InitializeComponent();
        }

        public int add_id_value;

        private void Adressbook_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "toysDataSet3.Adressbook". При необходимости она может быть перемещена или удалена.
            this.adressbookTableAdapter.Fill(this.toysDataSet3.Adressbook);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "toysDataSet3.Users". При необходимости она может быть перемещена или удалена.
            this.usersTableAdapter.Fill(this.toysDataSet3.Users);
            add_id_value = dataGridView1.Rows.Count + 1;

        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            var adrAdd = new Adress_add(add_id_value);
            adrAdd.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                int id = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                string first_name = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                string last_name = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                DateTime birtday = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
                string company = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                string phone_number = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();

                Address_update adr_upd = new Address_update(id, first_name, last_name, birtday, company, phone_number);
                adr_upd.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выберите строку, которую хотите редактировать");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.ConnectionString))
            {
                String query = "SELECT * FROM Adressbook";

                using (OleDbCommand command = new OleDbCommand(query, connection))
                {

                    connection.Open();

                    OleDbDataAdapter sqlDataAdap = new OleDbDataAdapter(command);

                    DataTable dtRecord = new DataTable();
                    sqlDataAdap.Fill(dtRecord);
                    dataGridView1.DataSource = dtRecord;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.ConnectionString))
                {
                    String query = "DELETE FROM Adressbook WHERE [Id] = @id";

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));

                        connection.Open();
                        int result = command.ExecuteNonQuery();

                        // Check Error
                        if (result < 0)
                            MessageBox.Show("Произошла ошибка при удалении данных!");
                        else
                        {
                            MessageBox.Show("Удаление прошло успешно!");
                        }
                    }
                }

            }
            else
            {
                MessageBox.Show("Выберите пожалуйста одну полную строку");
            }
            button3_Click(sender, e);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                textBox1.ReadOnly = true;
            }
            else
            {
                textBox1.ReadOnly = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                dateTimePicker1.Enabled = true;
            }
            else
            {
                dateTimePicker1.Enabled = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox3.Checked)
            {
                textBox2.ReadOnly = true;
            }
            else
            {
                textBox2.ReadOnly = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // по всем параметрам
            if (checkBox1.Checked && checkBox2.Checked && checkBox3.Checked && textBox1.Text != "" && textBox2.Text != "")
            {
                using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.ConnectionString))
                {
                    String query = "SELECT * FROM Adressbook WHERE second_name = @last_name AND Birthday = @birthday AND Company = @company;";

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@last_name", textBox1.Text);
                        command.Parameters.AddWithValue("@birthday", dateTimePicker1.Value.Date);
                        command.Parameters.AddWithValue("@company", textBox2.Text);


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
            else if (checkBox1.Checked && checkBox2.Checked && !checkBox3.Checked && textBox1.Text != "")
            {
                // фамилия и др
                using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.ConnectionString))
                {
                    String query = "SELECT * FROM Adressbook WHERE second_name = @last_name AND Birthday = @birthday";

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@last_name", textBox1.Text);
                        command.Parameters.AddWithValue("@birthday", dateTimePicker1.Value.Date);


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
            else if (checkBox1.Checked && !checkBox2.Checked && checkBox3.Checked && textBox1.Text != "" && textBox2.Text != "")
            {
                // фамилия компания
                using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.ConnectionString))
                {
                    String query = "SELECT * FROM Adressbook WHERE second_name = @last_name AND Company = @company;";

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@last_name", textBox1.Text);
                        command.Parameters.AddWithValue("@company", textBox2.Text);


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
            else if (!checkBox1.Checked && checkBox2.Checked && checkBox3.Checked && textBox2.Text != "")
            {
                //др компания
                using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.ConnectionString))
                {
                    String query = "SELECT * FROM Adressbook WHERE Birthday = @birthday AND Company = @company;";

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@birthday", dateTimePicker1.Value.Date);
                        command.Parameters.AddWithValue("@company", textBox2.Text);


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
            else if (checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked && textBox1.Text != "")
            {
                //фамилия
                using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.ConnectionString))
                {
                    String query = "SELECT * FROM Adressbook WHERE second_name = @last_name;";

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@last_name", textBox1.Text);


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
            else if (!checkBox1.Checked && checkBox2.Checked && !checkBox3.Checked)
            {
                //др

                using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.ConnectionString))
                {
                    String query = "SELECT * FROM Adressbook WHERE Birthday = @birthday;";

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@birthday", dateTimePicker1.Value.Date);


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
            else if (!checkBox1.Checked && !checkBox2.Checked && checkBox3.Checked && textBox2.Text != "")
            {
                //Компания
                using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.ConnectionString))
                {
                    String query = "SELECT * FROM Adressbook WHERE Company = @company;";

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@company", textBox2.Text);


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

        }
    }
}
