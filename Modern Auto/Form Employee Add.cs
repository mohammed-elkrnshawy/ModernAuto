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
    public partial class Form_Employee_Add : Form
    {
        private int Emp_ID;
        private DataSet ds;
        public Form_Employee_Add()
        {
            InitializeComponent();
        }

        private void Form_Employee_Add_Load(object sender, EventArgs e)
        {
            RefForm();
        }

        private void RefForm()
        {
            bt_save.Enabled = true;
            bt_edit.Enabled = false;
            


            tb_name.Text = tb_phone.Text = tb_address.Text = tb_carNumber.Text = tb_money.Text = "";

            using (ds = Ezzat.GetDataSet("Employee_selectAll", "X"))
            {
                dataGridView1.DataSource = ds.Tables["X"];
            }
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            ValidDataSave();
        }

        private void ValidDataSave()
        {
            if (ValidText(tb_name) && ValidText(tb_carNumber) && ValidText(tb_money) && ValidText(tb_phone) && ValidText(tb_phone))
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

        private bool ValidText(TextBox text)
        {
            if (text.Text == "")
                return false;
            else
                return true;
        }

        private void SaveData()
        {
            Ezzat.ExecutedNoneQuery("Employee_insertEmployee"
                , new SqlParameter("@emp_name", tb_name.Text)
                , new SqlParameter("@emp_card", tb_carNumber.Text)
                , new SqlParameter("@emp_address", tb_address.Text)
                , new SqlParameter("@emp_phone", tb_phone.Text)
                , new SqlParameter("@emp_money", double.Parse(tb_money.Text))
                );
        }

        private void bt_edit_Click(object sender, EventArgs e)
        {
            ValidDataEdit();
        }

        private void ValidDataEdit()
        {
            if (ValidText(tb_name) && ValidText(tb_carNumber) && ValidText(tb_money) && ValidText(tb_phone) && ValidText(tb_phone))
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
            Ezzat.ExecutedNoneQuery("Employee_UpdateEmployee"
                , new SqlParameter("@emp_name", tb_name.Text)
                , new SqlParameter("@emp_card", tb_carNumber.Text)
                , new SqlParameter("@emp_address", tb_address.Text)
                , new SqlParameter("@emp_phone", tb_phone.Text)
                , new SqlParameter("@emp_money", double.Parse(tb_money.Text))
                , new SqlParameter("@emp_id", Emp_ID)
                );
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Emp_ID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            ShowDetails(Emp_ID);
        }

        private void ShowDetails(int customer_ID)
        {
            bt_edit.Enabled = true;
            bt_save.Enabled = false;

            SqlConnection con;
            SqlDataReader dataReader = Ezzat.GetDataReader("Employee_selectSearch_BYID", out con, new SqlParameter("@Customer_Id", customer_ID));


            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    tb_name.Text = dataReader["emp_name"].ToString();
                    tb_carNumber.Text = dataReader["emp_card"].ToString();
                    tb_address.Text = dataReader["emp_address"].ToString();
                    tb_phone.Text = dataReader["emp_phone"].ToString();
                    tb_money.Text = dataReader["emp_money"].ToString();
                   
                }
            }
            con.Close();


        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            using (ds = Ezzat.GetDataSet("Employee_selectSearch", "X", new SqlParameter("text", textBox6.Text)))
            {
                dataGridView1.DataSource = ds.Tables["X"];
            }
        }
    }
}
