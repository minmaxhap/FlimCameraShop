using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsAppTest0915
{
    public class CommonUtil
    {
        //비밀번호 정규식
        public static bool PwdCheck(string txtPwd)
        {
            return Regex.IsMatch(txtPwd, @"^(?=.*?[a-z])(?=.*?[0-9])(?=.*?[~!@#$%^*()?]).{8,15}$");
        }
        //ID 정규식
        public static bool IDCheck(string txtID)
        {
            return Regex.IsMatch(txtID, @"^(?=.*?[a-z])(?=.*?[0-9]).{7,15}$");
        }
        //이름 정규식
        public static bool NameCheck(string txtID)
        {
            return Regex.IsMatch(txtID, @"^[a-zA-Z가-힣].*$");
        }

        //이메일 정규식
        public static bool EmailCheck(string txtEmail)
        {
            return Regex.IsMatch(txtEmail, 
                @"[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?");
        }

        public static void ComboBinding(ComboBox cbo, string category, DataTable dt, bool blankItem = true, string blankText = "선택", ComboBoxStyle style = ComboBoxStyle.DropDownList)
        {
            if (blankItem)
            {
                DataRow dr = dt.NewRow();
                dr["code"] = "";
                dr["name"] = blankText;
                dr["category"] = category;
                dt.Rows.InsertAt(dr, 0);
                dt.AcceptChanges();
            }
            DataView dv = new DataView(dt);
            dv.RowFilter = "category = '" + category + "'";
            cbo.DisplayMember = "name";
            cbo.ValueMember = "code";
            cbo.DataSource = dv;
            cbo.DropDownStyle = style;

        }

        public static void AddListTextColumn(
           ListView lv,
           string columnheader,
           string propertyName,
           int width = 80,
           HorizontalAlignment align = HorizontalAlignment.Center
           )
        {
            ColumnHeader ch = new ColumnHeader();
            ch.Text = columnheader;
            ch.Tag = propertyName;
            ch.Width = width;
            ch.TextAlign = align;

            lv.Columns.Add(ch);
        }



    }
}
