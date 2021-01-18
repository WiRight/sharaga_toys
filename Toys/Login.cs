using System;
using System.Windows.Forms;

namespace Toys
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var isLogin = LoginService.Login(textusername.Text.Trim(), textpassword.Text.Trim());

            if (isLogin)
            {
                var cd = new Calendar();
                cd.ShowDialog();
            }
            else
            {
                MessageBox.Show("Такого пользователя не существует");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void Login_Load(object sender, EventArgs e)
        {
        }
    }
}