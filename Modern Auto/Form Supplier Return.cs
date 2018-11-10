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
    public partial class Form_Supplier_Return : Form
    {
        private double totalMaterial;
        private DataSet ds;
        public Form_Supplier_Return()
        {
            InitializeComponent();
        }

        private void Form_Supplier_Return_Load(object sender, EventArgs e)
        {
            RefForm();
        }

        private void RefForm()
        {
            textBox2.Text = String.Format("{0:HH:mm:ss  dd/MM/yyyy}", DateTime.Now);

            using (ds = Ezzat.GetDataSet("Product_selectAll", "X"))
            {
                combo_Product.DataSource = ds.Tables["X"];
                combo_Product.DisplayMember = "اسم المنتج";
                combo_Product.ValueMember = "رقم المنتج";
                combo_Product.Text = "";
                combo_Product.SelectedText = "اختار اسم المنتج";
            }

            object o = Ezzat.ExecutedScalar("IMReturning_selectID");
            txt_BillNumber.Text = o + "";


            using (ds = Ezzat.GetDataSet("Supplier_selectAll", "X"))
            {
                combo_name.DataSource = ds.Tables["X"];
                combo_name.DisplayMember = "اسم المورد";
                combo_name.ValueMember = "رقم المورد";
                combo_name.Text = "";
                combo_name.SelectedText = "اختار اسم المورد";
            }

            dataGridView1.Rows.Clear();
            txt_Discount.Text = txt_MaterialTotal.Text = txt_OldMoney.Text = "0.00";
            txt_Prise.Text = txt_Quantity.Text = txt_TotalMoney.Text = txt_AfterDiscount.Text = "0.00";
        }

        private void combo_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_name.Focused)
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
                    txt_OldMoney.Text = dataReader["TotalMoney"].ToString();
                }
            }
            con.Close();

        }

        private void combo_Product_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_Product.Focused)
            {
                ShowDetailsProduct((int)combo_Product.SelectedValue);
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
                    txt_Prise.Text = dataReader["Product_Price"].ToString();
                }
            }
            con.Close();


        }

        private void txt_Discount_TextChanged(object sender, EventArgs e)
        {
            Calcolate();
        }

        private void Calcolate()
        {
            txt_MaterialTotal.Text = String.Format("{0:0.00}", totalMaterial);
            txt_AfterDiscount.Text = String.Format("{0:0.00}", (double.Parse(txt_MaterialTotal.Text) - double.Parse(txt_Discount.Text)));
            txt_TotalMoney.Text = String.Format("{0:0.00}", (double.Parse(txt_OldMoney.Text) - double.Parse(txt_AfterDiscount.Text)));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            AddRow();
        }

        private void AddRow()
        {
            if (txt_Quantity.Text != "" && txt_Prise.Text != "" && combo_Product.SelectedIndex >= 0)
            {
                dataGridView1.Rows.Add();
                dataGridView1[0, dataGridView1.Rows.Count - 1].Value = combo_Product.SelectedValue;
                dataGridView1[1, dataGridView1.Rows.Count - 1].Value = combo_Product.Text;
                dataGridView1[2, dataGridView1.Rows.Count - 1].Value = txt_Prise.Text;
                dataGridView1[3, dataGridView1.Rows.Count - 1].Value = txt_Quantity.Text;
                dataGridView1[4, dataGridView1.Rows.Count - 1].Value = "وحدة";
                dataGridView1[5, dataGridView1.Rows.Count - 1].Value = String.Format("{0:0.00}", Math.Round((double.Parse(txt_Quantity.Text) * double.Parse(txt_Prise.Text)), 2));


                totalMaterial += double.Parse(dataGridView1[5, dataGridView1.Rows.Count - 1].Value.ToString());
                Calcolate();
            }
            else
                MessageBox.Show(SharedParameter.Check_Message);
        }

        private void txt_Payment_TextChanged(object sender, EventArgs e)
        {
            Calcolate();
        }

        private void btuSave_Click(object sender, EventArgs e)
        {
            if (combo_name.SelectedIndex >= 0 && dataGridView1.Rows.Count > 0)
            {
                EditSuplierAccount();
                AddPurchasingBill();
                AddIMBill_Details();
                EditStore();
                MessageBox.Show(SharedParameter.Successful_Message);
                RefForm();
            }
            else
                MessageBox.Show(SharedParameter.Check_Message);
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
                new SqlParameter("@Bill_Type", "مرتجع الى مورد"),
                new SqlParameter("@Report_Notes", richTextBox1.Text)
                );
        }

        private void AddIMBill_Details()
        {
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                Ezzat.ExecutedNoneQuery("Supplier_insertIMBillDetails"
                    , new SqlParameter("@Bill_ID", txt_billNumberSupplier.Text)
                    , new SqlParameter("@Material_ID", int.Parse(item.Cells[0].Value.ToString()))
                    , new SqlParameter("@Material_Name", item.Cells[1].Value.ToString())
                    , new SqlParameter("@Material_PricePerUnit", item.Cells[2].Value.ToString())
                    , new SqlParameter("@Material_Quantity", float.Parse(item.Cells[3].Value + ""))
                    , new SqlParameter("@Unit", item.Cells[4].Value.ToString())
                    , new SqlParameter("@Total", float.Parse(item.Cells[5].Value + ""))
                    , new SqlParameter("@Bill_Type", false)
                    , new SqlParameter("@Bill_Date", DateTime.Parse(DateTime.Now.ToString()))
                    );
            }
        }

        private void AddPurchasingBill()
        {
            Ezzat.ExecutedNoneQuery("Supplier_insertReturningBill"
                        , new SqlParameter("@Purchasing_ID", int.Parse(txt_BillNumber.Text))
                        , new SqlParameter("@Supplier_ID", (int)combo_name.SelectedValue)
                        , new SqlParameter("@Bill_Date", DateTime.Parse(DateTime.Now.ToString()))
                        , new SqlParameter("@Payment_Method", "مرتجع نقدى")
                        , new SqlParameter("@Material_Money", double.Parse(txt_MaterialTotal.Text))
                        , new SqlParameter("@Discount_Money", double.Parse(txt_Discount.Text))
                        , new SqlParameter("@After_Discount", double.Parse(txt_AfterDiscount.Text))
                        , new SqlParameter("@Total_oldMoney", double.Parse(txt_OldMoney.Text))
                        , new SqlParameter("@Total_Money", double.Parse(txt_TotalMoney.Text))
                        , new SqlParameter("@Bill_Details", richTextBox1.Text)
                        , new SqlParameter("@Bill_Number_Supplier", int.Parse(txt_billNumberSupplier.Text))
                );
        }

        private void EditSuplierAccount()
        {
            Ezzat.ExecutedNoneQuery("Supplier_updateTotalMoney"
                , new SqlParameter("@Supplier_ID", combo_name.SelectedValue)
                , new SqlParameter("@Total_Money", double.Parse(txt_TotalMoney.Text)));
        }
    }
}
