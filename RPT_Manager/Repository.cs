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
    public partial class Repository : MetroFramework.Forms.MetroForm
    {
        public Repository()
        {
            InitializeComponent();

            DataTable dd = new DataTable();
            string sql = "data source = Data.db";
            SQLiteConnection conn = new SQLiteConnection(sql);
            conn.Open();
            string query = "select Pharm_id as 'Id', Pharm_name as'أسم الدواء',Pharm_ghram as 'عيار الدواء',Pharm_price as'السعر',Pharm_quan as'الكمية',Pharm_unit as 'وحدة القياس' ,Pharm_pro as'تاريخ الانتاج',Pharm_exp as 'تاريخ الانتهاء',Pharm_company as'أسم الشركة'   from Pharm";
            SQLiteDataAdapter da = new SQLiteDataAdapter(query, conn);
            da.Fill(dd);
            dataGridView1.DataSource = dd;
            conn.Close();

            DataTable ddd = new DataTable();
            conn.Open();
            string query1 = "select Members_id as 'Id', Members_name as'أسم العميل', Members_add as'العنوان', Members_tel as'الهاتف', Members_name_pharm as'أسم الصيدلية' from Members";
            SQLiteDataAdapter da1 = new SQLiteDataAdapter(query1, conn);
            da1.Fill(ddd);
            dataGridView2.DataSource = ddd;
            conn.Close();

            DataTable dddd = new DataTable();
            conn.Open();
            string query2 = "select Bills_id as 'رقم الفاتورة' ,Bills_members_name as 'أسم الصيدلاني' ,Bills_sum as 'المجموع الكلي للفاتورة' ,Bills_date as 'تاريخ أنشاء الفاتورة'  from Bills2";
            SQLiteDataAdapter da2 = new SQLiteDataAdapter(query2, conn);
            da2.Fill(dddd);
            dataGridView3.DataSource = dddd;
            conn.Close();


        }

        private void Form4_Load(object sender, EventArgs e)
        {
            dataGridView3.Columns[0].Width = 80;
            dataGridView3.Columns[1].Width = 250;
            dataGridView3.Columns[2].Width = 210;
            dataGridView3.Columns[3].Width = 220;

            dataGridView2.Columns[0].Width = 70;
            dataGridView2.Columns[1].Width = 155;
            dataGridView2.Columns[2].Width = 290;
            dataGridView2.Columns[3].Width = 100;
            dataGridView2.Columns[4].Width = 145;
            for (int hh = 0; hh < dataGridView2.Rows.Count; hh++)
            {
                dataGridView2.Rows[hh].Height = 40;
            }

            dataGridView1.Columns[0].Width = 59;
            dataGridView1.Columns[1].Width = 154;
            dataGridView1.Columns[2].Width = 80;
            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[4].Width = 60;
            dataGridView1.Columns[5].Width = 70;
            dataGridView1.Columns[6].Width = 80;
            dataGridView1.Columns[7].Width = 80;
            dataGridView1.Columns[8].Width = 80;
            for (int hh = 0; hh < dataGridView1.Rows.Count; hh++)
            {
                dataGridView1.Rows[hh].Height = 40;
            }

        }

        private void flatButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_ColumnDisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
        {

        }
    }
}
