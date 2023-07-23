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
    public partial class Staffing : Form
    {
        DataSet dsD;
        DataSet dsO;
        DataSet dsH;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        SqlDataAdapter adapterTipKl;
        string connectionString = @"Data Source=DBSrv\SQL2021;Initial Catalog=007ca2_chernavinaMelnitsa;Integrated Security=True";
        string sqlD = "SELECT * FROM Должности";
        string sqlO = "SELECT * FROM Отделы";
        string sqlH = "SELECT * FROM Штатное_расписание";
        public Staffing()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView3.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sqlD, connection);
                dsD = new DataSet();
                adapter.Fill(dsD);
                dataGridView1.DataSource = dsD.Tables[0];
                // делаем недоступным столбец id для изменения
                dataGridView1.Columns["ID_должности"].ReadOnly = true;

                adapter = new SqlDataAdapter(sqlO, connection);
                dsO = new DataSet();
                adapter.Fill(dsO);
                dataGridView2.DataSource = dsO.Tables[0];
                // делаем недоступным столбец id для изменения
                dataGridView2.Columns["ID_отдела"].ReadOnly = true;

                adapter = new SqlDataAdapter(sqlH, connection);
                dsH = new DataSet();
                adapter.Fill(dsH);
                dataGridView3.DataSource = dsH.Tables[0];
                // делаем недоступным столбец id для изменения
                dataGridView3.Columns["ID_шр"].ReadOnly = true;

            }
        }

        private void addButtonD_Click(object sender, EventArgs e)
        {
            DataRow row = dsD.Tables[0].NewRow(); // добавляем новую строку в DataTable
            dsD.Tables[0].Rows.Add(row);
        }
        private void addButtonO_Click(object sender, EventArgs e)
        {
            DataRow row = dsO.Tables[0].NewRow(); // добавляем новую строку в DataTable
            dsO.Tables[0].Rows.Add(row);
        }
        private void addButtonH_Click(object sender, EventArgs e)
        {
            DataRow row = dsH.Tables[0].NewRow(); // добавляем новую строку в DataTable
            dsH.Tables[0].Rows.Add(row);
        }

        private void SavebuttonD_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sqlD, connection);
                commandBuilder = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = new SqlCommand("sp_CreateDepartment", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Название_должности", SqlDbType.NVarChar, 30, "Название_должности"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Описание", SqlDbType.NVarChar, 30, "Описание"));

                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@ID_должности", SqlDbType.Int, 0, "ID_должности");
                parameter.Direction = ParameterDirection.Output;

                adapter.Update(dsD);
            }
        }
        private void SavebuttonO_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sqlO, connection);
                commandBuilder = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = new SqlCommand("sp_CreatePosition", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Название_отдела", SqlDbType.NVarChar, 30, "Название_отдела"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Номер_телефона", SqlDbType.NVarChar, 30, "Номер_телефона"));

                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@ID_отдела", SqlDbType.Int, 0, "ID_отдела");

                adapter.Update(dsO);
            }
        }
        private void SavebuttonH_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sqlH, connection);
                commandBuilder = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = new SqlCommand("sp_CreateStaffing", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@FK_должности", SqlDbType.Int, 0, "FK_должности"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@FK_сотрудника", SqlDbType.Int, 0, "FK_сотрудника"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@FK_отдела", SqlDbType.Int, 0, "FK_отдела"));

                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@ID_шр", SqlDbType.Int, 0, "ID_шр");
                parameter.Direction = ParameterDirection.Output;

                adapter.Update(dsH);
            }
        }
    }
}
