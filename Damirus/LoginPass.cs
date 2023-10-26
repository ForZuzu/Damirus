using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Damirus
{
    public partial class LoginPass : Form
    {
        public LoginPass()
        {
            InitializeComponent();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if(textBox2.Text == "5543")
            {
                MessageBox.Show("Успешная авторизация!");
                Form2 form2 = new Form2();
                form2.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неправильный Код! Повторите попытку!");
            }
        }
    }
}
