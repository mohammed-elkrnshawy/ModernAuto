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
    public partial class Form_Material__Gard : Form
    {
        private DataSet ds;
        public Form_Material__Gard()
        {
            InitializeComponent();
        }

        private void Form_Material__Gard_Load(object sender, EventArgs e)
        {
            using (ds=Ezzat.GetDataSet("Store_Gard", "X"))
            {
                dataGridView1.DataSource = ds.Tables["X"];
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
    }
}
