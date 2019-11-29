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
    public partial class Pharm_add : MetroFramework.Forms.MetroForm
    {
        public Pharm_add()
        {
            InitializeComponent();



            string sql = "data source = Data.db";
            SQLiteConnection conn = new SQLiteConnection(sql);
            DataTable compda1 = new DataTable();
            conn.Open();
            string qq1 = "select * from Pharm";
            SQLiteDataAdapter compdd1 = new SQLiteDataAdapter(qq1, conn);
            compdd1.Fill(compda1);
            metroComboBox1.DataSource = compda1;
            metroComboBox1.DisplayMember = "Pharm_name";
            DataTable dt = new DataTable();
            if (metroComboBox1.Text == string.Empty)
            {
                metroTextBox2.Text = " ";
            }
            else
            {
                string sql11 = "data source = Data.db";
                SQLiteConnection conn1 = new SQLiteConnection(sql11);
                conn1.Open();
                string q = "select Pharm_unit from Pharm where Pharm_name= '" + metroComboBox1.Text + "'";
                SQLiteDataAdapter da = new SQLiteDataAdapter(q, conn);
                da.Fill(dt);
                metroTextBox2.Text = dt.Rows[0][0].ToString();
                conn1.Close();
            }

        }

        private void Pharm_add_Load(object sender, EventArgs e)
        {
            if (metroComboBox1.Text == string.Empty)
            {
                metroTextBox2.Text = " ";
            }
            else
            {
                string sql = "data source = Data.db";
                SQLiteConnection conn = new SQLiteConnection(sql);
                DataTable dt = new DataTable();
                conn.Open();
                string q = "select Pharm_quan from Pharm where Pharm_name= '" + metroComboBox1.Text + "'";
                SQLiteDataAdapter da = new SQLiteDataAdapter(q, conn);
                da.Fill(dt);
                metroLabel1.Text = dt.Rows[0][0].ToString(); ;
                conn.Close();
            }
        }

        private void flatButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void flatButton1_Click(object sender, EventArgs e)
        {
            if (metroTextBox1.Text == string.Empty)
            {
                MessageBox.Show("يرجى تحديد الكمية المراد اضافتها", "معلومات الاضافة", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                string sql2 = " data source = Data.db";
                SQLiteConnection conn = new SQLiteConnection(sql2);
                conn.Open();
                string query2 = "update Pharm set Pharm_quan = Pharm_quan+'"+Convert.ToInt64(metroTextBox1.Text)+"' where Pharm_name = '"+metroComboBox1.Text+"'";
                SQLiteCommand cmd2 = new SQLiteCommand(query2, conn);
                cmd2.Parameters.AddWithValue("@Pharm_name", metroComboBox1.Text);
                cmd2.Parameters.AddWithValue("@Pharm_quan", Convert.ToInt64(metroTextBox1.Text));
                cmd2.ExecuteNonQuery();
                metroTextBox1.Clear();
                MessageBox.Show("تمت الاضافة", "معلومات الاضافة", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conn.Close();
            }
            string sql = "data source = Data.db";
            SQLiteConnection con = new SQLiteConnection(sql);
            DataTable dt = new DataTable();
            con.Open();
            string q = "select Pharm_quan from Pharm where Pharm_name= '" + metroComboBox1.Text + "'";
            SQLiteDataAdapter da = new SQLiteDataAdapter(q, con);
            da.Fill(dt);
            metroLabel1.Text = dt.Rows[0][0].ToString(); ;
            con.Close();
        }

        private void metroTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassNumbersOnly cc = new ClassNumbersOnly();
            cc.UserNumbersOnly(e);
        }

        private void metroComboBox1_TextChanged(object sender, EventArgs e)
        {
            string sql = "data source = Data.db";
            SQLiteConnection conn = new SQLiteConnection(sql);
            DataTable dt = new DataTable();
            conn.Open();
            string q = "select Pharm_unit from Pharm where Pharm_name= '" + metroComboBox1.Text + "'";
            SQLiteDataAdapter da = new SQLiteDataAdapter(q, conn);
            da.Fill(dt);
            metroTextBox2.Text = dt.Rows[0][0].ToString();
            DataTable dt1 = new DataTable();
            string q1 = "select Pharm_quan from Pharm where Pharm_name= '" + metroComboBox1.Text + "'";
            SQLiteDataAdapter da1 = new SQLiteDataAdapter(q1, conn);
            da1.Fill(dt1);
            metroLabel1.Text = dt1.Rows[0][0].ToString(); ;
            conn.Close();
        }

        private void flatButton3_Click(object sender, EventArgs e)
        {
            if (metroTextBox1.Text == string.Empty)
            {
                MessageBox.Show("يرجى تحديد الكمية المراد حذفها", "معلومات الحذف", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                if (Convert.ToInt64(metroLabel1.Text) >= Convert.ToInt64(metroTextBox1.Text))
                {
                    string sql2 = " data source = Data.db";
                    SQLiteConnection conn = new SQLiteConnection(sql2);
                    conn.Open();
                    string query2 = "update Pharm set Pharm_quan = Pharm_quan-'" + Convert.ToInt64(metroTextBox1.Text) + "' where Pharm_name = '" + metroComboBox1.Text + "'";
                    SQLiteCommand cmd2 = new SQLiteCommand(query2, conn);
                    cmd2.Parameters.AddWithValue("@Pharm_name", metroComboBox1.Text);
                    cmd2.Parameters.AddWithValue("@Pharm_quan", Convert.ToInt64(metroTextBox1.Text));
                    cmd2.ExecuteNonQuery();
                    metroTextBox1.Clear();
                    MessageBox.Show("تم الحذف", "معلومات الحذف", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                }
                else
                {
                    MessageBox.Show("لا يمكن الحذف لان الكمية المراد حذفها أكبر من الكمية الموجودة", "معلومات الحذف", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            string sql = "data source = Data.db";
            SQLiteConnection con = new SQLiteConnection(sql);
            DataTable dt = new DataTable();
            con.Open();
            string q = "select Pharm_quan from Pharm where Pharm_name= '" + metroComboBox1.Text + "'";
            SQLiteDataAdapter da = new SQLiteDataAdapter(q, con);
            da.Fill(dt);
            metroLabel1.Text = dt.Rows[0][0].ToString(); ;
            con.Close();
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void metroTextBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
