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
    public partial class Login : MetroFramework.Forms.MetroForm
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            metrotxt1.Focus();
        }

        private void metrobtn1_Click(object sender, EventArgs e)
        {
            string sql = "Data Source = Data.db";
            SQLiteConnection conn = new SQLiteConnection(sql);
            conn.Open();
            string query = "SELECT count(*) FROM Login WHERE Login_user = '" + metrotxt1.Text + "' AND Login_pass = '" + metrotxt2.Text + "' ";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);
            int result = Convert.ToInt16(cmd.ExecuteScalar());
            if (result > 0)
            {
                metrobtn1.Focus();
                this.Hide();
                Main_Program mainprogram = new Main_Program();
                mainprogram.Show();
                   

            }
            else
            {
                metrotxt1.Clear();
                metrotxt2.Clear();
                MessageBox.Show("أسم المستخدم أو كلمة المرور خطأ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                metrotxt1.Focus();
            }
            conn.Close();
            
        }

        private void metrobtn2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            metrotxt1.Focus();
        }

        private void Login_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }
    }
}
