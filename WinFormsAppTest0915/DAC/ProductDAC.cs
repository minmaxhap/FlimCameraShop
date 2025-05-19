using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace WinFormsAppTest0915
{
   
    public class Product
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public string Detail { get; set; }

        public int Qty { get; set; }

        public int Price { get; set; }
        public string Image{ get; set; }

        public string Deleted { get; set; }


    }
    public class ProductDAC :IDisposable
    {
        MySqlConnection conn;

        public ProductDAC()
        {
            conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["project"].ConnectionString);
            conn.Open();
        }

        public int Insert(Product prd)
        {
            try
            {
                string sql = $@"INSERT INTO product(code,name,detail,qty,price,image) 
                            VALUES (@code,@name,@detail,@qty,@price,@image);";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@code", prd.Code);
                cmd.Parameters.AddWithValue("@name", prd.Name);
                cmd.Parameters.AddWithValue("@detail", prd.Detail);
                cmd.Parameters.AddWithValue("@qty", prd.Qty);
                cmd.Parameters.AddWithValue("@price", prd.Price);
                cmd.Parameters.AddWithValue("@Image", prd.Image);


                return cmd.ExecuteNonQuery();

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return -1;
            }
        }

        public DataTable GetPrdInfo()
        {
            try
            {
                string sql = @"SELECT code, name, detail,qty,price,image from product where deleted='N' ";

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
                cmd.CommandText = $"UPDATE product SET image =@image WHERE code=@code";

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
        public bool Update(Product prd)
        {
            try 
            { 
            
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = $"UPDATE product SET name =@name,detail=@detail,qty=@qty,price=@price, image =@image WHERE code=@code ";

                cmd.Parameters.AddWithValue("@name", prd.Name);
                cmd.Parameters.AddWithValue("@detail", prd.Detail);
                cmd.Parameters.AddWithValue("@qty", prd.Qty);
                cmd.Parameters.AddWithValue("@price", prd.Price);
                cmd.Parameters.AddWithValue("@image", prd.Image);
                cmd.Parameters.AddWithValue("@code", prd.Code);

                int iRowsAffect = cmd.ExecuteNonQuery();
                return (iRowsAffect > 0);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return false;
            }
        }

        public bool Delete (string prdCode)
        {
            try
            {

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = $"UPDATE product SET deleted='Y' WHERE code=@code";

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

        public void Dispose()
        {
            conn.Close();
        }

        
    }
}
 