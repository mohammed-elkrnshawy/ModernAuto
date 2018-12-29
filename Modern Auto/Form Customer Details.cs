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

            ShowDetails();
        }

        private void ShowDetails()
        {
            SqlConnection con;
            SqlDataReader dataReader = Ezzat.GetDataReader("customer_selectBillDetails2", out con, new SqlParameter("@bill_ID", bill_ID));


            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    //tb_name.Text = dataReader["Customer_Name"].ToString();
                    //tb_carNumber.Text = dataReader["Car_Number"].ToString();
                    //tb_carShaseh.Text = dataReader["Car_Chaseh"].ToString();
                    //comboBox1.Text = dataReader["Car_Type"].ToString();
                    //tb_phone.Text = dataReader["Customer_Phone"].ToString();
                    //tb_kilo.Text = dataReader["Kilomaters"].ToString();


                    txt_AfterDiscount.Text = dataReader["After_Discount"].ToString();
                    txt_Discount.Text = dataReader["Discount_Money"].ToString();
                    txt_MaterialTotal.Text = dataReader["Material_Money"].ToString();
                    txt_OldMoney.Text = dataReader["Total_oldMoney"].ToString();
                    txt_Payment.Text = dataReader["Payment_Money"].ToString();
                    txt_Render.Text = dataReader["After_Payment"].ToString();
                    txt_Total.Text = dataReader["Total_Money"].ToString();
                
                }
            }
            con.Close();


        }
    }
}
