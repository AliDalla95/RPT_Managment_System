using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace RPT_Manager
{
    public partial class Main_Program : MetroFramework.Forms.MetroForm
    {
        public Main_Program()
        {
            InitializeComponent();
        }

        private void Main_Program_Load(object sender, EventArgs e)
        {

        }

        private void الموادToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void الصيادلةToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ـنشاءنسخةاحتياطيةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql = "Data Source = Data.db";
            SQLiteConnection conn = new SQLiteConnection(sql);
            conn.Open();
            string baack = @"Data source = D:\Data.db ";
            SQLiteConnection bacck = new SQLiteConnection(baack);
            bacck.Open();
            conn.BackupDatabase(bacck, "main", "main", -1, null, 0);
            bacck.Close();
            conn.Close();
            MessageBox.Show("تم الأمر", "معلومات", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        private void تسجيلالخروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ControlPanel frm5 = new ControlPanel();
            frm5.ShowDialog();
        }

        private void تسجيلالخروجToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
            Login log = new Login();
            log.Show();
        }

        private void الفواتيرToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void حولToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutProgram frm6 = new AboutProgram();
            frm6.ShowDialog();
        }

        private void المستودعToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void الجردToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string sql = " data source = Data.db";
            SQLiteConnection conn = new SQLiteConnection(sql);
            conn.Open();
            string query = "delete from Pharm";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            cmd.ExecuteNonQuery();
            string query1 = "delete from Members";
            SQLiteCommand cmd1 = new SQLiteCommand(query1, conn);
            cmd1.ExecuteNonQuery();
            string query2 = "delete from Bills2";
            SQLiteCommand cmd2 = new SQLiteCommand(query2, conn);
            cmd2.ExecuteNonQuery();
            string query3 = "delete from Bills";
            SQLiteCommand cmd3 = new SQLiteCommand(query3, conn);
            cmd3.ExecuteNonQuery();
            MessageBox.Show("تم الحذف", "حذف البيانات", MessageBoxButtons.OK, MessageBoxIcon.Information);
            conn.Close();
        }

        private void ادخالكميةجديدةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pharm up = new Pharm();
            up.ShowDialog();
        }

        private void crToolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void crToolStripTextBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void قائمةالموادToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pharm_add add = new Pharm_add();
            add.ShowDialog();
        }

        private void قائمةالصيادلةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Members frm3 = new Members();
            frm3.ShowDialog();
        }

        private void قائمةالفواتيرToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Bills1 bl = new Bills1();
            bl.Show();
        }

        private void معلوماتالمستودعToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Repository frm4 = new Repository();
            frm4.ShowDialog();
        }

        private void حذفجميعالموادToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql = " data source = Data.db";
            SQLiteConnection conn = new SQLiteConnection(sql);
            conn.Open();
            string query = "delete from Pharm";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("تم الحذف", "حذف جميع المواد", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void حذفجميعالصيادلةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql = " data source = Data.db";
            SQLiteConnection conn = new SQLiteConnection(sql);
            conn.Open();
            string query = "delete from Members";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("تم الحذف", "حذف جميع الصيادلة", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void حذفجميعالفواتيرToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql = " data source = Data.db";
            SQLiteConnection conn = new SQLiteConnection(sql);
            conn.Open();
            string query2 = "delete from Bills2";
            SQLiteCommand cmd2 = new SQLiteCommand(query2, conn);
            cmd2.ExecuteNonQuery();
            string query3 = "delete from Bills";
            SQLiteCommand cmd3 = new SQLiteCommand(query3, conn);
            cmd3.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("تم الحذف", "حذف جميع الفواتير", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
