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
    public partial class Form_Material_Account_Store : Form
    {
        private DataSet ds;

        public Form_Material_Account_Store()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (ds = Ezzat.GetDataSet("Store_selectStoreTransaction", "X"
                       , new SqlParameter("@Day", dateTimePicker1.Value)
                       , new SqlParameter("@Day2", dateTimePicker2.Value)))
            {
                dataGridView1.DataSource = ds.Tables["X"];

                dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }
    }
}
