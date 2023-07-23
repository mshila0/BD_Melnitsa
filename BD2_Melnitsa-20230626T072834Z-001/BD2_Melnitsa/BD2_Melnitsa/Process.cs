using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BD2_Melnitsa
{
    public partial class Process : Form
    {
        DataSet ds1;
        DataSet ds2;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        string connectionString = @"Data Source=DBSrv\SQL2021;Initial Catalog=007ca2_chernavinaMelnitsa;Integrated Security=True";
        string sql1 = "SELECT * FROM Этапы";
        string sql2 = "SELECT * FROM Процессы";

        public Process()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql1, connection);
                ds1 = new DataSet();
                adapter.Fill(ds1);
                dataGridView1.DataSource = ds1.Tables[0];
                // делаем недоступным столбец id для изменения
                dataGridView1.Columns["ID_этапа"].ReadOnly = true;

                adapter = new SqlDataAdapter(sql2, connection);
                ds2 = new DataSet();
                adapter.Fill(ds2);
                dataGridView2.DataSource = ds2.Tables[0];
                // делаем недоступным столбец id для изменения
                dataGridView2.Columns["ID_процесса"].ReadOnly = true;
            }
        }

        private void Savebutton1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql1, connection);
                commandBuilder = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = new SqlCommand("sp_CreateStages", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Название", SqlDbType.NVarChar, 30, "Название"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Описание", SqlDbType.NVarChar, 30, "Описание"));

                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@ID_этапа", SqlDbType.Int, 0, "ID_этапа");
                parameter.Direction = ParameterDirection.Output;

                adapter.Update(ds1);
            }
        }
        private void Savebutton2_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql2, connection);
                commandBuilder = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = new SqlCommand("sp_CreateProcess", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@FK_этапа", SqlDbType.Int, 0, "FK_этапа"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Название_процесса", SqlDbType.NVarChar, 30, "Название_процесса"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Примерное_время", SqlDbType.DateTime, 0, "Примерное_время"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Описание", SqlDbType.NVarChar, 30, "Описание"));

                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@ID_процесса", SqlDbType.Int, 0, "ID_процесса");
                parameter.Direction = ParameterDirection.Output;

                adapter.Update(ds2);
            }
        }

        private void addButton1_Click(object sender, EventArgs e)
        {
            DataRow row = ds1.Tables[0].NewRow(); // добавляем новую строку в DataTable
            ds1.Tables[0].Rows.Add(row);
        }

        private void addbutton2_Click(object sender, EventArgs e)
        {
            DataRow row = ds2.Tables[0].NewRow(); // добавляем новую строку в DataTable
            ds2.Tables[0].Rows.Add(row);
        }
    }
}
