using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPT_Manager

{
    public partial class CrystalViewer : MetroFramework.Forms.MetroForm
    {
        public CrystalViewer()
        {
            InitializeComponent();
        }

        private void CrystalViewer_Load(object sender, EventArgs e)
        {

        }

        private void crystalReportViewer1_Load_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            Bills1 cc = new Bills1();
            cc.Show();
        }
    }
}
