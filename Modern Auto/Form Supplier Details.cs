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
    public partial class Form_Supplier_Details : Form
    {
        private DataSet ds;
        private bool b;
        private int bill_ID;
        public Form_Supplier_Details(int bill_ID, bool b)
        {
            InitializeComponent();
            this.bill_ID = bill_ID;
            this.b = b;
        }

        private void Form_Supplier_Details_Load(object sender, EventArgs e)
        {
            using (ds=Ezzat.GetDataSet("Supplier_billDetails", "X",new SqlParameter("@Bill_ID", bill_ID),new SqlParameter("@Bill_Type", b)))
            {
                dataGridView1.DataSource = ds.Tables["X"];
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
    }
}
