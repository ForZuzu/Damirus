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

namespace Damirus
{
    public partial class addGhost : Form
    {
        DataBase dataBase = new DataBase();
        int selectedRow;
        public addGhost()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void addGhost_Load(object sender, EventArgs e)
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
            var id6 = textBox6.Text;
            var id7 = textBox7.Text;
            var id8 = textBox8.Text;
            var id9 = textBox10.Text;

            var addQuery = $"Insert into Призраки ([Номер призрака], [Номер набора улик], [Номер охоты], [Наименование призрака], [Редкость (%)], [Способности призраков], [Слабости призраков], Рекомендации, [Минимальный рассудок для начала охоты]) values ('{id1}', '{id2}', '{id3}', '{id4}', '{id5}', '{id6}', '{id7}', '{id8}', '{id9}');";

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
