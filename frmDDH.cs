using QLVT_DH;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace QLVT
{
    public partial class frmDDH : Form
    {
        int vitri = 0;
        bool dangThemMoi = false;

        public frmDDH()
        {
            InitializeComponent();
        }

        private void datHangBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsDH.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS);

        }

        private void frmDDH_Load(object sender, EventArgs e)
        {




            DS.EnforceConstraints = false;
            // TODO: This line of code loads data into the 'dS.DatHang' table. You can move, or remove it, as needed.

            this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
            this.datHangTableAdapter.Fill(this.DS.DatHang);
            // TODO: This line of code loads data into the 'DS.CTDDH' table. You can move, or remove it, as needed.
            this.cTDDHTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cTDDHTableAdapter.Fill(this.DS.CTDDH);
            // TODO: This line of code loads data into the 'DS.PhieuNhap' table. You can move, or remove it, as needed.
            this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
            this.phieuNhapTableAdapter.Fill(this.DS.PhieuNhap);
            // TODO: This line of code loads data into the 'DS.Kho' table. You can move, or remove it, as needed.
            this.khoTableAdapter.Connection.ConnectionString = Program.connstr;
            this.khoTableAdapter.Fill(this.DS.Kho);
            // TODO: This line of code loads data into the 'DS.NhanVien1' table. You can move, or remove it, as needed.
            this.nhanVien1TableAdapter.Connection.ConnectionString = Program.connstr;
            this.nhanVien1TableAdapter.Fill(this.DS.NhanVien1);
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

        private void cmbChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbChiNhanh.SelectedValue.ToString() == "System.Data.DataRowView")
                return;
            Program.servername = cmbChiNhanh.SelectedValue.ToString();

            if (cmbChiNhanh.SelectedIndex != Program.mChinhanh)
            {
                Program.mlogin = Program.remotelogin;
                Program.password = Program.remotepassword;
            }
            else
            {
                Program.mlogin = Program.mloginDN;
                Program.password = Program.passwordDN;
            }
            if (Program.KetNoi() == 0)
            {
                MessageBox.Show("Lỗi kết nối về chi nhánh mới", "", MessageBoxButtons.OK);
            }
            else
            {

                /*this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
                this.datHangTableAdapter.Fill(this.DS.DatHang);
                this.cTDDHTableAdapter.Connection.ConnectionString = Program.connstr;
                this.cTDDHTableAdapter.Fill(this.DS.CTDDH);*/

                this.nhanVien1TableAdapter.Connection.ConnectionString = Program.connstr;
                this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
                this.cTDDHTableAdapter.Connection.ConnectionString = Program.connstr;
                this.khoTableAdapter.Connection.ConnectionString = Program.connstr;
                this.vattuTableAdapter.Connection.ConnectionString = Program.connstr;
                this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
                this.nhanVien1TableAdapter.Fill(this.DS.NhanVien1);
                this.khoTableAdapter.Fill(this.DS.Kho);
                this.vattuTableAdapter.Fill(this.DS.Vattu);
                this.datHangTableAdapter.Fill(this.DS.DatHang);
                this.cTDDHTableAdapter.Fill(this.DS.CTDDH);
                this.phieuNhapTableAdapter.Fill(this.DS.PhieuNhap);


            }
        }

        private void cmbNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtMaNV.Text = cmbNV.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void cmbTenKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtMaKho.Text = cmbTenKho.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsDH.Position;
            dangThemMoi = true;
            panelControl2.Enabled = true;
            bdsDH.AddNew();
            dtpNgay.DateTime = DateTime.Now;
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {


            if (bdsPN.Count > 0)
            {
                MessageBox.Show("Không thể xóa đơn đặt hàng này vì đã lập phiếu nhập", "", MessageBoxButtons.OK);
                return;
            }
            if (bdsCTDDH.Count > 0)
            {
                MessageBox.Show("Không thể xóa đơn đặt hàng này vì đã tạo chi tiết phiếu nhập", "", MessageBoxButtons.OK);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa đơn đạt hàng này?", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                String maDDH = ((DataRowView)bdsDH[bdsDH.Position])["MaSoDDH"].ToString();
                try
                {

                    bdsDH.RemoveCurrent();
                    this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.datHangTableAdapter.Update(this.DS.DatHang);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa đơn đặt hàng. Bạn hãy xóa lại\n" + ex.Message, "", MessageBoxButtons.OK);
                    this.datHangTableAdapter.Fill(this.DS.DatHang);
                    bdsDH.Position = bdsDH.Find("MaSoDDH", maDDH);
                    return;
                }
            }
            if (bdsDH.Count == 0) btnXoa.Enabled = false;
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            vitri = bdsDH.Position;
            panelControl2.Enabled = true;
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnReload.Enabled = btnThoat.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;
            gcDatHang.Enabled = false;
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dtpNgay.DateTime > DateTime.Now)
            {
                MessageBox.Show("Ngày lập đơn không hợp lệ", "", MessageBoxButtons.OK);
                return;
            }


            try
            {

                // kiểm tra xem mã đơn đặt hàng có trùng không rồi làm tiếp 
                if (dangThemMoi)
                {


                    dangThemMoi = false;

                    // trước tiên cần kiểm tra xem ngày lập có đang trong tương lai. 




                    string strLenh = "declare @result int exec @result = sp_KiemTraMaDonDatHang '" + txtMSDDH.Text + "' select @result";

                    Program.myReader = Program.ExecSqlDataReader(strLenh);
                    Program.myReader.Read();
                    int result = int.Parse(Program.myReader.GetValue(0).ToString());
                    Program.myReader.Close();

                    if (result == 1)
                    {
                        throw new Exception("Mã đơn đặt hàng đã được sử dụng! Vui lòng chọn mã đơn đặt hàng khác");
                    }


                }

                bdsDH.EndEdit();
                bdsDH.ResetCurrentItem();
                this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
                this.datHangTableAdapter.Update(this.DS.DatHang);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Ghi đơn đặt hàng\n" + ex.Message, "", MessageBoxButtons.OK);
                this.datHangTableAdapter.Fill(this.DS.DatHang);
                bdsDH.Position = vitri;
            }
            txtMSDDH.Enabled = true;
            panelControl2.Enabled = false;
            gcDatHang.Enabled = true;
            btnThem.Enabled = btnXoa.Enabled = btnHieuChinh.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
            btnPhucHoi.Enabled = btnGhi.Enabled = false;
        }

        private void btnPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dangThemMoi)
            {
                dangThemMoi = false;
            }
            bdsDH.CancelEdit();
            if (btnThem.Enabled == false)
            {
                reloadBdsDH();
            }
            else txtMSDDH.Enabled = true;


            panelControl2.Enabled = false;
            gcDatHang.Enabled = true;

            btnThem.Enabled = btnXoa.Enabled = btnHieuChinh.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
            btnPhucHoi.Enabled = btnGhi.Enabled = false;
        }
        private void reloadBdsDH()
        {

            try
            {
                vitri = bdsDH.Position;
                this.datHangTableAdapter.Fill(this.DS.DatHang);
                this.cTDDHTableAdapter.Fill(this.DS.CTDDH);
                bdsDH.Position = vitri;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi reload: " + ex.Message, "", MessageBoxButtons.OK);
                return;
            }
        }

        private void btnReload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            reloadBdsDH();
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Dispose();
        }

        private void cmbMaVT_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtMaVT.Text = cmbMaVT.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnThemCT_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = btnXoa.Enabled = btnHieuChinh.Enabled = btnReload.Enabled = btnThoat.Enabled = true;
            btnPhucHoi.Enabled = btnGhi.Enabled = false;

            bdsCTDDH.AddNew();
            txtMSDDHCT.Text = txtMSDDH.Text;

            btnThemCT.Enabled = btnXoaCT.Enabled = btnHieuChinhCT.Enabled = false;
            btnGhiCT.Enabled = btnPhucHoiCT.Enabled = true;
        }

        private void btnXoaCT_Click(object sender, EventArgs e)
        {
            // xóa đi vật tư đang đứng hiện tại. 
            // thống nhất như này. trước khi sữa - thêm - xóa 1 cái gì đó trên phiếu nhập 
            // nếu như đơn hàng này đã lập phiếu nhập. 
            // viết 1 hàm kiểm tra xem đã lập phiếu nhập chưa 
            if (DDHDaLapPhieuNhap())  // nếu đã lập phiếu nhập rồi thì không chỉnh sữa hay xóa gì cả. 
            {
                MessageBox.Show("Không chỉnh sữa với đơn hàng đã lập phiếu nhập\n", "", MessageBoxButtons.OK);
                return;
            }

            // nếu không ấy thì xóa. 
            if (MessageBox.Show("Bạn có thực muốn xóa chi tiết đơn đặt hàng này không ?", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    bdsCTDDH.RemoveCurrent();
                    this.cTDDHTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.cTDDHTableAdapter.Update(this.DS.CTDDH);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa chi tiết đơn đặt hàng. Bạn hãy xóa lại\n" + ex.Message, "", MessageBoxButtons.OK);
                    this.cTDDHTableAdapter.Fill(this.DS.CTDDH);

                    return;
                }
            }

        }
        private bool DDHDaLapPhieuNhap()
        {
            if (bdsPN.Count != 0) // đã lập phiếu nhập rồi. 
            {
                return true;
            }
            return false;
        }

        private void btnHieuChinhCT_Click(object sender, EventArgs e)
        {
            if (DDHDaLapPhieuNhap())  // nếu đã lập phiếu nhập rồi thì không chỉnh sữa hay xóa gì cả. 
            {
                MessageBox.Show("Không thao tác với đơn hàng đã lập phiếu nhập\n", "", MessageBoxButtons.OK);
                return;
            }
            panelControl3.Enabled = true;
            btnReload.Enabled = btnThem.Enabled = btnXoa.Enabled = btnHieuChinh.Enabled = false;

            btnThemCT.Enabled = btnXoaCT.Enabled = btnHieuChinhCT.Enabled = false;
            btnGhiCT.Enabled = btnPhucHoiCT.Enabled = true;
        }

        private void btnGhiCT_Click(object sender, EventArgs e)
        {
            try
            {
                bdsCTDDH.EndEdit();
                bdsCTDDH.ResetCurrentItem();
                this.cTDDHTableAdapter.Connection.ConnectionString = Program.connstr;
                this.cTDDHTableAdapter.Update(this.DS.CTDDH);



            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Ghi chi tiết đơn đặt hàng\n" + ex.Message, "", MessageBoxButtons.OK);
                this.cTDDHTableAdapter.Fill(this.DS.CTDDH);
            }

            panelControl3.Enabled = false;
            btnThemCT.Enabled = btnXoaCT.Enabled = btnHieuChinhCT.Enabled = true;
            btnGhiCT.Enabled = btnPhucHoiCT.Enabled = false;
            btnReload.Enabled = btnThem.Enabled = btnXoa.Enabled = btnHieuChinh.Enabled = true;
        }

        private void btnPhucHoiCT_Click(object sender, EventArgs e)
        {
            bdsCTDDH.CancelEdit();
            reloadBdsDH();
            panelControl3.Enabled = false;
            gcCTDDH.Enabled = true;

            btnThemCT.Enabled = btnXoaCT.Enabled = btnHieuChinhCT.Enabled = true;
            btnGhiCT.Enabled = btnPhucHoiCT.Enabled = false;

            btnReload.Enabled = btnThem.Enabled = btnXoa.Enabled = btnHieuChinh.Enabled = true;
        }
    }
    
}