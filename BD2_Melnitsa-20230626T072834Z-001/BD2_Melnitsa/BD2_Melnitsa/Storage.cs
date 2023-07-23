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
    public partial class Storage : Form
    {
        DataSet ds1;
        DataSet ds2;
        SqlDataAdapter adapterTovar;
        SqlDataAdapter adapterTip;
        SqlDataAdapter adapterStorage;
        SqlDataAdapter adapterStaff;
        SqlCommandBuilder commandBuilder;
        string connectionString = @"Data Source=DBSrv\SQL2021;Initial Catalog=007ca2_chernavinaMelnitsa;Integrated Security=True";
        string sql1 = "SELECT * FROM Склады ORDER BY ID_склада";
        string sql2 = "SELECT * FROM Типы_складов";

        string sql3 = "SELECT * FROM Сотрудники ORDER BY Логин_БД";
        string sql4 = "SELECT * FROM Типы_складов ORDER BY Вид_хранимого";
        string sql5 = "SELECT * FROM Справочник_товаров ORDER BY Название";

        public Storage()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView4.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView4.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                ds1 = new DataSet();
                ds2 = new DataSet();
                connection.Open();
                // Создаем объект DataAdapter
                adapterStorage = new SqlDataAdapter(sql1, connection);
                adapterTip = new SqlDataAdapter(sql4, connection);
                adapterTovar = new SqlDataAdapter(sql5, connection);
                adapterStaff = new SqlDataAdapter(sql3, connection);

                // Заполняем Dataset
                adapterStorage.Fill(ds1, "Склады");
                adapterTovar.Fill(ds1, "Справочник_товаров");
                adapterTip.Fill(ds1, "Типы_складов");
                adapterStaff.Fill(ds1, "Сотрудники");

                ds1.Tables["Сотрудники"].Columns["Логин_БД"].Unique = true;
                ds1.Tables["Справочник_товаров"].Columns["Название"].Unique = true;
                ds1.Tables["Типы_складов"].Columns["Вид_хранимого"].Unique = true;

                // Установка связи таблиц
                ds1.Relations.Add(new DataRelation("связьСотрудники/Склады", ds1.Tables["Сотрудники"].Columns["Табельный_номер"],
                    ds1.Tables["Склады"].Columns["FK_проверяющего"]));
                ds1.Relations.Add(new DataRelation("связьСправочник_товаров/Склады", ds1.Tables["Справочник_товаров"].Columns["ID_товара"],
                    ds1.Tables["Склады"].Columns["FK_товара"]));
                ds1.Relations.Add(new DataRelation("связьТипы_складов/Склады", ds1.Tables["Типы_складов"].Columns["ID_типа"],
                    ds1.Tables["Склады"].Columns["FK_тип_склада"]));

                // Отображаем данные
                dataGridView1.DataSource = ds1.Tables["Склады"];

                // делаем недоступным столбец id для изменения
                dataGridView1.Columns["ID_склада"].ReadOnly = true;

                // скрыть колонку с идентификатором
                dataGridView1.Columns["FK_тип_склада"].Visible = false;
                dataGridView1.Columns["FK_товара"].Visible = false;
                dataGridView1.Columns["FK_проверяющего"].Visible = false;

                var cbx_TipKl = new DataGridViewComboBoxColumn(); // добавить новую колонку
                cbx_TipKl.Name = "Тип_склада";
                cbx_TipKl.DataSource = ds1.Tables["Типы_складов"];
                cbx_TipKl.DisplayMember = "Вид_хранимого"; // Отображать из Series
                cbx_TipKl.ValueMember = "ID_типа";
                cbx_TipKl.DataPropertyName = "FK_тип_склада"; // Для связи с Books
                cbx_TipKl.MaxDropDownItems = 10;
                cbx_TipKl.FlatStyle = FlatStyle.Flat;
                dataGridView1.Columns.Insert(3, cbx_TipKl);

                var cbx_Tovar = new DataGridViewComboBoxColumn(); // добавить новую колонку
                cbx_Tovar.Name = "Товар";
                cbx_Tovar.DataSource = ds1.Tables["Справочник_товаров"];
                cbx_Tovar.DisplayMember = "Название"; // Отображать из Series
                cbx_Tovar.ValueMember = "ID_товара";
                cbx_Tovar.DataPropertyName = "FK_товара"; // Для связи с Books
                cbx_Tovar.MaxDropDownItems = 10;
                cbx_Tovar.FlatStyle = FlatStyle.Flat;
                dataGridView1.Columns.Insert(3, cbx_Tovar);

                var cbx_Staff = new DataGridViewComboBoxColumn(); // добавить новую колонку
                cbx_Staff.Name = "Проверяющий";
                cbx_Staff.DataSource = ds1.Tables["Сотрудники"];
                cbx_Staff.DisplayMember = "Логин_БД"; // Отображать из Series
                cbx_Staff.ValueMember = "Табельный_номер";
                cbx_Staff.DataPropertyName = "FK_проверяющего"; // Для связи с Books
                cbx_Staff.MaxDropDownItems = 10;
                cbx_Staff.FlatStyle = FlatStyle.Flat;
                dataGridView1.Columns.Insert(3, cbx_Staff);

                adapterTip = new SqlDataAdapter(sql2, connection);
                ds2 = new DataSet();
                adapterTip.Fill(ds2, "Склады");
                dataGridView4.DataSource = ds2.Tables[0];
                // делаем недоступным столбец id для изменения
                dataGridView4.Columns["ID_типа"].ReadOnly = true;
            }
        }

        private void Savebutton_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapterStorage = new SqlDataAdapter(sql1, connection);
                commandBuilder = new SqlCommandBuilder(adapterStorage);
                adapterStorage.InsertCommand = new SqlCommand("sp_CreateWarehouse", connection);
                adapterStorage.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapterStorage.InsertCommand.Parameters.Add(new SqlParameter("@Название", SqlDbType.NVarChar, 30, "Название"));
                adapterStorage.InsertCommand.Parameters.Add(new SqlParameter("@FK_тип_склада", SqlDbType.Int, 0, "FK_тип_склада"));
                adapterStorage.InsertCommand.Parameters.Add(new SqlParameter("@FK_товара", SqlDbType.Int, 0, "FK_товара"));
                adapterStorage.InsertCommand.Parameters.Add(new SqlParameter("@FK_проверяющего", SqlDbType.Int, 0, "FK_проверяющего"));
                adapterStorage.InsertCommand.Parameters.Add(new SqlParameter("@Колво_на_складе", SqlDbType.Int, 0, "Колво_на_складе"));
                adapterStorage.InsertCommand.Parameters.Add(new SqlParameter("@Дата_проверки", SqlDbType.DateTime, 0, "Дата_проверки"));

                SqlParameter parameter = adapterStorage.InsertCommand.Parameters.Add("@ID_склада", SqlDbType.Int, 0, "ID_склада");
                parameter.Direction = ParameterDirection.Output;

                adapterStorage.Update(ds1, "Склады");
            }
        }
        private void SavebuttonY_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapterTip = new SqlDataAdapter(sql2, connection);
                commandBuilder = new SqlCommandBuilder(adapterTip);
                adapterTip.InsertCommand = new SqlCommand("sp_CreateWarehouseTypes", connection);
                adapterTip.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapterTip.InsertCommand.Parameters.Add(new SqlParameter("@Название", SqlDbType.NVarChar, 30, "Название"));
                adapterTip.InsertCommand.Parameters.Add(new SqlParameter("@Вид_хранимого", SqlDbType.NVarChar, 30, "Вид_хранимого"));
                adapterTip.InsertCommand.Parameters.Add(new SqlParameter("@Описание", SqlDbType.NVarChar, 30, "Описание"));

                SqlParameter parameter = adapterTip.InsertCommand.Parameters.Add("@ID_типа", SqlDbType.Int, 0, "ID_типа");
                parameter.Direction = ParameterDirection.Output;

                adapterTip.Update(ds2, "Типы_складов");
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            DataRow row = ds1.Tables[0].NewRow(); // добавляем новую строку в DataTable
            ds1.Tables[0].Rows.Add(row);
        }

        private void addbuttonY_Click(object sender, EventArgs e)
        {
            DataRow row = ds2.Tables[0].NewRow(); // добавляем новую строку в DataTable
            ds2.Tables[0].Rows.Add(row);
        }

        private void Delivery_Click(object sender, EventArgs e)
        {
            Delivery newForm = new Delivery();
            newForm.Show();
        }

        private void Order_Click(object sender, EventArgs e)
        {
            Order newForm = new Order();
            newForm.Show();
        }

        private void Product_Click(object sender, EventArgs e)
        {
            Product newForm = new Product();
            newForm.Show();
        }
    }
}
