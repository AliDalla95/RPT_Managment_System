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
    public partial class Bills_update : MetroFramework.Forms.MetroForm
    {
        void cal1()
        {
            if (metroTextBox10.Text != string.Empty && metroTextBox9.Text != string.Empty)
            {
                Int64 cal11 = Convert.ToInt64(metroTextBox10.Text) * Convert.ToInt64(metroTextBox9.Text);
                metroTextBox8.Text = cal11.ToString();
            }
            else
            {
                metroTextBox4.Text = "0";
            }
        }

        void cal()
        {
            Int64 q = Convert.ToInt64(dataGridView1.CurrentRow.Cells[3].Value.ToString()) * Convert.ToInt64(dataGridView1.CurrentRow.Cells[5].Value.ToString());
            dataGridView1.CurrentRow.Cells[4].Value = q;
        }

        void sum()
        {
            Int64 sum = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                sum += Convert.ToInt64(dataGridView1.Rows[i].Cells[6].Value.ToString());
            }
            this.metroTextBox4.Text = sum.ToString();
        }



        public Bills_update()
        {
            InitializeComponent();

            string sql1sql = "data source = Data.db";
            SQLiteConnection conn = new SQLiteConnection(sql1sql);
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

        private void Bills_update_Load(object sender, EventArgs e)
        {
            DataTable dd = new DataTable();
            string sql1 = "data source = Data.db";
            SQLiteConnection conn1 = new SQLiteConnection(sql1);
            conn1.Open();
            string query1 = "select Bills_Bills_id,Bills_pharm_name,Bills_pharm_ghram,Bills_pharm_quan,Bills_unit,Bills_pharm_price,Bills_count from Bills where Bills_id = '" + Convert.ToInt64(metroTextBox1.Text) + "'";
            SQLiteDataAdapter da1 = new SQLiteDataAdapter(query1, conn1);
            da1.Fill(dd);
            dataGridView1.DataSource = dd;
            conn1.Close();
            if (dataGridView1.CurrentRow == null)
            {
                metroTextBox11.Text = "";
                metroTextBox9.Text = "0";
                metroComboBox3.Text = "";
                metroTextBox10.Text = "0";
                metroTextBox8.Text = "0";
            }
            else
            {
                metroTextBox11.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                metroTextBox9.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                metroComboBox3.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                metroTextBox10.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                metroTextBox8.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            }
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "أسم الدواء";
            dataGridView1.Columns[2].HeaderText = "عيار الدواء";
            dataGridView1.Columns[3].HeaderText = "كمية الدواء";
            dataGridView1.Columns[4].HeaderText = "وحدة القياس";
            dataGridView1.Columns[5].HeaderText = "سعر الدواء";
            dataGridView1.Columns[6].HeaderText = "المجموع";

            dataGridView1.Columns[0].Width = 90;
            dataGridView1.Columns[1].Width = 220;
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[4].Width = 90;
            dataGridView1.Columns[5].Width = 149;
            dataGridView1.Columns[6].Width = 153;
            for (int hh = 0; hh < dataGridView1.Rows.Count; hh++)
            {
                dataGridView1.Rows[hh].Height = 40;
            }

            sum();
        }

        private void flatButton1_Click(object sender, EventArgs e)
        {
            this.Close();
            Bills1 b = new Bills1();
            b.Show();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            sum();
        }

        private void flatButton5_Click(object sender, EventArgs e)
        {
            if (metroTextBox4.Text == "0")
            {
                MessageBox.Show("لايمكن الحفظ لأن مبلغ الفاتورة= 0 /n هناك تغييرات لم تقم بحفظها يرجى التحديث ثم معاودة الحفظ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (metroTextBox3.Text == string.Empty)
                {
                    MessageBox.Show("يرجى أدخال تاريخ التعديل وشكرا", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {


                    string sql1 = "data source = Data.db";
                    SQLiteConnection conn1 = new SQLiteConnection(sql1);
                    conn1.Open();
                    string query_save1 = "update Bills2 set Bills_date=@Bills_date,Bills_date_update=@Bills_date_update,Bills_sum=@Bills_sum where Bills_id = '" + Convert.ToInt64(metroTextBox1.Text) + "'";
                    SQLiteCommand cmd1 = new SQLiteCommand(query_save1, conn1);
                    cmd1.Parameters.AddWithValue("@Bills_date", metroTextBox5.Text);
                    cmd1.Parameters.AddWithValue("@Bills_date_update", metroTextBox3.Text);
                    cmd1.Parameters.AddWithValue("@Bills_sum", Convert.ToInt64(metroTextBox4.Text));
                    cmd1.ExecuteNonQuery();

                    string query_save3 = "update Bills set Bills_date_update=@Bills_date_update,Bills_sum=@Bills_sum,Bills_date=@Bills_date where Bills_id = '" + Convert.ToInt64(metroTextBox1.Text) + "'";
                    SQLiteCommand cmd3 = new SQLiteCommand(query_save3, conn1);
                    cmd3.Parameters.AddWithValue("@Bills_sum", Convert.ToInt64(metroTextBox4.Text));
                    cmd3.Parameters.AddWithValue("@Bills_date_update", metroTextBox3.Text);
                    cmd3.Parameters.AddWithValue("@Bills_date", metroTextBox5.Text);
                    cmd3.ExecuteNonQuery();
                    conn1.Close();

                    MessageBox.Show("تم تعديل محتويات الفاتورة", "تعديل", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                    Bills1 b = new Bills1();
                    b.Show();

                }
            }
        }

        private void metroTextBox5_Click(object sender, EventArgs e)
        {

        }

        private void flatButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("لم يتم تحديد أي عنصر", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (metroTextBox9.Text == string.Empty | metroTextBox10.Text == string.Empty | metroComboBox2.Text == string.Empty | metroComboBox3.Text == string.Empty | metroTextBox11.Text == string.Empty)
                {
                    MessageBox.Show("أحد الحقول المطلوبة فارغ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else
                {
                    if (Convert.ToInt64(metroTextBox9.Text) == 0)
                    {
                        MessageBox.Show("لايمكن تحديد كمية 0 في حقل الكمية وشكرا", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        string sql = "data source = Data.db";
                        SQLiteConnection conn = new SQLiteConnection(sql);
                        conn.Open();
                        string query_insert = "update Pharm set Pharm_quan=Pharm_quan+@Pharm_quan where Pharm_name='" + dataGridView1.CurrentRow.Cells[1].Value + "'";
                        SQLiteCommand cc = new SQLiteCommand(query_insert, conn);
                        cc.Parameters.AddWithValue("@Pharm_quan", Convert.ToInt64(dataGridView1.CurrentRow.Cells[3].Value));
                        cc.ExecuteNonQuery();
                        string query_insert1 = "update Pharm set Pharm_quan=Pharm_quan-@Pharm_quan where Pharm_name='" + dataGridView1.CurrentRow.Cells[1].Value + "'";
                        SQLiteCommand cc1 = new SQLiteCommand(query_insert1, conn);
                        cc1.Parameters.AddWithValue("@Pharm_quan", Convert.ToInt64(metroTextBox9.Text));
                        cc1.ExecuteNonQuery();


                        string update = "update Bills set Bills_id='" + Convert.ToInt32(metroTextBox1.Text) + "',Bills_members_name='" + metroTextBox6.Text + "',Bills_pharm_ghram='" + metroTextBox11.Text + "',Bills_pharm_price='" + Convert.ToInt64(metroTextBox10.Text) + "',Bills_pharm_quan='" + Convert.ToInt64(metroTextBox9.Text) + "',Bills_unit='" + metroComboBox3.Text + "',Bills_count='" + Convert.ToInt64(metroTextBox8.Text) + "' where Bills_Bills_id = '" + Convert.ToInt64(dataGridView1.CurrentRow.Cells[0].Value) + "' ";
                        SQLiteCommand update1 = new SQLiteCommand(update, conn);
                        update1.ExecuteNonQuery();


                        DataTable dd = new DataTable();
                        string query1 = "select Bills_Bills_id,Bills_pharm_name,Bills_pharm_ghram,Bills_pharm_quan,Bills_unit,Bills_pharm_price,Bills_count from Bills where Bills_id = '" + Convert.ToInt32(metroTextBox1.Text) + "'";
                        SQLiteDataAdapter da1 = new SQLiteDataAdapter(query1, conn);
                        da1.Fill(dd);
                        dataGridView1.DataSource = dd;
                        conn.Close();
                        if (dataGridView1.CurrentRow == null)
                        {
                            metroTextBox11.Text = "";
                            metroTextBox9.Text = "0";
                            metroComboBox3.Text = "";
                            metroTextBox10.Text = "0";
                            metroTextBox8.Text = "0";
                        }
                        else
                        {
                            metroTextBox11.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                            metroTextBox9.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                            metroComboBox3.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                            metroTextBox10.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                            metroTextBox8.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                        }
                        dataGridView1.Columns[0].HeaderText = "ID";
                        dataGridView1.Columns[1].HeaderText = "أسم الدواء";
                        dataGridView1.Columns[2].HeaderText = "عيار الدواء";
                        dataGridView1.Columns[3].HeaderText = "كمية الدواء";
                        dataGridView1.Columns[4].HeaderText = "وحدة القياس";
                        dataGridView1.Columns[5].HeaderText = "سعر الدواء";
                        dataGridView1.Columns[6].HeaderText = "المجموع";

                        dataGridView1.Columns[0].Width = 90;
                        dataGridView1.Columns[1].Width = 220;
                        dataGridView1.Columns[2].Width = 150;
                        dataGridView1.Columns[3].Width = 150;
                        dataGridView1.Columns[4].Width = 90;
                        dataGridView1.Columns[5].Width = 149;
                        dataGridView1.Columns[6].Width = 153;
                        for (int hh = 0; hh < dataGridView1.Rows.Count; hh++)
                        {
                            dataGridView1.Rows[hh].Height = 40;
                        }

                        sum();


                    }
                }
            }

        }

        private void flatButton4_Click(object sender, EventArgs e)
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
                string query_del = "delete from Bills where Bills_Bills_id = '" + Convert.ToInt64(dataGridView1.CurrentRow.Cells[0].Value.ToString()) + "' ";
                SQLiteCommand cmd = new SQLiteCommand(query_del, conn);
                cmd.ExecuteNonQuery();
                string query_insert = "update Pharm set Pharm_quan=Pharm_quan+@Pharm_quan where Pharm_name='" +dataGridView1.CurrentRow.Cells[1].Value + "'";
                SQLiteCommand cc = new SQLiteCommand(query_insert, conn);
                cc.Parameters.AddWithValue("@Pharm_quan", Convert.ToInt64(dataGridView1.CurrentRow.Cells[3].Value));
                cc.ExecuteNonQuery();

                DataTable dd = new DataTable();
                string query1 = "select Bills_Bills_id,Bills_pharm_name,Bills_pharm_ghram,Bills_pharm_quan,Bills_unit,Bills_pharm_price,Bills_count from Bills where Bills_id = '" + Convert.ToInt32(metroTextBox1.Text) + "'";
                SQLiteDataAdapter da1 = new SQLiteDataAdapter(query1, conn);
                da1.Fill(dd);
                dataGridView1.DataSource = dd;
                conn.Close();
                if (dataGridView1.CurrentRow == null)
                {
                    metroTextBox11.Text = "";
                    metroTextBox9.Text = "0";
                    metroComboBox3.Text = "";
                    metroTextBox10.Text = "0";
                    metroTextBox8.Text = "0";
                }
                else
                {
                    metroTextBox11.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    metroTextBox9.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    metroComboBox3.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    metroTextBox10.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                    metroTextBox8.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                }
                dataGridView1.Columns[0].HeaderText = "ID";
                dataGridView1.Columns[1].HeaderText = "أسم الدواء";
                dataGridView1.Columns[2].HeaderText = "عيار الدواء";
                dataGridView1.Columns[3].HeaderText = "كمية الدواء";
                dataGridView1.Columns[4].HeaderText = "وحدة القياس";
                dataGridView1.Columns[5].HeaderText = "سعر الدواء";
                dataGridView1.Columns[6].HeaderText = "المجموع";

                dataGridView1.Columns[0].Width = 90;
                dataGridView1.Columns[1].Width = 220;
                dataGridView1.Columns[2].Width = 150;
                dataGridView1.Columns[3].Width = 150;
                dataGridView1.Columns[4].Width = 90;
                dataGridView1.Columns[5].Width = 149;
                dataGridView1.Columns[6].Width = 153;
                for (int hh = 0; hh < dataGridView1.Rows.Count; hh++)
                {
                    dataGridView1.Rows[hh].Height = 40;
                }

                sum();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void flatButton2_Click(object sender, EventArgs e)
        {
            if (metroTextBox4.Text == "0")
            {
                MessageBox.Show("لايمكن الحفظ لأن مبلغ الفاتورة= 0 /n هناك تغييرات لم تقم بحفظها يرجى التحديث ثم معاودة الحفظ ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (metroTextBox3.Text == string.Empty)
                {
                    MessageBox.Show("يرجى أدخال تاريخ التعديل وشكرا", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    string sql1 = "data source = Data.db";
                    SQLiteConnection conn1 = new SQLiteConnection(sql1);
                    conn1.Open();
                    string query_save1 = "update Bills2 set Bills_date=@Bills_date,Bills_date_update=@Bills_date_update,Bills_sum=@Bills_sum where Bills_id = '" + Convert.ToInt64(metroTextBox1.Text) + "'";
                    SQLiteCommand cmd1 = new SQLiteCommand(query_save1, conn1);
                    cmd1.Parameters.AddWithValue("@Bills_date", metroTextBox5.Text);
                    cmd1.Parameters.AddWithValue("@Bills_date_update", metroTextBox3.Text);
                    cmd1.Parameters.AddWithValue("@Bills_sum", Convert.ToInt64(metroTextBox4.Text));
                    cmd1.ExecuteNonQuery();

                    string query_save3 = "update Bills set Bills_date_update=@Bills_date_update,Bills_sum=@Bills_sum where Bills_id = '" + Convert.ToInt64(metroTextBox1.Text) + "'";
                    SQLiteCommand cmd3 = new SQLiteCommand(query_save3, conn1);
                    cmd3.Parameters.AddWithValue("@Bills_sum", Convert.ToInt64(metroTextBox4.Text));
                    cmd3.Parameters.AddWithValue("@Bills_date_update", metroTextBox3.Text);
                    cmd3.Parameters.AddWithValue("@Bills_date", metroTextBox5.Text);
                    cmd3.ExecuteNonQuery();
                    conn1.Close();


                    string sql = "data source = Data.db";
                    SQLiteConnection con4 = new SQLiteConnection(sql);
                    DS1 ds = new DS1();
                    CrystalReport2 cr2 = new CrystalReport2();
                    Print p = new Print();
                    con4.Open();
                    SQLiteDataAdapter da1 = new SQLiteDataAdapter("select Bills_id,Bills_members_name,Bills_pharm_name,Bills_pharm_ghram,Bills_pharm_quan,Bills_pharm_price,Bills_count,Bills_unit from Bills where Bills_id=@Bills_id", con4);
                    da1.SelectCommand.Parameters.AddWithValue("@Bills_id", Convert.ToInt64(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                    da1.Fill(ds, "Bills");
                    cr2.SetDataSource(ds.Tables["Bills"]);
                    p.crystalReportViewer1.ReportSource = cr2;
                    p.Show();
                    this.Close();


                }
            }
        }

        private void flatButton6_Click(object sender, EventArgs e)
        {
            string sql = "data source = Data.db";
            SQLiteConnection conn = new SQLiteConnection(sql);
            if (metroTextBox11.Text == string.Empty | metroTextBox10.Text == string.Empty | metroComboBox2.Text == string.Empty | metroComboBox3.Text == string.Empty)
            {
                MessageBox.Show("أحد الحقول المطلوبة فارغ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                if (Convert.ToInt64(metroTextBox9.Text) == 0)
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
                    if (Convert.ToInt64(dt.Rows[0][0].ToString()) < Convert.ToInt64(metroTextBox9.Text))
                    {
                        MessageBox.Show("لاتوجد كمية كافية من هذه المادة\nان الكمية الموجودة بالمستودع هي" + (dt.Rows[0][0].ToString()) + " ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    else
                    {
                        conn.Open();
                        string query = "insert into Bills (Bills_id,Bills_members_name,Bills_pharm_name,Bills_pharm_ghram,Bills_pharm_price,Bills_pharm_quan,Bills_Bills_unit,Bills_count) values('" + Convert.ToInt32(metroTextBox1.Text) + "','" + metroTextBox6.Text + "','" + metroComboBox2.Text + "','" + metroTextBox11.Text + "','" + Convert.ToInt64(metroTextBox10.Text) + "','" + Convert.ToInt64(metroTextBox9.Text) + "','" + metroComboBox3.Text + "','" + Convert.ToInt64(metroTextBox8.Text) + "') ";
                        SQLiteCommand cmd = new SQLiteCommand(query, conn);
                        cmd.ExecuteNonQuery();



                        string query2 = "update Pharm set Pharm_quan=Pharm_quan-'" + Convert.ToInt64(metroTextBox9.Text) + "' where Pharm_name = '" + metroComboBox2.Text + "'";
                        SQLiteCommand cmd2 = new SQLiteCommand(query2, conn);
                        cmd2.ExecuteNonQuery();
                        conn.Close();
                        metroTextBox9.Clear();
                        metroTextBox10.Clear();
                        metroTextBox11.Clear();
                        metroTextBox8.Clear();
                        conn.Close();
                    }


                }

                string sql1 = "data source = Data.db";
                SQLiteConnection conn1 = new SQLiteConnection(sql1);
                conn1.Open();
                DataTable dd = new DataTable();
                string query1 = "select Bills_Bills_id,Bills_pharm_name,Bills_pharm_ghram,Bills_pharm_quan,Bills_unit,Bills_pharm_price,Bills_count from Bills where Bills_id = '" + Convert.ToInt32(metroTextBox1.Text) + "'";
                SQLiteDataAdapter da1 = new SQLiteDataAdapter(query1, conn);
                da1.Fill(dd);
                dataGridView1.DataSource = dd;
                conn1.Close();
                if (dataGridView1.CurrentRow == null)
                {
                    metroTextBox11.Text = "";
                    metroTextBox9.Text = "0";
                    metroComboBox3.Text = "";
                    metroTextBox10.Text = "0";
                    metroTextBox8.Text = "0";
                }
                else
                {
                    metroTextBox11.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    metroTextBox9.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    metroComboBox3.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    metroTextBox10.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                    metroTextBox8.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                }
                dataGridView1.Columns[0].HeaderText = "ID";
                dataGridView1.Columns[1].HeaderText = "أسم الدواء";
                dataGridView1.Columns[2].HeaderText = "عيار الدواء";
                dataGridView1.Columns[3].HeaderText = "كمية الدواء";
                dataGridView1.Columns[4].HeaderText = "وحدة القياس";
                dataGridView1.Columns[5].HeaderText = "سعر الدواء";
                dataGridView1.Columns[6].HeaderText = "المجموع";

                dataGridView1.Columns[0].Width = 90;
                dataGridView1.Columns[1].Width = 220;
                dataGridView1.Columns[2].Width = 150;
                dataGridView1.Columns[3].Width = 150;
                dataGridView1.Columns[4].Width = 90;
                dataGridView1.Columns[5].Width = 149;
                dataGridView1.Columns[6].Width = 153;
                for (int hh = 0; hh < dataGridView1.Rows.Count; hh++)
                {
                    dataGridView1.Rows[hh].Height = 40;
                }

                sum();
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void metroTextBox9_Click(object sender, EventArgs e)
        {

        }

        private void metroTextBox10_TextChanged(object sender, EventArgs e)
        {
            cal1();
        }

        private void metroTextBox9_TextChanged(object sender, EventArgs e)
        {
            cal1();
        }

        private void metroTextBox10_KeyUp(object sender, KeyEventArgs e)
        {
            cal1();
        }

        private void metroTextBox10_KeyDown(object sender, KeyEventArgs e)
        {
            cal1();
        }

        private void metroTextBox9_KeyUp(object sender, KeyEventArgs e)
        {
            cal1();
        }

        private void metroTextBox9_KeyDown(object sender, KeyEventArgs e)
        {
            cal1();
        }

        private void metroTextBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassNumbersOnly ccc = new ClassNumbersOnly();
            ccc.UserNumbersOnly(e);
        }

        private void metroTextBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassNumbersOnly ccc = new ClassNumbersOnly();
            ccc.UserNumbersOnly(e);
        }

        private void metroTextBox7_TextChanged(object sender, EventArgs e)
        {
            AutoCompleteStringCollection datasearch = new AutoCompleteStringCollection();
            metroTextBox5.AutoCompleteCustomSource = datasearch;
            metroTextBox5.AutoCompleteSource = AutoCompleteSource.CustomSource;
            metroTextBox5.AutoCompleteMode = AutoCompleteMode.Append;

            DataTable dt = new DataTable();
            string sql1 = "data source = Data.db";
            SQLiteConnection conn1 = new SQLiteConnection(sql1);
            conn1.Open();
            string query1 = "select * from Bills where Bills_pharm_name like '%" + metroTextBox7.Text + "%' and Bills_id = '" + metroTextBox7.Text + "' and Bills_pharm_ghram = '" + metroTextBox7.Text + "'and Bills_pharm_price = '" + metroTextBox7.Text + "'";
            SQLiteDataAdapter da = new SQLiteDataAdapter(query1, conn1);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            if (dataGridView1.CurrentRow == null)
            {
                metroTextBox11.Text = "";
                metroTextBox9.Text = "0";
                metroComboBox3.Text = "";
                metroTextBox10.Text = "0";
                metroTextBox8.Text = "0";
            }
            else
            {
                metroTextBox11.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                metroTextBox9.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                metroComboBox3.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                metroTextBox10.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                metroTextBox8.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            }

            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "أسم الدواء";
            dataGridView1.Columns[2].HeaderText = "عيار الدواء";
            dataGridView1.Columns[3].HeaderText = "كمية الدواء";
            dataGridView1.Columns[4].HeaderText = "وحدة القياس";
            dataGridView1.Columns[5].HeaderText = "سعر الدواء";
            dataGridView1.Columns[6].HeaderText = "المجموع";

            dataGridView1.Columns[0].Width = 90;
            dataGridView1.Columns[1].Width = 220;
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[4].Width = 90;
            dataGridView1.Columns[5].Width = 149;
            dataGridView1.Columns[6].Width = 153;
            for (int hh = 0; hh < dataGridView1.Rows.Count; hh++)
            {
                dataGridView1.Rows[hh].Height = 40;
            }

            conn1.Close();
        }

        private void metroTextBox7_Click(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                metroTextBox11.Text = "";
                metroTextBox9.Text = "0";
                metroComboBox3.Text = "";
                metroTextBox10.Text = "0";
                metroTextBox8.Text = "0";
            }
            else
            {
                metroTextBox11.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                metroTextBox9.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                metroComboBox3.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                metroTextBox10.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                metroTextBox8.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            }
        }

        private void flatButton7_Click(object sender, EventArgs e)
        {
            DataTable dd = new DataTable();
            string sql1 = "data source = Data.db";
            SQLiteConnection conn1 = new SQLiteConnection(sql1);
            conn1.Open();
            string query1 = "select Bills_Bills_id,Bills_pharm_name,Bills_pharm_ghram,Bills_pharm_quan,Bills_unit,Bills_pharm_price,Bills_count from Bills where Bills_id = '" + Convert.ToInt64(metroTextBox1.Text) + "'";
            SQLiteDataAdapter da1 = new SQLiteDataAdapter(query1, conn1);
            da1.Fill(dd);
            dataGridView1.DataSource = dd;
            conn1.Close();
            if (dataGridView1.CurrentRow == null)
            {
                metroTextBox11.Text = "";
                metroTextBox9.Text = "0";
                metroComboBox3.Text = "";
                metroTextBox10.Text = "0";
                metroTextBox8.Text = "0";
            }
            else
            {
                metroTextBox11.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                metroTextBox9.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                metroComboBox3.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                metroTextBox10.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                metroTextBox8.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            }
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "أسم الدواء";
            dataGridView1.Columns[2].HeaderText = "عيار الدواء";
            dataGridView1.Columns[3].HeaderText = "كمية الدواء";
            dataGridView1.Columns[4].HeaderText = "وحدة القياس";
            dataGridView1.Columns[5].HeaderText = "سعر الدواء";
            dataGridView1.Columns[6].HeaderText = "المجموع";
            metroTextBox3.Clear();

            dataGridView1.Columns[0].Width = 90;
            dataGridView1.Columns[1].Width = 220;
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[4].Width = 90;
            dataGridView1.Columns[5].Width = 149;
            dataGridView1.Columns[6].Width = 153;
            for (int hh = 0; hh < dataGridView1.Rows.Count; hh++)
            {
                dataGridView1.Rows[hh].Height = 40;
            }

            sum();
        } 
    }
}
