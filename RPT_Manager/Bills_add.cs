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
    public partial class Bills_add : MetroFramework.Forms.MetroForm
    {


        void cal()
        {
            if (metroTextBox2.Text != string.Empty && metroTextBox3.Text != string.Empty)
            {
                Int64 cal = Convert.ToInt64(metroTextBox2.Text) * Convert.ToInt64(metroTextBox3.Text);
                metroTextBox4.Text = cal.ToString();
            }
            else
            {
                metroTextBox4.Text = "0";
            }
        }

        void sum()
        {
            Int64 sum = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                sum += Convert.ToInt64(dataGridView1.Rows[i].Cells[5].Value);
            }
            this.metroTextBox8.Text = sum.ToString();
        }


        public Bills_add()
        {
            InitializeComponent();

            DataTable dt = new DataTable();
            string sql = "data source = Data.db";
            SQLiteConnection conn = new SQLiteConnection(sql);
            string query = "select Bills_pharm_name from Bills_Bills";
            SQLiteDataAdapter da = new SQLiteDataAdapter(query, conn);
            da.Fill(dt);
            AutoCompleteStringCollection datasearch = new AutoCompleteStringCollection();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                datasearch.Add(dt.Rows[i][0].ToString());
            }
            this.metroTextBox5.AutoCompleteCustomSource = datasearch;
            this.metroTextBox5.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.metroTextBox5.AutoCompleteMode = AutoCompleteMode.Append;


            DataTable compda = new DataTable();
            conn.Open();
            string q = "select * from Members";
            SQLiteDataAdapter compdd = new SQLiteDataAdapter(q, conn);
            compdd.Fill(compda);
            metroComboBox1.DataSource = compda;
            metroComboBox1.DisplayMember = "Members_name";
            conn.Close();

            DataTable compda1 = new DataTable();
            conn.Open();
            string qq1 = "select * from Pharm";
            SQLiteDataAdapter compdd1 = new SQLiteDataAdapter(qq1, conn);
            compdd1.Fill(compda1);
            metroComboBox2.DataSource = compda1;
            metroComboBox2.DisplayMember = "Pharm_name";
            conn.Close();
            sum();

        }

        private void Bills_add_Load(object sender, EventArgs e)
        {
            metroComboBox1.Focus();
            DataTable dd = new DataTable();
            string sql = "data source = Data.db";
            SQLiteConnection conn = new SQLiteConnection(sql);
            conn.Open();
            string query = "select Bills_id from Bills2";
            SQLiteDataAdapter da1 = new SQLiteDataAdapter(query, conn);
            da1.Fill(dd);
            if (dd.Rows.Count == 0)
            {
                metroTextBox6.Text = Convert.ToString(1);
            }
            else
            {
                int a = dd.Rows.Count;
                Int64 id = Convert.ToInt64(dd.Rows[a - 1][0].ToString());
                metroTextBox6.Text = Convert.ToString(id + 1);
                conn.Close();
            }
            
        }

        private void metroTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void flatButton5_Click(object sender, EventArgs e)
        {
            if (metroTextBox7.Text == string.Empty)
            {
                MessageBox.Show("لم يتم تحديد تاريخ للفاتورة يرجى التحديد وشكرا","خطأ",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                string sql = "data source = Data.db";
                SQLiteConnection conn = new SQLiteConnection(sql);
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    conn.Open();
                    string query_save2 = "insert into Bills (Bills_id,Bills_members_name,Bills_pharm_name,Bills_pharm_ghram,Bills_pharm_price,Bills_pharm_quan,Bills_unit,Bills_count,Bills_date,Bills_sum) values(@Bills_id,@Bills_members_name,@Bills_pharm_name,@Bills_pharm_ghram,@Bills_pharm_price,@Bills_pharm_quan,@Bills_unit,@Bills_count,@Bills_date,@Bills_sum)";
                    SQLiteCommand cmd2 = new SQLiteCommand(query_save2, conn);
                    cmd2.Parameters.AddWithValue("@Bills_id", Convert.ToInt64(metroTextBox6.Text));
                    cmd2.Parameters.AddWithValue("@Bills_members_name", metroComboBox1.Text);
                    cmd2.Parameters.AddWithValue("@Bills_pharm_name", dataGridView1.Rows[i].Cells[0].Value.ToString());
                    cmd2.Parameters.AddWithValue("@Bills_pharm_ghram", dataGridView1.Rows[i].Cells[1].Value.ToString());
                    cmd2.Parameters.AddWithValue("@Bills_pharm_price", Convert.ToInt64(dataGridView1.Rows[i].Cells[4].Value.ToString()));
                    cmd2.Parameters.AddWithValue("@Bills_pharm_quan", Convert.ToInt64(dataGridView1.Rows[i].Cells[2].Value.ToString()));
                    cmd2.Parameters.AddWithValue("@Bills_unit", dataGridView1.Rows[i].Cells[3].Value.ToString());
                    cmd2.Parameters.AddWithValue("@Bills_count", Convert.ToInt64(dataGridView1.Rows[i].Cells[5].Value.ToString()));
                    cmd2.Parameters.AddWithValue("@Bills_date", metroTextBox7.Text);
                    cmd2.Parameters.AddWithValue("@Bills_sum", Convert.ToInt64(metroTextBox8.Text));
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                }


            conn.Open();
            string query_save1 = "insert into Bills2 (Bills_id,Bills_members_name,Bills_date,Bills_sum) values(@Bills_id,@Bills_members_name,@Bills_date,@Bills_sum)";
            SQLiteCommand cmd1 = new SQLiteCommand(query_save1,conn);
            cmd1.Parameters.AddWithValue("@Bills_id",Convert.ToInt64(metroTextBox6.Text));
            cmd1.Parameters.AddWithValue("@Bills_members_name",metroComboBox1.Text);
            cmd1.Parameters.AddWithValue("@Bills_date", metroTextBox7.Text);
            cmd1.Parameters.AddWithValue("@Bills_sum",Convert.ToInt64(metroTextBox8.Text));
            cmd1.ExecuteNonQuery();
            conn.Close();

            DS ds = new DS();
            CrystalReport1 cr = new CrystalReport1();
            CrystalViewer cv = new CrystalViewer();
            conn.Open();
            SQLiteDataAdapter da = new SQLiteDataAdapter("select Bills_id,Bills_members_name,Bills_pharm_name,Bills_pharm_ghram,Bills_pharm_quan,Bills_pharm_price,Bills_count,Bills_Bills_date,Bills_Bills_sum from Bills_Bills where Bills_id=@Bills_id", conn);
            da.SelectCommand.Parameters.AddWithValue("@Bills_id", metroTextBox6.Text);
            da.Fill(ds, "Bills_Bills");
            cr.SetDataSource(ds.Tables["Bills_Bills"]);
            cv.crystalReportViewer1.ReportSource = cr;
            cv.Show();
            }
        }

        private void metroTextBox5_Click(object sender, EventArgs e)
        {

        }

        private void metroTextBox5_TextChanged(object sender, EventArgs e)
        {
            AutoCompleteStringCollection datasearch = new AutoCompleteStringCollection();
            metroTextBox5.AutoCompleteCustomSource = datasearch;
            metroTextBox5.AutoCompleteSource = AutoCompleteSource.CustomSource;
            metroTextBox5.AutoCompleteMode = AutoCompleteMode.Append;

            DataTable dt = new DataTable();
            string sql1 = "data source = Data.db";
            SQLiteConnection conn1 = new SQLiteConnection(sql1);
            conn1.Open();
            string query1 = "select * from Bills_Bills where Bills_pharm_name like '%" + metroTextBox6.Text + "%' and Bills_id = '" + metroTextBox6.Text + "' and Bills_pharm_price = '" + Convert.ToInt64(metroTextBox6.Text) + "' and Bills_pharm_ghram = '" + metroTextBox6.Text + "'";
            SQLiteDataAdapter da = new SQLiteDataAdapter(query1, conn1);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            dataGridView1.Columns[0].HeaderText = "أسم الدواء";
            dataGridView1.Columns[1].HeaderText = "عيار الدواء";
            dataGridView1.Columns[2].HeaderText = "كمية الدواء";
            dataGridView1.Columns[3].HeaderText = "وحدة القياس";
            dataGridView1.Columns[4].HeaderText = "سعر الدواء";
            dataGridView1.Columns[5].HeaderText = "المجموع";
            conn1.Close();

            dataGridView1.Columns[0].Width = 165;
            dataGridView1.Columns[1].Width = 110;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[4].Width = 65;
            dataGridView1.Columns[2].Width = 90;
            dataGridView1.Columns[5].Width = 140;

        }

        private void flatButton6_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void metroTextBox6_Click(object sender, EventArgs e)
        {

        }

        private void flatButton3_Click(object sender, EventArgs e)
        {
            DataTable dd = new DataTable();
            string sql1 = "data source = Data.db";
            SQLiteConnection conn1 = new SQLiteConnection(sql1);
            conn1.Open();
            string query1 = "select Bills_pharm_name,Bills_pharm_ghram,Bills_pharm_price,Bills_pharm_quan,Bills_count from Bills_Bills where Bills_id = '" + metroTextBox6.Text + "'";
            SQLiteDataAdapter da1 = new SQLiteDataAdapter(query1, conn1);
            da1.Fill(dd);
            dataGridView1.DataSource = dd;
            conn1.Close();
            dataGridView1.Columns[0].HeaderText = "أسم الدواء";
            dataGridView1.Columns[1].HeaderText = "عيار الدواء";
            dataGridView1.Columns[2].HeaderText = "كمية الدواء";
            dataGridView1.Columns[3].HeaderText = "وحدة القياس";
            dataGridView1.Columns[4].HeaderText = "سعر الدواء";
            dataGridView1.Columns[5].HeaderText = "المجموع";

            dataGridView1.Columns[0].Width = 165;
            dataGridView1.Columns[1].Width = 110;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[4].Width = 65;
            dataGridView1.Columns[2].Width = 90;
            dataGridView1.Columns[5].Width = 140;
            sum();
        }

        private void flatButton1_Click(object sender, EventArgs e)
        {
            string sql = "data source = Data.db";
            SQLiteConnection conn = new SQLiteConnection(sql);
            if (metroTextBox3.Text == string.Empty | metroTextBox2.Text == string.Empty | metroComboBox1.Text == string.Empty | metroComboBox2.Text == string.Empty | metroComboBox3.Text == string.Empty)
            {
                MessageBox.Show("أحد الحقول المطلوبة فارغ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                if (Convert.ToInt64(metroTextBox3.Text) == 0)
                {
                    MessageBox.Show("لايمكن تحديد كمية 0 في حقل الكمية وشكرا", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    conn.Open();
                    DataTable dt = new DataTable();
                    string query33 = "select Pharm_quan from Pharm where Pharm_name = '" + metroComboBox2.Text + "'";
                    SQLiteDataAdapter da = new SQLiteDataAdapter(query33, conn);
                    da.Fill(dt);
                    conn.Close();
                    if (Convert.ToInt64(dt.Rows[0][0].ToString()) < Convert.ToInt64(metroTextBox3.Text))
                    {
                        MessageBox.Show("لاتوجد كمية كافية من هذه المادة\nان الكمية الموجودة بالمستودع هي" + (dt.Rows[0][0].ToString()) + " ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    else
                    {
                        conn.Open();
                        string query = "insert into Bills_Bills (Bills_id,Bills_members_name,Bills_pharm_name,Bills_pharm_ghram,Bills_pharm_price,Bills_pharm_quan,Bills_Bills_unit,Bills_count) values('" + Convert.ToInt32(metroTextBox6.Text) + "','" + metroComboBox1.Text + "','" + metroComboBox2.Text + "','" + metroTextBox1.Text + "','" + Convert.ToInt64(metroTextBox2.Text) + "','" + Convert.ToInt64(metroTextBox3.Text) + "','" + metroComboBox3.Text + "','" + Convert.ToInt64(metroTextBox4.Text) + "') ";
                        SQLiteCommand cmd = new SQLiteCommand(query, conn);
                        cmd.ExecuteNonQuery();



                        string query2 = "update Pharm set Pharm_quan=Pharm_quan-'" + Convert.ToInt64(metroTextBox3.Text) + "' where Pharm_name = '" + metroComboBox2.Text + "'";
                        SQLiteCommand cmd2 = new SQLiteCommand(query2, conn);
                        cmd2.ExecuteNonQuery();
                        conn.Close();
                        metroTextBox1.Clear();
                        metroTextBox2.Clear();
                        metroTextBox3.Clear();
                        metroTextBox4.Clear();
                        conn.Close();
                    }


                }

                DataTable dd = new DataTable();
                string sql1 = "data source = Data.db";
                SQLiteConnection conn1 = new SQLiteConnection(sql1);
                conn1.Open();
                string query1 = "select Bills_pharm_name,Bills_pharm_ghram,Bills_pharm_quan,Bills_Bills_unit,Bills_pharm_price,Bills_count from Bills_Bills where Bills_id = '" + Convert.ToInt32(metroTextBox6.Text) + "'";
                SQLiteDataAdapter da1 = new SQLiteDataAdapter(query1, conn1);
                da1.Fill(dd);
                dataGridView1.DataSource = dd;
                conn1.Close();
                


                dataGridView1.Columns[0].Width = 230;
                dataGridView1.Columns[1].Width = 150;
                dataGridView1.Columns[2].Width = 103;
                dataGridView1.Columns[3].Width = 120;
                dataGridView1.Columns[4].Width = 100;
                dataGridView1.Columns[5].Width = 150;
                for (int hh = 0; hh < dataGridView1.Rows.Count; hh++)
                {
                    dataGridView1.Rows[hh].Height = 40;
                }

                dataGridView1.Columns[0].HeaderText = "أسم الدواء";
                dataGridView1.Columns[1].HeaderText = "عيار الدواء";
                dataGridView1.Columns[2].HeaderText = "كمية الدواء";
                dataGridView1.Columns[3].HeaderText = "وحدة القياس";
                dataGridView1.Columns[4].HeaderText = "سعر الدواء";
                dataGridView1.Columns[5].HeaderText = "المجموع";

                sum();
            }
            
        }

        private void metroTextBox2_TextChanged(object sender, EventArgs e)
        {
            cal();
        }

        private void metroTextBox3_TextChanged(object sender, EventArgs e)
        {
            cal();
        }

        private void metroTextBox3_KeyUp(object sender, KeyEventArgs e)
        {
            cal();
        }

        private void metroTextBox2_Click(object sender, EventArgs e)
        {

        }

        private void metroTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            cal();
        }

        private void metroTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            cal();
        }

        private void metroTextBox3_KeyDown(object sender, KeyEventArgs e)
        {
            cal();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void flatButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("لم يتم تحديد أي عنصر", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string sql = "data source = Data.db";
                SQLiteConnection conn = new SQLiteConnection(sql);
                conn.Open();
                string query_del = "delete from Bills_Bills where Bills_pharm_name = '" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "' and Bills_pharm_ghram = '" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "' and Bills_pharm_quan = '" + Convert.ToInt64(dataGridView1.CurrentRow.Cells[2].Value.ToString()) + "' and Bills_pharm_price = '" + Convert.ToInt64(dataGridView1.CurrentRow.Cells[4].Value.ToString()) + "' and Bills_count = '" + Convert.ToInt64(dataGridView1.CurrentRow.Cells[5].Value.ToString()) + "' ";
                SQLiteCommand cmd = new SQLiteCommand(query_del, conn);
                cmd.ExecuteNonQuery();
                string query_insert = "update Pharm set Pharm_quan=Pharm_quan+@Pharm_quan where Pharm_name='"+metroComboBox2.Text+"'";
                SQLiteCommand cc = new SQLiteCommand(query_insert, conn);
                cc.Parameters.AddWithValue("@Pharm_quan",dataGridView1.CurrentRow.Cells[2].Value);
                cc.ExecuteNonQuery();

                DataTable dd = new DataTable();
                string query1 = "select Bills_pharm_name,Bills_pharm_ghram,Bills_pharm_quan,Bills_Bills_unit,Bills_pharm_price,Bills_count from Bills_Bills where Bills_id = '" + Convert.ToInt32(metroTextBox6.Text) + "'";
                SQLiteDataAdapter da1 = new SQLiteDataAdapter(query1, conn);
                da1.Fill(dd);
                dataGridView1.DataSource = dd;
                conn.Close();
                

                dataGridView1.Columns[0].Width = 230;
                dataGridView1.Columns[1].Width = 150;
                dataGridView1.Columns[2].Width = 103;
                dataGridView1.Columns[3].Width = 120;
                dataGridView1.Columns[4].Width = 100;
                dataGridView1.Columns[5].Width = 150;
                for (int hh = 0; hh < dataGridView1.Rows.Count; hh++)
                {
                    dataGridView1.Rows[hh].Height = 40;
                }
                dataGridView1.Columns[0].HeaderText = "أسم الدواء";
                dataGridView1.Columns[1].HeaderText = "عيار الدواء";
                dataGridView1.Columns[2].HeaderText = "كمية الدواء";
                dataGridView1.Columns[3].HeaderText = "وحدة القياس";
                dataGridView1.Columns[4].HeaderText = "سعر الدواء";
                dataGridView1.Columns[5].HeaderText = "المجموع";

                sum();
            }
        }

        private void metroTextBox8_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            sum();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string sql = "data source = Data.db";
            SQLiteConnection conn = new SQLiteConnection(sql);
            conn.Open();
            string query_del = "delete from Bills_Bills";
            SQLiteCommand cmd = new SQLiteCommand(query_del, conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            this.Close();
            Bills1 b = new Bills1();
            b.Show();
        }

        private void flatButton4_Click(object sender, EventArgs e)
        {
            if (metroTextBox7.Text == string.Empty)
            {
                MessageBox.Show("لم يتم تحديد تاريخ للفاتورة يرجى التحديد وشكرا", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string sql = "data source = Data.db";
                SQLiteConnection conn = new SQLiteConnection(sql);
                conn.Open();
                string query_save1 = "insert into Bills2 (Bills_id,Bills_members_name,Bills_date,Bills_sum) values(@Bills_id,@Bills_members_name,@Bills_date,@Bills_sum)";
                SQLiteCommand cmd1 = new SQLiteCommand(query_save1, conn);
                cmd1.Parameters.AddWithValue("@Bills_id", Convert.ToInt64(metroTextBox6.Text));
                cmd1.Parameters.AddWithValue("@Bills_members_name", metroComboBox1.Text);
                cmd1.Parameters.AddWithValue("@Bills_date", metroTextBox7.Text);
                cmd1.Parameters.AddWithValue("@Bills_sum", Convert.ToInt64(metroTextBox8.Text));
                cmd1.ExecuteNonQuery();
                conn.Close();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    conn.Open();
                    string query_save2 = "insert into Bills (Bills_id,Bills_members_name,Bills_pharm_name,Bills_pharm_ghram,Bills_pharm_quan,Bills_unit,Bills_pharm_price,Bills_count,Bills_date,Bills_sum) values(@Bills_id,@Bills_members_name,@Bills_pharm_name,@Bills_pharm_ghram,@Bills_pharm_quan,@Bills_unit,@Bills_pharm_price,@Bills_count,@Bills_date,@Bills_sum)";
                    SQLiteCommand cmd2 = new SQLiteCommand(query_save2, conn);
                    cmd2.Parameters.AddWithValue("@Bills_id", Convert.ToInt64(metroTextBox6.Text));
                    cmd2.Parameters.AddWithValue("@Bills_members_name", metroComboBox1.Text);
                    cmd2.Parameters.AddWithValue("@Bills_pharm_name", dataGridView1.Rows[i].Cells[0].Value.ToString());
                    cmd2.Parameters.AddWithValue("@Bills_pharm_ghram", dataGridView1.Rows[i].Cells[1].Value.ToString());
                    cmd2.Parameters.AddWithValue("@Bills_pharm_quan", Convert.ToInt64(dataGridView1.Rows[i].Cells[2].Value.ToString()));
                    cmd2.Parameters.AddWithValue("@Bills_unit", dataGridView1.Rows[i].Cells[3].Value.ToString());
                    cmd2.Parameters.AddWithValue("@Bills_pharm_price", Convert.ToInt64(dataGridView1.Rows[i].Cells[4].Value));
                    cmd2.Parameters.AddWithValue("@Bills_count", Convert.ToInt64(dataGridView1.Rows[i].Cells[5].Value.ToString()));
                    cmd2.Parameters.AddWithValue("@Bills_date", metroTextBox7.Text);
                    cmd2.Parameters.AddWithValue("@Bills_sum", Convert.ToInt64(metroTextBox8.Text));
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                }
                MessageBox.Show("/تم الحفظ دون الطباعة/اذا اردت الطباعة راجع الفواتير وشكرا", "حفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                Bills1 b = new Bills1();
                b.Show();
            }
        }

        private void metroTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassNumbersOnly ccc = new ClassNumbersOnly();
            ccc.UserNumbersOnly(e);
        }

        private void metroTextBox3_Click(object sender, EventArgs e)
        {

        }

        private void metroTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassNumbersOnly ccc = new ClassNumbersOnly();
            ccc.UserNumbersOnly(e);
        }

        private void metroComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void flatLabel7_Click(object sender, EventArgs e)
        {

        }
    }
}
