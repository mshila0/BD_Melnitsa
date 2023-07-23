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
    public partial class Order : Form
    {
        DataSet dsD;
        DataSet dsO;
        DataSet dsP;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        string connectionString = @"Data Source=DBSrv\SQL2021;Initial Catalog=007ca2_chernavinaMelnitsa;Integrated Security=True";
        string sqlD = "SELECT * FROM Договор_на_поставку";
        string sqlO = "SELECT * FROM Описание_товаров_в_заказе";
        string sqlP = "SELECT * FROM Поставщики";
        public Order()
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
                dataGridView1.Columns["ID_договора"].ReadOnly = true;

                adapter = new SqlDataAdapter(sqlO, connection);
                dsO = new DataSet();
                adapter.Fill(dsO);
                dataGridView2.DataSource = dsO.Tables[0];
                // делаем недоступным столбец id для изменения
                dataGridView2.Columns["ID_описания"].ReadOnly = true;

                adapter = new SqlDataAdapter(sqlP, connection);
                dsP = new DataSet();
                adapter.Fill(dsP);
                dataGridView3.DataSource = dsP.Tables[0];
                // делаем недоступным столбец id для изменения
                dataGridView3.Columns["ID_поставщика"].ReadOnly = true;

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
        private void addButtonP_Click(object sender, EventArgs e)
        {
            DataRow row = dsP.Tables[0].NewRow(); // добавляем новую строку в DataTable
            dsP.Tables[0].Rows.Add(row);
        }

        private void SavebuttonD_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sqlD, connection);
                commandBuilder = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = new SqlCommand("sp_CreateOrderContract", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Планируемая_дата_поставки", SqlDbType.Date, 0, "Планируемая_дата_поставки"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Телефон", SqlDbType.NVarChar, 30, "Телефон"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@FK_поставщика", SqlDbType.Int, 0, "FK_поставщика"));

                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@ID_договора", SqlDbType.Int, 0, "ID_договора");
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
                adapter.InsertCommand = new SqlCommand("sp_CreateOrderDescription", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@FK_заявки", SqlDbType.Int, 0, "FK_заявки"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@FK_товара", SqlDbType.Int, 0, "FK_товара"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Колво", SqlDbType.Int, 0, "Колво"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Сумма_за_товар", SqlDbType.Int, 0, "Сумма_за_товар"));

                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@ID_описания", SqlDbType.Int, 0, "ID_описания");

                adapter.Update(dsO);
            }
        }
        private void SavebuttonP_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sqlP, connection);
                commandBuilder = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = new SqlCommand("sp_CreateProvider", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Название", SqlDbType.NVarChar, 30, "Название"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Телефон", SqlDbType.NVarChar, 30, "Телефон"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Адрес", SqlDbType.NVarChar, 30, "Адрес"));
                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@ID_поставщика", SqlDbType.Int, 0, "ID_поставщика");
                parameter.Direction = ParameterDirection.Output;

                adapter.Update(dsP);
            }
        }
    }
}
