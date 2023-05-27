using QLVT_DH;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLVT
{
    public partial class frmPhieuXuat : Form
    {
        bool dangThemMoi = false;
        int viTri;

        DataTable vtpx;
        public frmPhieuXuat()
        {
            InitializeComponent();
        }

        private void phieuXuatBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsPX.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS);

        }

        private void frmPhieuXuat_Load(object sender, EventArgs e)
        {
            DS.EnforceConstraints = false;
            // TODO: This line of code loads data into the 'dS.PhieuXuat' table. You can move, or remove it, as needed.
            this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
            this.phieuXuatTableAdapter.Fill(this.DS.PhieuXuat);
            // TODO: This line of code loads data into the 'DS.Kho' table. You can move, or remove it, as needed.
            this.khoTableAdapter.Connection.ConnectionString = Program.connstr;
            this.khoTableAdapter.Fill(this.DS.Kho);
            // TODO: This line of code loads data into the 'DS.NhanVien1' table. You can move, or remove it, as needed.
            this.nhanVien1TableAdapter.Connection.ConnectionString = Program.connstr;
            this.nhanVien1TableAdapter.Fill(this.DS.NhanVien1);
            // TODO: This line of code loads data into the 'DS.CTPX' table. You can move, or remove it, as needed.
            this.cTPXTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cTPXTableAdapter.Fill(this.DS.CTPX);
            // TODO: This line of code loads data into the 'DS.Vattu' table. You can move, or remove it, as needed.
            this.vattuTableAdapter.Connection.ConnectionString = Program.connstr;
            this.vattuTableAdapter.Fill(this.DS.Vattu);

            cmbChiNhanh.DataSource = Program.bds_dspm; //sao chep bds_dspm da load o form dang nhap
            cmbChiNhanh.DisplayMember = "TENCN";
            cmbChiNhanh.ValueMember = "TENSERVER";
            cmbChiNhanh.SelectedIndex = Program.mChinhanh;
            if (Program.mGroup == "CONGTY")
            {
                cmbChiNhanh.Enabled = true;
                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnGhi.Enabled = btnPhucHoi.Enabled = false;
            }
            else
            {
                cmbChiNhanh.Enabled = false;
                btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnGhi.Enabled = btnPhucHoi.Enabled = true;
            }
        }

        private void cmbKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtMaKho.Text = cmbKho.SelectedValue.ToString();
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message);
            }
        }

        private void cmbTenNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtMaNV.Text = cmbTenNV.SelectedValue.ToString();
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message); 
            }
        }

        private void cmbVT_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtMaVTCT.Text = cmbVT.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dangThemMoi = true;
            viTri = bdsPX.Position;
            bdsPX.AddNew();



            dtpNgay.DateTime = DateTime.Now;








            panelControl2.Enabled = true;  // chỉ cho nó chọn các đơn hàng đó thôi. 

            gcPhieuXuat.Enabled = false;
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnThoat.Enabled = btnReload.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;
            txtMaPX.Enabled = true;
            dtpNgay.Enabled = true;
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dtpNgay.DateTime != DateTime.Now.Date)
            {
                MessageBox.Show("Không được phép hiệu chỉnh", "", MessageBoxButtons.OK);
                return;
            }
            if (bdsCTPX.Count > 0)
            {
                MessageBox.Show("Không thể xóa phiếu xuất đã lập chi tiết phiếu nhập", "", MessageBoxButtons.OK);
                return;
            }

            if (MessageBox.Show("Bạn có thực muốn xóa phiếu nhập này không ?", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                String msPXXoa = ((DataRowView)bdsPX[bdsPX.Position])["MAPX"].ToString();
                try
                {
                    bdsPX.RemoveCurrent();
                    this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.phieuXuatTableAdapter.Update(this.DS.PhieuXuat);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa phiếu xuất.\n" + ex.Message, "", MessageBoxButtons.OK);
                    this.phieuXuatTableAdapter.Fill(this.DS.PhieuXuat);
                    bdsPX.Position = bdsPX.Find("MAPX", msPXXoa);
                    return;
                }
            }

            if (bdsPX.Count == 0) btnXoa.Enabled = false;
        }

        private void btnHieuChinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dtpNgay.DateTime != DateTime.Now.Date)
            {
                MessageBox.Show("Không được phép hiệu chỉnh", "", MessageBoxButtons.OK);
                return;
            }
            panelControl2.Enabled = true;
            gcPhieuXuat.Enabled = false;
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnThoat.Enabled = btnReload.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;
            txtMaPX.Enabled = false;
            dtpNgay.Enabled = false;
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dtpNgay.DateTime > DateTime.Now)
            {
                MessageBox.Show("Ngày lập phiếu nhập không hợp lệ", "", MessageBoxButtons.OK);
                return;
            } 
            if (Regex.IsMatch(txtHoTenKH.Text, @"^[A-Za-z ]+$") == false)
            {
                MessageBox.Show("Họ tên khách hàng chỉ có chữ cái và khoảng trắng, không được chưa ký tự đặc biệt.", "", MessageBoxButtons.OK);
                return;
            }

            try
            {

                if (dangThemMoi)
                {
                    dangThemMoi = false;
                    string strLenh = "declare @result int exec @result = spKiemTraMaPhieuXuat '" + txtMaPX.Text + "' select @result";

                    Program.myReader = Program.ExecSqlDataReader(strLenh);
                    Program.myReader.Read();
                    int result = int.Parse(Program.myReader.GetValue(0).ToString());
                    Program.myReader.Close();

                    if (result == 1)
                    {
                        throw new Exception("Mã phiếu xuất đã được sử dụng! Vui lòng chọn mã kho khác");
                    }

                }

                bdsPX.EndEdit();
                bdsPX.ResetCurrentItem();
                this.phieuXuatTableAdapter.Connection.ConnectionString = Program.connstr;
                this.phieuXuatTableAdapter.Update(this.DS.PhieuXuat);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Ghi phiếu xuất\n" + ex.Message, "", MessageBoxButtons.OK);
                this.phieuXuatTableAdapter.Fill(this.DS.PhieuXuat);
                bdsPX.Position = viTri;
            }

            panelControl2.Enabled = false;
            gcPhieuXuat.Enabled = true;
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnThoat.Enabled = btnReload.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
        }
        private void reload()
        {
            try
            {
                viTri = bdsPX.Position;
                this.phieuXuatTableAdapter.Fill(this.DS.PhieuXuat);
                this.cTPXTableAdapter.Fill(this.DS.CTPX);
                bdsPX.Position = viTri;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi reload: " + ex.Message, "", MessageBoxButtons.OK);
                return;
            }
        }
        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dangThemMoi)
            {
                dangThemMoi = false;
                reload();
            }
            bdsPX.CancelEdit();


            panelControl2.Enabled = false;
            gcPhieuXuat.Enabled = true;

            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnThoat.Enabled = btnReload.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
        }

        private void btnReload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            reload();
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Dispose();
        }

        private void btnThemCT_Click(object sender, EventArgs e)
        {
            if (dtpNgay.DateTime != DateTime.Now.Date)
            {
                MessageBox.Show("Đơn hàng ngày hôm nay không được phép thao tác", "", MessageBoxButtons.OK);
                return;
            }

            dangThemMoi = true;
            cmbVT.Visible = true;

            bdsCTPX.AddNew();
            txtMaPXCT.Text = txtMaPX.Text;

            panelControl3.Enabled = true;
            txtMaPXCT.Enabled = false;

            string strLenh = "EXEC sp_LayVTPN '" + txtMaPXCT.Text + "'" + ",'"; //+ txt.Text + "'";
            vtpx = Program.ExecSqlDataTable(strLenh);
            BindingSource bdsVTPX = new BindingSource();
            bdsVTPX.DataSource = vtpx;
            cmbVT.DataSource = bdsVTPX;
            cmbVT.DisplayMember = "TENVT";
            cmbVT.ValueMember = "MAVT";
            cmbVT.SelectedIndex = -1;

            // xóa tất cả. 
            txtDonGia.Clear();
            txtSoLuong.Clear();
            txtMaVTCT.Clear();

            btnGhiCT.Enabled = btnPhucHoiCT.Enabled = true;
            btnThemCT.Enabled = btnXoaCT.Enabled = btnHieuChinhCT.Enabled = false;
            btnThem.Enabled = btnXoa.Enabled = btnHieuChinh.Enabled = btnReload.Enabled = false;
        }
    }
}
