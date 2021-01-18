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
    public partial class Calendar : Form
    {
        public Calendar()
        {
            InitializeComponent();
        }

        private void Calendar_Load(object sender, EventArgs e)
        {
            using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.ConnectionString))
            {
                String query = "SELECT * FROM Events WHERE Date = @date";

                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@date", DateTime.Today);


                    connection.Open();

                    OleDbDataAdapter sqlDataAdap = new OleDbDataAdapter(command);

                    DataTable dtRecord = new DataTable();
                    sqlDataAdap.Fill(dtRecord);
                    dataGridView1.DataSource = dtRecord;
                    connection.Close();
                }
            }
        }

        private void событияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ev = new Events();
            ev.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var adbook = new Adressbook();
            adbook.ShowDialog();
        }

        private void адресснаяКнигаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Adressbook adbook = new Adressbook();
            adbook.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Event_add evadd = new Event_add();
            evadd.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.ConnectionString))
            {
                String query = "SELECT * FROM Events WHERE Date = @date";

                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@date", DateTime.Today);


                    connection.Open();

                    OleDbDataAdapter sqlDataAdap = new OleDbDataAdapter(command);

                    DataTable dtRecord = new DataTable();
                    sqlDataAdap.Fill(dtRecord);
                    dataGridView1.DataSource = dtRecord;
                    connection.Close();
                }
            }
        }

        private void файлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm mf = new MainForm();
            mf.ShowDialog();
        }
    }
}