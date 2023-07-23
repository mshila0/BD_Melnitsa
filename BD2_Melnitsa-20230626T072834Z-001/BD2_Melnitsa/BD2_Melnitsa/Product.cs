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
    public partial class Product : Form
    {
        DataSet ds1;
        DataSet ds2;
        SqlDataAdapter adapterTovar;
        SqlDataAdapter adapterTip;
        SqlCommandBuilder commandBuilder;
        string connectionString = @"Data Source=DBSrv\SQL2021;Initial Catalog=007ca2_chernavinaMelnitsa;Integrated Security=True";
        string sql1 = "SELECT * FROM Справочник_товаров ORDER BY ID_товара";
        string sql2 = "SELECT * FROM Категории_товаров";
        string sql3 = "SELECT * FROM Категории_товаров ORDER BY Название";
        public Product()
        {
            InitializeComponent();
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView3.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                ds1 = new DataSet();
                ds2 = new DataSet();
                connection.Open();
                // Создаем объект DataAdapter
                adapterTovar = new SqlDataAdapter(sql1, connection);
                adapterTip = new SqlDataAdapter(sql2, connection);

                // Заполняем Dataset
                adapterTovar.Fill(ds1, "Справочник_товаров");
                adapterTip.Fill(ds1, "Категории_товаров");


                ds1.Tables["Категории_товаров"].Columns["Название"].Unique = true;

                // Установка связи таблиц
                ds1.Relations.Add(new DataRelation("связьКатегории_товаров/Справочник_товаров", ds1.Tables["Категории_товаров"].Columns["ID_категории"],
                    ds1.Tables["Справочник_товаров"].Columns["FK_тип_товара"]));

                // Отображаем данные
                dataGridView2.DataSource = ds1.Tables["Справочник_товаров"];
                // делаем недоступным столбец id для изменения
                dataGridView2.Columns["ID_товара"].ReadOnly = true;
                // скрыть колонку с идентификатором
                dataGridView2.Columns["FK_тип_товара"].Visible = false;

                var cbx_TipKl = new DataGridViewComboBoxColumn(); // добавить новую колонку
                cbx_TipKl.Name = "Тип_товара";
                cbx_TipKl.DataSource = ds1.Tables["Категории_товаров"];
                cbx_TipKl.DisplayMember = "Название"; // Отображать из Series
                cbx_TipKl.ValueMember = "ID_категории";
                cbx_TipKl.DataPropertyName = "FK_тип_товара"; // Для связи с Books
                cbx_TipKl.MaxDropDownItems = 10;
                cbx_TipKl.FlatStyle = FlatStyle.Flat;
                dataGridView2.Columns.Insert(2, cbx_TipKl);

                adapterTip.Fill(ds2, "Тип_клиента");
                dataGridView3.DataSource = ds2.Tables[0];
                // делаем недоступным столбец id для изменения
                dataGridView3.Columns["ID_категории"].ReadOnly = true;

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
                adapterTovar = new SqlDataAdapter(sql1, connection);
                commandBuilder = new SqlCommandBuilder(adapterTovar);
                adapterTovar.InsertCommand = new SqlCommand("sp_CreateProduct", connection);
                adapterTovar.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapterTovar.InsertCommand.Parameters.Add(new SqlParameter("@Название", SqlDbType.NVarChar, 30, "Название"));
                adapterTovar.InsertCommand.Parameters.Add(new SqlParameter("@FK_тип_товара", SqlDbType.Int, 0, "FK_тип_товара"));
                adapterTovar.InsertCommand.Parameters.Add(new SqlParameter("@Ед_измерения", SqlDbType.NVarChar, 10, "Ед_измерения"));
                adapterTovar.InsertCommand.Parameters.Add(new SqlParameter("@Вес_товара", SqlDbType.Float, 0, "Вес_товара"));
                adapterTovar.InsertCommand.Parameters.Add(new SqlParameter("@Цена_продажи", SqlDbType.Int, 0, "Цена_продажи"));
                adapterTovar.InsertCommand.Parameters.Add(new SqlParameter("@Примечание", SqlDbType.NVarChar, 100, "Примечание"));

                SqlParameter parameter = adapterTovar.InsertCommand.Parameters.Add("@ID_товара", SqlDbType.Int, 0, "ID_товара");
                parameter.Direction = ParameterDirection.Output;

                adapterTovar.Update(ds1, "Справочник_товаров");
            }
        }
        private void Savebutton2_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapterTip = new SqlDataAdapter(sql2, connection);
                commandBuilder = new SqlCommandBuilder(adapterTip);
                adapterTip.InsertCommand = new SqlCommand("sp_CreateProductTypes", connection);
                adapterTip.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapterTip.InsertCommand.Parameters.Add(new SqlParameter("@Название", SqlDbType.NVarChar, 30, "Название"));

                SqlParameter parameter = adapterTip.InsertCommand.Parameters.Add("@ID_категории", SqlDbType.Int, 0, "ID_категории");

                adapterTip.Update(ds2, "Категории_товаров");
            }
        }
    }
}
