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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Damirus
{
    enum RowState6
    {
        A,
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }
    public partial class Uliki : Form
    {
        DataBase dataBase = new DataBase();
        int selectedRow;
        public Uliki()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void CreateColumns1()
        {
            dataGridView1.Columns.Add("ID1", "Номер набора улик");
            dataGridView1.Columns.Add("u1", "Улика 1");
            dataGridView1.Columns.Add("u2", "Улика 2");
            dataGridView1.Columns.Add("u3", "Улика 3");
            dataGridView1.Columns.Add("IsNew", String.Empty);
            dataGridView1.Columns["IsNew"].Visible = false;
        }

        private void ReadSingleRow1(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), RowState6.ModifiedNew);
        }

        private void RefreshDataGrid1(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string queryString = $"Select * From Улики;";

            SqlCommand sqlCommand = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow1(dgw, reader);
            }

            reader.Close();
        }

        private void Uliki_Load(object sender, EventArgs e)
        {
            CreateColumns1();
            RefreshDataGrid1(dataGridView1);
        }

        private void ClearFields()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
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
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid1(dataGridView1);
        }

        private void deleteRow()
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            dataGridView1.Rows[index].Visible = false;

            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView1.Rows[index].Cells[4].Value = RowState6.Deleted;
                return;
            }
            dataGridView1.Rows[index].Cells[4].Value = RowState6.Deleted;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            deleteRow();
        }

        private void ChangeRow()
        {
            var SelectedRowIndex = dataGridView1.CurrentCell.RowIndex;

            var id1 = textBox1.Text;
            var id2 = textBox2.Text;
            var id3 = textBox3.Text;
            var id4 = textBox4.Text;

            if (dataGridView1.Rows[SelectedRowIndex].Cells[0].Value.ToString() != String.Empty)
            {
                dataGridView1.Rows[SelectedRowIndex].SetValues(id1, id2, id3, id4);
                dataGridView1.Rows[SelectedRowIndex].Cells[4].Value = RowState6.Modified;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChangeRow();
        }

        private void updateRows()
        {

            dataBase.openConnection();
            for (int ind = 0; ind < dataGridView1.Rows.Count; ind++)
            {
                Debug.WriteLine(ind);
                var rowState = RowState6.A;

                try
                {
                    rowState = (RowState6)dataGridView1.Rows[ind].Cells[4].Value;
                }
                catch
                {

                }

                if (rowState == RowState6.Existed)
                {
                    MessageBox.Show("Ничего не происходит");
                    continue;
                }

                if (rowState == RowState6.Deleted)
                {
                    MessageBox.Show("Изменения сохранены!");
                    var id = Convert.ToInt32(dataGridView1.Rows[ind].Cells[0].Value);
                    var deleteQuery = $"Delete from Улики Where [Номер набора улик] = '{id}';";

                    var command = new SqlCommand(deleteQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }

                if (rowState == RowState6.Modified)
                {
                    MessageBox.Show("Изменения сохранены!");
                    var id1 = dataGridView1.Rows[ind].Cells[0].Value.ToString();
                    var id2 = dataGridView1.Rows[ind].Cells[1].Value.ToString();
                    var id3 = dataGridView1.Rows[ind].Cells[2].Value.ToString();
                    var id4 = dataGridView1.Rows[ind].Cells[3].Value.ToString();

                    var changeQuery = $"Update Улики Set [Номер набора улик] = '{id1}', [Улика 1] = '{id2}', [Улика 2] = '{id3}', [Улика 3] = '{id4}' Where [Номер набора улик] = '{id1}'";

                    var command = new SqlCommand(changeQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }
            }
            dataBase.closeConnection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            updateRows();
            ClearFields();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            addUliki addUliki = new addUliki();
            addUliki.Show();
        }

        private void SearchBox(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string query = $"Select * from Улики Where concat ([Номер набора улик], [Улика 1], [Улика 2], [Улика 3]) like '%" + textBox9.Text + "%'";

            SqlCommand sqlCommand = new SqlCommand(query, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow1(dgw, reader);
            }

            reader.Close();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            SearchBox(dataGridView1);
        }
    }
}
