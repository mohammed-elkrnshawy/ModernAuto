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
    public partial class Form_Owner_Money : Form
    {
        public Form_Owner_Money()
        {
            InitializeComponent();
        }

        private void Form_Owner_Money_Load(object sender, EventArgs e)
        {
            RefForm();
        }

        private void RefForm()
        {
            textBox2.Text = String.Format("{0:HH:mm:ss  dd/MM/yyyy}", DateTime.Now);


            object o = Ezzat.ExecutedScalar("OwnerTransaction_selectID");
            textBox1.Text = o.ToString();

            tb_money.Text = "0.00";
            richTextBox1.Text = "اكتب البيان";
        }

        private void bt_Save_Click(object sender, EventArgs e)
        {
            if (tb_money.Text != "")
            {
                if (radioButton1.Checked)
                {
                    IM();
                    MessageBox.Show(SharedParameter.Successful_Message);
                }
                else if (radioButton2.Checked)
                {
                    EX();
                    MessageBox.Show(SharedParameter.Successful_Message);
                }
            }
            else
            {
                MessageBox.Show(SharedParameter.Check_Message);
            }
        }

        private void EX()
        {
            OwnerTransacton(radioButton2.Text, false);
            Edit_TheSafe_Decrease();
            RefForm();
        }

        private void IM()
        {
            OwnerTransacton(radioButton1.Text, true);
            EditSafe_Increase();
            RefForm();
        }

        private void OwnerTransacton(string report_type, bool transaction_type)
        {
            Ezzat.ExecutedNoneQuery("Owner_insertTransaction",
                new SqlParameter("@report_id", textBox1.Text),
                new SqlParameter("@report_type", report_type),
                new SqlParameter("@report_date", DateTime.Parse(DateTime.Now.ToString())),
                new SqlParameter("@reportdetails", richTextBox1.Text),
                new SqlParameter("@totalmoney", double.Parse(tb_money.Text)),
                new SqlParameter("@transaction_type", transaction_type)
                );
        }

        private void Edit_TheSafe_Decrease()
        {
            // تعديل المبلغ الموجود ف الخزنة
            Ezzat.ExecutedNoneQuery("Safe_updateDecrease", new SqlParameter("@Money_Quantity", float.Parse(tb_money.Text)));

            // عمل بيان صرف من الخزنة للعميل
            Ezzat.ExecutedNoneQuery("Safe_insertTransaction",
                new SqlParameter("@Report_Type", false),
                new SqlParameter("@Bill_ID", int.Parse(textBox1.Text)),
                new SqlParameter("@Bill_Type", "تسديد الى مورد"),
                new SqlParameter("@Report_Date", DateTime.Parse(DateTime.Now.ToString())),
                new SqlParameter("@Report_Money", float.Parse(tb_money.Text)),
                new SqlParameter("@Report_Notes", richTextBox1.Text)
                );
        }

        private void EditSafe_Increase()
        {
            // تعديل المبلغ الموجود ف الخزنة
            Ezzat.ExecutedNoneQuery("Safe_updateIncrease", new SqlParameter("@Money_Quantity", float.Parse(tb_money.Text)));

            // عمل بيان صرف من الخزنة للعميل
            Ezzat.ExecutedNoneQuery("Safe_insertTransaction",
                new SqlParameter("@Report_Type", true),
                new SqlParameter("@Bill_ID", int.Parse(textBox1.Text)),
                new SqlParameter("@Bill_Type", "تسديد من عميل"),
                new SqlParameter("@Report_Date", DateTime.Parse(DateTime.Now.ToString())),
                new SqlParameter("@Report_Money", float.Parse(tb_money.Text)),
                new SqlParameter("@Report_Notes", richTextBox1.Text)
                );
        }
    }
}
