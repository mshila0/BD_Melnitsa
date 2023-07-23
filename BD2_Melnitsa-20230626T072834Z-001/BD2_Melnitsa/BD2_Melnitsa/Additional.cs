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
    public partial class Additional : Form
    {
        DataSet ds1;
        DataSet ds2;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        string connectionString = @"Data Source=DBSrv\SQL2021;Initial Catalog=007ca2_chernavinaMelnitsa;Integrated Security=True";
        string sql1 = "SELECT * FROM Участки";
        string sql2 = "SELECT * FROM Техника_для_производства";

        public Additional()
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
                dataGridView1.Columns["ID_участка"].ReadOnly = true;

                adapter = new SqlDataAdapter(sql2, connection);
                ds2 = new DataSet();
                adapter.Fill(ds2);
                dataGridView2.DataSource = ds2.Tables[0];
                // делаем недоступным столбец id для изменения
                dataGridView2.Columns["ID_техники"].ReadOnly = true;
            }
        }

        private void Savebutton1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql1, connection);
                commandBuilder = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = new SqlCommand("sp_CreateArea", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Тип", SqlDbType.NVarChar, 30, "Тип"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Название_участка", SqlDbType.NVarChar, 30, "Название_участка"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Описание", SqlDbType.NVarChar, 30, "Описание"));

                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@ID_участка", SqlDbType.Int, 0, "ID_участка");
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
                adapter.InsertCommand = new SqlCommand("sp_CreateTex", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Название_техники", SqlDbType.NVarChar, 30, "Название_техники"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Описание", SqlDbType.NVarChar, 30, "Описание"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Колво", SqlDbType.Int, 0, "Колво"));

                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@ID_техники", SqlDbType.Int, 0, "ID_техники");
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
