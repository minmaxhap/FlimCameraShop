using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace WinFormsAppTest0915
{
    public class NonMember
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        public string Email { get; set; }
        public string Zip { get; set; }

        public string Addr1 { get; set; }

        public string Addr2 { get; set; }
    }
    public class NonMemDAC :IDisposable
    {
        MySqlConnection conn;

        public NonMemDAC()
        {
            conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["project"].ConnectionString);
            conn.Open();
        }

        public void Dispose()
        {
            conn.Close();
        }

        public int Insert(NonMember nonmem)
        {
            string sql = @"insert into nonmemLogin(code,name, phone1, phone2, phone3)
values( @code,@name,  @phone1, @phone2, @phone3)";
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@code", nonmem.Code);
                cmd.Parameters.AddWithValue("@name", nonmem.Name);
                cmd.Parameters.AddWithValue("@phone1", nonmem.Phone1);
                cmd.Parameters.AddWithValue("@phone2", nonmem.Phone2);
                cmd.Parameters.AddWithValue("@phone3", nonmem.Phone3);


                return cmd.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return -1;
            }

        }
    }
}
