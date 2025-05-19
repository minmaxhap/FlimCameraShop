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
    public class Member
    {
        public string ID { get; set; }
        public string Pwd { get; set; }

        public string Name { get; set; }

        public string Birth { get; set; }

       
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }

        public string Zip { get; set; }

        public string Addr1 { get; set; }

        public string Addr2 { get; set; }
        public string Email { get; set; }

        public DateTime SignUpDate { get; set; }
        public int Points { get; set; }
        public int Amass { get; set; }
        public string Recom { get; set; }



    }
    public class MemDAC:IDisposable
    {
        MySqlConnection conn;

        public MemDAC()
        {
            conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["project"].ConnectionString);
            conn.Open();
        }

        public int IDCheck(string id)
        {
           string sql = "select count(*) from member where id = @id";
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                int cnt = Convert.ToInt32(cmd.ExecuteScalar());
                return cnt;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return -1;
            }
        }

        public int Insert(Member mem)
        {
            string sql = @"insert into member(id, pwd, name, birth, phone1, phone2, phone3, zipcode, addr1, addr2, email, recom)
values(@id, @pwd, @name, @birth, @phone1, @phone2, @phone3, @zipcode, @addr1, @addr2, @email, @recom)";
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id",mem.ID);
                cmd.Parameters.AddWithValue("@pwd", mem.Pwd);
                cmd.Parameters.AddWithValue("@name", mem.Name);
                cmd.Parameters.AddWithValue("@birth", mem.Birth);
                cmd.Parameters.AddWithValue("@phone1", mem.Phone1);
                cmd.Parameters.AddWithValue("@phone2", mem.Phone2);
                cmd.Parameters.AddWithValue("@phone3", mem.Phone3);
                cmd.Parameters.AddWithValue("@zipcode", mem.Zip);
                cmd.Parameters.AddWithValue("@addr1", mem.Addr1);
                cmd.Parameters.AddWithValue("@addr2", mem.Addr2);
                cmd.Parameters.AddWithValue("@email", mem.Email);
                cmd.Parameters.AddWithValue("@recom", mem.Recom);


                return cmd.ExecuteNonQuery();
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
                return -1;
            }

        }
        public Member Login(string id)
        {
            string sql = "select id, pwd, name, birth, phone1, phone2, phone3, zipcode, addr1, addr2, email, recom, points, amass from member where id=@id";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                Member loginMem = new Member
                { 
                    ID = reader["id"].ToString(),
                    Pwd = reader["pwd"].ToString(),
                    Name = reader["name"].ToString(),
                    Birth = reader["birth"].ToString(),
                    Phone1 = reader["phone1"].ToString(),
                    Phone2 = reader["phone2"].ToString(),
                    Phone3 = reader["phone3"].ToString(),
                    Zip = reader["zipcode"].ToString(),
                    Addr1 = reader["addr1"].ToString(),
                    Addr2 = reader["addr2"].ToString(),
                    Email = reader["email"].ToString(),
                    Recom = reader["recom"].ToString(),
                    Points = Convert.ToInt32(reader["points"]),
                    Amass = Convert.ToInt32(reader["amass"])
                };

                return loginMem;
            }
            else
            {
                return null;
            }
        }

        public bool Update(Member mem)
        {
            try
            {

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"UPDATE member SET  pwd=@pwd, birth=@birth, phone1=@phone1, phone2=@phone2, phone3=@phone3, zipcode=@zipcode, addr1=@addr1, addr2=@addr2, email=@email WHERE id=@id";
                cmd.Parameters.AddWithValue("@id", mem.ID);
                cmd.Parameters.AddWithValue("@pwd", mem.Pwd);
                cmd.Parameters.AddWithValue("@birth", mem.Birth);
                cmd.Parameters.AddWithValue("@phone1", mem.Phone1);
                cmd.Parameters.AddWithValue("@phone2", mem.Phone2);
                cmd.Parameters.AddWithValue("@phone3", mem.Phone3);
                cmd.Parameters.AddWithValue("@zipcode", mem.Zip);
                cmd.Parameters.AddWithValue("@addr1", mem.Addr1);
                cmd.Parameters.AddWithValue("@addr2", mem.Addr2);
                cmd.Parameters.AddWithValue("@email", mem.Email);

                int iRowsAffect = cmd.ExecuteNonQuery();
                return (iRowsAffect > 0);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return false;
            }
        }

        internal bool Quit(string id)
        {
            try
            {

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = $"UPDATE member SET isquit='Y', quitdate = @quitdate WHERE id=@id";

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@quitdate", DateTime.Now);
                int iRowsAffect = cmd.ExecuteNonQuery();
                return (iRowsAffect > 0);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return false;
            }
        }

        public DataTable GetMemInfo() //frmMemMg 데이터 그리드 뷰에 데이터 불러오기
        {
            string sql = @"SELECT id, pwd, m.name , birth, concat(c.name, '-', phone2,'-', phone3) phone, email, zipcode, addr1, addr2, recom, points, amass, signupdate, isquit, quitdate
FROM commoncode c inner join member m on c.code = phone1 ";

            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
            da.Fill(dt);
            return dt;

        }

        public void Dispose()
        {
            conn.Close();
        }
    }
}
