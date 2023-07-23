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
    public partial class ReportWork : Form
    {
        DataSet ds;
        SqlDataAdapter adapterWorks;
        SqlDataAdapter adapterStaff;
        SqlDataAdapter adapterTovar;
        SqlCommandBuilder commandBuilder;
        string connectionString = @"Data Source=DBSrv\SQL2021;Initial Catalog=007ca2_chernavinaMelnitsa;Integrated Security=True";
        string sql1 = "SELECT * FROM Расписание_работ ORDER BY ID_расписания";
        string sql5 = "SELECT * FROM Справочник_товаров ORDER BY Название";
        string sql6 = "SELECT * FROM Сотрудники ORDER BY Логин_БД";
        DataTable table = new DataTable();

        public ReportWork()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                ds = new DataSet();
                connection.Open();

                // Создаем объект DataAdapter
                adapterTovar = new SqlDataAdapter(sql5, connection);
                adapterStaff = new SqlDataAdapter(sql6, connection);
                adapterWorks = new SqlDataAdapter(sql1, connection);

                adapterWorks.Fill(ds, "Расписание_работ");
                adapterTovar.Fill(ds, "Справочник_товаров");
                adapterStaff.Fill(ds, "Сотрудники");

                ds.Tables["Сотрудники"].Columns["Логин_БД"].Unique = true;
                ds.Tables["Справочник_товаров"].Columns["Название"].Unique = true;


                ds.Relations.Add(new DataRelation("связьСотрудники/Расписание_работ", ds.Tables["Сотрудники"].Columns["Табельный_номер"],
                    ds.Tables["Расписание_работ"].Columns["FK_сотрудника"]));
                ds.Relations.Add(new DataRelation("связьСправочник_товаров/Расписание_работ", ds.Tables["Справочник_товаров"].Columns["ID_товара"],
                    ds.Tables["Расписание_работ"].Columns["FK_товара"]));

                dataGridView1.DataSource = ds.Tables["Расписание_работ"];
                // делаем недоступным столбец id для изменения
                dataGridView1.Columns["ID_расписания"].ReadOnly = true;


                // скрыть колонку с идентификатором
                dataGridView1.Columns["FK_сотрудника"].Visible = false;
                dataGridView1.Columns["FK_товара"].Visible = false;

                var cbx_Tovar = new DataGridViewComboBoxColumn(); // добавить новую колонку
                cbx_Tovar.Name = "Товар";
                cbx_Tovar.DataSource = ds.Tables["Справочник_товаров"];
                cbx_Tovar.DisplayMember = "Название"; // Отображать из Series
                cbx_Tovar.ValueMember = "ID_товара";
                cbx_Tovar.DataPropertyName = "FK_товара"; // Для связи с Books
                cbx_Tovar.MaxDropDownItems = 10;
                cbx_Tovar.FlatStyle = FlatStyle.Flat;
                dataGridView1.Columns.Insert(3, cbx_Tovar);

                var cbx_Staff = new DataGridViewComboBoxColumn(); // добавить новую колонку
                cbx_Staff.Name = "Сотрудник";
                cbx_Staff.DataSource = ds.Tables["Сотрудники"];
                cbx_Staff.DisplayMember = "Логин_БД"; // Отображать из Series
                cbx_Staff.ValueMember = "Табельный_номер";
                cbx_Staff.DataPropertyName = "FK_сотрудника"; // Для связи с Books
                cbx_Staff.MaxDropDownItems = 10;
                cbx_Staff.FlatStyle = FlatStyle.Flat;
                dataGridView1.Columns.Insert(3, cbx_Staff);

                table = ds.Tables[0];
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataView dv = table.DefaultView;
            if (textBox1.Text == "")
            {
                dv.RowFilter = "";
            }
            else
            {
                try
                {
                    dv.RowFilter = "FK_участка = " + textBox1.Text;
                }
                catch { }
            }
        }
    }
}
