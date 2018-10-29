﻿using System;
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
    public partial class Form_Supplier_Purchasing : Form
    {
        private double totalMaterial = 0;
        private DataSet ds;
        public Form_Supplier_Purchasing()
        {
            InitializeComponent();
        }

        private void Form_Supplier_Purchasing_Load(object sender, EventArgs e)
        {
            RefForm();
        }

        private void RefForm()
        {

            textBox2.Text = String.Format("{0:HH:mm:ss  dd/MM/yyyy}", DateTime.Now);

            using (ds = Ezzat.GetDataSet("Product_selectAll", "X"))
            {
                comboProduct.DataSource = ds.Tables["X"];
                comboProduct.DisplayMember = "اسم المنتج";
                comboProduct.ValueMember = "رقم المنتج";
                comboProduct.Text = "";
                comboProduct.SelectedText = "اختار اسم المنتج";
            }

            object o = Ezzat.ExecutedScalar("IMPurchasing_selectID");
            txt_billNumber.Text = o + "";


            using (ds = Ezzat.GetDataSet("Supplier_selectAll", "X"))
            {
                combo_name.DataSource= ds.Tables["X"];
                combo_name.DisplayMember = "اسم المورد";
                combo_name.ValueMember = "رقم المورد";
                combo_name.Text = "";
                combo_name.SelectedText = "اختار اسم المورد";
            }

            dataGridView1.Rows.Clear();
            txt_afterDiscount.Text = txt_afterPayment.Text = txt_billNumber.Text = txt_billNumberSupplier.Text = txt_discount.Text = "0.00";
            txt_oldTotal.Text = txt_payment.Text = txt_Price.Text = txt_quantity.Text = txt_total.Text = txt_totalMaterial.Text = "0.00";

        }

        private void comboProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboProduct.Focused)
            {
                ShowDetailsProduct((int)comboProduct.SelectedValue);
            }
        }

        private void ShowDetailsProduct(int customer_ID)
        {

            SqlConnection con;
            SqlDataReader dataReader = Ezzat.GetDataReader("Product_selectSearch_BYID", out con, new SqlParameter("@Product_ID", customer_ID));


            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    txt_Price.Text = dataReader["Product_Price"].ToString();
                }
            }
            con.Close();


        }

        private void combo_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(combo_name.Focused)
            {
                ShowDetailsSupplier((int)combo_name.SelectedValue);
            }
        }

        private void ShowDetailsSupplier(int selectedValue)
        {
            SqlConnection con;
            SqlDataReader dataReader = Ezzat.GetDataReader("Supplier_selectSearch_BYID", out con, new SqlParameter("@Customer_ID", selectedValue));


            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    txt_oldTotal.Text = dataReader["TotalMoney"].ToString();
                }
            }
            con.Close();

        }

        private void AddRow()
        {
            if (txt_quantity.Text!=""&&txt_Price.Text!="" && comboProduct.SelectedIndex >= 0)
            {
                dataGridView1.Rows.Add();
                dataGridView1[0, dataGridView1.Rows.Count - 1].Value = comboProduct.SelectedValue;
                dataGridView1[1, dataGridView1.Rows.Count - 1].Value = comboProduct.Text;
                dataGridView1[2, dataGridView1.Rows.Count - 1].Value = txt_Price.Text;
                dataGridView1[3, dataGridView1.Rows.Count - 1].Value = txt_quantity.Text;
                dataGridView1[4, dataGridView1.Rows.Count - 1].Value = "وحدة";
                dataGridView1[5, dataGridView1.Rows.Count - 1].Value = String.Format("{0:0.00}", Math.Round((double.Parse(txt_quantity.Text) * double.Parse(txt_Price.Text)), 2));
                

                totalMaterial += double.Parse(dataGridView1[5, dataGridView1.Rows.Count - 1].Value.ToString());
                Calcolate();
            }
            else
                MessageBox.Show(SharedParameter.Check_Message);
        }

        private void Calcolate()
        {
            txt_totalMaterial.Text = String.Format("{0:0.00}", totalMaterial);
            txt_afterDiscount.Text = String.Format("{0:0.00}", (double.Parse(txt_totalMaterial.Text) - double.Parse(txt_discount.Text)));
            txt_total.Text = String.Format("{0:0.00}", (double.Parse(txt_afterDiscount.Text) + double.Parse(txt_oldTotal.Text)));
            txt_afterPayment.Text = String.Format("{0:0.00}", (double.Parse(txt_total.Text) - double.Parse(txt_payment.Text)));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            AddRow();
        }

        private void txt_discount_TextChanged(object sender, EventArgs e)
        {
            SharedParameter.Change(txt_discount);
            Calcolate();
        }

        private void txt_payment_TextChanged(object sender, EventArgs e)
        {
            Calcolate();
        }

        private void txt_discount_KeyPress(object sender, KeyPressEventArgs e)
        {
            SharedParameter.KeyPress(txt_discount, e);
        }

        private void txt_payment_KeyPress(object sender, KeyPressEventArgs e)
        {
            SharedParameter.KeyPress(txt_payment, e);
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            totalMaterial -= double.Parse(dataGridView1.CurrentRow.Cells[5].Value.ToString());
            Calcolate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (combo_name.SelectedIndex >= 0 && dataGridView1.Rows.Count > 0)
            {
                EditSuplierAccount();
                AddPurchasingBill();
                AddIMBill_Details();
                EditStore();
                EditSafe();
                RefForm();
            }
            else
                MessageBox.Show(SharedParameter.Check_Message);
        }

        private void EditSafe()
        {
            // تعديل المبلغ الموجود ف الخزنة
            Ezzat.ExecutedNoneQuery("Safe_updateDecrease", new SqlParameter("@Money_Quantity", float.Parse(txt_payment.Text)));

            // عمل بيان صرف من الخزنة للعميل
            Ezzat.ExecutedNoneQuery("Safe_insertTransaction",
                new SqlParameter("@Report_Type", false),
                new SqlParameter("@Bill_ID", int.Parse(txt_billNumber.Text)),
                new SqlParameter("@Bill_Type", "صرف الى مورد"),
                new SqlParameter("@Report_Date", DateTime.Parse(DateTime.Now.ToString())),
                new SqlParameter("@Report_Money", float.Parse(txt_payment.Text)),
                new SqlParameter("@Report_Notes", richTextBox1.Text)
                );
        }

        private void EditStore()
        {

            // تعديل الكميات ف المخازن
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                Ezzat.ExecutedNoneQuery("Product_updateQuantity_Increase"
                    , new SqlParameter("@Material_ID", int.Parse(item.Cells[0].Value.ToString()))
                    , new SqlParameter("@Material_Quantity", item.Cells[3].Value.ToString())
                    );
            }

            // اضافة تعاملات ف المخازن
            Ezzat.ExecutedNoneQuery("StoreTransaction_insert",
                new SqlParameter("@Report_Type", true),
                new SqlParameter("@Report_Date", DateTime.Parse(DateTime.Now.ToString())),
                new SqlParameter("@Bill_ID", int.Parse(txt_billNumber.Text)),
                new SqlParameter("@Bill_Type", "شراء من مورد"),
                new SqlParameter("@Report_Notes", richTextBox1.Text)
                );
        }

        private void AddIMBill_Details()
        {
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                Ezzat.ExecutedNoneQuery("Supplier_insertIMBillDetails"
                    , new SqlParameter("@Bill_ID", txt_billNumber.Text)
                    , new SqlParameter("@Material_ID", int.Parse(item.Cells[0].Value.ToString()))
                    , new SqlParameter("@Material_Name", item.Cells[1].Value.ToString())
                    , new SqlParameter("@Material_PricePerUnit", item.Cells[2].Value.ToString())
                    , new SqlParameter("@Material_Quantity", float.Parse(item.Cells[3].Value + ""))
                    , new SqlParameter("@Unit", item.Cells[4].Value.ToString())
                    , new SqlParameter("@Total", float.Parse(item.Cells[5].Value + ""))
                    , new SqlParameter("@Bill_Type", true)
                    );
            }
        }

        private void AddPurchasingBill()
        {
            Ezzat.ExecutedNoneQuery("Supplier_insertPurchasingBill"
                        , new SqlParameter("@Purchasing_ID",int.Parse(txt_billNumber.Text))
                        ,new SqlParameter("@Supplier_ID",(int)combo_name.SelectedValue)
                        ,new SqlParameter("@Bill_Date",DateTime.Parse(DateTime.Now.ToString()))
                        ,new SqlParameter("@Payment_Method","دفع نقدى")
                        ,new SqlParameter("@Material_Money",double.Parse(txt_totalMaterial.Text))
                        ,new SqlParameter("@Discount_Money",double.Parse(txt_discount.Text))
                        ,new SqlParameter("@After_Discount",double.Parse(txt_afterDiscount.Text))
                        ,new SqlParameter("@Total_oldMoney",double.Parse(txt_oldTotal.Text))
                        ,new SqlParameter("@Total_Money",double.Parse(txt_oldTotal.Text))
                        ,new SqlParameter("@Payment_Money",double.Parse(txt_payment.Text))
                        ,new SqlParameter("@After_Payment",double.Parse(txt_afterPayment.Text))
                        ,new SqlParameter("@Bill_Details",richTextBox1.Text)
                        ,new SqlParameter("@Bill_Number_Supplier",int.Parse(txt_billNumberSupplier.Text))
                );
        }

        private void EditSuplierAccount()
        {
            Ezzat.ExecutedNoneQuery("Supplier_updateTotalMoney", new SqlParameter("@Supplier_ID",combo_name.SelectedValue), new SqlParameter("@Total_Money", double.Parse(txt_afterPayment.Text)));
        }
    }
}
