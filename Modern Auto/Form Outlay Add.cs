using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modern_Auto
{
    public partial class Form_Outlay_Add : Form
    {
        public Form_Outlay_Add()
        {
            InitializeComponent();
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            if(txt_Name.Text!="")
            {
                SaveData();
            }
        }

        private void SaveData()
        {
            Ezzat.ExecutedNoneQuery("Outlay_insertOutlay", new System.Data.SqlClient.SqlParameter("@Band_Name", txt_Name.Text));

            MessageBox.Show(SharedParameter.Add_Message);

            txt_Name.Text = "";
        }
    }
}
