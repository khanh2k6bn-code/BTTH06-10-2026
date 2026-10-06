using System;
using System.Windows.Forms;

namespace Bai5_1_DangKyTaiKhoan
{
    public partial class FrmDangKy : Form
    {
        public FrmDangKy()
        {
            InitializeComponent();
        }

        // Sự kiện khi bấm nút Đăng Ký
        private void btnDangKy_Click(object sender, EventArgs e)
        {
            // 1. Xóa các cảnh báo lỗi cũ
            epCheck.Clear();

            bool isValid = true;

            // 2. Kiểm tra Tên đăng nhập
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text))
            {
                epCheck.SetError(txtTenDangNhap, "Tên đăng nhập không được để trống!");
                isValid = false;
            }

            // 3. Kiểm tra Mật khẩu
            if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                epCheck.SetError(txtMatKhau, "Mật khẩu không được để trống!");
                isValid = false;
            }

            // 4. Kiểm tra Mật khẩu xác nhận khớp với Mật khẩu ban đầu
            if (txtXacNhanMatKhau.Text != txtMatKhau.Text)
            {
                epCheck.SetError(txtXacNhanMatKhau, "Mật khẩu xác nhận không khớp!");
                isValid = false;
            }

            // 5. Kiểm tra độ tuổi (>= 18 tuổi)
            DateTime ngaySinh = dtpNgaySinh.Value;
            int tuoi = DateTime.Now.Year - ngaySinh.Year;
            if (ngaySinh.Date > DateTime.Now.AddYears(-tuoi))
            {
                tuoi--;
            }

            if (tuoi < 18)
            {
                epCheck.SetError(dtpNgaySinh, "Bạn phải đủ 18 tuổi trở lên!");
                isValid = false;
            }

            // 6. Kiểm tra tích chọn Điều khoản dịch vụ
            if (!chkDieuKhoan.Checked)
            {
                epCheck.SetError(chkDieuKhoan, "Bạn phải tích chọn đồng ý với Điều khoản dịch vụ!");
                isValid = false;
            }

            // 7. Thông báo thành công nếu tất cả dữ liệu hợp lệ
            if (isValid)
            {
                MessageBox.Show("Đăng ký tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Sự kiện khi bấm nút Làm Mới
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            txtXacNhanMatKhau.Clear();
            dtpNgaySinh.Value = DateTime.Now;
            rdoNam.Checked = true;
            chkDieuKhoan.Checked = false;

            // Xóa toàn bộ biểu tượng lỗi hiện tại
            epCheck.Clear();

            // Đặt con trỏ nhập liệu vào ô Tên đăng nhập
            txtTenDangNhap.Focus();
        }

        private void btnLamMoi_Click_1(object sender, EventArgs e)
        {

        }
    }
}
