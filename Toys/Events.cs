using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Toys
{
    public partial class Events : Form
    {
        public Events()
        {
            InitializeComponent();
        }

        private void Events_Load(object sender, EventArgs e)
        {
            dateTimePicker1.MaxDate = dateTimePicker2.Value;
            dateTimePicker2.MinDate = dateTimePicker1.Value;

            using (var connection = new OleDbConnection(Properties.Settings.Default.ConnectionString))
            {
                const string query = "SELECT * FROM Events";

                using (var command = new OleDbCommand(query, connection))
                {
                    connection.Open();

                    var sqlDataAdap = new OleDbDataAdapter(command);

                    var dtRecord = new DataTable();
                    sqlDataAdap.Fill(dtRecord);
                    dataGridView1.DataSource = dtRecord;
                    connection.Close();
                }
            }
        }

        public void datagridview_datetime_to_time()
        {
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.MaxDate = dateTimePicker2.Value;
            dateTimePicker2.MinDate = dateTimePicker1.Value;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.MaxDate = dateTimePicker2.Value;
            dateTimePicker2.MinDate = dateTimePicker1.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var connection = new OleDbConnection(Properties.Settings.Default.ConnectionString))
            {
                const string query = "SELECT * FROM Events WHERE Date >= @date_from AND Date <= @date_to";

                using (var command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@date_from", dateTimePicker1.Value.Date.ToString());
                    command.Parameters.AddWithValue("@date_to", dateTimePicker2.Value.Date.ToString());

                    connection.Open();

                    var sqlDataAdap = new OleDbDataAdapter(command);

                    var dtRecord = new DataTable();
                    sqlDataAdap.Fill(dtRecord);
                    dataGridView1.DataSource = dtRecord;
                    connection.Close();
                }
            }
        }
    }
}