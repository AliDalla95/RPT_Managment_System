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
using System.Data.SqlClient;

namespace RPT_Manager
{
    public partial class Bills1 : MetroFramework.Forms.MetroForm
    {


        public Bills1()
        {
            InitializeComponent();

            DataTable dd = new DataTable();
            string sql = "data source = Data.db";
            SQLiteConnection conn = new SQLiteConnection(sql);
            conn.Open();
            string query = "select Bills_members_name from Bills2";
            SQLiteDataAdapter daa = new SQLiteDataAdapter(query, conn);
            daa.Fill(dd);
            AutoCompleteStringCollection datasearch = new AutoCompleteStringCollection();

            for (int i = 0; i < dd.Rows.Count; i++)
            {
                datasearch.Add(dd.Rows[i][0].ToString());
            }
            this.metroTextBox1.AutoCompleteCustomSource = datasearch;
            this.metroTextBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.metroTextBox1.AutoCompleteMode = AutoCompleteMode.Append;
            conn.Clone();


            DataTable dt = new DataTable();
            string sql1 = "data source = Data.db";
            SQLiteConnection conn1 = new SQLiteConnection(sql1);
            conn1.Open();
            string query1 = "select Bills_id,Bills_members_name,Bills_date,Bills_sum from Bills2";
            SQLiteDataAdapter da = new SQLiteDataAdapter(query1, conn1);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            dataGridView1.Columns[0].HeaderText = "رقم الفاتورة";
            dataGridView1.Columns[1].HeaderText = "أسم الصيدلاني";
            dataGridView1.Columns[2].HeaderText = "تاريخ الفاتورة";
            dataGridView1.Columns[3].HeaderText = "مبلغ الفاتورة";
            conn1.Close();
            
        }

        private void Bills1_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string sql = "data source = Data.db";
            SQLiteConnection conn = new SQLiteConnection(sql);
            conn.Open();
            string query = "select Bills_id,Bills_members_name,Bills_date,Bills_sum from Bills2";
            SQLiteDataAdapter da = new SQLiteDataAdapter(query, conn);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();

            dataGridView1.Columns[0].Width = 95;
            dataGridView1.Columns[1].Width = 296;
            dataGridView1.Columns[2].Width = 296;
            dataGridView1.Columns[3].Width = 196;
            for (int hh = 0; hh < dataGridView1.Rows.Count; hh++)
            {
                dataGridView1.Rows[hh].Height = 40;
            }

            dataGridView1.Columns[0].HeaderText = "رقم الفاتورة";
            dataGridView1.Columns[1].HeaderText = "أسم الصيدلاني";
            dataGridView1.Columns[2].HeaderText = "تاريخ الفاتورة";
            dataGridView1.Columns[3].HeaderText = "مبلغ الفاتورة";

        }

