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
    public partial class Form_Supplier_Add : Form
    {
        private int Supplier_ID;
        private DataSet ds;

        public Form_Supplier_Add()
        {
            InitializeComponent();
        }

        private void Form_Supplier_Add_Load(object sender, EventArgs e)
        {
            RefForm();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Supplier_ID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            ShowDetails(Supplier_ID);
        }

        private void ShowDetails(int customer_ID)
        {
            bt_edit.Enabled = true;
            bt_save.Enabled = false;

            SqlConnection con;
            SqlDataReader dataReader = Ezzat.GetDataReader("Supplier_selectSearch_BYID", out con, new SqlParameter("@Customer_Id", customer_ID));


            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    txtName.Text = dataReader["Supplier_Name"].ToString();
                    txtCompany.Text = dataReader["Company_Name"].ToString();
                    txAddress.Text = dataReader["Company_Address"].ToString();
                    txtPhone.Text = dataReader["Phone1"].ToString();
                    txtPhone2.Text = dataReader["Phone2"].ToString();
                }
            }
            con.Close();


        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            using (ds=Ezzat.GetDataSet("Supplier_selectSearch","X",new SqlParameter("@text",textBox6.Text)))
            {
                dataGridView1.DataSource = ds.Tables["X"];
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            ValidDataSave();
        }

        private void ValidDataSave()
        {
            if (ValidText(txtName) && ValidText(txtCompany) && ValidText(txAddress) && ValidText(txtPhone) && ValidText(txtPhone2))
            {
                SaveData();
                MessageBox.Show(SharedParameter.Successful_Message);
                RefForm();
            }
            else
            {
                MessageBox.Show(SharedParameter.Check_Message);
            }
        }

        private void SaveData()
        {
            Ezzat.ExecutedNoneQuery("Supplier_insertSupplier"
                , new SqlParameter("@Supplier_Name", txtName.Text)
                , new SqlParameter("@Company_Name", txtCompany.Text)
                , new SqlParameter("@Company_Address", txAddress.Text)
                , new SqlParameter("@Phone1", txtPhone.Text)
                , new SqlParameter("@Phone2", txtPhone2.Text)
                , new SqlParameter("@TotalMoney", double.Parse("0"))
                );
        }

        private void RefForm()
        {
            bt_save.Enabled = true;
            bt_edit.Enabled = false;


            txtPhone2.Text = txtPhone.Text = txtName.Text = txtCompany.Text = txAddress.Text = "";

            using (ds = Ezzat.GetDataSet("Supplier_selectAll", "X"))
            {
                dataGridView1.DataSource = ds.Tables["X"];
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private bool ValidText(TextBox text)
        {
            if (text.Text == "")
                return false;
            else
                return true;
        }

        private void bt_edit_Click(object sender, EventArgs e)
        {
            if (ValidText(txtName) && ValidText(txtCompany) && ValidText(txAddress) && ValidText(txtPhone) && ValidText(txtPhone2))
            {
                EditData();
                MessageBox.Show(SharedParameter.Successful_Message);
                RefForm();
            }
            else
            {
                MessageBox.Show(SharedParameter.Check_Message);
            }
        }

        private void EditData()
        {
            Ezzat.ExecutedNoneQuery("Supplier_updateSupplier",
               new SqlParameter("@Supplier_Name", txtName.Text)
                , new SqlParameter("@Company_Name", txtCompany.Text)
                , new SqlParameter("@Company_Address", txAddress.Text)
                , new SqlParameter("@Phone1", txtPhone.Text)
                , new SqlParameter("@Phone2", txtPhone2.Text)
                , new SqlParameter("@Supplier_ID", Supplier_ID)
                );
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefForm();
        }
    }
}
