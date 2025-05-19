using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;

namespace WinFormsAppTest0915
{
    public class Employee
    {
        public string ID { get; set; }
        public string Pwd { get; set; }

        public string Name { get; set; }

        public DateTime Birth { get; set; }

        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }

        public string Zip { get; set; }

        public string Addr1 { get; set; }

        public string Addr2 { get; set; }
        public string Email { get; set; }

        public string SNS { get; set; }
        public int Salary { get; set; }
        public int Incen { get; set; }

        public string Task { get; set; }
        public DateTime HireDate { get; set; }
        public string IsQuit { get; set; }
        public DateTime QuitDate { get; set; }
        public string Memo { get; set; }
        public string Reservation { get; set; }


    }
    public class EmpDAC : IDisposable
    {
        MySqlConnection conn;

        public EmpDAC()
        {
            conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["project"].ConnectionString);
            conn.Open();
        }

        public int IDCheck(string id)
        {
            string sql = "select count(*) from employee where id = @id";
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

        public int Insert(Employee emp)
        {
            string sql = @"insert into employee(id, pwd, name,hiredate) values(@id, @pwd, @name,@hiredate)";
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", emp.ID);
                cmd.Parameters.AddWithValue("@pwd", emp.Pwd);
                cmd.Parameters.AddWithValue("@name", emp.Name);
                cmd.Parameters.AddWithValue("@hiredate", emp.HireDate);


                return cmd.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return -1;
            }

        }
        public Employee Login(string id)
        {
            try
            
            {
                string sql = "select id, pwd, name from employee where id=@id";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Employee loginEmp = new Employee();
                    loginEmp.ID = reader["id"].ToString();
                    loginEmp.Pwd = reader["pwd"].ToString();
                    loginEmp.Name = reader["name"].ToString();


                    return loginEmp;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
                return null;
            }
        }

        public DataTable MyInfo(string id)
        {
            try
            {
                string sql = "select id, pwd, name, birth, phone1, phone2, phone3, zipcode, addr1, addr2, email,sns,hiredate,isquit,quitdate,task,incen,salary from employee where id=@id";

                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("@id", id);
                da.Fill(dt);
                return dt;
    
                
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return null;
            }
        }

        public DataTable GetEmpInfo() //'관리자'로 로그인했을 때 frmEmpMg 데이터 그리드 뷰에 뜰 데이터 불러오기
        {
            string sql = @"SELECT id, pwd, e.name , ifnull(birth,'2021-01-01:00:00:00')birth, phone1, phone2, phone3,concat(c.name, '-', phone2,'-', phone3) phone, email, ifnull(task,'0') task, ct.name taskname, salary, incen, sns, 
hiredate, isquit,  quitdate
FROM employee e
left outer join commoncode c on c.code = phone1
left outer join commoncode ct on ct.code = task ";

            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
            da.Fill(dt);
            return dt;

        }

        public DataTable GetResEmpInfo() //'관리자'로 로그인했을 때 frmEmpMg 데이터 그리드 뷰에 뜰 데이터 불러오기
        {
            string sql = @"SELECT id, e.name, ifnull(task,'0') task, ct.name taskname,
FROM employee e
inner join commoncode ct on ct.code = task 
where isquit ='N'";

            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
            da.Fill(dt);
            return dt;

        }

        public bool Update(Employee emp)
        {
            try
            {

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"UPDATE employee SET  pwd=@pwd, birth=@birth, phone1=@phone1, phone2=@phone2, phone3=@phone3, zipcode=@zipcode, addr1=@addr1, addr2=@addr2, email=@email,sns=@sns WHERE id=@id";
                cmd.Parameters.AddWithValue("@id", emp.ID);
                cmd.Parameters.AddWithValue("@pwd", emp.Pwd);
                cmd.Parameters.AddWithValue("@birth", emp.Birth);
                cmd.Parameters.AddWithValue("@phone1", emp.Phone1);
                cmd.Parameters.AddWithValue("@phone2", emp.Phone2);
                cmd.Parameters.AddWithValue("@phone3", emp.Phone3);
                cmd.Parameters.AddWithValue("@zipcode",emp.Zip);
                cmd.Parameters.AddWithValue("@addr1", emp.Addr1);
                cmd.Parameters.AddWithValue("@addr2", emp.Addr2);
                cmd.Parameters.AddWithValue("@email", emp.Email);
                cmd.Parameters.AddWithValue("@sns", emp.SNS);
                int iRowsAffect = cmd.ExecuteNonQuery();
                return (iRowsAffect > 0);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return false;
            }
        }

        public bool UpdateByAdmin(Employee emp)
        {
            try
            {

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"UPDATE employee SET
task=@task,salary=@salary,incen=@incen,isquit=@isquit,quitdate=@quitdate
WHERE id=@id";
                cmd.Parameters.AddWithValue("@id", emp.ID);
                cmd.Parameters.AddWithValue("@task", emp.Task);
                cmd.Parameters.AddWithValue("@salary", emp.Salary);
                cmd.Parameters.AddWithValue("@incen", emp.Incen);
                cmd.Parameters.AddWithValue("@isquit", emp.IsQuit);
                cmd.Parameters.AddWithValue("@quitdate", emp.QuitDate);

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
                cmd.CommandText = $"UPDATE employee SET isquit='Y', quitdate = @quitdate WHERE id=@id";

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


        public void Dispose()
        {
            conn.Close();
        }
    }
}
