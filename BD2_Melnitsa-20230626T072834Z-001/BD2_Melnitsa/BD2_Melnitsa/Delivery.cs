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
    public partial class Delivery : Form
    {
        DataSet ds1;
        DataSet ds2;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        string connectionString = @"Data Source=DBSrv\SQL2021;Initial Catalog=007ca2_chernavinaMelnitsa;Integrated Security=True";
        string sql1 = "SELECT * FROM Приход_товара";
        string sql2 = "SELECT * FROM Описание_товара_на_приход";
        public Delivery()
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
                dataGridView1.Columns["ID_прихода"].ReadOnly = true;

                adapter = new SqlDataAdapter(sql2, connection);
                ds2 = new DataSet();
                adapter.Fill(ds2);
                dataGridView2.DataSource = ds2.Tables[0];
                // делаем недоступным столбец id для изменения
                dataGridView2.Columns["ID_описания"].ReadOnly = true;

            }
        }

        private void addButton1_Click(object sender, EventArgs e)
        {
            DataRow row = ds1.Tables[0].NewRow(); // добавляем новую строку в DataTable
            ds1.Tables[0].Rows.Add(row);
        }
        private void addButton2_Click(object sender, EventArgs e)
        {
            DataRow row = ds2.Tables[0].NewRow(); // добавляем новую строку в DataTable
            ds2.Tables[0].Rows.Add(row);
        }

        private void Savebutton1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql1, connection);
                commandBuilder = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = new SqlCommand("sp_CreateComing", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@FK_заявки", SqlDbType.Int, 0, "FK_заявки"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@FK_принимающего", SqlDbType.Int, 0, "FK_принимающего"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Дата_получения", SqlDbType.DateTime, 0, "Дата_получения"));

                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@ID_прихода", SqlDbType.Int, 0, "ID_прихода");
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
                adapter.InsertCommand = new SqlCommand("sp_CreateComingDescription", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@FK_прихода", SqlDbType.Int, 0, "FK_прихода"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@FK_товара", SqlDbType.Int, 0, "FK_товара"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Колво", SqlDbType.Int, 0, "Колво"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Сумма_за_товар", SqlDbType.Int, 0, "Сумма_за_товар"));

                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@ID_описания", SqlDbType.Int, 0, "ID_описания");
                parameter.Direction = ParameterDirection.Output;

                adapter.Update(ds2);
            }
        }
    }
}
