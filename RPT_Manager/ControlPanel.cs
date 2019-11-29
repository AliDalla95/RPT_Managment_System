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
    public partial class ControlPanel : MetroFramework.Forms.MetroForm
    {
        public ControlPanel()
        {
            InitializeComponent();


            SQLiteConnection conn = new SQLiteConnection(" Data Source = Data.db ");
            conn.Open();
            SQLiteDataAdapter ad = new SQLiteDataAdapter("select * from Login", conn);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            metroComboBox1.DataSource = dt;
            metroComboBox1.DisplayMember = "Login_user";
            conn.Close();

        }

        private void ControlPanel_Load(object sender, EventArgs e)
        {
            SQLiteConnection conn = new SQLiteConnection(" Data Source = Data.db ");
            conn.Open();
            SQLiteDataAdapter ad = new SQLiteDataAdapter("select * from Login", conn);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            metroComboBox1.DataSource = dt;
            metroComboBox1.DisplayMember = "Login_user";
            conn.Close();
        }

        private void flatButton1_Click(object sender, EventArgs e)
        {
            SQLiteConnection conn = new SQLiteConnection(" Data Source = Data.db ");
            conn.Open();
            if (metroTextBox2.Text != string.Empty && metroTextBox3.Text != string.Empty)
            {
                if (metroTextBox2.Text == metroTextBox3.Text)
                {
                    SQLiteDataAdapter ad = new SQLiteDataAdapter("update Login set Login_pass = '" + metroTextBox2.Text + "' where  Login_user = '" + metroComboBox1.Text + "' ", conn);
                    ad.SelectCommand.ExecuteNonQuery();
                    MessageBox.Show(" تم  ", "التغييرات", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("عدم تطابق كلمة السر يرجى اعادة المحاولة", "خطأ", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                }
                conn.Close();
                metroTextBox2.Clear();
                metroTextBox3.Clear();
            }
            else 
            {
                MessageBox.Show(" يرجى ادخال كلمة السر وتأكيدها  ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void flatButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void flatButton3_Click(object sender, EventArgs e)
        {
            SQLiteConnection conn = new SQLiteConnection(" Data Source = Data.db ");
            conn.Open();
            if (metroTextBox5.Text != string.Empty && metroTextBox6.Text != string.Empty && metroTextBox7.Text != string.Empty)
            {
                if (metroTextBox6.Text == metroTextBox7.Text)
                {
                    string query = "insert into Login (Login_user,Login_pass) values (@Login_user,@Login_pass)";
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Login_user", metroTextBox5.Text);
                    cmd.Parameters.AddWithValue("@Login_pass", metroTextBox6.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(" تم  ", "اضافة مستخدم جديد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("عدم تطابق كلمة السر يرجى اعادة المحاولة", "خطأ", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                }
                conn.Close();
                metroTextBox5.Clear();
                metroTextBox6.Clear();
                metroTextBox7.Clear();

                SQLiteDataAdapter ad = new SQLiteDataAdapter("select * from Login", conn);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                metroComboBox1.DataSource = dt;
                metroComboBox1.DisplayMember = "Login_user";
                conn.Close();
            }
            else
            {
                MessageBox.Show(" يرجى ادخال كلمة السر وتأكيدها  ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ControlPanel_MouseDown(object sender, MouseEventArgs e)
        {
          
        }

        private void ControlPanel_MouseMove(object sender, MouseEventArgs e)
        {
           
        }

        private void flatButton4_Click(object sender, EventArgs e)
        {
            SQLiteConnection conn = new SQLiteConnection(" Data Source = Data.db ");
            conn.Open();
            string query = "delete from Login where Login_user=@Login_user ";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@Login_user", metroComboBox1.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show(" تم الحذف  ", "حذف المستخدم المحدد", MessageBoxButtons.OK, MessageBoxIcon.Information);

            conn.Open();
            SQLiteDataAdapter ad = new SQLiteDataAdapter("select * from Login", conn);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            metroComboBox1.DataSource = dt;
            metroComboBox1.DisplayMember = "Login_user";
            conn.Close();
        }
    }
}
