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
    public partial class WorksLog : Form
    {
        DataSet ds1;
        DataSet ds2;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        string connectionString = @"Data Source=DBSrv\SQL2021;Initial Catalog=007ca2_chernavinaMelnitsa;Integrated Security=True";
        string sql1 = "SELECT * FROM Журнал_выполнения_работ";

        public WorksLog()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql1, connection);
                ds1 = new DataSet();
                adapter.Fill(ds1);
                dataGridView1.DataSource = ds1.Tables[0];
                // делаем недоступным столбец id для изменения
                dataGridView1.Columns["ID_журнала"].ReadOnly = true;
            }
        }

        private void Savebutton_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql1, connection);
                commandBuilder = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = new SqlCommand("sp_CreateWorkingLog", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@FK_расписания", SqlDbType.Int, 0, "FK_расписания"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Дата_работ", SqlDbType.DateTime, 0, "Дата_работ"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Описание", SqlDbType.NVarChar, 30, "Описание"));

                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@ID_журнала", SqlDbType.Int, 0, "ID_журнала");
                parameter.Direction = ParameterDirection.Output;

                adapter.Update(ds1);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            DataRow row = ds1.Tables[0].NewRow(); // добавляем новую строку в DataTable
            ds1.Tables[0].Rows.Add(row);
        }
    }
}
