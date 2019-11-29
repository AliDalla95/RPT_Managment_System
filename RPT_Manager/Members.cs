using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using System.Data.SQLite;

namespace RPT_Manager
{
    public partial class Members : MetroFramework.Forms.MetroForm
    {

        public Members()
        {
            InitializeComponent();

            DataTable dt = new DataTable();
            string sql = "data source = Data.db";
            SQLiteConnection conn = new SQLiteConnection(sql);
            string query = "select Members_name from Members";
            SQLiteDataAdapter da = new SQLiteDataAdapter(query, conn);
            da.Fill(dt);
            AutoCompleteStringCollection datasearch = new AutoCompleteStringCollection();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                datasearch.Add(dt.Rows[i][0].ToString());
            }
            this.textBox6.AutoCompleteCustomSource = datasearch;
            this.textBox6.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.textBox6.AutoCompleteMode = AutoCompleteMode.Append;

            DataTable dd = new DataTable();
            conn.Open();
            string query1 = "select Members_id as 'Id', Members_name as'أسم الصيدلاني', Members_add as'العنوان', Members_tel as'الهاتف', Members_name_pharm as'أسم الصيدلية' from Members";
            SQLiteDataAdapter da1 = new SQLiteDataAdapter(query1, conn);
            da1.Fill(dd);
            dataGridView1.DataSource = dd;
            conn.Close();



        }


