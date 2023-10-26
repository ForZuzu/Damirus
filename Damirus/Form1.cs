using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Damirus
{
    enum RowState1
    {
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }
    enum RowState2
    {
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }
    enum RowState3
    {
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }
    public partial class Form1 : Form
    {
        DataBase dataBase = new DataBase();
        DataBase dataBase2 = new DataBase();

        int k = 0;
        int t = 0;
        int selectedRow;
        string Sort1 = "";
        string Sort2 = "";
        string Sort3 = "";
        string Sort4 = "";
        string Sort5 = "";
        public Form1()
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
            dgw.Rows.Add(record.GetInt32(0), record.GetInt32(1), record.GetInt32(2), record.GetString(3), record.GetString(4), record.GetString(5), record.GetString(6), record.GetString(7), record.GetInt32(8), RowState1.ModifiedNew);
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

        private void CreateColumns2()
        {
            dataGridView2.Columns.Add("ID1", "Номер призрака");
            dataGridView2.Columns.Add("active", "Активность");
            dataGridView2.Columns.Add("Love", "Предпочитаемые вещи для взаимодействия");
            dataGridView2.Columns.Add("predpod", "Предпочтение комнат");
            dataGridView2.Columns.Add("Vliyan", "Влияние на активность");
            dataGridView2.Columns.Add("IsNew", String.Empty);
            dataGridView2.Columns["IsNew"].Visible = false;
        }

        private void ReadSingleRow2(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4), RowState2.ModifiedNew);
        }

        private void RefreshDataGrid2(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string queryString = $"Select * From [Взаимодействие с окружением];";

            SqlCommand sqlCommand = new SqlCommand(queryString, dataBase2.getConnection());

            dataBase2.openConnection();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow2(dgw, reader);
            }

            reader.Close();
        }

        private void CreateColumns3()
        {
            dataGridView3.Columns.Add("ID1", "Номер Охоты");
            dataGridView3.Columns.Add("Status", "Стадия Охоты");
            dataGridView3.Columns.Add("speed", "Скорость призрака");
            dataGridView3.Columns.Add("percentV", "Процент выживаемости без укрытий (%)");
            dataGridView3.Columns.Add("Rec", "Рекомендации для выживаемости во время охоты");
            dataGridView3.Columns.Add("IsNew", String.Empty);
            dataGridView3.Columns["IsNew"].Visible = false;
        }

        private void ReadSingleRow3(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetInt32(3), record.GetString(4), RowState3.ModifiedNew);
        }

        private void RefreshDataGrid3(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string queryString = $"Select * From Охота;";

            SqlCommand sqlCommand = new SqlCommand(queryString, dataBase2.getConnection());

            dataBase2.openConnection();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow3(dgw, reader);
            }

            reader.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            CreateColumns1();
            RefreshDataGrid1(dataGridView1);
            CreateColumns2();
            RefreshDataGrid2(dataGridView2);
            CreateColumns3();
            RefreshDataGrid3(dataGridView3);
        }

        private void Sorting1(DataGridView dgw)
        {
            dgw.Rows.Clear();
            MessageBox.Show(Sort5);
            string queryString = $"Select * From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' or [Улика 2] = '{Sort1}' or [Улика 3] = '{Sort1}') and ([Улика 1] = '{Sort2}' or [Улика 2] = '{Sort2}' or [Улика 3] = '{Sort2}') and ([Улика 1] = '{Sort3}' or [Улика 2] = '{Sort3}' or [Улика 3] = '{Sort3}')) And [Номер охоты] IN (Select [Номер охоты] From Охота Where [Скорость призрака] = '{Sort4}') And [Номер призрака] IN (Select [Номер призрака] From [Взаимодействие с окружением] Where [Предпочитаемые вещи для взаимодействия] = '{Sort5}');";
            //And [Номер призрака] IN (Select [Номер призрака] From [Взаимодействие с окружением] Where [Предпочитаемые вещи для взаимодействия] = '{Sort5}')
            string queryString2 = $"Select * From [Взаимодействие с окружением] Where [Номер призрака] = (Select [Номер призрака] From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' or [Улика 2] = '{Sort1}' or [Улика 3] = '{Sort1}') and ([Улика 1] = '{Sort2}' or [Улика 2] = '{Sort2}' or [Улика 3] = '{Sort2}') and ([Улика 1] = '{Sort3}' or [Улика 2] = '{Sort3}' or [Улика 3] = '{Sort3}')) And [Номер охоты] IN (Select [Номер охоты] From Охота Where [Скорость призрака] = '{Sort4}')) And [Номер призрака] IN (Select [Номер призрака] From [Взаимодействие с окружением] Where [Предпочитаемые вещи для взаимодействия] = '{Sort5}');";
            //And [Номер призрака] IN (Select [Номер призрака] From [Взаимодействие с окружением] Where [Предпочитаемые вещи для взаимодействия] = '{Sort5}')


            SqlCommand sqlCommand = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow1(dgw, reader);
            }

            reader.Close();
            SqlCommand sqlCommand2 = new SqlCommand(queryString2, dataBase.getConnection());

            dataGridView2.Rows.Clear();

            SqlDataReader reader2 = sqlCommand2.ExecuteReader();

            while (reader2.Read())
            {
                ReadSingleRow2(dataGridView2, reader2);
            }

            reader2.Close();
            string queryString3 = $"Select * From Охота Where [Номер охоты] = (Select [Номер охоты] From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' or [Улика 2] = '{Sort1}' or [Улика 3] = '{Sort1}') and ([Улика 1] = '{Sort2}' or [Улика 2] = '{Sort2}' or [Улика 3] = '{Sort2}') and ([Улика 1] = '{Sort3}' or [Улика 2] = '{Sort3}' or [Улика 3] = '{Sort3}')) And [Номер охоты] IN (Select [Номер охоты] From Охота Where [Скорость призрака] = '{Sort4}') And [Номер призрака] In (Select [Номер призрака] From [Взаимодействие с окружением] Where [Предпочитаемые вещи для взаимодействия] = '{Sort5}'));";
            SqlCommand sqlCommand3 = new SqlCommand(queryString3, dataBase.getConnection());
            dataGridView3.Rows.Clear();
            SqlDataReader reader3 = sqlCommand3.ExecuteReader();
            while (reader3.Read())
            {
                ReadSingleRow3(dataGridView3, reader3);
            }
            reader3.Close();
        }

        private void Sorting1CCC(DataGridView dgw)
        {
            dgw.Rows.Clear();
            MessageBox.Show(Sort5);
            string queryString = $"Select * From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' or [Улика 2] = '{Sort1}' or [Улика 3] = '{Sort1}') and ([Улика 1] = '{Sort2}' or [Улика 2] = '{Sort2}' or [Улика 3] = '{Sort2}') and ([Улика 1] = '{Sort3}' or [Улика 2] = '{Sort3}' or [Улика 3] = '{Sort3}')) And [Номер охоты] IN (Select [Номер охоты] From Охота Where [Скорость призрака] = '{Sort4}');";
            //And [Номер призрака] IN (Select [Номер призрака] From [Взаимодействие с окружением] Where [Предпочитаемые вещи для взаимодействия] = '{Sort5}')
            string queryString2 = $"Select * From [Взаимодействие с окружением] Where [Номер призрака] = (Select [Номер призрака] From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' or [Улика 2] = '{Sort1}' or [Улика 3] = '{Sort1}') and ([Улика 1] = '{Sort2}' or [Улика 2] = '{Sort2}' or [Улика 3] = '{Sort2}') and ([Улика 1] = '{Sort3}' or [Улика 2] = '{Sort3}' or [Улика 3] = '{Sort3}')) And [Номер охоты] IN (Select [Номер охоты] From Охота Where [Скорость призрака] = '{Sort4}'));";
            //And [Номер призрака] IN (Select [Номер призрака] From [Взаимодействие с окружением] Where [Предпочитаемые вещи для взаимодействия] = '{Sort5}')


            SqlCommand sqlCommand = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow1(dgw, reader);
            }

            reader.Close();
            SqlCommand sqlCommand2 = new SqlCommand(queryString2, dataBase.getConnection());

            dataGridView2.Rows.Clear();

            SqlDataReader reader2 = sqlCommand2.ExecuteReader();

            while (reader2.Read())
            {
                ReadSingleRow2(dataGridView2, reader2);
            }

            reader2.Close();
            string queryString3 = $"Select * From Охота Where [Номер охоты] = (Select [Номер охоты] From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' or [Улика 2] = '{Sort1}' or [Улика 3] = '{Sort1}') and ([Улика 1] = '{Sort2}' or [Улика 2] = '{Sort2}' or [Улика 3] = '{Sort2}') and ([Улика 1] = '{Sort3}' or [Улика 2] = '{Sort3}' or [Улика 3] = '{Sort3}')) And [Номер охоты] IN (Select [Номер охоты] From Охота Where [Скорость призрака] = '{Sort4}'));";
            SqlCommand sqlCommand3 = new SqlCommand(queryString3, dataBase.getConnection());
            dataGridView3.Rows.Clear();
            SqlDataReader reader3 = sqlCommand3.ExecuteReader();
            while (reader3.Read())
            {
                ReadSingleRow3(dataGridView3, reader3);
            }
            reader3.Close();
        }

        private void Sorting1_1(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string queryString = $"Select * From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' Or [Улика 2] = '{Sort1}' Or [Улика 3] = '{Sort1}') And ([Улика 1] = '{Sort2}' Or [Улика 2] = '{Sort2}' Or [Улика 3] = '{Sort2}') And ([Улика 1] = '{Sort3}' Or [Улика 2] = '{Sort3}' Or [Улика 3] = '{Sort3}')) And [Номер призрака] IN (Select [Номер призрака] From [Взаимодействие с окружением] Where [Предпочитаемые вещи для взаимодействия] = '{Sort5}');";
            string queryString2 = $"Select * From [Взаимодействие с окружением] Where [Номер призрака] IN (Select [Номер призрака] From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' Or [Улика 2] = '{Sort1}' Or [Улика 3] = '{Sort1}') And ([Улика 1] = '{Sort2}' Or [Улика 2] = '{Sort2}' Or [Улика 3] = '{Sort2}') And ([Улика 1] = '{Sort3}' Or [Улика 2] = '{Sort3}' Or [Улика 3] = '{Sort3}')) And [Номер призрака] IN (Select [Номер призрака] From [Взаимодействие с окружением] Where [Предпочитаемые вещи для взаимодействия] = '{Sort5}'));";

            SqlCommand sqlCommand = new SqlCommand(queryString, dataBase.getConnection());
            SqlCommand sqlCommand2 = new SqlCommand(queryString2, dataBase.getConnection());

            dataBase.openConnection();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow1(dgw, reader);
            }
            reader.Close();

            dataGridView2.Rows.Clear();

            SqlDataReader reader2 = sqlCommand2.ExecuteReader();

            while (reader2.Read())
            {
                ReadSingleRow2(dataGridView2, reader2);
            }

            reader2.Close();
            string queryString3 = $"Select * From Охота Where [Номер охоты] IN (Select [Номер охоты] From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' Or [Улика 2] = '{Sort1}' Or [Улика 3] = '{Sort1}') And ([Улика 1] = '{Sort2}' Or [Улика 2] = '{Sort2}' Or [Улика 3] = '{Sort2}') And ([Улика 1] = '{Sort3}' Or [Улика 2] = '{Sort3}' Or [Улика 3] = '{Sort3}')) And [Номер призрака] In (Select [Номер призрака] From [Взаимодействие с окружением] Where [Предпочитаемые вещи для взаимодействия] = '{Sort5}'));";
            SqlCommand sqlCommand3 = new SqlCommand(queryString3, dataBase.getConnection());
            dataGridView3.Rows.Clear();
            SqlDataReader reader3 = sqlCommand3.ExecuteReader();
            while (reader3.Read())
            {
                ReadSingleRow3(dataGridView3, reader3);
            }
            reader3.Close();
        }

        private void Sorting1_1CCC(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string queryString = $"Select * From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' Or [Улика 2] = '{Sort1}' Or [Улика 3] = '{Sort1}') And ([Улика 1] = '{Sort2}' Or [Улика 2] = '{Sort2}' Or [Улика 3] = '{Sort2}') And ([Улика 1] = '{Sort3}' Or [Улика 2] = '{Sort3}' Or [Улика 3] = '{Sort3}'));";
            string queryString2 = $"Select * From [Взаимодействие с окружением] Where [Номер призрака] IN (Select [Номер призрака] From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' Or [Улика 2] = '{Sort1}' Or [Улика 3] = '{Sort1}') And ([Улика 1] = '{Sort2}' Or [Улика 2] = '{Sort2}' Or [Улика 3] = '{Sort2}') And ([Улика 1] = '{Sort3}' Or [Улика 2] = '{Sort3}' Or [Улика 3] = '{Sort3}')));";

            SqlCommand sqlCommand = new SqlCommand(queryString, dataBase.getConnection());
            SqlCommand sqlCommand2 = new SqlCommand(queryString2, dataBase.getConnection());

            dataBase.openConnection();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow1(dgw, reader);
            }
            reader.Close();

            dataGridView2.Rows.Clear();

            SqlDataReader reader2 = sqlCommand2.ExecuteReader();

            while (reader2.Read())
            {
                ReadSingleRow2(dataGridView2, reader2);
            }

            reader2.Close();
            string queryString3 = $"Select * From Охота Where [Номер охоты] IN (Select [Номер охоты] From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' Or [Улика 2] = '{Sort1}' Or [Улика 3] = '{Sort1}') And ([Улика 1] = '{Sort2}' Or [Улика 2] = '{Sort2}' Or [Улика 3] = '{Sort2}') And ([Улика 1] = '{Sort3}' Or [Улика 2] = '{Sort3}' Or [Улика 3] = '{Sort3}')));";
            SqlCommand sqlCommand3 = new SqlCommand(queryString3, dataBase.getConnection());
            dataGridView3.Rows.Clear();
            SqlDataReader reader3 = sqlCommand3.ExecuteReader();
            while (reader3.Read())
            {
                ReadSingleRow3(dataGridView3, reader3);
            }
            reader3.Close();
        }

        private void Sorting2(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string queryString = $"Select * From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' Or [Улика 2] = '{Sort1}' Or [Улика 3] = '{Sort1}') And ([Улика 1] = '{Sort2}' Or [Улика 2] = '{Sort2}' Or [Улика 3] = '{Sort2}')) And [Номер охоты] IN (Select [Номер охоты] From Охота Where [Скорость призрака] = '{Sort4}') And [Номер призрака] IN (Select [Номер призрака] From [Взаимодействие с окружением] Where [Предпочитаемые вещи для взаимодействия] = '{Sort5}');";

            SqlCommand sqlCommand = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow1(dgw, reader);
            }

            reader.Close();
            string queryString2 = $"Select * From [Взаимодействие с окружением] Where [Номер призрака] IN (Select [Номер призрака] From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' Or [Улика 2] = '{Sort1}' Or [Улика 3] = '{Sort1}') And ([Улика 1] = '{Sort2}' Or [Улика 2] = '{Sort2}' Or [Улика 3] = '{Sort2}')) And [Номер охоты] IN (Select [Номер охоты] From Охота Where [Скорость призрака] = '{Sort4}')) And [Номер призрака] IN (Select [Номер призрака] From [Взаимодействие с окружением] Where [Предпочитаемые вещи для взаимодействия] = '{Sort5}');";

            SqlCommand sqlCommand2 = new SqlCommand(queryString2, dataBase.getConnection());

            dataGridView2.Rows.Clear();

            SqlDataReader reader2 = sqlCommand2.ExecuteReader();

            while (reader2.Read())
            {
                ReadSingleRow2(dataGridView2, reader2);
            }

            reader2.Close();
            string queryString3 = $"Select * From Охота Where [Номер охоты] IN (Select [Номер охоты] From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' Or [Улика 2] = '{Sort1}' Or [Улика 3] = '{Sort1}') And ([Улика 1] = '{Sort2}' Or [Улика 2] = '{Sort2}' Or [Улика 3] = '{Sort2}')) And [Номер охоты] IN (Select [Номер охоты] From Охота Where [Скорость призрака] = '{Sort4}') And [Номер призрака] In (Select [Номер призрака] From [Взаимодействие с окружением] Where [Предпочитаемые вещи для взаимодействия] = '{Sort5}'));";
            SqlCommand sqlCommand3 = new SqlCommand(queryString3, dataBase.getConnection());
            dataGridView3.Rows.Clear();
            SqlDataReader reader3 = sqlCommand3.ExecuteReader();
            while (reader3.Read())
            {
                ReadSingleRow3(dataGridView3, reader3);
            }
            reader3.Close();
        }

        private void Sorting2CCC(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string queryString = $"Select * From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' Or [Улика 2] = '{Sort1}' Or [Улика 3] = '{Sort1}') And ([Улика 1] = '{Sort2}' Or [Улика 2] = '{Sort2}' Or [Улика 3] = '{Sort2}')) And [Номер охоты] IN (Select [Номер охоты] From Охота Where [Скорость призрака] = '{Sort4}');";

            SqlCommand sqlCommand = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow1(dgw, reader);
            }

            reader.Close();
            string queryString2 = $"Select * From [Взаимодействие с окружением] Where [Номер призрака] IN (Select [Номер призрака] From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' Or [Улика 2] = '{Sort1}' Or [Улика 3] = '{Sort1}') And ([Улика 1] = '{Sort2}' Or [Улика 2] = '{Sort2}' Or [Улика 3] = '{Sort2}')) And [Номер охоты] IN (Select [Номер охоты] From Охота Where [Скорость призрака] = '{Sort4}'));";

            SqlCommand sqlCommand2 = new SqlCommand(queryString2, dataBase.getConnection());

            dataGridView2.Rows.Clear();

            SqlDataReader reader2 = sqlCommand2.ExecuteReader();

            while (reader2.Read())
            {
                ReadSingleRow2(dataGridView2, reader2);
            }

            reader2.Close();
            string queryString3 = $"Select * From Охота Where [Номер охоты] IN (Select [Номер охоты] From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' Or [Улика 2] = '{Sort1}' Or [Улика 3] = '{Sort1}') And ([Улика 1] = '{Sort2}' Or [Улика 2] = '{Sort2}' Or [Улика 3] = '{Sort2}')) And [Номер охоты] IN (Select [Номер охоты] From Охота Where [Скорость призрака] = '{Sort4}'));";
            SqlCommand sqlCommand3 = new SqlCommand(queryString3, dataBase.getConnection());
            dataGridView3.Rows.Clear();
            SqlDataReader reader3 = sqlCommand3.ExecuteReader();
            while (reader3.Read())
            {
                ReadSingleRow3(dataGridView3, reader3);
            }
            reader3.Close();
        }

        private void Sorting2_2(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string queryString = $"Select * From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' Or [Улика 2] = '{Sort1}' Or [Улика 3] = '{Sort1}') And ([Улика 1] = '{Sort2}' Or [Улика 2] = '{Sort2}' Or [Улика 3] = '{Sort2}')) And [Номер призрака] IN (Select [Номер призрака] From [Взаимодействие с окружением] Where [Предпочитаемые вещи для взаимодействия] = '{Sort5}');";

            SqlCommand sqlCommand = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow1(dgw, reader);
            }

            reader.Close();
            string queryString2 = $"Select * From [Взаимодействие с окружением] Where [Номер призрака] IN (Select [Номер призрака] From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' Or [Улика 2] = '{Sort1}' Or [Улика 3] = '{Sort1}') And ([Улика 1] = '{Sort2}' Or [Улика 2] = '{Sort2}' Or [Улика 3] = '{Sort2}')) And [Номер призрака] IN (Select [Номер призрака] From [Взаимодействие с окружением] Where [Предпочитаемые вещи для взаимодействия] = '{Sort5}'));";

            SqlCommand sqlCommand2 = new SqlCommand(queryString2, dataBase.getConnection());

            dataGridView2.Rows.Clear();

            SqlDataReader reader2 = sqlCommand2.ExecuteReader();

            while (reader2.Read())
            {
                ReadSingleRow2(dataGridView2, reader2);
            }

            reader2.Close();
            string queryString3 = $"Select * From Охота Where [Номер охоты] IN (Select [Номер охоты] From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' Or [Улика 2] = '{Sort1}' Or [Улика 3] = '{Sort1}') And ([Улика 1] = '{Sort2}' Or [Улика 2] = '{Sort2}' Or [Улика 3] = '{Sort2}')) And [Номер призрака] In (Select [Номер призрака] From [Взаимодействие с окружением] Where [Предпочитаемые вещи для взаимодействия] = '{Sort5}'));";
            SqlCommand sqlCommand3 = new SqlCommand(queryString3, dataBase.getConnection());
            dataGridView3.Rows.Clear();
            SqlDataReader reader3 = sqlCommand3.ExecuteReader();
            while (reader3.Read())
            {
                ReadSingleRow3(dataGridView3, reader3);
            }
            reader3.Close();
        }
        private void Sorting2_2CCC(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string queryString = $"Select * From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' Or [Улика 2] = '{Sort1}' Or [Улика 3] = '{Sort1}') And ([Улика 1] = '{Sort2}' Or [Улика 2] = '{Sort2}' Or [Улика 3] = '{Sort2}'));";

            SqlCommand sqlCommand = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow1(dgw, reader);
            }

            reader.Close();
            string queryString2 = $"Select * From [Взаимодействие с окружением] Where [Номер призрака] IN (Select [Номер призрака] From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' Or [Улика 2] = '{Sort1}' Or [Улика 3] = '{Sort1}') And ([Улика 1] = '{Sort2}' Or [Улика 2] = '{Sort2}' Or [Улика 3] = '{Sort2}')));";

            SqlCommand sqlCommand2 = new SqlCommand(queryString2, dataBase.getConnection());

            dataGridView2.Rows.Clear();

            SqlDataReader reader2 = sqlCommand2.ExecuteReader();

            while (reader2.Read())
            {
                ReadSingleRow2(dataGridView2, reader2);
            }

            reader2.Close();
            string queryString3 = $"Select * From Охота Where [Номер охоты] IN (Select [Номер охоты] From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' Or [Улика 2] = '{Sort1}' Or [Улика 3] = '{Sort1}') And ([Улика 1] = '{Sort2}' Or [Улика 2] = '{Sort2}' Or [Улика 3] = '{Sort2}')));";
            SqlCommand sqlCommand3 = new SqlCommand(queryString3, dataBase.getConnection());
            dataGridView3.Rows.Clear();
            SqlDataReader reader3 = sqlCommand3.ExecuteReader();
            while (reader3.Read())
            {
                ReadSingleRow3(dataGridView3, reader3);
            }
            reader3.Close();
        }

        private void Sorting3(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string queryString = $"Select * From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' Or [Улика 2] = '{Sort1}' Or [Улика 3] = '{Sort1}')) And [Номер охоты] IN (Select [Номер охоты] From Охота Where [Скорость призрака] = '{Sort4}') And [Номер призрака] IN (Select [Номер призрака] From [Взаимодействие с окружением] Where [Предпочитаемые вещи для взаимодействия] = '{Sort5}');";

            SqlCommand sqlCommand = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow1(dgw, reader);
            }

            reader.Close();
            string queryString2 = $"Select * From [Взаимодействие с окружением] Where [Номер призрака] IN (Select [Номер призрака] From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' Or [Улика 2] = '{Sort1}' Or [Улика 3] = '{Sort1}')) And [Номер охоты] IN (Select [Номер охоты] From Охота Where [Скорость призрака] = '{Sort4}')) And [Номер призрака] IN (Select [Номер призрака] From [Взаимодействие с окружением] Where [Предпочитаемые вещи для взаимодействия] = '{Sort5}');";

            SqlCommand sqlCommand2 = new SqlCommand(queryString2, dataBase.getConnection());

            dataGridView2.Rows.Clear();

            SqlDataReader reader2 = sqlCommand2.ExecuteReader();

            while (reader2.Read())
            {
                ReadSingleRow2(dataGridView2, reader2);
            }

            reader2.Close();
            string queryString3 = $"Select * From Охота Where [Номер охоты] IN (Select [Номер охоты] From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' Or [Улика 2] = '{Sort1}' Or [Улика 3] = '{Sort1}')) And [Номер охоты] IN (Select [Номер охоты] From Охота Where [Скорость призрака] = '{Sort4}') And [Номер призрака] In (Select [Номер призрака] From [Взаимодействие с окружением] Where [Предпочитаемые вещи для взаимодействия] = '{Sort5}'));";
            SqlCommand sqlCommand3 = new SqlCommand(queryString3, dataBase.getConnection());
            dataGridView3.Rows.Clear();
            SqlDataReader reader3 = sqlCommand3.ExecuteReader();
            while (reader3.Read())
            {
                ReadSingleRow3(dataGridView3, reader3);
            }
            reader3.Close();
        }
        private void Sorting3CCC(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string queryString = $"Select * From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' Or [Улика 2] = '{Sort1}' Or [Улика 3] = '{Sort1}')) And [Номер охоты] IN (Select [Номер охоты] From Охота Where [Скорость призрака] = '{Sort4}');";

            SqlCommand sqlCommand = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow1(dgw, reader);
            }

            reader.Close();
            string queryString2 = $"Select * From [Взаимодействие с окружением] Where [Номер призрака] IN (Select [Номер призрака] From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' Or [Улика 2] = '{Sort1}' Or [Улика 3] = '{Sort1}')) And [Номер охоты] IN (Select [Номер охоты] From Охота Where [Скорость призрака] = '{Sort4}'));";

            SqlCommand sqlCommand2 = new SqlCommand(queryString2, dataBase.getConnection());

            dataGridView2.Rows.Clear();

            SqlDataReader reader2 = sqlCommand2.ExecuteReader();

            while (reader2.Read())
            {
                ReadSingleRow2(dataGridView2, reader2);
            }

            reader2.Close();
            string queryString3 = $"Select * From Охота Where [Номер охоты] IN (Select [Номер охоты] From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' Or [Улика 2] = '{Sort1}' Or [Улика 3] = '{Sort1}')) And [Номер охоты] IN (Select [Номер охоты] From Охота Where [Скорость призрака] = '{Sort4}'));";
            SqlCommand sqlCommand3 = new SqlCommand(queryString3, dataBase.getConnection());
            dataGridView3.Rows.Clear();
            SqlDataReader reader3 = sqlCommand3.ExecuteReader();
            while (reader3.Read())
            {
                ReadSingleRow3(dataGridView3, reader3);
            }
            reader3.Close();
        }

        private void Sorting3_3(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string queryString = $"Select * From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' Or [Улика 2] = '{Sort1}' Or [Улика 3] = '{Sort1}')) And [Номер призрака] IN (Select [Номер призрака] From [Взаимодействие с окружением] Where [Предпочитаемые вещи для взаимодействия] = '{Sort5}');";

            SqlCommand sqlCommand = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow1(dgw, reader);
            }

            reader.Close();
            string queryString2 = $"Select * From [Взаимодействие с окружением] Where [Номер призрака] IN (Select [Номер призрака] From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' Or [Улика 2] = '{Sort1}' Or [Улика 3] = '{Sort1}')) And [Номер призрака] IN (Select [Номер призрака] From [Взаимодействие с окружением] Where [Предпочитаемые вещи для взаимодействия] = '{Sort5}'));";

            SqlCommand sqlCommand2 = new SqlCommand(queryString2, dataBase.getConnection());

            dataGridView2.Rows.Clear();

            SqlDataReader reader2 = sqlCommand2.ExecuteReader();

            while (reader2.Read())
            {
                ReadSingleRow2(dataGridView2, reader2);
            }

            reader2.Close();
            string queryString3 = $"Select * From Охота Where [Номер охоты] IN (Select [Номер охоты] From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' Or [Улика 2] = '{Sort1}' Or [Улика 3] = '{Sort1}')) And [Номер призрака] In (Select [Номер призрака] From [Взаимодействие с окружением] Where [Предпочитаемые вещи для взаимодействия] = '{Sort5}'));";
            SqlCommand sqlCommand3 = new SqlCommand(queryString3, dataBase.getConnection());
            dataGridView3.Rows.Clear();
            SqlDataReader reader3 = sqlCommand3.ExecuteReader();
            while (reader3.Read())
            {
                ReadSingleRow3(dataGridView3, reader3);
            }
            reader3.Close();
        }
        private void Sorting3_3CCC(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string queryString = $"Select * From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' Or [Улика 2] = '{Sort1}' Or [Улика 3] = '{Sort1}'));";

            SqlCommand sqlCommand = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow1(dgw, reader);
            }

            reader.Close();
            string queryString2 = $"Select * From [Взаимодействие с окружением] Where [Номер призрака] IN (Select [Номер призрака] From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' Or [Улика 2] = '{Sort1}' Or [Улика 3] = '{Sort1}')));";

            SqlCommand sqlCommand2 = new SqlCommand(queryString2, dataBase.getConnection());

            dataGridView2.Rows.Clear();

            SqlDataReader reader2 = sqlCommand2.ExecuteReader();

            while (reader2.Read())
            {
                ReadSingleRow2(dataGridView2, reader2);
            }

            reader2.Close();
            string queryString3 = $"Select * From Охота Where [Номер охоты] IN (Select [Номер охоты] From Призраки Where [Номер набора улик] IN (Select [Номер набора улик] From [Улики] Where ([Улика 1] = '{Sort1}' Or [Улика 2] = '{Sort1}' Or [Улика 3] = '{Sort1}')));";
            SqlCommand sqlCommand3 = new SqlCommand(queryString3, dataBase.getConnection());
            dataGridView3.Rows.Clear();
            SqlDataReader reader3 = sqlCommand3.ExecuteReader();
            while (reader3.Read())
            {
                ReadSingleRow3(dataGridView3, reader3);
            }
            reader3.Close();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Sort5 = comboBox1.Text;
            if (Sort1 != "" && Sort2 != "" && Sort3 != "" && Sort4 != "" && Sort5 != "")
            {
                MessageBox.Show("1");
                Sorting1(dataGridView1);
            }
            else if (Sort1 != "" && Sort2 != "" && Sort3 != "" && Sort4 != "" && Sort5 == "")
            {
                MessageBox.Show("2");
                Sorting1CCC(dataGridView1);
            }
            else if (Sort1 != "" && Sort2 != "" && Sort3 != "" && Sort4 == "" && Sort5 != "")
            {
                MessageBox.Show("3");
                Sorting1_1(dataGridView1);
            }
            else if (Sort1 != "" && Sort2 != "" && Sort3 != "" && Sort4 == "" && Sort5 == "")
            {
                MessageBox.Show("4");
                Sorting1_1CCC(dataGridView1);
            }
            else if (Sort1 != "" && Sort2 != "" && Sort3 == "" && Sort4 != "" && Sort5 != "")
            {
                MessageBox.Show("1");
                Sorting2(dataGridView1);
            }
            else if (Sort1 != "" && Sort2 != "" && Sort3 == "" && Sort4 != "" && Sort5 == "")
            {
                MessageBox.Show("2");
                Sorting2CCC(dataGridView1);
            }
            else if (Sort1 != "" && Sort2 != "" && Sort3 == "" && Sort4 == "" && Sort5 != "")
            {
                MessageBox.Show("3");
                Sorting2_2(dataGridView1);
            }
            else if (Sort1 != "" && Sort2 != "" && Sort3 == "" && Sort4 == "" && Sort5 == "")
            {
                MessageBox.Show("4");
                Sorting2_2CCC(dataGridView1);
            }
            else if (Sort1 != "" && Sort2 == "" && Sort3 == "" && Sort4 != "" && Sort5 != "")
            {
                MessageBox.Show(Sort1.ToString() + " " + Sort2.ToString() + " " + Sort3.ToString());
                Sorting3(dataGridView1);
            }
            else if (Sort1 != "" && Sort2 == "" && Sort3 == "" && Sort4 != "" && Sort5 == "")
            {
                MessageBox.Show(Sort1.ToString() + " " + Sort2.ToString() + " " + Sort3.ToString());
                Sorting3CCC(dataGridView1);
            }
            else if (Sort1 != "" && Sort2 == "" && Sort3 == "" && Sort4 == "" && Sort5 != "")
            {
                MessageBox.Show(Sort1.ToString() + " " + Sort2.ToString() + " " + Sort3.ToString());
                Sorting3_3(dataGridView1);
            }
            else if (Sort1 != "" && Sort2 == "" && Sort3 == "" && Sort4 == "" && Sort5 == "")
            {
                MessageBox.Show(Sort1.ToString() + " " + Sort2.ToString() + " " + Sort3.ToString());
                Sorting3_3CCC(dataGridView1);
            }
            else
            {
                MessageBox.Show("Нужно выбрать улики!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button1.BackColor = Color.Green;
            k++;
            if(Sort1 == "")
            {
                Sort1 = "ЭМП 5";
            }
            else if (Sort2 == "")
            {
                Sort2 = "ЭМП 5";
            }
            else if (Sort3 == "")
            {
                Sort3 = "ЭМП 5";
            }            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button2.BackColor = Color.Green;
            k++;
            if (Sort1 == "")
            {
                Sort1 = "Лазерный проектор";
            }
            else if (Sort2 == "")
            {
                Sort2 = "Лазерный проектор";
            }
            else if (Sort3 == "")
            {
                Sort3 = "Лазерный проектор";
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.Gray;
            button2.BackColor = Color.Gray;
            button3.BackColor = Color.Gray;
            button6.BackColor = Color.Gray;
            button7.BackColor = Color.Gray;
            button8.BackColor = Color.Gray;
            button10.BackColor = Color.Gray;
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button10.Enabled = true;
            Sort1 = "";
            Sort2 = "";
            Sort3 = "";
            k = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button3.BackColor = Color.Green;
            k++;
            if (Sort1 == "")
            {
                Sort1 = "Минусовая температура";
            }
            else if (Sort2 == "")
            {
                Sort2 = "Минусовая температура";
            }
            else if (Sort3 == "")
            {
                Sort3 = "Минусовая температура";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button6.Enabled = false;
            button6.BackColor = Color.Green;
            k++;
            if (Sort1 == "")
            {
                Sort1 = "Отпечатки рук";
            }
            else if (Sort2 == "")
            {
                Sort2 = "Отпечатки рук";
            }
            else if (Sort3 == "")
            {
                Sort3 = "Отпечатки рук";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (k == 3)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                button8.Enabled = false;
                button10.Enabled = false;
                k = 0;
            }
            if (t == 1)
            {
                button12.Enabled = false;
                button11.Enabled = false;
                button9.Enabled = false;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button7.Enabled = false;
            button7.BackColor = Color.Green;
            k++;
            if (Sort1 == "")
            {
                Sort1 = "Призрачный огонёк";
            }
            else if (Sort2 == "")
            {
                Sort2 = "Призрачный огонёк";
            }
            else if (Sort3 == "")
            {
                Sort3 = "Призрачный огонёк";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            button8.Enabled = false;
            button8.BackColor = Color.Green;
            k++;
            if (Sort1 == "")
            {
                Sort1 = "Записи в блокноте";
            }
            else if (Sort2 == "")
            {
                Sort2 = "Записи в блокноте";
            }
            else if (Sort3 == "")
            {
                Sort3 = "Записи в блокноте";
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            button10.Enabled = false;
            button10.BackColor = Color.Green;
            k++;
            if (Sort1 == "")
            {
                Sort1 = "Радиоприёмник";
            }
            else if (Sort2 == "")
            {
                Sort2 = "Радиоприёмник";
            }
            else if (Sort3 == "")
            {
                Sort3 = "Радиоприёмник";
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            RefreshDataGrid1(dataGridView1);
            RefreshDataGrid2(dataGridView2);
            RefreshDataGrid3(dataGridView3);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            t++;
            button12.Enabled = false;
            button12.BackColor = Color.Green;
            if (Sort4 == "")
            {
                Sort4 = "Быстрая";
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            t++;
            button11.Enabled = false;
            button11.BackColor = Color.Green;
            if (Sort4 == "")
            {
                Sort4 = "Обычная";
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            t++;
            button9.Enabled = false;
            button9.BackColor = Color.Green;
            if(Sort4 == "")
            {
                Sort4 = "Меняющаяся";
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            button9.BackColor = Color.Gray;
            button12.BackColor = Color.Gray;
            button11.BackColor = Color.Gray;
            button9.Enabled = true;
            button12.Enabled = true;
            button11.Enabled = true;
            Sort4 = "";
            t = 0;
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
        }

        private void button18_Click(object sender, EventArgs e)
        {
            LoginPass form2 = new LoginPass();
            form2.Show();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            SomeThing1(textBox9.Text);
        }

        private void SomeThing1(string id)
        {
            var query = $"EXECUTE P1 '{id}';";
            var command = new SqlCommand(query, dataBase.getConnection());
            dataBase.openConnection();
            command.ExecuteNonQuery();
            MessageBox.Show("Процедура выполнена!");
        }
        private void SomeThing2(string id)
        {
            var query = $"EXECUTE P2 '{id}';";
            var command = new SqlCommand(query, dataBase.getConnection());
            dataBase.openConnection();
            command.ExecuteNonQuery();
            MessageBox.Show("Процедура выполнена!");
        }

        private void button20_Click(object sender, EventArgs e)
        {
            SomeThing2(textBox9.Text);
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44) //цифры, клавиша BackSpace и запятая а ASCII
            {
                e.Handled = true;
                MessageBox.Show("Неправильнный ввод! Вводить можно только цифры!");
            }
        }
    }
}
