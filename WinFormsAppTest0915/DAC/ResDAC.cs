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
    public class Reserve
    {

        public string Code { get; set; }
        public string Name { get; set; }
        public string IsMem { get; set; }
        public string MemID { get; set; }
        public string Email { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        public DateTime Date { get; set; }
        public string Empname { get; set; }
        public string Type { get; set; }
        public string Style { get; set; }
        public int Price { get; set; }
        public DateTime Paydate { get; set; }
        public string Payopt { get; set; }
        public string Isfinished { get; set; }
    }
    class ResDAC :IDisposable
    {
        MySqlConnection conn;



        public ResDAC()
        {
            conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["project"].ConnectionString);
            conn.Open();
        }

        public void Dispose()
        {
            conn.Close();
        }


        public DataTable GetResInfo()
        {
            string sql = @"SELECT r.code as code, r.name , phone1, concat(c.name, '-', phone2,'-', phone3) phone,  ismem, memid, email, date, empname, ct.name as typename, type, style, price, paydate, cp.name as payoptname, payopt, isfinished
from reservation r
left outer join commoncode c on c.code = phone1
left outer join  commoncode ct on ct.code = type
left outer join  commoncode cp on cp.code = payopt
where isdelete = 'N'";

            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
            da.Fill(dt);
            return dt;


        }

        public DataTable GetResInfo(string id)
        {
            string sql = @"SELECT r.code as code, r.name , phone1, concat(c.name, '-', phone2,'-', phone3) phone,  ismem, memid, email, date, empname, ct.name as typename, type, style, price, paydate, cp.name as payoptname, payopt, isfinished
from reservation r
left outer join commoncode c on c.code = phone1
left outer join  commoncode ct on ct.code = type
left outer join  commoncode cp on cp.code = payopt
where isdelete = 'N'
and memid=@memid";

            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
            da.SelectCommand.Parameters.AddWithValue("@memid", id);
            da.Fill(dt);
            return dt;


        }

        public bool Insert(Reserve res)
        {
            try
            {


                string sql = @"insert into reservation (code,name,email, phone1, phone2, phone3, date, type, style, price, payopt, ismem, memid,paydate) 
values (@code,@name, @email, @phone1, @phone2, @phone3, @date, @type, @style, @price, @payopt,@ismem,@memid,@paydate)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@code", res.Code);
                cmd.Parameters.AddWithValue("@name", res.Name);
                cmd.Parameters.AddWithValue("@memid", res.MemID);
                cmd.Parameters.AddWithValue("@ismem", res.IsMem);
                cmd.Parameters.AddWithValue("@email", res.Email);
                cmd.Parameters.AddWithValue("@phone1", res.Phone1);
                cmd.Parameters.AddWithValue("@phone2", res.Phone2);
                cmd.Parameters.AddWithValue("@phone3", res.Phone3);
                cmd.Parameters.AddWithValue("@date", res.Date);
                cmd.Parameters.AddWithValue("@style", res.Style);
                cmd.Parameters.AddWithValue("@price", res.Price);
                cmd.Parameters.AddWithValue("@paydate", DateTime.Now);
                cmd.Parameters.AddWithValue("@payopt", res.Payopt);
                cmd.Parameters.AddWithValue("@type", res.Type);
                // cmd.Parameters.AddWithValue("@isfinished", res.Isfinished);



                int iRowaffect = cmd.ExecuteNonQuery();
                return (iRowaffect > 0);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return false;
            }
        }


        public bool Update(Reserve res)
        {
            try
            {

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"UPDATE reservation SET name= @name,phone1=@phone1, phone2=@phone2,phone3=@phone3, date=@date,ismem=@ismem,
memid=@memid,style=@style,payopt=@payopt,paydate=@paydate,empname=@empname,isfinished=@isfinished,email=@email
 WHERE code=@code";
                cmd.Parameters.AddWithValue("@code", res.Code);
                cmd.Parameters.AddWithValue("@name", res.Name);
                cmd.Parameters.AddWithValue("@phone1", res.Phone1);
                cmd.Parameters.AddWithValue("@phone2", res.Phone2);
                cmd.Parameters.AddWithValue("@phone3", res.Phone3);
                cmd.Parameters.AddWithValue("@date", res.Date);
                cmd.Parameters.AddWithValue("@ismem", res.IsMem);
                cmd.Parameters.AddWithValue("@memid", res.MemID);
                cmd.Parameters.AddWithValue("@style", res.Style);
                cmd.Parameters.AddWithValue("@payopt", res.Payopt);
                cmd.Parameters.AddWithValue("@paydate", res.Paydate);
                cmd.Parameters.AddWithValue("@empname", res.Empname);
                cmd.Parameters.AddWithValue("@isfinished", res.Isfinished);
                cmd.Parameters.AddWithValue("@@email", res.Email);


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
                cmd.CommandText = $"UPDATE reservation SET isdelete='Y', deldate = @deldate WHERE code=@code";

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

        internal void AddEmp(string code, string empInfo,Employee emp)
        {
            MySqlTransaction trans = conn.BeginTransaction();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"update reservation set empname = @empname where code=@code";
                cmd.Parameters.AddWithValue("@code", code);
                cmd.Parameters.AddWithValue("@empname",empInfo);
 
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();

                MySqlCommand cmd2 = new MySqlCommand();
                cmd2.Connection = conn;
                cmd2.CommandText = "update employee set reservation =@reservation where id=@id;";
                cmd2.Transaction = trans;
                cmd2.Parameters.AddWithValue("reservation",code);
                cmd2.Parameters.AddWithValue("id",emp.ID);

                cmd2.ExecuteNonQuery();


                

                trans.Commit();
            }
            catch (Exception err)
            {
                trans.Rollback();
                throw err;
            }
        }
    }
}
