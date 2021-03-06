﻿using System;
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
    public partial class Form_Shared_Backup : Form
    {
        public Form_Shared_Backup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            f.ShowDialog();
            textBox1.Text = f.SelectedPath;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                String path = string.Format("{0}\\ShopDB {1}{2}.bak", textBox1.Text, DateTime.Now.ToShortDateString().Replace('/', '-'), DateTime.Now.ToShortTimeString().Replace(':', '-'));
                More.Backup(path);
                MessageBox.Show(SharedParameter.Successful_Message);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
