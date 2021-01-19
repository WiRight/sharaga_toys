using System;
using System.Windows.Forms;

namespace Toys
{
    public partial class Adress_add : Form
    {
        public Adress_add(int id)
        {
            InitializeComponent();

            textBox1.Text = id.ToString();
        }

        private void Adress_add_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" &&
                textBox5.Text != "")
            {
                var result = AddressService.Add(new AddressData
                {
                    Id = int.Parse(textBox1.Text),
                    FirstName = textBox2.Text,
                    SecondName = textBox3.Text,
                    Birthday = dateTimePicker1.Value.Date,
                    Company = textBox4.Text,
                    PhoneNumber = textBox5.Text
                });

                // Check Error
                MessageBox.Show(result < 0 ? "Произошла ошибка при добавлении данных!" : "Добавление прошло успешно!");

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