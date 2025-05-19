using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WinFormsAppTest0915
{
    public class Advice
    {
        //code, name, birthday, phone1, phone2, phone3,platform, contents, date, isreply, repdate, repid, repname, repcontents
        public string Code { get; set; }
        public string Name { get; set; }
        public string ID { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        public string Platform { get; set; }
        public string Contents { get; set; }
        public DateTime Date { get; set; }
        public string IsReply { get; set; }
        public DateTime RepDate { get; set; }
        public string RepID { get; set; }
        public string RepName{ get; set; }
        public string  RepContents { get; set; }


    }
 
    class AdvDAC : IDisposable
    {
        MySqlConnection conn;

       

        public AdvDAC()
        {
            conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["project"].ConnectionString);
            conn.Open();
        }

        public DataTable GetAdvInfo() 
        {
            string sql = @"SELECT a.code as code, a.name , phone1, concat(c.name, '-', phone2,'-', phone3) phone, cp.name as platformname, platform, contents, date, isreply, repdate, repid, repname, repcontents
FROM advice a 
left outer join commoncode c on c.code = phone1  
left outer join  commoncode cp on cp.code = platform
where isdelete ='N'
";

            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
            da.Fill(dt);
            return dt;


        }

        internal DataTable GetAdvInfo(string id)
        {
            string sql = @"SELECT a.code as code, a.name , phone1, concat(c.name, '-', phone2,'-', phone3) phone, cp.name as platformname, platform, contents, date, isreply, repdate, repid, repname, repcontents
FROM advice a 
left outer join commoncode c on c.code = phone1  
left outer join  commoncode cp on cp.code = platform
where isdelete ='N'
and id = @id
";

            DataTable dt = new DataTable();

            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
            da.SelectCommand.Parameters.AddWithValue("@id", id);

            da.Fill(dt);
            return dt;
        }

        

        public bool AskAdvice(Advice adv)
        {

            string sql = @"insert into advice(code, id, name, phone1, phone2, phone3,platform, contents, date)
values(@code, @id, @name,@phone1, @phone2, @phone3,@platform, @contents, @date)";
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@code", adv.Code);
                cmd.Parameters.AddWithValue("@id", adv.ID);
                cmd.Parameters.AddWithValue("@name", adv.Name);
                cmd.Parameters.AddWithValue("@phone1", adv.Phone1);
                cmd.Parameters.AddWithValue("@phone2", adv.Phone2);
                cmd.Parameters.AddWithValue("@phone3", adv.Phone3);
                cmd.Parameters.AddWithValue("@platform", adv.Platform);
                cmd.Parameters.AddWithValue("@contents", adv.Contents);
                cmd.Parameters.AddWithValue("@date", adv.Date);


                int iRowsAffect = cmd.ExecuteNonQuery();
                return (iRowsAffect > 0);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return false;
            }
        }

        public bool Insert(Advice adv)
        {
            string sql = @"insert into advice(code, name, phone1, phone2, phone3,platform, contents, date, isreply, repdate, repid, repname, repcontents)
values(@code, @name,@phone1, @phone2, @phone3,@platform, @contents, @date, @isreply, @repdate, @repid, @repname, @repcontents)";
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@code", adv.Code);
                cmd.Parameters.AddWithValue("@name", adv.Name);
                cmd.Parameters.AddWithValue("@phone1", adv.Phone1);
                cmd.Parameters.AddWithValue("@phone2", adv.Phone2);
                cmd.Parameters.AddWithValue("@phone3", adv.Phone3);
                cmd.Parameters.AddWithValue("@platform", adv.Platform);
                cmd.Parameters.AddWithValue("@contents", adv.Contents);
                cmd.Parameters.AddWithValue("@date", adv.Date);
                cmd.Parameters.AddWithValue("@isreply", adv.IsReply);
                cmd.Parameters.AddWithValue("@repdate", adv.RepDate);
                cmd.Parameters.AddWithValue("@repid", adv.RepID);
                cmd.Parameters.AddWithValue("@repname", adv.RepName);
                cmd.Parameters.AddWithValue("@repcontents", adv.RepContents);


                int iRowsAffect = cmd.ExecuteNonQuery();
                return (iRowsAffect > 0);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return false;
            }

        }


        public bool Update(Advice adv)
        {

            try
            {

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"UPDATE advice SET name= @name,phone1=@phone1, phone2=@phone2,phone3 =@phone3,
platform=@platform, contents=@contents, date=@date,isreply= @isreply, repdate=@repdate,repid= @repid, repname=@repname,repcontents= @repcontents WHERE code=@code";
                cmd.Parameters.AddWithValue("@code", adv.Code);
                cmd.Parameters.AddWithValue("@name", adv.Name);
                cmd.Parameters.AddWithValue("@phone1", adv.Phone1);
                cmd.Parameters.AddWithValue("@phone2", adv.Phone2);
                cmd.Parameters.AddWithValue("@phone3", adv.Phone3);
                cmd.Parameters.AddWithValue("@platform", adv.Platform);
                cmd.Parameters.AddWithValue("@contents", adv.Contents);
                cmd.Parameters.AddWithValue("@date", adv.Date);
                cmd.Parameters.AddWithValue("@isreply", adv.IsReply);
                cmd.Parameters.AddWithValue("@repdate", adv.RepDate);
                cmd.Parameters.AddWithValue("@repid", adv.RepID);
                cmd.Parameters.AddWithValue("@repname", adv.RepName);
                cmd.Parameters.AddWithValue("@repcontents", adv.RepContents);

                int iRowsAffect = cmd.ExecuteNonQuery();
                return (iRowsAffect > 0);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return false;
            }
        }

        public bool Delete(string code)
        {
            try
            {

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = $"UPDATE advice SET isdelete='Y', deldate = @deldate WHERE code=@code";

                cmd.Parameters.AddWithValue("@code", code);
                cmd.Parameters.AddWithValue("@deldate", DateTime.Now);
                int iRowsAffect = cmd.ExecuteNonQuery();
                return (iRowsAffect > 0);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return false;
            }
        }



        public void Dispose()
        {
            conn.Close();
        }
    }
    }
