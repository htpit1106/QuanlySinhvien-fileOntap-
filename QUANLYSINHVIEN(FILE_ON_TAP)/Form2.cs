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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable dt = new DataTable();
        string chuoiketnoi = "Data Source=DESKTOP-GE1C4PK\\SQLEXPRESS;Initial Catalog=QuanLySinhVien;Integrated Security=True";
       /* void loadData()
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
        }*/
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            cmd = conn.CreateCommand();
            string query = "select * from sinhvien where 1 = 1";
            if (!string.IsNullOrEmpty(cb_noisinh.Text))
            {
                query += "and [Nơi sinh] = @noisinh ";

            }
            if (!string.IsNullOrEmpty(cb_gioitinh.Text))
            {
                query += "and [Giới tinh] = @gioitinh";
            }

            if (!string.IsNullOrEmpty(tb_masv.Text))
            {
                query += " and [Mã SV] =@ma";
            }
            cmd.Parameters.AddWithValue("@noisinh", cb_noisinh.Text);
            cmd.Parameters.AddWithValue("@gioitinh",cb_gioitinh.Text);
            cmd.Parameters.AddWithValue("@ma", tb_masv.Text);
            cmd.CommandText = query;
            adapter.SelectCommand = cmd;
            dt.Clear();
            adapter.Fill(dt);

            dataGridView1.DataSource = dt;


        }

        private void Form2_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(chuoiketnoi);
            conn.Open();
        }
    }
}
