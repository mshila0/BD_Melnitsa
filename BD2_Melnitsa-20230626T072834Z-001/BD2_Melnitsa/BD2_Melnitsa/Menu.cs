using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BD2_Melnitsa
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Staff_Click(object sender, EventArgs e)
        {
            Staff newForm = new Staff();
            newForm.Show();
        }

        private void Works_Click(object sender, EventArgs e)
        {
            Works newForm = new Works();
            newForm.Show();
        }

        private void Storage_Click(object sender, EventArgs e)
        {
            Storage newForm = new Storage();
            newForm.Show();
        }

        private void Sales_Click(object sender, EventArgs e)
        {
            Sales newForm = new Sales();
            newForm.Show();
        }

        private void ReportSales_Click(object sender, EventArgs e)
        {
            ReportSales newForm = new ReportSales();
            newForm.Show();
        }
        private void ReportWork_Click(object sender, EventArgs e)
        {
            ReportWork newForm = new ReportWork();
            newForm.Show();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show(
                "Вы действительно хотите выйти из программы?",
                "Завершение программы",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            if (dialog == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
