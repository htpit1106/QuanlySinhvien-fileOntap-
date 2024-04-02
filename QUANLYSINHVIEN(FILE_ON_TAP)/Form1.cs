using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QUANLYSINHVIEN_FILE_ON_TAP_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable dt= new DataTable();
        string chuoiketnoi = "Data Source=DESKTOP-GE1C4PK\\SQLEXPRESS;Initial Catalog=QuanLySinhVien;Integrated Security=True";
        void loadData()
        {
            cmd = conn.CreateCommand();
            cmd.CommandText = "select * from sinhvien";
            adapter.SelectCommand = cmd;
            dt.Clear();
            adapter.Fill(dt);

            dataGridView1.DataSource = dt;
            
        }
        void runCommand(string query)
        {
            cmd = conn.CreateCommand();
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            loadData();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(chuoiketnoi);
            conn.Open();
            loadData();

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            cmd = conn.CreateCommand();
            string gioitinh;
            if (radioButton1.Checked)
            {
                gioitinh = "Nam";
            }
            else gioitinh = "Nu";
            string query = "insert into sinhvien values (@ma, @hoten, @ngaysinh, @gioitinh, @noisinh)";
            cmd.Parameters.AddWithValue("@ma", tb_masv.Text);
            cmd.Parameters.AddWithValue("@hoten", tb_hoten.Text);
            cmd.Parameters.AddWithValue("@ngaysinh", ngaysinh.Value.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@gioitinh", gioitinh);
            cmd.Parameters.AddWithValue("@noisinh", cb_noisinh.Text);

            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            loadData();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            string msv = dataGridView1.Rows[i].Cells[0].Value.ToString();
            string query = "delete from sinhvien where [Mã SV] = " + msv;
            runCommand(query);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string gioitinh;
            if (radioButton1.Checked)
            {
                gioitinh = "Nam";
            }
            else gioitinh = "Nu";

            int i = dataGridView1.CurrentRow.Index;
            string msv = dataGridView1.Rows[i].Cells[0].Value.ToString();
            string query = "update sinhvien set [Mã SV] = @ma, [Họ tên]= @hoten, [Ngày sinh] = @ngaysinh, [Giới tinh]=@gioitinh, [Nơi sinh] = @noisinh" +
                " where  [Mã SV] = @ma";
            cmd.Parameters.AddWithValue("@ma", msv);
            cmd.Parameters.AddWithValue("@hoten", tb_hoten.Text);
            cmd.Parameters.AddWithValue("@ngaysinh", ngaysinh.Value.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@gioitinh", gioitinh);
            cmd.Parameters.AddWithValue("@noisinh", cb_noisinh.Text);

            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            loadData();


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            if (row != null) {
                tb_masv.Text = row.Cells[0].Value.ToString();
                tb_hoten.Text = row.Cells[1].Value.ToString();
                ngaysinh.Value = Convert.ToDateTime(row.Cells[2].Value);
                string gioitinh = row.Cells[3].Value.ToString();
                if (gioitinh == "Nam") radioButton1.Checked = true;
                else radioButton2.Checked = true;
                cb_noisinh.Text = row.Cells[4].Value.ToString();
            
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.ShowDialog();
            loadData();
        }
    }
}
