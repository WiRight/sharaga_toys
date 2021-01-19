using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Toys
{
    public partial class Address_update : Form
    {
        private int id_u;
        private string first_name_u;
        private string last_name_u;
        private DateTime birthday_u;
        private string company_u;
        private string phone_number_u;

        public Address_update(int Id, string first_name, string last_name, DateTime birthday, string company,
            string phone_number)
        {
            InitializeComponent();

            textBox1.Text = Id.ToString();
            textBox2.Text = first_name;
            textBox3.Text = last_name;
            dateTimePicker1.Value = birthday;
            textBox4.Text = company;
            textBox5.Text = phone_number;

            id_u = Id;
            first_name_u = first_name;
            last_name_u = last_name;
            birthday_u = birthday;
            company_u = company;
            phone_number_u = phone_number;
        }

        private void Adress_update_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" &&
                textBox5.Text != "")
            {
                var result = AddressService.Update(new AddressData
                {
                    Id = int.Parse(textBox1.Text),
                    Birthday = Convert.ToDateTime(dateTimePicker1.Value.ToString(CultureInfo.CurrentCulture)),
                    Company = textBox4.Text,
                    FirstName = textBox2.Text,
                    PhoneNumber = textBox5.Text,
                    SecondName = textBox3.Text
                });

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
            else
            {
                MessageBox.Show("Есть отсуствующие данные");
            }
        }
    }
}