using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Damirus
{
    enum RowState4
    {
        A,
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }
    public partial class Ghosts : Form
    {
        DataBase dataBase = new DataBase();
        int selectedRow;
        public Ghosts()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void CreateColumns1()
        {
            dataGridView1.Columns.Add("ID1", "Номер призрака");
            dataGridView1.Columns.Add("ID2", "Номер набора улик");
            dataGridView1.Columns.Add("ID3", "Номер охоты");
            dataGridView1.Columns.Add("namePriz", "Наименование призрака");
            dataGridView1.Columns.Add("Rare", "Редкость (%)");
            dataGridView1.Columns.Add("Skills", "Способности призраков");
            dataGridView1.Columns.Add("Weaks", "Слабости призраков");
            dataGridView1.Columns.Add("Recs", "Рекомендации");
            dataGridView1.Columns.Add("MinRas", "Минимальный рассудок для начала охоты");
            dataGridView1.Columns.Add("IsNew", String.Empty);
            dataGridView1.Columns["IsNew"].Visible = false;
        }

        private void ReadSingleRow1(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetInt32(1), record.GetInt32(2), record.GetString(3), record.GetString(4), record.GetString(5), record.GetString(6), record.GetString(7), record.GetInt32(8), RowState4.ModifiedNew);
        }

        private void RefreshDataGrid1(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string queryString = $"Select * From Призраки;";

            SqlCommand sqlCommand = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow1(dgw, reader);
            }

            reader.Close();
        }

        private void Ghosts_Load(object sender, EventArgs e)
        {
            CreateColumns1();
            RefreshDataGrid1(dataGridView1);
        }

        private void ChangeRow()
        {
            var SelectedRowIndex = dataGridView1.CurrentCell.RowIndex;

            var id1 = textBox1.Text;
            var id2 = textBox2.Text;
            var id3 = textBox3.Text;
            var id4 = textBox4.Text;
            var id5 = textBox5.Text;
            var id6 = textBox6.Text;
            var id7 = textBox7.Text;
            var id8 = textBox8.Text;
            var id9 = textBox10.Text;

            if (dataGridView1.Rows[SelectedRowIndex].Cells[0].Value.ToString() != String.Empty)
            {
                dataGridView1.Rows[SelectedRowIndex].SetValues(id1, id2, id3, id4, id5, id6, id7, id8, id9);
                dataGridView1.Rows[SelectedRowIndex].Cells[9].Value = RowState4.Modified;
            }
        }

        private void updateRows()
        {

            dataBase.openConnection();
            for (int ind = 0; ind < dataGridView1.Rows.Count; ind++)
            {
                Debug.WriteLine(ind);
                var rowState = RowState4.A;

                try
                {
                    rowState = (RowState4)dataGridView1.Rows[ind].Cells[9].Value;
                }
                catch
                {

                }

                if (rowState == RowState4.Existed)
                {
                    MessageBox.Show("Ничего не происходит");
                    continue;
                }

                if (rowState == RowState4.Deleted)
                {
                    MessageBox.Show("Изменения сохранены!");
                    var id = Convert.ToInt32(dataGridView1.Rows[ind].Cells[0].Value);
                    var deleteQuery = $"Delete from Призраки Where [Номер призрака] = '{id}';";

                    var command = new SqlCommand(deleteQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }

                if (rowState == RowState4.Modified)
                {
                    MessageBox.Show("Изменения сохранены!");
                    var id1 = dataGridView1.Rows[ind].Cells[0].Value.ToString();
                    var id2 = dataGridView1.Rows[ind].Cells[1].Value.ToString();
                    var id3 = dataGridView1.Rows[ind].Cells[2].Value.ToString();
                    var id4 = dataGridView1.Rows[ind].Cells[3].Value.ToString();
                    var id5 = dataGridView1.Rows[ind].Cells[4].Value.ToString();
                    var id6 = dataGridView1.Rows[ind].Cells[5].Value.ToString();
                    var id7 = dataGridView1.Rows[ind].Cells[6].Value.ToString();
                    var id8 = dataGridView1.Rows[ind].Cells[7].Value.ToString();
                    var id9 = dataGridView1.Rows[ind].Cells[8].Value.ToString();


                    var changeQuery = $"Update Призраки Set [Номер призрака] = '{id1}', [Номер набора улик] = '{id2}', [Номер охоты] = '{id3}', [Наименование призрака] = '{id4}', [Редкость (%)] = '{id5}', [Способности призраков] = '{id6}', [Слабости призраков] = '{id7}', Рекомендации = '{id8}', [Минимальный рассудок для начала охоты] = '{id9}' Where [Номер призрака] = '{id1}'";

                    var command = new SqlCommand(changeQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }
            }
            dataBase.closeConnection();
        }

        

        private void button12_Click(object sender, EventArgs e)
        {
            addGhost addGhost = new addGhost();
            addGhost.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChangeRow();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            updateRows();
        }

        private void SearchBox(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string query = $"Select * from Призраки Where concat ([Номер призрака], [Номер набора улик], [Номер охоты], [Наименование призрака], [Редкость (%)], [Способности призраков], [Слабости призраков], Рекомендации) like '%" + textBox9.Text + "%'";

            SqlCommand sqlCommand = new SqlCommand(query, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow1(dgw, reader);
            }

            reader.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                textBox3.Text = row.Cells[2].Value.ToString();
                textBox4.Text = row.Cells[3].Value.ToString();
                textBox5.Text = row.Cells[4].Value.ToString();
                textBox6.Text = row.Cells[5].Value.ToString();
                textBox7.Text = row.Cells[6].Value.ToString();
                textBox8.Text = row.Cells[7].Value.ToString();
                textBox10.Text = row.Cells[8].Value.ToString();
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            SearchBox(dataGridView1);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid1(dataGridView1);
        }

        private void ClearFields()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            textBox4.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox10.Text = "";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
    }
}
