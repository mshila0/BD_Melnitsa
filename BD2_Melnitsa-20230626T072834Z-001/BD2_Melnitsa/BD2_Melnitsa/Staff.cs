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
    public partial class Staff : Form
    {
        DataSet ds;
        DataSet ds2;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        string connectionString = @"Data Source=DBSrv\SQL2021;Initial Catalog=007ca2_chernavinaMelnitsa;Integrated Security=True";
        string sql = "SELECT * FROM Сотрудники";
        string sql2 = "SELECT * FROM Увольнение";

        public Staff()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);
                ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                // делаем недоступным столбец id для изменения
                dataGridView1.Columns["Табельный_номер"].ReadOnly = true;

                adapter = new SqlDataAdapter(sql2, connection);
                ds2 = new DataSet();
                adapter.Fill(ds2);
                dataGridView2.DataSource = ds2.Tables[0];
                // делаем недоступным столбец id для изменения
                dataGridView2.Columns["ID_увольнения"].ReadOnly = true;
            }
        }

        private void Savebutton_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);
                commandBuilder = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = new SqlCommand("sp_CreateUser", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Имя", SqlDbType.NVarChar, 20, "Имя"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Фамилия", SqlDbType.NVarChar, 30, "Фамилия"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Отчество", SqlDbType.NVarChar, 30, "Отчество"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Дата_принятия_на_работу", SqlDbType.DateTime, 20, "Дата_принятия_на_работу"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Номер_телефона", SqlDbType.NVarChar, 20, "Номер_телефона"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Логин_БД", SqlDbType.NVarChar, 20, "Логин_БД"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Пароль_БД", SqlDbType.NVarChar, 20, "Пароль_БД"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Примечание", SqlDbType.NVarChar, 50, "Примечание"));

                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@Табельный_номер", SqlDbType.Int, 0, "Табельный_номер");
                parameter.Direction = ParameterDirection.Output;

                adapter.Update(ds);
            }
        }
        private void SavebuttonY_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql2, connection);
                commandBuilder = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = new SqlCommand("sp_CreateDismiss", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@FK_шт", SqlDbType.Int, 0, "FK_шт"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Дата_увольнения", SqlDbType.DateTime, 20, "Дата_увольнения"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Причина_увольнения", SqlDbType.NVarChar, 30, "Причина_увольнения"));


                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@ID_увольнения", SqlDbType.Int, 0, "ID_увольнения");
                parameter.Direction = ParameterDirection.Output;

                adapter.Update(ds2);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            DataRow row = ds.Tables[0].NewRow(); // добавляем новую строку в DataTable
            ds.Tables[0].Rows.Add(row);
        }

        private void addbuttonY_Click(object sender, EventArgs e)
        {
            DataRow row = ds2.Tables[0].NewRow(); // добавляем новую строку в DataTable
            ds2.Tables[0].Rows.Add(row);
        }
        private void Staffing_Click(object sender, EventArgs e)
        {
            Staffing newForm = new Staffing();
            newForm.Show();
        }
    }
}
