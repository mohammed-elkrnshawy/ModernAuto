using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modern_Auto
{
    public partial class Form_Customer_Debtors : Form
    {
        private DataSet ds;

        public Form_Customer_Debtors()
        {
            InitializeComponent();
        }

        private void Form_Customer_Debtors_Load(object sender, EventArgs e)
        {
            using (ds=Ezzat.GetDataSet("Customer_selectDebtors", "X"))
            {
                dataGridView1.DataSource = ds.Tables["X"];

                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            getSum();
        }

        private void getSum()
        {
            object sum = Ezzat.ExecutedScalar("Customer_selectSumDebtors");
            txt_billNumber.Text = sum + "";
        }
    }
}
