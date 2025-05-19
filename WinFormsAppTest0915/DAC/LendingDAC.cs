using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;

namespace WinFormsAppTest0915
{
    public class Lending
    {
   
        public string Code { get; set; } //카메라 대여 코드 L+날짜 난수
        public string IsMem { get; set; }
        public string MemID { get; set; }
        public string LoginCode { get; set; }
        public DateTime lendingdate { get; set; }
        public string IsReturn { get; set; }
        public string Camera { get; set; }
        public string Payopt { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        //public DateTime StartDate { get; set; }
        //public DateTime ReturnDate { get; set; }
    }
    public class Camera
    {
        public string Code { get; set; } //카메라 대여 코드 C+날짜 난수
        public string Name { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public string LendingState { get; set; }
        public string Deleted { get; set; }
        public string Detail { get; internal set; }
        public int Qty { get; internal set; }
    }

    public class LendingDAC :IDisposable
    {
        MySqlConnection conn;
        public LendingDAC()
        {
            conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["project"].ConnectionString);
            conn.Open();
        }


        public int Insert(Camera cam)
        {
            try 
            {
                string sql = @"Insert into camera(code,name,detail,price,image,qty) values(@code,@name,@detail,@price,@image,@qty)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@code", cam.Code);
                cmd.Parameters.AddWithValue("@name", cam.Name);
                cmd.Parameters.AddWithValue("@detail", cam.Detail);
                cmd.Parameters.AddWithValue("@price", cam.Price);
                cmd.Parameters.AddWithValue("@image", cam.Image);
                cmd.Parameters.AddWithValue("@qty", cam.Qty);
                //cmd.Parameters.AddWithValue("@lendingstate", cam.LendingState);



                return cmd.ExecuteNonQuery();


            }
            catch(Exception err)
            {
                Debug.WriteLine(err);
                return -1;
            }
        }

        public DataTable GetLendingInfo()
        {
            try
            {
                string sql = @"select  ca.code cameracode, ca.name cameraname, detail, qty, price, image, lendingstate, l.code lendingcode, ismem, memid, logincode,lendingdate, isreturn, 
camera, payopt, cp.name as payoptname,paydate,l.phone1, concat(c.name, '-', l.phone2,'-', l.phone3) phone, email, l.name
from camera ca
inner join lending l  on l.camera = ca.code
left outer join commoncode c on c.code = phone1
left outer join  commoncode cp on cp.code = payopt";

                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                da.Fill(dt);
                return dt;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return null;
            }


        }
        public DataTable GetLendingInfo(string id)
        {
            try
            {
                string sql = @"select  ca.code cameracode, ca.name cameraname, detail, qty, price, image, lendingstate, l.code lendingcode,memid, lendingdate, isreturn, 
camera, payopt, cp.name as payoptname,paydate,startdate,returndate
from camera ca
inner join lending l  on l.camera = ca.code
left outer join  commoncode cp on cp.code = payopt
where memid = @memid";

                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("@memid", id);
                da.Fill(dt);
                return dt;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return null;
            }


        }
        public DataTable GetCamInfo()
        {
            try
            {
                string sql = @"SELECT code, name, detail,qty,price,image, lendingstate from camera where deleted='N' ";

                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                da.Fill(dt);
                return dt;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return null;
            }


        }



        public bool InsertImage(string prdCode, string imgFile)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = $"UPDATE camera SET image =@image WHERE code=@code";

                cmd.Parameters.AddWithValue("@image", imgFile);
                cmd.Parameters.AddWithValue("@code", prdCode);


