using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace WinFormsAppTest0915
{
    public class OrderMaster
    {
        public string Code { get; set; }
        public int TotPrice { get; set; }

        public int TotQty { get; set; }

        public int Tracking { get; set; }
        public string Payopt { get; set; }
        public string IsMem { get; set; }
        public string MemID { get; set; }
        public string Detail { get; set; }
    }

    public class OrderDetail
    {
        public string DetailCode { get; set; }
        public string MasterCode { get; set; }
        public string Product { get; set; }
        public int Price { get; set; }
        public int Qty { get; set; }
        
    }



    class OrderDAC : IDisposable
    {
        MySqlConnection conn;
        public OrderDAC()
        {

            conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["project"].ConnectionString);
            conn.Open();
        }

     

        public void MemOrder(OrderMaster master, OrderDetail[] products)
        {
            //if(카트에 담긴 게 상품 2개)
            //OrderMaster 테이블 insert 1건
            //OrderDetail 테이블에 insert 2건
            //member일 경우 member 테이블에 update 1건(포인트, 구매금액)
            //nonmember 일 경우 insert 1건(주소 등 비회원 정보)
            MySqlTransaction trans = conn.BeginTransaction();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"insert into ordermaster(code, totqty, totprice, payopt,ismem,memid,detail )
        values( @code,  @totqty, @totprice, @payopt,'Y',@memid ,@detail)";
                cmd.Transaction = trans;
                cmd.Parameters.AddWithValue("@code", master.Code);
                cmd.Parameters.AddWithValue("@totprice", master.TotPrice);
                cmd.Parameters.AddWithValue("@totqty", master.TotQty);
                cmd.Parameters.AddWithValue("@detail", master.Detail);
                cmd.Parameters.AddWithValue("@payopt", master.Payopt);
                cmd.Parameters.AddWithValue("@memid", master.MemID);
          
                cmd.ExecuteNonQuery();

                MySqlCommand cmd2 = new MySqlCommand();
                cmd2.Connection = conn;
                cmd2.CommandText = @"insert into orderdetail(detailcode,mastercode,product,price,qty) 
values (@detailcode,@mastercode,@product,@price,@qty);";
                cmd2.Transaction = trans;
                cmd2.Parameters.Add("@detailcode", MySqlDbType.VarChar);
                cmd2.Parameters.Add("@mastercode", MySqlDbType.VarChar);
                cmd2.Parameters.Add("@product", MySqlDbType.VarChar);
                cmd2.Parameters.Add("@price", MySqlDbType.Int32);
                cmd2.Parameters.Add("@qty", MySqlDbType.Int32);

                
                MySqlCommand cmd3 = new MySqlCommand();
                cmd3.Connection = conn;
                cmd3.CommandText = "update member set points = points +  @points, amass = amass + @amass where id = @id;";
                cmd3.Transaction = trans; 
                cmd3.Parameters.AddWithValue("@points", Math.Round( master.TotPrice*0.01));
                cmd3.Parameters.AddWithValue("@amass", master.TotPrice);
                cmd3.Parameters.AddWithValue("@id", master.MemID);
                cmd3.ExecuteNonQuery();

                for (int i = 0; i < products.Length; i++)
                {

                    cmd2.Parameters["@detailcode"].Value = products[i].DetailCode;
                    cmd2.Parameters["@mastercode"].Value = products[i].MasterCode;
                    cmd2.Parameters["@product"].Value = products[i].Product;
                    cmd2.Parameters["@price"].Value = products[i].Price;
                    cmd2.Parameters["@qty"].Value = products[i].Qty;

                    cmd2.ExecuteNonQuery();
                    
                }

                trans.Commit();
            }
            catch (Exception err)
            {
                trans.Rollback();
                throw err;
            }
        }

        //internal bool Delete(string text)
        //{

        //    //try
        //    //{

        //    //    MySqlCommand cmd = new MySqlCommand();
        //    //    cmd.Connection = conn;
        //    //    cmd.CommandText = $"UPDATE product SET deleted='Y' WHERE code=@code";

        //    //    cmd.Parameters.AddWithValue("@code", prdCode);
        //    //    int iRowsAffect = cmd.ExecuteNonQuery();
        //    //    return (iRowsAffect > 0);
        //    //}
        //    //catch (Exception err)
        //    //{
        //    //    MessageBox.Show(err.Message);
        //    //    return false;
        //    //}
        //}

        //internal bool Update()
        //{
        //    //try
        //    //{

        //    //    MySqlCommand cmd = new MySqlCommand();
        //    //    cmd.Connection = conn;
        //    //    cmd.CommandText = $"UPDATE product SET name =@name,detail=@detail,qty=@qty,price=@price, image =@image WHERE code=@code ";

        //    //    cmd.Parameters.AddWithValue("@name", prd.Name);
        //    //    cmd.Parameters.AddWithValue("@detail", prd.Detail);
        //    //    cmd.Parameters.AddWithValue("@qty", prd.Qty);
        //    //    cmd.Parameters.AddWithValue("@price", prd.Price);
        //    //    cmd.Parameters.AddWithValue("@image", prd.Image);
        //    //    cmd.Parameters.AddWithValue("@code", prd.Code);

        //    //    int iRowsAffect = cmd.ExecuteNonQuery();
        //    //    return (iRowsAffect > 0);
        //    //}
        //    //catch (Exception err)
        //    //{
        //    //    MessageBox.Show(err.Message);
        //    //    return false;
        //    //} 
        //}

        public void NonMemOrder(NonMember nonmem, OrderMaster master, OrderDetail[] products)
        {
            //if(카트에 담긴 게 상품 2개)
            //OrderMaster 테이블 insert 1건
            //OrderDetail 테이블에 insert 2건
            //member일 경우 member 테이블에 update 1건(포인트, 구매금액)
            //nonmember 일 경우 insert 1건(주소 등 비회원 정보)
            MySqlTransaction trans = conn.BeginTransaction();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"insert into ordermaster(code, totqty, totprice, payopt,ismem,detail )
        values( @code,  @totqty, @totprice, @payopt, 'N',@detail )";
                cmd.Parameters.AddWithValue("@code", master.Code);
                cmd.Parameters.AddWithValue("@totprice", master.TotPrice);
                cmd.Parameters.AddWithValue("@totqty", master.TotQty);
                cmd.Parameters.AddWithValue("@detail", master.Detail);
                cmd.Parameters.AddWithValue("@payopt", master.Payopt);
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();

                MySqlCommand cmd2 = new MySqlCommand();
                cmd2.Connection = conn;
                cmd2.CommandText = @"insert into orderdetail(detailcode,mastercode,product,price,qty) 
values (@detailcode,@mastercode,@product,@price,@qty);";
                cmd2.Transaction = trans;
                cmd2.Parameters.Add("@detailcode", MySqlDbType.VarChar);
                cmd2.Parameters.Add("@mastercode", MySqlDbType.VarChar);
                cmd2.Parameters.Add("@product", MySqlDbType.VarChar);
                cmd2.Parameters.Add("@price", MySqlDbType.Int32);
                cmd2.Parameters.Add("@qty", MySqlDbType.Int32);

                MySqlCommand cmd3 = new MySqlCommand();
                cmd3.Connection = conn;
                cmd3.CommandText = "insert into nonmemorder (code, logincode, email,zipcode, addr1, addr2) values(@code,@logincode,@email,@zipcode,@addr1,@addr2);";
                cmd3.Transaction = trans;
                cmd3.Parameters.AddWithValue("@code", master.Code);
                cmd3.Parameters.AddWithValue("@logincode", nonmem.Code);
                cmd3.Parameters.AddWithValue("@email", nonmem.Email);
                cmd3.Parameters.AddWithValue("@zipcode", nonmem.Zip);
                cmd3.Parameters.AddWithValue("@addr1", nonmem.Addr1);
                cmd3.Parameters.AddWithValue("@addr2", nonmem.Addr2);
                cmd3.ExecuteNonQuery();

                for (int i = 0; i < products.Length; i++)
                {

                    cmd2.Parameters["@detailcode"].Value = products[i].DetailCode;
                    cmd2.Parameters["@mastercode"].Value = products[i].MasterCode;
                    cmd2.Parameters["@product"].Value = products[i].Product;
                    cmd2.Parameters["@price"].Value = products[i].Price;
                    cmd2.Parameters["@qty"].Value = products[i].Qty;

                    cmd2.ExecuteNonQuery();

                }

                trans.Commit();
            }
            catch (Exception err)
            {
                trans.Rollback();
                throw err;
            }
        }

        public DataTable GetMemOrderInfo()
        {
            string sql = @"select om.code, m.name as name,  ismem, memid, totqty, totprice, paydate, cp.name as payoptname, payopt, tracking, d.name as deliveryname, delivery,
detailcode, mastercode, product, qty, price, phone1,concat(c.name, '-', phone2,'-', phone3) phone, m.zipcode, m.addr1, m.addr2, m.email, ifnull(om.detail,'없음')
from OrderMaster om
inner join member m on m.id = memid
left outer join commoncode c on c.code = phone1
left outer join  commoncode d on d.code = delivery
left outer join  commoncode cp on cp.code = payopt
inner join orderdetail od on mastercode = om.code
where om.isdelete = 'N'";
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
            da.Fill(dt);
            return dt;
        }

        public bool SendProduct(string code, int tracking, string delivery)
        {
            try
            {

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = $"UPDATE ordermaster SET tracking=@tracking,delivery=@delivery WHERE code=@code ";
                cmd.Parameters.AddWithValue("@tracking", tracking);
                cmd.Parameters.AddWithValue("@delivery", delivery);

                cmd.Parameters.AddWithValue("@code", code);

                int iRowsAffect = cmd.ExecuteNonQuery();
                return (iRowsAffect > 0);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return false;
            }
        }


        public DataTable GetNonMemOrderInfo()
        {
            string sql = @"select om.code, nl.name as name, ismem, memid, totqty, totprice, paydate, cp.name as payoptname, payopt, tracking, d.name as deliveryname, delivery,
detailcode, mastercode, product, qty, price,
non.logincode, non.zipcode,non.addr1, non.addr2, non.email,nl.code, nl.phone1,concat(c.name, '-', nl.phone2,'-', nl.phone3) phone, ifnull(om.detail,'없음')
from OrderMaster om
inner join orderdetail od on mastercode = om.code
inner join nonmemorder non on non.code = om.code
inner join nonmemlogin nl on nl.code = non.logincode
left outer join commoncode c on c.code = phone1
left outer join  commoncode d on d.code = delivery
left outer join  commoncode cp on cp.code = payopt
where om.isdelete = 'N'";
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
            da.Fill(dt);
            return dt;
        }

        public DataTable GetDetailOrder(string mastercode)
        {
            string sql = @"select mastercode,  detailcode, product, pr. name,od.qty, od.price price
from OrderMaster om
inner join orderdetail od on mastercode = om.code
inner join product pr on product = pr.code
where om.isdelete = 'N' and mastercode = @mastercode;";
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
            da.SelectCommand.Parameters.AddWithValue("@mastercode", mastercode);
            da.Fill(dt);
            return dt;
        }

        public DataTable GetOrderInfo(string id)
        {
            string sql = @"select om.code, m.name as name,  ismem, memid, totqty, totprice, paydate, cp.name as payoptname, payopt, tracking, d.name as deliveryname, delivery,
detailcode, mastercode, product, qty, price, phone1,concat(c.name, '-', phone2,'-', phone3) phone, m.zipcode, m.addr1, m.addr2, m.email,ifnull(om.detail,'없음')
from OrderMaster om
inner join member m on m.id = memid
left outer join commoncode c on c.code = phone1
left outer join  commoncode d on d.code = delivery
left outer join  commoncode cp on cp.code = payopt
inner join orderdetail od on mastercode = om.code
where om.isdelete = 'N'and memid =@memid
group by om.code";

            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
            da.SelectCommand.Parameters.AddWithValue("@memid", id);
            da.Fill(dt);
            return dt;


        }


        public void Dispose()
        {
            conn.Close();
        }
       

    }
}
