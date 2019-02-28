using pv03_1700424_Rivia.Model;
using pv03_1700424_Rivia.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pv03_1700424_Rivia
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //method cari
        private void button1_Click(object sender, EventArgs e)
        {
            TodoRepository todo = new TodoRepository();

            tbNim.Enabled = false;
            string nim = tbNim.Text;

            btnAdd.Enabled = true;
            submitNim.Enabled = false;

            //cari nim mahasiswa di tabel mhs
            if (todo.findMahasiswa(nim) == true)
            {
                //jika ada tampilkan
                dataGridView1.DataSource = todo.getByNim(nim);
            }else{
                //tidak ada maka warning
                MessageBox.Show("Data tidak ditemukan","",MessageBoxButtons.OK);
            }
        }

        //method tambah data
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Todo temp = new Todo();

            //isi var
            temp.NimMhs = tbNim.Text;
            temp.Nama = tbNama.Text;
            temp.Kategori = tbKategori.Text;

            TodoRepository todo = new TodoRepository();

            //panggil method tambah
            todo.addTodo(temp);

            string nim = tbNim.Text;
            
            //tampilkan
            dataGridView1.DataSource = todo.getByNim(nim);
        }

       //method hapus data
        private void btnDelete_Click(object sender, EventArgs e)
        {
            Todo temp = new Todo();

            Int64 nim = Convert.ToInt64(tbDelete.Text);
            TodoRepository todo = new TodoRepository();
            todo.delete(nim);
            
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            tbId.Text = row.Cells["id"].Value.ToString();
            tbNamaUpdate.Text = row.Cells["nama"].Value.ToString();
            tbKatUpdate.Text = row.Cells["kategori"].Value.ToString();

            tbId.Enabled = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            tbId.Text = row.Cells["id"].Value.ToString();
            tbNamaUpdate.Text = row.Cells["nama"].Value.ToString();
            tbKatUpdate.Text = row.Cells["kategori"].Value.ToString();

            
        }

        //method update
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Todo temp = new Todo();

            //isi var
            temp.Id = Convert.ToInt16(tbId.Text);
            temp.Nama = tbNamaUpdate.Text;
            temp.Kategori = tbKatUpdate.Text;

            TodoRepository todo = new TodoRepository();

            //panggil method
            todo.updateTodo(temp);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

    }

}
