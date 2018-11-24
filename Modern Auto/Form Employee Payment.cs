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
    public partial class Form_Employee_Payment : Form
    {
        private DataSet ds;
        public Form_Employee_Payment()
        {
            InitializeComponent();
        }

        private void Form_Employee_Payment_Load(object sender, EventArgs e)
        {
            RefForm();
        }

        private void RefForm()
        {
            richTextBox1.Text = "لا يوجد ملاحطات";

            txt_Payment.Text = tb_money.Text = "0.00";

            textBox1.Text = String.Format("{0:HH:mm:ss  dd/MM/yyyy}", DateTime.Now);

            using (ds = Ezzat.GetDataSet("Employee_selectAll", "X"))
            {
                comboBox1.DataSource = ds.Tables["X"];
                comboBox1.DisplayMember = "اسم الموظف";
                comboBox1.ValueMember = "رقم المسلسل";
                comboBox1.Text = "";
                comboBox1.SelectedText = "اختار اسم الموظف";
            }

            object o = Ezzat.ExecutedScalar("EmployeeTransaction_selectID");
            txt_billNumber.Text = o + "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.Focused)
            {
                ShowDetails((int)comboBox1.SelectedValue);
            }
        }

        private void ShowDetails(int customer_ID)
        {
           
            SqlConnection con;
            SqlDataReader dataReader = Ezzat.GetDataReader("Employee_selectSearch_BYID", out con, new SqlParameter("@Customer_Id", customer_ID));


            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    tb_money.Text = dataReader["emp_money"].ToString();
                }
            }
            con.Close();


        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            EditSafe();
            AddTransaction();
            MessageBox.Show(SharedParameter.Successful_Message);
            RefForm();
        }

        private void AddTransaction()
        {
            Ezzat.ExecutedNoneQuery("Employee_insertTransaction"
                ,new SqlParameter("@Employee_ID",comboBox1.SelectedValue)
                ,new SqlParameter("@Employee_Money",txt_Payment.Text)
                ,new SqlParameter("@Report_Notes",richTextBox1.Text)
                ,new SqlParameter("@Report_Date",DateTime.Parse(DateTime.Now.ToString()))
                ,new SqlParameter("@Report_ID",txt_billNumber.Text)
                );
        }

        private void EditSafe()
        {
            // تعديل المبلغ الموجود ف الخزنة
            Ezzat.ExecutedNoneQuery("Safe_updateDecrease", new SqlParameter("@Money_Quantity", float.Parse(txt_Payment.Text)));

            // عمل بيان صرف من الخزنة للعميل
            Ezzat.ExecutedNoneQuery("Safe_insertTransaction",
                new SqlParameter("@Report_Type", false),
                new SqlParameter("@Bill_ID", int.Parse(txt_billNumber.Text)),
                new SqlParameter("@Bill_Type", "قبض الى موظف"),
                new SqlParameter("@Report_Date", DateTime.Parse(DateTime.Now.ToString())),
                new SqlParameter("@Report_Money", float.Parse(txt_Payment.Text)),
                new SqlParameter("@Report_Notes", richTextBox1.Text)
                );
        }
    }
}
