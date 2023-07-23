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
using System.Data.OleDb;

namespace BD2_Melnitsa
{
    public partial class ReportSales : Form
    {
        string connectionString = @"Data Source=DBSrv\SQL2021;Initial Catalog=007ca2_chernavinaMelnitsa;Integrated Security=True";

        public ReportSales()
        {
            InitializeComponent();
        }

        private void Search_Click(object sender, EventArgs e)
        {
            string date1 = maskedTextBox1.Text;
            string date2 = maskedTextBox2.Text;

            string sql1 = "SELECT Сведения_о_продаже.ID_сведения, Сотрудники.Логин_БД, Продажа_товаров.Дата_продажи, Справочник_товаров.Название, Сведения_о_продаже.Колво_проданного_товара, Справочник_товаров.Цена_продажи,"
                + " Сведения_о_продаже.Колво_проданного_товара * Справочник_товаров.Цена_продажи AS Сумма"
                + " FROM Сведения_о_продаже INNER JOIN Справочник_товаров ON Справочник_товаров.ID_товара = FK_товара INNER JOIN Продажа_товаров ON Продажа_товаров.ID_продажи = FK_продажи"
                + " INNER JOIN Сотрудники ON Сотрудники.Табельный_номер = Продажа_товаров.FK_сотрудника" +
                " WHERE Продажа_товаров.Дата_продажи BETWEEN";

            sql1 += " '" + date1 + "' AND '" + date2 + "'";

            string sql2 = "CREATE VIEW report_sales AS " + sql1;
            string sql3 = "SELECT SUM(Сумма) FROM report_sales";
            string sql4 = "DROP VIEW IF EXISTS report_sales";


            SqlConnection myconnectionString = new SqlConnection(connectionString);
            myconnectionString.Open();

            SqlCommand cmd = new SqlCommand(sql1, myconnectionString);
            SqlDataAdapter adapterReportSold = new SqlDataAdapter(cmd);
            adapterReportSold.SelectCommand = cmd;
            DataTable dt = new DataTable();
            adapterReportSold.Fill(dt);
            dataGridView1.DataSource = dt;

            cmd = new SqlCommand(sql4, myconnectionString);
            adapterReportSold = new SqlDataAdapter(cmd);
            adapterReportSold.SelectCommand = cmd;
            adapterReportSold.Fill(dt);

            cmd = new SqlCommand(sql2, myconnectionString);
            adapterReportSold = new SqlDataAdapter(cmd);
            adapterReportSold.SelectCommand = cmd;
            adapterReportSold.Fill(dt);

            cmd = new SqlCommand(sql3, myconnectionString);
            SqlDataReader dataReader = cmd.ExecuteReader();
            try
            {
                while (dataReader.Read())
                {
                    label5.Text = dataReader.GetInt32(0).ToString();
                }
            }
            catch
            {

            }

            myconnectionString.Close();
        }
    }
}
