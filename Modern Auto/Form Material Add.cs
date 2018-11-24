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
    public partial class Form_Material_Add : Form
    {
        private int Product_ID;
        private DataSet ds;

        public Form_Material_Add()
        {
            InitializeComponent();
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            if(txt_Name.Text!="")
            {
                insertProduct();
            }
            else
            {
                MessageBox.Show(SharedParameter.Check_Message);
            }
        }

        private void insertProduct()
        {
            Ezzat.ExecutedNoneQuery("Product_insertProduct"
                                    , new SqlParameter("@ProductName", txt_Name.Text)
                                    , new SqlParameter("@ProductPrice",double.Parse(txt_Price.Text))
                                    , new SqlParameter("@ProductSPrice",double.Parse(txt_SPrice.Text))
                                    , new SqlParameter("@Product_Time",int.Parse(textBox1.Text))
                                    );
            MessageBox.Show(SharedParameter.Successful_Message);
            RefForm();
        }

        private void RefForm()
        {
            textBox1.Text=txt_SPrice.Text = txt_Price.Text = "0";
            txt_Name.Text = "";

            bt_save.Enabled = true;
            bt_edit.Enabled = false;

            using (ds=Ezzat.GetDataSet("Product_selectAll", "X"))
            {
                dataGridView1.DataSource = ds.Tables["X"];
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void Form_Material_Add_Load(object sender, EventArgs e)
        {
            RefForm();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            using (ds = Ezzat.GetDataSet("Product_selectSearch", "X",new SqlParameter("@text",textBox6.Text)))
            {
                dataGridView1.DataSource = ds.Tables["X"];
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Product_ID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            ShowDetails(Product_ID);
        }

        private void ShowDetails(int product_ID)
        {
            bt_edit.Enabled = true;
            bt_save.Enabled = false;

            SqlConnection con;
            SqlDataReader dataReader = Ezzat.GetDataReader("Product_selectSearch_BYID", out con, new SqlParameter("@Product_ID", product_ID));


            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    txt_Name.Text = dataReader["Product_Name"].ToString();
                    txt_Price.Text = dataReader["Product_Price"].ToString();
                    txt_SPrice.Text = dataReader["Product_SPrice"].ToString();
                    textBox1.Text = dataReader["Product_Time"].ToString();
                }
            }
            con.Close();


        }

        private void bt_edit_Click(object sender, EventArgs e)
        {
            if(txt_Name.Text!="")
            {
                EditData();
            }
            else
            {
                MessageBox.Show(SharedParameter.Check_Message);
            }
        }

        private void EditData()
        {
            Ezzat.ExecutedNoneQuery("Product_updateProduct",
                        new SqlParameter("@Product_Name", txt_Name.Text),
                        new SqlParameter("@Product_Price", txt_Price.Text),
                        new SqlParameter("@Product_SPrice", txt_SPrice.Text),
                        new SqlParameter("@Product_ID", Product_ID),
                        new SqlParameter("@Product_Time", textBox1.Text)
                );

            MessageBox.Show(SharedParameter.Successful_Message);

            RefForm();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefForm();
        }
    }
}