        private void رجوعToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Main_Program ma = new Main_Program();
            ma.Show();
        }

        private void metroTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void flatButton2_Click(object sender, EventArgs e)
        {
            string sql = "data source = Data.db";
            SQLiteConnection conn = new SQLiteConnection(sql);
            conn.Open();
            string query_del = "delete from Bills_Bills";
            SQLiteCommand cmd = new SQLiteCommand(query_del, conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            Bills_add ad = new Bills_add();
            ad.Show();
            this.Close();
        }

        private void flatButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("لم يتم تحديد أي فاتورة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
            Bills_update blu = new Bills_update();
            blu.metroTextBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            blu.metroTextBox6.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            blu.metroTextBox5.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            blu.metroTextBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            blu.Show();
            this.Close();
            }
        }

        private void flatButton1_Click(object sender, EventArgs e)
        {

        }

        private void flatButton4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("لم يتم تحديد أي فاتورة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string sql = "data source = Data.db";
                SQLiteConnection con = new SQLiteConnection(sql);
                con.Open();
                string query = "delete from Bills2 where Bills_id = '" + dataGridView1.CurrentRow.Cells[0].Value + "' ";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("تم الحذف ", "معلومات الحذف", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DataTable dt = new DataTable();
                con.Open();
                string query1 = "select Bills_id,Bills_members_name,Bills_date,Bills_sum from Bills2";
                SQLiteDataAdapter da = new SQLiteDataAdapter(query1, con);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();

                dataGridView1.Columns[0].Width = 95;
                dataGridView1.Columns[1].Width = 296;
                dataGridView1.Columns[2].Width = 296;
                dataGridView1.Columns[3].Width = 196;
                for (int hh = 0; hh < dataGridView1.Rows.Count; hh++)
                {
                    dataGridView1.Rows[hh].Height = 40;
                }

                dataGridView1.Columns[0].HeaderText = "رقم الفاتورة";
                dataGridView1.Columns[1].HeaderText = "أسم الصيدلاني";
                dataGridView1.Columns[2].HeaderText = "تاريخ الفاتورة";
                dataGridView1.Columns[3].HeaderText = "مبلغ الفاتورة";

                dataGridView1.Focus();

            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_ColumnStateChanged(object sender, DataGridViewColumnStateChangedEventArgs e)
        {
           
        }

        private void dataGridView1_ColumnDisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
        {
          
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void metroTextBox1_TextChanged(object sender, EventArgs e)
        {
            AutoCompleteStringCollection datasearch = new AutoCompleteStringCollection();
            metroTextBox1.AutoCompleteCustomSource = datasearch;
            metroTextBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            metroTextBox1.AutoCompleteMode = AutoCompleteMode.Append;

            DataTable dt = new DataTable();
            string sql1 = "data source = Data.db";
            SQLiteConnection conn1 = new SQLiteConnection(sql1);
            conn1.Open();
            string query1 = "select Bills_id,Bills_members_name,Bills_date,Bills_sum from Bills2 where Bills_members_name like '%" + metroTextBox1.Text + "%' or Bills_date like '%" + metroTextBox1.Text + "%' or Bills_date_update like '%" + metroTextBox1.Text + "%' or Bills_sum like '%" + metroTextBox1.Text + "%' ";
            SQLiteDataAdapter da = new SQLiteDataAdapter(query1, conn1);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn1.Close();

            dataGridView1.Columns[0].Width = 95;
            dataGridView1.Columns[1].Width = 296;
            dataGridView1.Columns[2].Width = 296;
            dataGridView1.Columns[3].Width = 196;
            for (int hh = 0; hh < dataGridView1.Rows.Count; hh++)
            {
                dataGridView1.Rows[hh].Height = 40;
            }

            dataGridView1.Columns[0].HeaderText = "رقم الفاتورة";
            dataGridView1.Columns[1].HeaderText = "أسم الصيدلاني";
            dataGridView1.Columns[2].HeaderText = "تاريخ الفاتورة";
            dataGridView1.Columns[3].HeaderText = "مبلغ الفاتورة";

            
        }

        private void flatButton6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("لم يتم تحديد أي فاتورة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string sql = "data source = Data.db";
                SQLiteConnection con = new SQLiteConnection(sql);
                con.Open();
                string query = "delete from Bills2";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("تم حذف الكل ", "معلومات الحذف", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DataTable dt = new DataTable();
                con.Open();
                string query1 = "select Bills_id,Bills_members_name,Bills_date,Bills_sum from Bills2";
                SQLiteDataAdapter da = new SQLiteDataAdapter(query1, con);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();

                dataGridView1.Columns[0].Width = 95;
                dataGridView1.Columns[1].Width = 296;
                dataGridView1.Columns[2].Width = 296;
                dataGridView1.Columns[3].Width = 196;
                for (int hh = 0; hh < dataGridView1.Rows.Count; hh++)
                {
                    dataGridView1.Rows[hh].Height = 40;
                }

                dataGridView1.Columns[0].HeaderText = "رقم الفاتورة";
                dataGridView1.Columns[1].HeaderText = "أسم الصيدلاني";
                dataGridView1.Columns[2].HeaderText = "تاريخ الفاتورة";
                dataGridView1.Columns[3].HeaderText = "مبلغ الفاتورة";

                dataGridView1.Focus();
            }
            
        }

        private void flatButton5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("لم يتم تحديد أي فاتورة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string sql = "data source = Data.db";
                SQLiteConnection con = new SQLiteConnection(sql);
                DS2 ds = new DS2();
                CrystalReport3 cr3 = new CrystalReport3();
                Print1 p1 = new Print1();
                con.Open();
                SQLiteDataAdapter da = new SQLiteDataAdapter("select Bills_id,Bills_members_name,Bills_date,Bills_date_update,Bills_sum from Bills2", con);
                da.Fill(ds, "Bills2");
                cr3.SetDataSource(ds.Tables["Bills2"]);
                p1.crystalReportViewer1.ReportSource = cr3;
                p1.Show();
                this.Close();
            }
        }

        private void flatButton7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("لم يتم تحديد أي فاتورة", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string sql = "data source = Data.db";
                SQLiteConnection con = new SQLiteConnection(sql);
                DS ds = new DS();
                CrystalReport2 cr2 = new CrystalReport2();
                Print p = new Print();
                con.Open();
                SQLiteDataAdapter da = new SQLiteDataAdapter("select Bills_id,Bills_members_name,Bills_pharm_name,Bills_pharm_ghram,Bills_pharm_quan,Bills_pharm_price,Bills_count,Bills_unit from Bills where Bills_id=@Bills_id", con);
                da.SelectCommand.Parameters.AddWithValue("@Bills_id", Convert.ToInt64(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                da.Fill(ds, "Bills");
                cr2.SetDataSource(ds.Tables["Bills"]);
                p.crystalReportViewer1.ReportSource = cr2;
                p.Show();
                this.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            Main_Program ma = new Main_Program();
            ma.Show();
        }
    }
}
