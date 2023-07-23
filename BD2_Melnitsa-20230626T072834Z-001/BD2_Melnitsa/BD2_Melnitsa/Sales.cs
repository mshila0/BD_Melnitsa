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
    public partial class Sales : Form
    {
        DataSet ds1;
        DataSet ds2;
        SqlDataAdapter adapterSale;
        SqlDataAdapter adapterSaleDis;
        SqlDataAdapter adapterTovar;
        SqlDataAdapter adapterStaff;
        SqlCommandBuilder commandBuilder;

        string connectionString = @"Data Source=DBSrv\SQL2021;Initial Catalog=007ca2_chernavinaMelnitsa;Integrated Security=True";
        string sql1 = "SELECT * FROM Продажа_товаров ORDER BY ID_продажи";
        string sql2 = "SELECT * FROM Сотрудники ORDER BY Логин_БД";
        string sql3 = "SELECT * FROM Справочник_товаров ORDER BY Название";
        string sql4 = "SELECT * FROM Сведения_о_продаже ORDER BY ID_сведения";

        public Sales()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                ds1 = new DataSet();
                ds2 = new DataSet();
                connection.Open();
                // Создаем объект DataAdapter
                adapterSale = new SqlDataAdapter(sql1, connection);
                adapterStaff = new SqlDataAdapter(sql2, connection);
                adapterSaleDis = new SqlDataAdapter(sql4, connection);
                adapterTovar = new SqlDataAdapter(sql3, connection);

                // Заполняем Dataset
                adapterSale.Fill(ds1, "Продажа_товаров");
                adapterStaff.Fill(ds1, "Сотрудники");

                ds1.Tables["Сотрудники"].Columns["Логин_БД"].Unique = true;

                // Установка связи таблиц
                ds1.Relations.Add(new DataRelation("связьСотрудники/Продажа_товаров", ds1.Tables["Сотрудники"].Columns["Табельный_номер"],
                    ds1.Tables["Продажа_товаров"].Columns["FK_сотрудника"]));

                // Отображаем данные
                dataGridView1.DataSource = ds1.Tables["Продажа_товаров"];

                // делаем недоступным столбец id для изменения
                dataGridView1.Columns["ID_продажи"].ReadOnly = true;

                // скрыть колонку с идентификатором
                dataGridView1.Columns["FK_сотрудника"].Visible = false;

                var cbx_Staff = new DataGridViewComboBoxColumn(); // добавить новую колонку
                cbx_Staff.Name = "Продавец";
                cbx_Staff.DataSource = ds1.Tables["Сотрудники"];
                cbx_Staff.DisplayMember = "Логин_БД"; // Отображать из Series
                cbx_Staff.ValueMember = "Табельный_номер";
                cbx_Staff.DataPropertyName = "FK_сотрудника"; // Для связи с Books
                cbx_Staff.MaxDropDownItems = 10;
                cbx_Staff.FlatStyle = FlatStyle.Flat;
                dataGridView1.Columns.Insert(2, cbx_Staff);



                adapterSaleDis.Fill(ds2, "Сведения_о_продаже");
                adapterTovar.Fill(ds2, "Справочник_товаров");

                ds2.Tables["Справочник_товаров"].Columns["Название"].Unique = true;

                ds2.Relations.Add(new DataRelation("связьСправочник_товаров/Сведения_о_продаже", ds2.Tables["Справочник_товаров"].Columns["ID_товара"],
                     ds2.Tables["Сведения_о_продаже"].Columns["FK_товара"]));

                dataGridView2.DataSource = ds2.Tables["Сведения_о_продаже"];

                dataGridView2.Columns["ID_сведения"].ReadOnly = true;
                dataGridView2.Columns["FK_товара"].Visible = false;

                var cbx_Tovar = new DataGridViewComboBoxColumn(); // добавить новую колонку
                cbx_Tovar.Name = "Товар";
                cbx_Tovar.DataSource = ds2.Tables["Справочник_товаров"];
                cbx_Tovar.DisplayMember = "Название"; // Отображать из Series
                cbx_Tovar.ValueMember = "ID_товара";
                cbx_Tovar.DataPropertyName = "FK_товара"; // Для связи с Books
                cbx_Tovar.MaxDropDownItems = 10;
                cbx_Tovar.FlatStyle = FlatStyle.Flat;
                dataGridView2.Columns.Insert(2, cbx_Tovar);
            }
        }

        private void Savebutton1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapterSale = new SqlDataAdapter(sql1, connection);
                commandBuilder = new SqlCommandBuilder(adapterSale);
                adapterSale.InsertCommand = new SqlCommand("sp_CreateSales", connection);
                adapterSale.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapterSale.InsertCommand.Parameters.Add(new SqlParameter("@FK_сотрудника", SqlDbType.Int, 0, "FK_сотрудника"));
                adapterSale.InsertCommand.Parameters.Add(new SqlParameter("@Дата_продажи", SqlDbType.DateTime, 0, "Дата_продажи"));

                SqlParameter parameter = adapterSale.InsertCommand.Parameters.Add("@ID_продажи", SqlDbType.Int, 0, "ID_продажи");
                parameter.Direction = ParameterDirection.Output;

                adapterSale.Update(ds1, "Продажа_товаров");
            }
        }
        private void Savebutton2_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapterSaleDis = new SqlDataAdapter(sql4, connection);
                commandBuilder = new SqlCommandBuilder(adapterSaleDis);
                adapterSaleDis.InsertCommand = new SqlCommand("sp_CreateSalesDescription", connection);
                adapterSaleDis.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapterSaleDis.InsertCommand.Parameters.Add(new SqlParameter("@FK_продажи", SqlDbType.Int, 0, "FK_продажи"));
                adapterSaleDis.InsertCommand.Parameters.Add(new SqlParameter("@FK_товара", SqlDbType.Int, 0, "FK_товара"));
                adapterSaleDis.InsertCommand.Parameters.Add(new SqlParameter("@Колво_проданного_товара", SqlDbType.Int, 0, "Колво_проданного_товара"));

                SqlParameter parameter = adapterSaleDis.InsertCommand.Parameters.Add("@ID_сведения", SqlDbType.Int, 0, "ID_сведения");
                parameter.Direction = ParameterDirection.Output;

                adapterSaleDis.Update(ds2, "Сведения_о_продаже");
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
    }
}
