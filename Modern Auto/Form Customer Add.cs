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
    public partial class Form_Customer_Add : Form
    {
        private DataSet ds;
        private int Customer_ID;

        public Form_Customer_Add()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.Focused)
            {
                if(comboBox1.SelectedIndex==1)
                {
                    pictureBox1.BackgroundImage = Properties.Resources.z181kOf3zB_400x400;
                }
                else if (comboBox1.SelectedIndex == 0)
                {
                    pictureBox1.BackgroundImage = Properties.Resources.z2download;
                }
                else if (comboBox1.SelectedIndex == 2)
                {
                    pictureBox1.BackgroundImage = Properties.Resources.z3download__1_;
                }
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            SharedParameter.KeyPress(tb_phone, e);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            SharedParameter.Change(tb_phone);
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            SharedParameter.Leave(tb_phone);
        }

        private void tb_kilo_TextChanged(object sender, EventArgs e)
        {
            SharedParameter.Change(tb_kilo);
        }

        private void tb_kilo_Leave(object sender, EventArgs e)
        {
            SharedParameter.Leave(tb_kilo);
        }

        private void tb_kilo_KeyPress(object sender, KeyPressEventArgs e)
        {
            SharedParameter.KeyPress(tb_kilo, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ValidDataSave();
        }

        private void ValidDataSave()
        {
            if(ValidText(tb_name)&& ValidText(tb_carNumber) && ValidText(tb_carShaseh) && ValidText(tb_phone) && ValidText(tb_kilo))
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

        private void RefForm()
        {
            bt_save.Enabled = true;
            bt_edit.Enabled = false;
            pictureBox1.BackgroundImage = null;
            comboBox1.Text = "اختار نوع السيارة";


            tb_name.Text = tb_phone.Text = tb_kilo.Text = tb_carShaseh.Text = tb_carNumber.Text = "";

            using (ds = Ezzat.GetDataSet("Customer_selectAll","X"))
            {
                dataGridView1.DataSource = ds.Tables["X"];
            }


        }

        private void SaveData()
        {
            Ezzat.ExecutedNoneQuery("Customer_insertCustomer"
                , new SqlParameter("@Customer_Name",tb_name.Text)
                , new SqlParameter("@Car_Number",tb_carNumber.Text)
                , new SqlParameter("@Car_Chaseh",tb_carShaseh.Text)
                , new SqlParameter("@Car_Type",comboBox1.Text)
                , new SqlParameter("@Customer_Phone",tb_phone.Text)
                , new SqlParameter("@Kilomaters",tb_kilo.Text)
                );
        }

        private bool ValidText(TextBox text)
        {
            if (text.Text == "")
                return false;
            else
                return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ValidDataEdit();
        }

        private void ValidDataEdit()
        {
            if (ValidText(tb_name) && ValidText(tb_carNumber) && ValidText(tb_carShaseh) && ValidText(tb_phone) && ValidText(tb_kilo))
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
            Ezzat.ExecutedNoneQuery("Customer_updateCustomer"
                , new SqlParameter("@Customer_Name", tb_name.Text)
                , new SqlParameter("@Car_Number", tb_carNumber.Text)
                , new SqlParameter("@Car_Chaseh", tb_carShaseh.Text)
                , new SqlParameter("@Car_Type", comboBox1.Text)
                , new SqlParameter("@Customer_Phone", tb_phone.Text)
                , new SqlParameter("@Kilomaters", tb_kilo.Text)
                , new SqlParameter("@Customer_ID", Customer_ID)
                );
        }

        private void label1_Click(object sender, EventArgs e)
        {
            RefForm();
        }

        private void Form_Customer_Add_Load(object sender, EventArgs e)
        {
            RefForm();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            using (ds=Ezzat.GetDataSet("Customer_selectSearch", "X",new SqlParameter("text",textBox6.Text)))
            {
                dataGridView1.DataSource = ds.Tables["X"];
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Customer_ID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            ShowDetails(Customer_ID);
        }

        private void ShowDetails(int customer_ID)
        {
            bt_edit.Enabled = true;
            bt_save.Enabled = false;

            SqlConnection con;
            SqlDataReader dataReader = Ezzat.GetDataReader("Customer_selectSearch_BYID", out con, new SqlParameter("@Customer_Id", customer_ID));


            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    tb_name.Text = dataReader["Customer_Name"].ToString();
                    tb_carNumber.Text = dataReader["Car_Number"].ToString();
                    tb_carShaseh.Text = dataReader["Car_Chaseh"].ToString();
                    comboBox1.Text = dataReader["Car_Type"].ToString();
                    tb_phone.Text = dataReader["Customer_Phone"].ToString();
                    tb_kilo.Text = dataReader["Kilomaters"].ToString();
                  

                }
            }
            con.Close();

           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefForm();
        }
    }
}
