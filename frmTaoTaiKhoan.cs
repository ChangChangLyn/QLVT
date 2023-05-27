using QLVT_DH;
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
using static DevExpress.XtraEditors.Mask.MaskSettings;

namespace QLVT
{
    public partial class frmTaoTaiKhoan : Form
    {
        private string taiKhoan = "";
        private string matKhau = "";
        private string maNhanVien = "";
        private string nhom = "";
        public frmTaoTaiKhoan()
        {
            InitializeComponent();
        }
        private void frmTaoTaiKhoan_Load(object sender, EventArgs e)
        {
            if (Program.mGroup == "CONGTY")
            {
                rbUser.Visible = false;
                rbChiNhanh.Text = "Công ty";
                rbChiNhanh.Checked = true;
                nhom = "CONGTY";
            }
            else
            {
                rbUser.Visible = true;
            }
        }

        private void btnTaoTaiKhoan_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text == "")
            {
                MessageBox.Show("Thiếu mã nhân viên", "Thông báo", MessageBoxButtons.OK);
                txtMaNV.Focus();
                return;
            }

            if (txtTaiKhoan.Text == "")
            {
                MessageBox.Show("Thiếu tài khoản", "Thông báo", MessageBoxButtons.OK);
                txtTaiKhoan.Focus();
                return;
            }

            if (txtMatKhau.Text == "")
            {
                MessageBox.Show("Thiếu mật khẩu", "Thông báo", MessageBoxButtons.OK);
                txtMatKhau.Focus();
                return;
            }

            if (txtXacNhanMK.Text == "")
            {
                MessageBox.Show("Thiếu mật khẩu xác nhận", "Thông báo", MessageBoxButtons.OK);
                txtXacNhanMK.Focus();
                return;
            }

            if (txtMatKhau.Text != txtXacNhanMK.Text)
            {
                MessageBox.Show("Mật khẩu không khớp với mật khẩu xác nhận", "Thông báo", MessageBoxButtons.OK);
                txtMatKhau.Focus();
                return;
            }

            taiKhoan = txtTaiKhoan.Text;
            matKhau = txtMatKhau.Text;
            maNhanVien = Program.maNhanVienDuocChon;
            if (nhom != "CONGTY")
            {
                nhom = (rbChiNhanh.Checked == true) ? "CHINHANH" : "USER";
            }

            String cauTruyVan =
                    "EXEC SP_TaoTaiKhoan '" + taiKhoan + "' , '" + matKhau + "', '"
                    + maNhanVien + "', '" + nhom + "'";

            SqlCommand sqlCommand = new SqlCommand(cauTruyVan, Program.conn);
            try
            {
                Program.myReader = Program.ExecSqlDataReader(cauTruyVan);
                if (Program.myReader == null)
                {
                    return;
                }

                MessageBox.Show("Đăng kí tài khoản thành công\n\nTài khoản: " + taiKhoan + "\nMật khẩu: " + matKhau + "\nMã Nhân Viên: " + maNhanVien + "\nNhóm: " + nhom, "Thông Báo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thực thi database thất bại!\n\n" + ex.Message, "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.Message);
                return;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnNV_Click(object sender, EventArgs e)
        {
            frmNVTaoTaiKhoan f = new frmNVTaoTaiKhoan();
            f.ShowDialog();

            txtMaNV.Text = Program.maNhanVienDuocChon;
        }
    }
}
