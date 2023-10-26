﻿using System;
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
    enum RowState7
    {
        A,
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }
    public partial class FeedBack : Form
    {
        DataBase dataBase = new DataBase();
        int selectedRow;
        public FeedBack()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void CreateColumns1()
        {
            dataGridView1.Columns.Add("ID1", "Номер призрака");
            dataGridView1.Columns.Add("active", "Активность");
            dataGridView1.Columns.Add("prefer1", "Предпочитаемые вещи для взаимодействия");
            dataGridView1.Columns.Add("prefer2", "Предпочтение комнат");
            dataGridView1.Columns.Add("power", "Влияние на активность");
            dataGridView1.Columns.Add("IsNew", String.Empty);
            dataGridView1.Columns["IsNew"].Visible = false;
        }

        private void ReadSingleRow1(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4), RowState7.ModifiedNew);
        }

        private void RefreshDataGrid1(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string queryString = $"Select * From [Взаимодействие с окружением];";

            SqlCommand sqlCommand = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow1(dgw, reader);
            }

            reader.Close();
        }

        private void FeedBack_Load(object sender, EventArgs e)
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
            textBox5.Text = "";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid1(dataGridView1);
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
            }
        }

        private void deleteRow()
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            dataGridView1.Rows[index].Visible = false;

            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView1.Rows[index].Cells[5].Value = RowState7.Deleted;
                return;
            }
            dataGridView1.Rows[index].Cells[5].Value = RowState7.Deleted;
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
            var id5 = textBox5.Text;

            if (dataGridView1.Rows[SelectedRowIndex].Cells[0].Value.ToString() != String.Empty)
            {
                dataGridView1.Rows[SelectedRowIndex].SetValues(id1, id2, id3, id4, id5);
                dataGridView1.Rows[SelectedRowIndex].Cells[5].Value = RowState7.Modified;
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
                var rowState = RowState7.A;

                try
                {
                    rowState = (RowState7)dataGridView1.Rows[ind].Cells[5].Value;
                }
                catch
                {

                }

                if (rowState == RowState7.Existed)
                {
                    MessageBox.Show("Ничего не происходит");
                    continue;
                }

                if (rowState == RowState7.Deleted)
                {
                    MessageBox.Show("Изменения сохранены!");
                    var id = Convert.ToInt32(dataGridView1.Rows[ind].Cells[0].Value);
                    var deleteQuery = $"Delete from [Взаимодействие с окружением] Where [Номер призрака] = '{id}';";

                    var command = new SqlCommand(deleteQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }

                if (rowState == RowState7.Modified)
                {
                    MessageBox.Show("Изменения сохранены!");
                    var id1 = dataGridView1.Rows[ind].Cells[0].Value.ToString();
                    var id2 = dataGridView1.Rows[ind].Cells[1].Value.ToString();
                    var id3 = dataGridView1.Rows[ind].Cells[2].Value.ToString();
                    var id4 = dataGridView1.Rows[ind].Cells[3].Value.ToString();
                    var id5 = dataGridView1.Rows[ind].Cells[4].Value.ToString();


                    var changeQuery = $"Update [Взаимодействие с окружением] Set [Номер призрака] = '{id1}', Активность = '{id2}', [Предпочитаемые вещи для взаимодействия] = '{id3}', [Предпочтение комнат] = '{id4}', [Влияние на активность] = '{id5}' Where [Номер призрака] = '{id1}'";

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

        private void SearchBox(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string query = $"Select * from [Взаимодействие с окружением] Where concat ([Номер призрака], Активность, [Предпочитаемые вещи для взаимодействия], [Предпочтение комнат], [Влияние на активность]) like '%" + textBox9.Text + "%'";

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

        private void button12_Click(object sender, EventArgs e)
        {
            FeedBackAdd feedBackAdd = new FeedBackAdd();
            feedBackAdd.Show();
        }
    }
}
