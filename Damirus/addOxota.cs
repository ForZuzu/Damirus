using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Damirus
{
    public partial class addOxota : Form
    {
        DataBase dataBase = new DataBase();
        int selectedRow;
        public addOxota()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void addOxota_Load(object sender, EventArgs e)
        {
            
        }

        private void button12_Click(object sender, EventArgs e)
        {
            dataBase.openConnection();
            var id1 = textBox1.Text;
            var id2 = textBox2.Text;
            var id3 = textBox3.Text;
            var id4 = textBox4.Text;
            var id5 = textBox5.Text;

            var addQuery = $"Insert into Охота ([Номер охоты], [Стадия охоты], [Скорость призрака], [Процент выживаемости без укрытий (%)], [Рекомендации для выживаемости во время охоты]) values ('{id1}', '{id2}', '{id3}', '{id4}', '{id5}');";

            var command = new SqlCommand(addQuery, dataBase.getConnection());
            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Запись успешно создана!");
            }
            catch
            {
                MessageBox.Show("Ошибка! Возможно вы забыли добавить новый набор улик!");
            }
            dataBase.closeConnection();
        }
    }
}