                int iRowsAffect = cmd.ExecuteNonQuery();
                return (iRowsAffect > 0);

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return false;
            }
        }
        public bool Update(Camera cam)
        {
            try
            {

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = $"UPDATE camera SET name =@name,detail=@detail,qty=@qty,price=@price, image =@image WHERE code=@code ";

                cmd.Parameters.AddWithValue("@name", cam.Name);
                cmd.Parameters.AddWithValue("@detail", cam.Detail);
                cmd.Parameters.AddWithValue("@qty", cam.Qty);
                cmd.Parameters.AddWithValue("@price", cam.Price);
                cmd.Parameters.AddWithValue("@image", cam.Image);
                cmd.Parameters.AddWithValue("@code", cam.Code);

                int iRowsAffect = cmd.ExecuteNonQuery();
                return (iRowsAffect > 0);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return false;
            }
        }

        public bool Delete(string camCode)
        {
            try
            {

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = $"UPDATE camera SET deleted='Y' WHERE code=@code";

                cmd.Parameters.AddWithValue("@code", camCode);
                int iRowsAffect = cmd.ExecuteNonQuery();
                return (iRowsAffect > 0);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return false;
            }
        }


        public void MemOrder(Member mem, Lending lending, Camera camera)
        {
            //lending 테이블 insert 1건
            //camera 테이블에 update 1건
            //member일 경우 member 테이블에 update 1건(포인트, 구매금액)
            //nonmember 일 경우 insert 1건(주소 등 비회원 정보)
            MySqlTransaction trans = conn.BeginTransaction();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"insert into lending(code, ismem, memid, lendingdate, camera,payopt,paydate,name,email,phone1,phone2,phone3) 
values (@code, 'Y', @memid, @lendingdate,@camera,@payopt,@paydate,@name,@email,@phone1,@phone2,@phone3)";
                cmd.Parameters.AddWithValue("@code", lending.Code);
                cmd.Parameters.AddWithValue("@ismem", 'Y');
                cmd.Parameters.AddWithValue("@memid", mem.ID);
                cmd.Parameters.AddWithValue("@lendingdate", lending.lendingdate);
                cmd.Parameters.AddWithValue("@camera", camera.Code);
                cmd.Parameters.AddWithValue("@payopt", lending.Payopt);
                cmd.Parameters.AddWithValue("@paydate", DateTime.Now);
                cmd.Parameters.AddWithValue("@name", mem.Name);
                cmd.Parameters.AddWithValue("@phone1", mem.Phone1);
                cmd.Parameters.AddWithValue("@phone2", mem.Phone2);
                cmd.Parameters.AddWithValue("@phone3", mem.Phone3);
                cmd.Parameters.AddWithValue("@email", mem.Email);


                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();

                MySqlCommand cmd2 = new MySqlCommand();
                cmd2.Connection = conn;
                cmd2.CommandText = @"update camera set qty = qty - 1, lendingstate = 'Y' where code = @code";
                cmd2.Transaction = trans;
                cmd2.Parameters.AddWithValue("@code", camera.Code);
                cmd2.ExecuteNonQuery();

                MySqlCommand cmd3 = new MySqlCommand();
                cmd3.Connection = conn;
                cmd3.CommandText = "update member set points = points +  @points, amass = amass + @amass where id = @id;";
                cmd3.Transaction = trans;
                cmd3.Parameters.AddWithValue("@points", Math.Round(camera.Price * 0.01));
                cmd3.Parameters.AddWithValue("@amass", camera.Price);
                cmd3.Parameters.AddWithValue("@id", mem.ID);
                cmd3.ExecuteNonQuery();


                trans.Commit();
            }
            catch (Exception err)
            {
                trans.Rollback();
                throw err;
            }
        }

       

        public void NonMemOrder(NonMember nonmem, Lending lending, Camera camera)
        {
            MySqlTransaction trans = conn.BeginTransaction();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"insert into lending(code, ismem, lendingdate, camera, payopt,paydate,name,email,phone1,phone2,phone3) 
values (@code, 'N', @lendingdate,@camera,@payopt,,@paydate,@name,@email,@phone1,@phone2,@phone3,@logincode)";
                cmd.Parameters.AddWithValue("@code", lending.Code);
                cmd.Parameters.AddWithValue("@lendingdate", lending.lendingdate);
                cmd.Parameters.AddWithValue("@camera", camera.Code);
                cmd.Parameters.AddWithValue("@payopt", lending.Payopt);
                cmd.Parameters.AddWithValue("@paydate", DateTime.Now);
                cmd.Parameters.AddWithValue("@name", nonmem.Name);
                cmd.Parameters.AddWithValue("@phone1", nonmem.Phone1);
                cmd.Parameters.AddWithValue("@phone2", nonmem.Phone2);
                cmd.Parameters.AddWithValue("@phone3", nonmem.Phone3);
                cmd.Parameters.AddWithValue("@logincode", nonmem.Code);
                cmd.Parameters.AddWithValue("@email", nonmem.Email);

                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();



                MySqlCommand cmd3 = new MySqlCommand();
                cmd3.Connection = conn;
                cmd3.CommandText = "insert into nonmemorder (code, logincode, email) values(@code,@logincode,@email);";
                cmd3.Transaction = trans;
                cmd3.Parameters.AddWithValue("@code", lending.Code);
                cmd3.Parameters.AddWithValue("@logincode", nonmem.Code);
                cmd3.Parameters.AddWithValue("@email", nonmem.Email);
                cmd3.ExecuteNonQuery();


                trans.Commit();
            }
            catch (Exception err)
            {
                trans.Rollback();
                throw err;
            }
        }
        public void StartLending(string cameraCode)
        {
            MySqlTransaction trans = conn.BeginTransaction();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"update lending set isreturn = 'N', startdate = @startdate where camera = @camera";
                cmd.Parameters.AddWithValue("@camera", cameraCode);
                cmd.Parameters.AddWithValue("@startdate", DateTime.Now);

                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();

                //12시간 대여니까 하루 단위로 수량을 세어야 한다. 
                MySqlCommand cmd2 = new MySqlCommand();
                cmd2.Connection = conn;
                cmd2.CommandText = @"update camera set qty = qty - 1, lendingstate = 'Y' where code = @code";
                cmd2.Parameters.AddWithValue("@code", cameraCode);
                cmd2.Transaction = trans;
                cmd2.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception err)
            {
                trans.Rollback();
                throw err;
            }
     
        }

        public void ReturnCamera(string cameraCode)
        {
            MySqlTransaction trans = conn.BeginTransaction();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"update lending set isreturn = 'Y', returndate=@returndate where camera = @camera";
                cmd.Parameters.AddWithValue("@camera", cameraCode);
                cmd.Parameters.AddWithValue("@returndate", DateTime.Now);

                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();

                //1일 대여니까 하루 단위로 수량을 세어야 한다. 
                MySqlCommand cmd2 = new MySqlCommand();
                cmd2.Connection = conn;
                cmd2.CommandText = @"update camera set qty = qty + 1, lendingstate = 'N' where code = @code";
                cmd2.Parameters.AddWithValue("@code", cameraCode);
                cmd2.Transaction = trans;
                cmd2.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception err)
            {
                trans.Rollback();
                throw err;
            }
        }


        public void Dispose()
        {
            conn.Close();
        }

       
    }
}