        private void Form3_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns[0].Width = 74;
            dataGridView1.Columns[1].Width = 237;
            dataGridView1.Columns[2].Width = 357;
            dataGridView1.Columns[3].Width = 158;
            dataGridView1.Columns[4].Width = 158;
            for (int hh= 0;hh< dataGridView1.Rows.Count;hh ++) 
            {
                dataGridView1.Rows[hh].Height = 40;
            }

        }

        private void flatButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void flatButton1_Click(object sender, EventArgs e)
        {
            string sql = "data source = Data.db";
            SQLiteConnection conn = new SQLiteConnection(sql);
            if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("يرجى ادخال البيانات", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                if (textBox3.Text == string.Empty)
                {
                    conn.Open();
                    string query = " insert into Members (Members_name,Members_add,Members_tel,Members_name_pharm) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "') ";
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("تمت الاضافة", "اضافة", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                }
                else
                {
                    conn.Open();
                    string query = " insert into Members (Members_name,Members_add,Members_tel,Members_name_pharm) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "') ";
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("تمت الاضافة", "اضافة", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                }
                
            }

            DataTable dd = new DataTable();
            conn.Open();
            string query1 = "select Members_id as 'Id', Members_name as'أسم الصيدلاني', Members_add as'العنوان', Members_tel as'الهاتف', Members_name_pharm as'أسم الصيدلية' from Members";
            SQLiteDataAdapter da1 = new SQLiteDataAdapter(query1, conn);
            da1.Fill(dd);
            dataGridView1.DataSource = dd;
            conn.Close();

            if (dataGridView1.CurrentRow == null)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "0";
                textBox4.Text = "";
            }
            else
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            }

            dataGridView1.Columns[0].Width = 74;
            dataGridView1.Columns[1].Width = 237;
            dataGridView1.Columns[2].Width = 357;
            dataGridView1.Columns[3].Width = 158;
            dataGridView1.Columns[4].Width = 158;
            for (int hh = 0; hh < dataGridView1.Rows.Count; hh++)
            {
                dataGridView1.Rows[hh].Height = 40;
            }


        }

        private void flatButton4_Click(object sender, EventArgs e)
        {
            string sql = "data source = Data.db";
            SQLiteConnection conn = new SQLiteConnection(sql);
            DataTable dd = new DataTable();
            conn.Open();
            string query1 = "select Members_id as 'Id', Members_name as'أسم الصيدلاني', Members_add as'العنوان', Members_tel as'الهاتف', Members_name_pharm as'أسم الصيدلية' from Members";
            SQLiteDataAdapter da1 = new SQLiteDataAdapter(query1, conn);
            da1.Fill(dd);
            dataGridView1.DataSource = dd;
            conn.Close();

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

            if (dataGridView1.CurrentRow == null)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "0";
                textBox4.Text = "";
            }
            else
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            }

            dataGridView1.Columns[0].Width = 74;
            dataGridView1.Columns[1].Width = 237;
            dataGridView1.Columns[2].Width = 357;
            dataGridView1.Columns[3].Width = 158;
            dataGridView1.Columns[4].Width = 158;
            for (int hh = 0; hh < dataGridView1.Rows.Count; hh++)
            {
                dataGridView1.Rows[hh].Height = 40;
            }

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            AutoCompleteStringCollection datasearch = new AutoCompleteStringCollection();
            textBox6.AutoCompleteCustomSource = datasearch;
            textBox6.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox6.AutoCompleteMode = AutoCompleteMode.Append;

            DataTable dd = new DataTable();
            dd.Clear();
            string sql = "data source = Data.db";
            SQLiteConnection conn = new SQLiteConnection(sql);
            conn.Open();
            string query1 = "select Members_id as 'Id', Members_name as'أسم الصيدلاني', Members_add as'العنوان', Members_tel as'الهاتف', Members_name_pharm as'أسم الصيدلية' from Members where Members_name like '%" + textBox6.Text + "%' or Members_add like '%" + textBox6.Text + "%' or Members_tel like '%" + textBox6.Text + "%' or Members_name_pharm like '%" + textBox6.Text + "%' ";
            SQLiteDataAdapter da1 = new SQLiteDataAdapter(query1, conn);
            da1.Fill(dd);
            dataGridView1.DataSource = dd;
            conn.Close();

            if (dataGridView1.CurrentRow == null)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "0";
                textBox4.Text = "";
            }
            else
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            }

            dataGridView1.Columns[0].Width = 74;
            dataGridView1.Columns[1].Width = 237;
            dataGridView1.Columns[2].Width = 357;
            dataGridView1.Columns[3].Width = 158;
            dataGridView1.Columns[4].Width = 158;
            for (int hh = 0; hh < dataGridView1.Rows.Count; hh++)
            {
                dataGridView1.Rows[hh].Height = 40;
            }


        }

        private void flatButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("لم يتم تحديد أي أسم", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string sql = " data source = Data.db";
                SQLiteConnection conn = new SQLiteConnection(sql);

                if (textBox1.Text == string.Empty)
                {
                    MessageBox.Show("(#يرجى ادخال البيانات(#أسم الصيدلاني", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                else
                {
                    conn.Open();
                    string q = "update Members set  Members_name=@Members_name,Members_add=@Members_add,Members_tel=@Members_tel,Members_name_pharm=@Members_name_pharm where Members_id = '" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
                    SQLiteCommand cmd1 = new SQLiteCommand(q, conn);
                    cmd1.Parameters.AddWithValue("@Members_name", textBox1.Text);
                    cmd1.Parameters.AddWithValue("@Members_add", textBox2.Text);
                    cmd1.Parameters.AddWithValue("@Members_tel", textBox3.Text);
                    cmd1.Parameters.AddWithValue("@Members_name_pharm", textBox4.Text);
                    cmd1.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("تم التعديل", "معلومات التعديل", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }



                DataTable dd = new DataTable();
                conn.Open();
                string query3 = "select Members_id as 'Id', Members_name as'أسم الصيدلاني', Members_add as'العنوان', Members_tel as'الهاتف', Members_name_pharm as'أسم الصيدلية' from Members";
                SQLiteDataAdapter da1 = new SQLiteDataAdapter(query3, conn);
                da1.Fill(dd);
                dataGridView1.DataSource = dd;
                conn.Close();

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();

                if (dataGridView1.CurrentRow == null)
                {
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "0";
                    textBox4.Text = "";
                }
                else
                {
                    textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                }

                dataGridView1.Columns[0].Width = 74;
                dataGridView1.Columns[1].Width = 237;
                dataGridView1.Columns[2].Width = 357;
                dataGridView1.Columns[3].Width = 158;
                dataGridView1.Columns[4].Width = 158;
                for (int hh = 0; hh < dataGridView1.Rows.Count; hh++)
                {
                    dataGridView1.Rows[hh].Height = 40;
                }
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
        }

        private void flatButton5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("لم يتم تحديد أي أسم", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string sql = " data source = Data.db";
                SQLiteConnection conn = new SQLiteConnection(sql);
                conn.Open();
                string query = "delete from Members where Members_id = '" + dataGridView1.CurrentRow.Cells[0].Value + "'";
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();


                DataTable dd = new DataTable();
                conn.Open();
                string query1 = "select Members_id as 'Id', Members_name as'أسم الصيدلاني', Members_add as'العنوان', Members_tel as'الهاتف', Members_name_pharm as'أسم الصيدلية' from Members";
                SQLiteDataAdapter da1 = new SQLiteDataAdapter(query1, conn);
                da1.Fill(dd);
                dataGridView1.DataSource = dd;
                conn.Close();

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();

                if (dataGridView1.CurrentRow == null)
                {
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "0";
                    textBox4.Text = "";
                }
                else
                {
                    textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                }


                dataGridView1.Columns[0].Width = 74;
                dataGridView1.Columns[1].Width = 237;
                dataGridView1.Columns[2].Width = 357;
                dataGridView1.Columns[3].Width = 158;
                dataGridView1.Columns[4].Width = 158;
                for (int hh = 0; hh < dataGridView1.Rows.Count; hh++)
                {
                    dataGridView1.Rows[hh].Height = 40;
                }
                MessageBox.Show("تم الحذف", "الحذف", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }      

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassNumbersOnly ccc = new ClassNumbersOnly();
            ccc.UserNumbersOnly(e);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassNumbersOnly ccc = new ClassNumbersOnly();
            ccc.UserNumbersOnly(e);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }
            else
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            }
        }
    }
}
