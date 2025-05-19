using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;

namespace WinFormsAppTest0915
{
    class CodeDAC : IDisposable
    {
        MySqlConnection conn;
        public CodeDAC()
        {

                conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["project"].ConnectionString);
                conn.Open();
        }

        public void Dispose()
        {
            conn.Clone();
        }

        public DataTable GetCommonCodes(string[] category)
        {
            string temp = "'" + string.Join("','", category) + "'";

            string sql = @"select code, name, category from commoncode 
where category in (" + temp + ")";

            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public string FindCode(string name)
        {
            string sql = @"select code
from commoncode
where name = @name";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", name);
           MySqlDataReader reader = cmd.ExecuteReader();
            return reader["code"].ToString();
        }
        public DataTable GetAllCommonCode()
        {
            string sql = @"select code, name, category from commoncode ";

            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public void InsertCode(string code, string name, string category)
        {
            try
            {
                string sql = "insert into commoncode(code,category,name) values(@code,@category,@name)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@code", code);
                cmd.Parameters.AddWithValue("@category", category);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.ExecuteNonQuery();
            }
            catch(Exception err)
            {
                throw err;
            }
        }

        public void UpdateCode(string code, string name, string category)
        {
            try
            {
                string sql = @"update commoncode set category=@category, name=@name 
                                 where code = @code";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@code", code);
                cmd.Parameters.AddWithValue("@category", category);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.ExecuteNonQuery();
            }
           
             catch (Exception err)
            {
                throw err;
            }
        }

        public void DeleteCode(string code)
        {
            try 
            {
                string sql = "delete from commoncode where code=@code";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@code", code);
                cmd.ExecuteNonQuery();
            }
            
             catch (Exception err)
            {
                throw err;
            }
        }
    
    }
}
