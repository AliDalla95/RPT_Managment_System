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
    public partial class Print1 : MetroFramework.Forms.MetroForm
    {
        public Print1()
        {
            InitializeComponent();
        }

        private void user_add_Load(object sender, EventArgs e)
        {

        }

        private void flatButton1_Click(object sender, EventArgs e)
        {

        }

        private void flatButton2_Click(object sender, EventArgs e)
        { 

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            Bills1 b = new Bills1();
            b.Show();
        }
    }
}
