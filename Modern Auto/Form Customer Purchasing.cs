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
    public partial class Form_Customer_Purchasing : Form
    {
        private int Time_Change = 0;
        private string car_number, car_kilometer, car_shasah;
        private double totalMaterial;
        private DataSet ds;
        public Form_Customer_Purchasing()
        {
            InitializeComponent();
        }

        private void Form_Customer_Purchasing_Load(object sender, EventArgs e)
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

            object o = Ezzat.ExecutedScalar("EXPurchasing_selectID");
            txt_BillNumber.Text = o + "";


            using (ds = Ezzat.GetDataSet("Customer_selectAll", "X"))
            {
                combo_car.DataSource = ds.Tables["X"];
                combo_car.DisplayMember = "اسم العميل";
                combo_car.ValueMember = "رقم المسلسل";
                combo_car.Text = "";
                combo_car.SelectedText = "اختار اسم العميل";
            }

            dataGridView1.Rows.Clear();

            txt_MaterialTotal.Text = txt_Discount.Text = txt_AfterDiscount.Text = "0";
            txt_OldMoney.Text = txt_Payment.Text = "0";
            txt_Total.Text = "0";
            txt_Render.Text = "0";
            txt_MaterialTotal.Text = "0";
            txt_AfterDiscount.Text = "0";
            totalMaterial = 0;
        }

        private void combo_car_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_car.Focused)
            {
                ShowDetailsCustomer((int)combo_car.SelectedValue);
            }
        }

        private void ShowDetailsCustomer(int selectedValue)
        {
            SqlConnection con;
            SqlDataReader dataReader = Ezzat.GetDataReader("Customer_selectSearch_BYID", out con, new SqlParameter("@Customer_ID", selectedValue));


            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    txt_OldMoney.Text = dataReader["Customer_Money"].ToString();
                    car_number = dataReader["Car_Number"].ToString();
                    car_shasah = dataReader["Car_Chaseh"].ToString();
                    car_kilometer = dataReader["Kilomaters"].ToString();

                }
            }
            con.Close();
            Calcolate();
        }

        private void comboProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboProduct.Focused)
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
                    txt_Price.Text = dataReader["Product_SPrice"].ToString();
                    Time_Change = (int)dataReader["Product_Time"];
                }
            }
            con.Close();


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if(combo_car.SelectedIndex>=0)
            AddRow();
            else
                MessageBox.Show(SharedParameter.Check_Message);
        }

        private void AddRow()
        {
            if (txt_Quantity.Text != "" && txt_Price.Text != "" && comboProduct.SelectedIndex >= 0)
            {
                dataGridView1.Rows.Add();
                dataGridView1[0, dataGridView1.Rows.Count - 1].Value = comboProduct.SelectedValue;
                dataGridView1[1, dataGridView1.Rows.Count - 1].Value = comboProduct.Text;
                dataGridView1[2, dataGridView1.Rows.Count - 1].Value = txt_Price.Text;
                dataGridView1[3, dataGridView1.Rows.Count - 1].Value = txt_Quantity.Text;
                dataGridView1[4, dataGridView1.Rows.Count - 1].Value = "وحدة";
                dataGridView1[5, dataGridView1.Rows.Count - 1].Value = String.Format("{0:0.00}", Math.Round((double.Parse(txt_Quantity.Text)
                    * double.Parse(txt_Price.Text)), 2));
                if(Time_Change==0)
                {
                    dataGridView1[6, dataGridView1.Rows.Count - 1].Value = Time_Change;
                }
                else
                {
                    dataGridView1[6, dataGridView1.Rows.Count - 1].Value = Time_Change + int.Parse(car_kilometer);
                }
              


                totalMaterial += double.Parse(dataGridView1[5, dataGridView1.Rows.Count - 1].Value.ToString());
                Calcolate();
            }
            else
                MessageBox.Show(SharedParameter.Check_Message);
        }

        private void Calcolate()
        {
            txt_MaterialTotal.Text = String.Format("{0:0.00}", totalMaterial);
            txt_AfterDiscount.Text = String.Format("{0:0.00}", (double.Parse(txt_MaterialTotal.Text) - double.Parse(txt_Discount.Text)));
            txt_Total.Text = String.Format("{0:0.00}", (double.Parse(txt_AfterDiscount.Text) + double.Parse(txt_OldMoney.Text)));
            txt_Render.Text = String.Format("{0:0.00}", (double.Parse(txt_Total.Text) - double.Parse(txt_Payment.Text)));
        }

        private void btuSave_Click(object sender, EventArgs e)
        {
            if (combo_car.SelectedIndex >= 0 && dataGridView1.Rows.Count > 0)
            {
                EditCustomerAccount();
                AddPurchasingBill();
                AddIMBill_Details();
                EditStore();
                EditSafe();
                MessageBox.Show(SharedParameter.Successful_Message);
                RefForm();
            }
            else
                MessageBox.Show(SharedParameter.Check_Message);
        }

        private void EditSafe()
        {
            // تعديل المبلغ الموجود ف الخزنة
            Ezzat.ExecutedNoneQuery("Safe_updateIncrease", new SqlParameter("@Money_Quantity", float.Parse(txt_Payment.Text)));

            // عمل بيان صرف من الخزنة للعميل
            Ezzat.ExecutedNoneQuery("Safe_insertTransaction",
                new SqlParameter("@Report_Type", true),
                new SqlParameter("@Bill_ID", int.Parse(txt_BillNumber.Text)),
                new SqlParameter("@Bill_Type", "مبلغ من بيع"),
                new SqlParameter("@Report_Date", DateTime.Parse(DateTime.Now.ToString())),
                new SqlParameter("@Report_Money", float.Parse(txt_Payment.Text)),
                new SqlParameter("@Report_Notes", richTextBox1.Text)
                );
        }

        private void EditStore()
        {
            // تعديل الكميات ف المخازن
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                Ezzat.ExecutedNoneQuery("Product_updateQuantity_Decrease"
                    , new SqlParameter("@Material_ID", int.Parse(item.Cells[0].Value.ToString()))
                    , new SqlParameter("@Material_Quantity", item.Cells[3].Value.ToString())
                    );
            }

            // اضافة تعاملات ف المخازن
            Ezzat.ExecutedNoneQuery("StoreTransaction_insert",
                new SqlParameter("@Report_Type", false),
                new SqlParameter("@Report_Date", DateTime.Parse(DateTime.Now.ToString())),
                new SqlParameter("@Bill_ID", int.Parse(txt_BillNumber.Text)),
                new SqlParameter("@Bill_Type", "بيع الى عميل"),
                new SqlParameter("@Report_Notes", richTextBox1.Text)
                );
        }

        private void AddIMBill_Details()
        {
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                Ezzat.ExecutedNoneQuery("Customer_insertEXBillDetails"
                    , new SqlParameter("@Bill_ID", txt_BillNumber.Text)
                    , new SqlParameter("@Material_ID", int.Parse(item.Cells[0].Value.ToString()))
                    , new SqlParameter("@Material_Name", item.Cells[1].Value.ToString())
                    , new SqlParameter("@Material_PricePerUnit", item.Cells[2].Value.ToString())
                    , new SqlParameter("@Material_Quantity", float.Parse(item.Cells[3].Value + ""))
                    , new SqlParameter("@Unit", item.Cells[4].Value.ToString())
                    , new SqlParameter("@Total", float.Parse(item.Cells[5].Value + ""))
                    , new SqlParameter("@Bill_Type", false)
                    , new SqlParameter("@Bill_Date", DateTime.Parse(DateTime.Now.ToString()))
                    , new SqlParameter("@Time_change",item.Cells[6].Value.ToString())
                    );
            }
        }

        private void AddPurchasingBill()
        {
            Ezzat.ExecutedNoneQuery("Customer_insertPurchasingBill"
                        , new SqlParameter("@Purchasing_ID", int.Parse(txt_BillNumber.Text))
                        , new SqlParameter("@Supplier_ID", (int)combo_car.SelectedValue)
                        , new SqlParameter("@Bill_Date", DateTime.Parse(DateTime.Now.ToString()))
                        , new SqlParameter("@Payment_Method", "دفع نقدى")
                        , new SqlParameter("@Material_Money", double.Parse(txt_MaterialTotal.Text))
                        , new SqlParameter("@Discount_Money", double.Parse(txt_Discount.Text))
                        , new SqlParameter("@After_Discount", double.Parse(txt_AfterDiscount.Text))
                        , new SqlParameter("@Total_oldMoney", double.Parse(txt_OldMoney.Text))
                        , new SqlParameter("@Total_Money", double.Parse(txt_Total.Text))
                        , new SqlParameter("@Payment_Money", double.Parse(txt_Payment.Text))
                        , new SqlParameter("@After_Payment", double.Parse(txt_Render.Text))
                        , new SqlParameter("@Bill_Details", richTextBox1.Text)
                        , new SqlParameter("@Report_type", "بيع الى عميل")
                );
        }

        private void EditCustomerAccount()
        {
            Ezzat.ExecutedNoneQuery("Customer_updateTotalMoney", new SqlParameter("@Customer_ID", combo_car.SelectedValue),
               new SqlParameter("@Total_Money", double.Parse(txt_Render.Text)));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (combo_car.SelectedIndex >= 0 && dataGridView1.Rows.Count > 0)
            {
                EditCustomerAccount();
                AddPurchasingBill();
                AddIMBill_Details();
                EditStore();
                EditSafe();
                MessageBox.Show(SharedParameter.Successful_Message);


                Form_Bill_Customer_Purchasing print = new Form_Bill_Customer_Purchasing(int.Parse(txt_BillNumber.Text),
                                                     combo_car.Text,
                                                     richTextBox1.Text,
                                                     double.Parse(txt_MaterialTotal.Text),
                                                     double.Parse(txt_Discount.Text),
                                                     double.Parse(txt_OldMoney.Text),
                                                     double.Parse(txt_Payment.Text),
                                                     car_kilometer,
                                                     car_number,
                                                     car_shasah
                                                     );
                print.ShowDialog();
                RefForm();
            }
            else
                MessageBox.Show(SharedParameter.Check_Message);
        }

        private void txt_Discount_KeyPress(object sender, KeyPressEventArgs e)
        {
            SharedParameter.KeyPress(txt_Discount, e);
        }

        private void txt_Payment_KeyPress(object sender, KeyPressEventArgs e)
        {
            SharedParameter.KeyPress(txt_Payment, e);
        }

        private void txt_Quantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            SharedParameter.KeyPress(txt_Quantity, e);
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            totalMaterial -= double.Parse(dataGridView1.CurrentRow.Cells[5].Value.ToString());
            Calcolate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefForm();
        }

        private void txt_Payment_TextChanged(object sender, EventArgs e)
        {
            SharedParameter.Change(txt_Payment);
            Calcolate();
        }

        private void txt_Discount_TextChanged(object sender, EventArgs e)
        {
            SharedParameter.Change(txt_Discount);
            Calcolate();
        }
    }
}
