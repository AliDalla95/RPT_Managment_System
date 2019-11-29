using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework;
using System.Data.SQLite;

namespace RPT_Manager
{
    class ClassNumbersOnly
    {
        public void UserNumbersOnly (KeyPressEventArgs e)
        {
              switch (e.KeyChar)
            {
                case '0':case '1':case '2':case '3':case '4':case '5':case '6':case '7':case '8': case '9':case'.':case (char)Keys.Back:
                    e.Handled = false;
                    break;
                default :
                    MessageBox.Show("أدخل أرقاما فقط", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    break;}
            }



        public void UserNumbersOnly1(KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case '.':
                    e.Handled = false;
                    break;
                default:
                    MessageBox.Show("أدخل أرقاما فقط", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    break;
            }
        }


        public void UserCharsOnly(KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case '0':case '1':case '2':case '3':case '4':case '5':case '6':case '7':case '8':case '9': case '.':
                e.Handled = true;
                MessageBox.Show("لاتدخل أرقاما في مربع الاسم رجاءا", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                break;
                default:
                e.Handled = false;
                break;
            }
        }
    }
}
