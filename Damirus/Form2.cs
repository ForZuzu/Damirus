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

    public partial class Form2 : Form
    {
        DataBase dataBase = new DataBase();
        int selectedRow;
        public Form2()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Ghosts ghosts = new Ghosts();
            ghosts.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Oxota oxota = new Oxota();
            oxota.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Uliki uliki = new Uliki();
            uliki.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FeedBack feedBack = new FeedBack();
            feedBack.Show();
        }
    }
}
