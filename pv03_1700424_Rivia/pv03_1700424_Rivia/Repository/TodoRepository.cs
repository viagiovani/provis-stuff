using pv03_1700424_Rivia.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pv03_1700424_Rivia.Repository
{
    class TodoRepository
    {
        string cs = @"server=localhost;userid=root;
                    database=provis_masterdetail;SslMode=none";
        //atribut
        MySqlConnection conn;
        MySqlDataReader rdr;

        //konstruktor
        public TodoRepository()
        {
            conn = null;
            rdr = null;
        }

        //method menampilkan baris data berdasarkan nim
        public List<Todo> getByNim(string nim)
        {
            List<Todo> listTodo = new List<Todo>();
            conn = new MySqlConnection(cs);
            conn.Open();

            string stm = "SELECT * FROM todos where nimMhs = '" + nim + "';";
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Todo temp = new Todo();
                temp.Id = rdr.GetInt32(0);
                temp.NimMhs = rdr.GetString(1);
                temp.Nama = rdr.GetString(2);
                temp.Kategori = rdr.GetString(3);
            
                listTodo.Add(temp);
            }

            conn.Close();
            return listTodo;
        }

        //method menampilkan baris data berdarsar nim dari table mahasiswa
        public Boolean findMahasiswa(string nim)
        {

            conn = new MySqlConnection(cs);
            conn.Open();

            string stm = "SELECT COUNT(Nim) FROM mahasiswas where Nim = '" + nim + "';";
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            rdr = cmd.ExecuteReader();

            Boolean flag = false;
            while (rdr.Read())
            {
                if (rdr.GetInt32(0) > 0)
                {
                    flag = true;
                }
            }
            

            conn.Close();
            return flag;
        }

        //menambah data
        public void addTodo(Todo todo)
        {
            string cs = @"server=localhost;userid=root;
            database=provis_masterdetail;SslMode=none";

            MySqlConnection conn = null;

            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO todos (nimMhs, Nama, Kategori) VALUES(@Nim, @Nama, @Kategori)";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@Nim", todo.NimMhs);
                cmd.Parameters.AddWithValue("@Nama", todo.Nama);
                cmd.Parameters.AddWithValue("@Kategori", todo.Kategori);
                cmd.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error:{0}", ex.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        //update data
        public void updateTodo(Todo todo)
        {

            //buat koneksi
            string cs = @"server=localhost;userid=root;
            database=provis_masterdetail;SslMode=none";

            MySqlConnection conn = null;

            try
            {
                conn = new MySqlConnection(cs);
                conn.Open(); //buka koneksi

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;

                //query update
                cmd.CommandText = "UPDATE todos SET Nama = @Nama, Kategori = @Kategori WHERE Id = @Id";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@Id", todo.Id);
                cmd.Parameters.AddWithValue("@Nama", todo.Nama);
                cmd.Parameters.AddWithValue("@Kategori", todo.Kategori);
                cmd.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error:{0}", ex.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        //delete data
        public void delete(Int64 nim)
        {
            //buat koneksi
            string cs = @"server=localhost;userid=root;
            database=provis_masterdetail;SslMode=none";

            MySqlConnection conn = null;

            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;

                //query delete
                cmd.CommandText = "DELETE FROM todos  WHERE nimMhs = '"+ nim +"';";
                cmd.Prepare();

                cmd.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error:{0}", ex.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

    }
}
