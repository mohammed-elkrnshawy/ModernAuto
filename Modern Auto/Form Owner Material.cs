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
    public partial class Form_Owner_Material : Form
    {
        private DataSet dataSet;
        public Form_Owner_Material()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (dataSet = Ezzat.GetDataSet("Owner_selectTransaction", "X", new SqlParameter("Day", dateTimePicker1.Value), new SqlParameter("Day2", dateTimePicker2.Value)))
            {
                dataGridView1.DataSource = dataSet.Tables["X"];
                dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            }

            label5.Text = Ezzat.ExecutedScalar("Select_TotalOwnerTransaction", new SqlParameter("Day", dateTimePicker1.Value), new SqlParameter("Day2", dateTimePicker2.Value), new SqlParameter("@f", true)).ToString();
            label6.Text = Ezzat.ExecutedScalar("Select_TotalOwnerTransaction", new SqlParameter("Day", dateTimePicker1.Value), new SqlParameter("Day2", dateTimePicker2.Value), new SqlParameter("@f", false)).ToString();

        }
    }
}
