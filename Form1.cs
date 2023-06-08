using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.NetworkInformation;

namespace QuanLyKhachSan
{
    public partial class Form1 : Form
    {   
        // Khai báo một biến để lưu đường dẫn hình ảnh con mắt mở
        private string eyeOpenImagePath = "C:\\Users\\nvduy\\Downloads\\eye-icon-vector-illustration.jpg";

        // Khai báo một biến để lưu đường dẫn hình ảnh con mắt đóng
        private string eyeCloseImagePath = "C:\\Users\\nvduy\\Downloads\\th.jpg";

        private void TogglePasswordVisibility()
        {
            if (txtMatKhau.UseSystemPasswordChar)
            {
                // Nếu đang hiển thị dạng password, thay đổi thành hiển thị dạng văn bản
                txtMatKhau.UseSystemPasswordChar = false;
                // Thay đổi hình ảnh của PictureBox thành hình ảnh con mắt đóng
                pictureBox4.Image = Image.FromFile(eyeCloseImagePath);
            }
            else
            {
                // Ngược lại, nếu đang hiển thị dạng văn bản, thay đổi thành hiển thị dạng password
                txtMatKhau.UseSystemPasswordChar = true;
                // Thay đổi hình ảnh của PictureBox thành hình ảnh con mắt mở
                pictureBox4.Image = Image.FromFile(eyeOpenImagePath);
            }
        }

       
        public Form1()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=JAVIS\\SQLEXPRESS;Initial Catalog=QuanLyKhachSan;Integrated Security=True";
            string tenTaiKhoan = txtTenTaiKhoan.Text;
            string matKhau = txtMatKhau.Text;
            if (tenTaiKhoan.Trim() == "" || matKhau.Trim() == "") { MessageBox.Show("Vui lòng nhập đầy đủ thông tin!"); }
            // Thực hiện truy vấn vào cơ sở dữ liệu
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "SELECT LoaiTaiKhoan FROM TaiKhoan WHERE TenTaiKhoan = @TenTaiKhoan AND MatKhau = @MatKhau";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@TenTaiKhoan", tenTaiKhoan);
            cmd.Parameters.AddWithValue("@MatKhau", matKhau);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                string loaiTaiKhoan = reader["LoaiTaiKhoan"].ToString();

                // Kiểm tra và xử lý theo vai trò của người dùng
                if (loaiTaiKhoan == "QUANLY")
                {
                    // Xử lý với vai trò quản lý
                }
                else if (loaiTaiKhoan == "LETAN")
                {
                    // Xử lý với vai trò lễ tân
                }
                else if (loaiTaiKhoan == "ADMIN")
                {
                    // Xử lý với vai trò admin
                    MessageBox.Show("dang nhap thanh cong");
                }
                else
                {
                    // Thông báo lỗi vai trò không hợp lệ
                }
            }
            else
            {
                // Thông báo lỗi đăng nhập không thành công
            }

            reader.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            TogglePasswordVisibility();
        }
    }
}
