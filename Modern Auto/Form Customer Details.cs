using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modern_Auto
{
    public partial class Form_Customer_Details : Form
    {
        private int bill_ID;
        private DataSet ds;
        public Form_Customer_Details(int bill)
        {
            InitializeComponent();
            this.bill_ID = bill;
        }

        private void Form_Customer_Details_Load(object sender, EventArgs e)
        {
            using (ds = Ezzat.GetDataSet("Customer_billDetails", "X", new SqlParameter("@Bill_ID", bill_ID)))
            {
                dataGridView1.DataSource = ds.Tables["X"];
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
    }
}
