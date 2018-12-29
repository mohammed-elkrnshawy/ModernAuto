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
    public partial class Form_Shared_Home : Form
    {
       
        Image closeImage, closeImageAct;

        public Form_Shared_Home()
        {
            InitializeComponent();
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            // minh viet san, khoi mat thoi gian
            Rectangle rect = tabControl1.GetTabRect(e.Index);
            Rectangle imageRec = new Rectangle(rect.Right - closeImage.Width,
                rect.Top + (rect.Height - closeImage.Height) / 2,
                closeImage.Width, closeImage.Height);
            // tang size rect
            rect.Size = new Size(rect.Width + 20, 38);

            Font f;
            Brush br = Brushes.Black;
            StringFormat strF = new StringFormat(StringFormat.GenericDefault);
            // neu tab dang duoc chon
            if (tabControl1.SelectedTab == tabControl1.TabPages[e.Index])
            {
                // hinh mau do, hinh nay them tu properti
                e.Graphics.DrawImage(closeImageAct, imageRec);
                f = new Font("Arial", 10, FontStyle.Bold);
                // Ten tabPage
                e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text,
                    f, br, rect, strF);
            }
            else
            {
                // Tap dang mo, nhung ko dc chon, hinh mau den
                e.Graphics.DrawImage(closeImage, imageRec);
                f = new Font("Arial", 9, FontStyle.Regular);
                // Ten tabPage
                e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text,
                    f, br, rect, strF);
            }
        }

        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            // Su kien click dong tabpage
            for (int i = 0; i < tabControl1.TabCount; i++)
            {
                // giong o DrawItem
                Rectangle rect = tabControl1.GetTabRect(i);
                Rectangle imageRec = new Rectangle(rect.Right - closeImage.Width,
                    rect.Top + (rect.Height - closeImage.Height) / 2,
                    closeImage.Width, closeImage.Height);

                if (imageRec.Contains(e.Location) && i != 0)
                    tabControl1.TabPages.Remove(tabControl1.SelectedTab);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Size mysize = new System.Drawing.Size(20, 20); // co anh chen vao
            Bitmap bt = new Bitmap(Properties.Resources.closeBlack);
            // anh nay ban dau minh da them vao
            Bitmap btm = new Bitmap(bt, mysize);
            closeImageAct = btm;
            //
            //
            Bitmap bt2 = new Bitmap(Properties.Resources.closeBlack);
            // anh nay ban dau minh da them vao
            Bitmap btm2 = new Bitmap(bt2, mysize);
            closeImage = btm2;
            tabControl1.Padding = new Point(30);

           
        }

        private void اضافةسيارةعميلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab(اضافةسيارةعميلToolStripMenuItem.Text, new Form_Customer_Add());
        }

        private void فاتورةصيانةجديدةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab(فاتورةصيانةجديدةToolStripMenuItem.Text, new Form_Customer_Purchasing());
        }

        private void تسديدمنعميلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab(تسديدمنعميلToolStripMenuItem.Text, new Form_Customer_Payback());
        }

        private void حسابعميلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab(حسابعميلToolStripMenuItem.Text, new Form_Customer_Account());
        }

        private void شراءمنموردToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab(شراءمنموردToolStripMenuItem.Text, new Form_Supplier_Purchasing());
        }

        private void اضافةوتغديلموردToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab(اضافةوتغديلموردToolStripMenuItem.Text, new Form_Supplier_Add());
        }

        private void تسديدالىموردToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab(تسديدالىموردToolStripMenuItem.Text, new Form_Supplier_Payback());
        }

        private void مرتجعالىموردToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab(مرتجعالىموردToolStripMenuItem.Text, new Form_Supplier_Return());
        }

        private void حسابموردToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab(حسابموردToolStripMenuItem.Text, new Form_Supplier_Account());
        }

        private void اضافةالاصنافToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab(اضافةالاصنافToolStripMenuItem.Text, new Form_Material_Add());
        }

        private void خردالمخازنToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab(خردالمخازنToolStripMenuItem.Text, new Form_Material__Gard());
        }

        private void تحركاتالمخازنToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab(تحركاتالمخازنToolStripMenuItem.Text, new Form_Material_Account_Store());
        }
        
        private void اضافةبندمصروفاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Outlay_Add form = new Form_Outlay_Add();
            form.ShowDialog();
        }

        private void حسابمصروفاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab(حسابمصروفاتToolStripMenuItem.Text, new Form_Outlay_Account());
        }

        private void تحريربيانمصروفاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab(تحريربيانمصروفاتToolStripMenuItem.Text, new Form_Outlay_Payment());
        }

        private void حركاتالخزنةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab(حركاتالخزنةToolStripMenuItem.Text, new Form_Safe_Account());
        }

        private void ايداعوصرفبضاعةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab(ايداعوصرفبضاعةToolStripMenuItem.Text, new Form_Owner_Material());
        }

        private void ايداعوصرفاموالToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab(ايداعوصرفاموالToolStripMenuItem.Text, new Form_Owner_Money());
        }

        private void اضافةموظفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab("اضافة موظف", new Form_Employee_Add());
        }

        private void قبضموظفينToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab("قبض موظف", new Form_Employee_Payment());
        }

        private void المديوناتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Tab("اجمالى المديونات", new Form_Customer_Debtors());
        }

        private void نسخالمحتوىToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Shared_Backup backup = new Form_Shared_Backup();
            backup.ShowDialog();
        }

        private void استعادةالمحتوىToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_ٍShared_Restore form = new Form_ٍShared_Restore();
            form.ShowDialog();
        }

        private void Add_Tab(string Name, Form form)
        {
            TabPage tp = new TabPage(Name);


            form.TopLevel = false;
            tp.Controls.Add(form);
            form.Dock = DockStyle.Fill;
            form.Show();

            tabControl1.TabPages.Add(tp);
            tabControl1.SelectedIndex = tabControl1.Controls.Count - 1;
        }
    }
}

