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
    public partial class Form_Customer_Account : Form
    {
        private DataSet ds;
        public Form_Customer_Account()
        {
            InitializeComponent();
        }

        private void Form_Customer_Account_Load(object sender, EventArgs e)
        {
            RefForm();
        }

        private void RefForm()
        {
            using (ds = Ezzat.GetDataSet("Customer_selectAll", "X"))
            {
                combo_Supliers.DataSource = ds.Tables["X"];
                combo_Supliers.DisplayMember = "اسم العميل";
                combo_Supliers.ValueMember = "رقم المسلسل";
                combo_Supliers.Text = "";
                combo_Supliers.SelectedText = "اختار اسم العميل";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            {
                if (combo_Supliers.SelectedIndex >= 0)
                {

                    using (ds = Ezzat.GetDataSet("Customer_selectCustomerAccount", "X"
                        , new SqlParameter("@Day", dateTimePicker1.Value)
                        , new SqlParameter("@Day2", dateTimePicker2.Value)
                        , new SqlParameter("@Supplier_ID", combo_Supliers.SelectedValue))) ;
                    {
                        dataGridView1.DataSource = ds.Tables["X"];

                        dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }

                    object o = Ezzat.ExecutedScalar("Customer_selectCustomerAccount_Pay", new SqlParameter("@Day", dateTimePicker1.Value)
                        , new SqlParameter("@Day2", dateTimePicker2.Value)
                        , new SqlParameter("@Supplier_ID", combo_Supliers.SelectedValue));

                    if (o.Equals(null))
                        textBox1.Text = "0.00";
                    else
                        textBox1.Text = o.ToString();

                    o = Ezzat.ExecutedScalar("Customer_selectCustomerAccount_Purchasing", new SqlParameter("@Day", dateTimePicker1.Value)
                        , new SqlParameter("@Day2", dateTimePicker2.Value)
                        , new SqlParameter("@Supplier_ID", combo_Supliers.SelectedValue));

                    if (o.Equals(null))
                        textBox2.Text = "0.00";
                    else
                        textBox2.Text = o.ToString();

                    textBox3.Text = String.Format("{0:0.00}", Math.Round((double.Parse(textBox1.Text) - double.Parse(textBox2.Text)), 2));
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.CurrentRow.Cells[4].Value.Equals("بيع الى عميل"))
            {
                Form_Customer_Details details = new Form_Customer_Details((int)dataGridView1.CurrentRow.Cells[0].Value);
                details.ShowDialog();
            }
        }
    }
}
