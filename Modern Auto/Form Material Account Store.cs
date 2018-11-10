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
    public partial class Form_Material_Account_Store : Form
    {
        private DataSet ds;

        public Form_Material_Account_Store()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (ds = Ezzat.GetDataSet("Store_selectStoreTransaction", "X"
                       , new SqlParameter("@Day", dateTimePicker1.Value)
                       , new SqlParameter("@Day2", dateTimePicker2.Value)))
            {
                dataGridView1.DataSource = ds.Tables["X"];

                dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            FillGrid_During(dateTimePicker1.Value, dateTimePicker2.Value);

        }

        private void FillGrid_During(DateTime StartDateTime, DateTime EndDateTime)
        {
            dataGridView2.Rows.Clear();
            SqlConnection con;

            SqlDataReader dataReader = Ezzat.GetDataReader("Product_selectAll", out con);


            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    dataGridView2.Rows.Add();
                    dataGridView2[0, dataGridView2.Rows.Count - 1].Value = dataReader[0].ToString();
                    dataGridView2[1, dataGridView2.Rows.Count - 1].Value = dataReader[1].ToString();
                    dataGridView2[2, dataGridView2.Rows.Count - 1].Value = Ezzat.ExecutedScalar("Material_IM"
                                                        , new SqlParameter("@Day", dateTimePicker1.Value)
                                                        , new SqlParameter("@Day2", dateTimePicker2.Value)
                                                        , new SqlParameter("@product_ID", dataReader[0])
                                                        );
                    dataGridView2[3, dataGridView2.Rows.Count - 1].Value = Ezzat.ExecutedScalar("Material_EX"
                                                        , new SqlParameter("@Day", dateTimePicker1.Value)
                                                        , new SqlParameter("@Day2", dateTimePicker2.Value)
                                                        , new SqlParameter("@product_ID", dataReader[0])
                                                        );
                }
            }
        }
    }
}
