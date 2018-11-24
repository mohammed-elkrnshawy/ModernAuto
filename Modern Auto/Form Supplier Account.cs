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
    public partial class Form_Supplier_Account : Form
    {
        private DataSet ds;
        public Form_Supplier_Account()
        {
            InitializeComponent();
        }

        private void Form_Supplier_Account_Load(object sender, EventArgs e)
        {
            RefForm();
        }

        private void RefForm()
        {
            using (ds = Ezzat.GetDataSet("Supplier_selectAll", "X"))
            {
                combo_Supliers.DataSource = ds.Tables["X"];
                combo_Supliers.DisplayMember = "اسم المورد";
                combo_Supliers.ValueMember = "رقم المورد";
                combo_Supliers.Text = "";
                combo_Supliers.SelectedText = "اختار اسم المورد";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (combo_Supliers.SelectedIndex >= 0)
            {

                using (ds = Ezzat.GetDataSet("Supplier_selectSupplierAccount", "X"
                    , new SqlParameter("@Day", dateTimePicker1.Value)
                    , new SqlParameter("@Day2", dateTimePicker2.Value)
                    , new SqlParameter("@Supplier_ID", combo_Supliers.SelectedValue))) ;
                {
                    dataGridView1.DataSource = ds.Tables["X"];

                    dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                object o=Ezzat.ExecutedScalar("Supplier_selectSupplierAccount_Purchasing", new SqlParameter("@Day", dateTimePicker1.Value)
                    , new SqlParameter("@Day2", dateTimePicker2.Value)
                    , new SqlParameter("@Supplier_ID", combo_Supliers.SelectedValue));

                if (o .Equals(null))
                    textBox1.Text = "0.00";
                else
                    textBox1.Text = o.ToString();

               o = Ezzat.ExecutedScalar("Supplier_selectSupplierAccount_Pay", new SqlParameter("@Day", dateTimePicker1.Value)
                   , new SqlParameter("@Day2", dateTimePicker2.Value)
                   , new SqlParameter("@Supplier_ID", combo_Supliers.SelectedValue));

                if (o.Equals(null))
                    textBox2.Text = "0.00";
                else
                    textBox2.Text = o.ToString();

                 textBox3.Text= String.Format("{0:0.00}", Math.Round((double.Parse(textBox1.Text) - double.Parse(textBox2.Text)), 2));
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.CurrentRow.Cells[4].Value.Equals("شراء من مورد"))
            {
                Form_Supplier_Details details = new Form_Supplier_Details((int)dataGridView1.CurrentRow.Cells[0].Value,true);
                details.ShowDialog();
            }
            else if (dataGridView1.CurrentRow.Cells[4].Value.Equals("مرتجع الى مورد"))
            {
                Form_Supplier_Details details = new Form_Supplier_Details((int)dataGridView1.CurrentRow.Cells[0].Value, false);
                details.ShowDialog();
            }
        }
    }
}
