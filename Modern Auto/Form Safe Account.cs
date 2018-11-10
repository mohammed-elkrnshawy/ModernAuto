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
    public partial class Form_Safe_Account : Form
    {
        private SqlDataReader dataReader;
        private DataSet ds;

        public Form_Safe_Account()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            dataGridView1.Rows.Clear();
            ReturnData();
            CalcolateTotal();



            //using (ds=Ezzat.GetDataSet("Safe_selectSafeTransaction","X"
            //           , new SqlParameter("@Day", dateTimePicker1.Value)
            //           , new SqlParameter("@Day2", dateTimePicker2.Value)))
            //{
            //    dataGridView1.DataSource = ds.Tables["X"];

            //    dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //    dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //}
        }

        private void ReturnData()
        {

            SqlConnection con;

            dataReader = Ezzat.GetDataReader("Safe_selectSafeTransaction", out con,
                new SqlParameter("@Day", DateTime.Parse(dateTimePicker1.Value.ToString())),
                new SqlParameter("@Day2", DateTime.Parse(dateTimePicker2.Value.ToString())));


            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1[0, dataGridView1.Rows.Count - 1].Value = dataReader[0].ToString();
                    dataGridView1[1, dataGridView1.Rows.Count - 1].Value = dataReader[1].ToString();
                    dataGridView1[2, dataGridView1.Rows.Count - 1].Value = dataReader[2].ToString();
                    dataGridView1[3, dataGridView1.Rows.Count - 1].Value = (double.Parse(dataReader[1].ToString()) - double.Parse(dataReader[2].ToString()));
                    dataGridView1[4, dataGridView1.Rows.Count - 1].Value = dataReader[3].ToString();
                    dataGridView1[5, dataGridView1.Rows.Count - 1].Value = dataReader[5].ToString();
                    dataGridView1[6, dataGridView1.Rows.Count - 1].Value = dataReader[4].ToString();
                }
            }

            con.Close();




        }

        private void CalcolateTotal()
        {

            double Total = 0, debit = 0;
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                Total += double.Parse(item.Cells[1].Value.ToString());
                debit += double.Parse(item.Cells[2].Value.ToString());
            }

            label8.Text = Total + "";
            label9.Text = debit + "";
            label10.Text = (Total - debit) + "";


         
            label5.Text = Ezzat.ExecutedScalar("Safe_selectCurrentMoney") + "";


        }
    }
}
