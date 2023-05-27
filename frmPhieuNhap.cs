using DevExpress.XtraPrinting.Native;
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

namespace QLVT
{
    public partial class frmPhieuNhap : Form
    {
        int viTri;
        bool dangThemMoi = false;

        DataTable vtpn; // khai báo để đây và sử dụng chung trong form.

        int slvtctddh; // sử dụng để chứa số lượng vật tư của chi tiết đơn đặt hàng 
        int slvtpnCu;
        public frmPhieuNhap()
        {
            InitializeComponent();
        }

        private void phieuNhapBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsPN.EndEdit();
            this.tableAdapterManager.UpdateAll(this.DS);

        }

        private void frmPhieuNhap_Load(object sender, EventArgs e)
        {
            DS.EnforceConstraints = false;
            // TODO: This line of code loads data into the 'dS.PhieuNhap' table. You can move, or remove it, as needed.
            this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
            this.phieuNhapTableAdapter.Fill(this.DS.PhieuNhap);
            // TODO: This line of code loads data into the 'dS.CTPN' table. You can move, or remove it, as needed.
            this.cTPNTableAdapter.Connection.ConnectionString = Program.connstr;
            this.cTPNTableAdapter.Fill(this.DS.CTPN);
            // TODO: This line of code loads data into the 'DS.NhanVien1' table. You can move, or remove it, as needed.
            this.nhanVien1TableAdapter.Connection.ConnectionString = Program.connstr;
            this.nhanVien1TableAdapter.Fill(this.DS.NhanVien1);
            // TODO: This line of code loads data into the 'DS.Vattu' table. You can move, or remove it, as needed.
            this.vattuTableAdapter.Connection.ConnectionString = Program.connstr;
            this.vattuTableAdapter.Fill(this.DS.Vattu);
            // TODO: This line of code loads data into the 'DS.Kho' table. You can move, or remove it, as needed.
            this.khoTableAdapter.Connection.ConnectionString = Program.connstr;
            this.khoTableAdapter.Fill(this.DS.Kho);


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

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dangThemMoi = true;
            viTri = bdsPN.Position;
            bdsPN.AddNew();
            cmbMSDDH.Visible = true;
            txtMSDDH.Visible = false;

            dtpNgay.DateTime = DateTime.Now;


            string strlenh = "EXEC sp_DDHChuaLapPN";

            DataTable dt = Program.ExecSqlDataTable(strlenh);


            BindingSource bdsDDH = new BindingSource();
            bdsDDH.DataSource = dt;

            cmbMSDDH.DataSource = bdsDDH;

            cmbMSDDH.DisplayMember = "MasoDDH";
            cmbMSDDH.ValueMember = "MasoDDH";
            cmbMSDDH.SelectedIndex = -1; // không chọn mã số đơn hàng nào. 



            panelControl2.Enabled = true;  // chỉ cho nó chọn các đơn hàng đó thôi. 
            panelControl3.Enabled = false;
            gcPhieuNhap.Enabled = false;
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnThoat.Enabled = btnReload.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;
            txtMaPN.Enabled = true;
            dtpNgay.Enabled = true;
            txtMSDDH.Enabled = true;

        }

        private void cmbTenNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtMaNV.Text = cmbTenNV.SelectedValue.ToString();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void panelControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtMaKho.Text = cmbKho.SelectedValue.ToString();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void cmbMSDDH_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*try
            {
                if (cmbMSDDH.SelectedValue.ToString() == "System.Data.DataRowView") return;
                txtMSDDH.Text = cmbMSDDH.SelectedValue.ToString();
                Console.WriteLine(txtMSDDH.Text + " da gan duoc roi ne");
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }*/
            try
            {
                txtMSDDH.Text = cmbMSDDH.SelectedValue.ToString();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void cmbTenVT_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbTenVT.SelectedValue.ToString() == "System.Data.DataRowView") return;


                DataRowCollection rows = vtpn.Rows;
                txtMaVT.Text = (string)rows[cmbTenVT.SelectedIndex]["MAVT"];
                int sl = (int)rows[cmbTenVT.SelectedIndex]["SOLUONG"];
                double donGia = (double)rows[cmbTenVT.SelectedIndex]["DONGIA"];
                txtSoLuong.Text = sl.ToString();
                txtDonGia.Text = donGia.ToString();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
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
                this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
                this.phieuNhapTableAdapter.Fill(this.DS.PhieuNhap);
                
                this.cTPNTableAdapter.Connection.ConnectionString = Program.connstr;
                this.cTPNTableAdapter.Fill(this.DS.CTPN);
               
                this.nhanVien1TableAdapter.Connection.ConnectionString = Program.connstr;
                this.nhanVien1TableAdapter.Fill(this.DS.NhanVien1);

                this.vattuTableAdapter.Connection.ConnectionString = Program.connstr;
                this.vattuTableAdapter.Fill(this.DS.Vattu);


                this.khoTableAdapter.Connection.ConnectionString = Program.connstr;
                this.khoTableAdapter.Fill(this.DS.Kho);
            }
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dtpNgay.DateTime != DateTime.Now.Date)
            {
                MessageBox.Show("Không được phép hiệu chỉnh", "", MessageBoxButtons.OK);
                return;
            }

            if (bdsCTPN.Count > 0)
            {
                MessageBox.Show("Không thể xóa phiếu nhập đã lập chi tiết phiếu nhập", "", MessageBoxButtons.OK);
                return;
            }

            if (MessageBox.Show("Bạn có thực muốn xóa phiếu nhập này không ?", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                String msPNXoa = ((DataRowView)bdsPN[bdsPN.Position])["MAPN"].ToString();

                try
                {
                    bdsPN.RemoveCurrent();
                    this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
                    this.phieuNhapTableAdapter.Update(this.DS.PhieuNhap);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa phiếu nhập. Bạn hãy xóa lại\n" + ex.Message, "", MessageBoxButtons.OK);
                    this.phieuNhapTableAdapter.Fill(this.DS.PhieuNhap);
                    bdsPN.Position = bdsPN.Find("MAPN", msPNXoa);
                    return;
                }
            }

            if (bdsPN.Count == 0) btnXoa.Enabled = false;
        }

        private void btnHieuChinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dtpNgay.DateTime != DateTime.Now.Date)
            {
                MessageBox.Show("Không được phép hiệu chỉnh", "", MessageBoxButtons.OK);
                return;
            }
            // bật cái panel lên 
            panelControl2.Enabled = true; // tiếp cho phép hiệu chỉnh 
            gcPhieuNhap.Enabled = false;
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnThoat.Enabled = btnReload.Enabled = false;
            btnGhi.Enabled = btnPhucHoi.Enabled = true;
            txtMaPN.Enabled = false;
            dtpNgay.Enabled = false;
            txtMSDDH.Enabled = false;
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dtpNgay.DateTime > DateTime.Now)
            {
                MessageBox.Show("Ngày lập phiếu nhập không hợp lệ", "", MessageBoxButtons.OK);
                return;
            }

            try
            {

                if (dangThemMoi)
                {
                    dangThemMoi = false;
                    string strLenh = "declare @result int exec @result = sp_KiemTraMaPhieuNhap '" + txtMaPN.Text + "' select @result";

                    Program.myReader = Program.ExecSqlDataReader(strLenh);
                    Program.myReader.Read();
                    int result = int.Parse(Program.myReader.GetValue(0).ToString());
                    Program.myReader.Close();

                    if (result == 1)
                    {
                        throw new Exception("Mã phiếu xuất đã được sử dụng! Vui lòng chọn mã kho khác");
                    }

                }

                bdsPN.EndEdit();
                bdsPN.ResetCurrentItem();
                this.phieuNhapTableAdapter.Connection.ConnectionString = Program.connstr;
                this.phieuNhapTableAdapter.Update(this.DS.PhieuNhap);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Ghi phiếu nhập\n" + ex.Message, "", MessageBoxButtons.OK);
                this.phieuNhapTableAdapter.Fill(this.DS.PhieuNhap);
                bdsPN.Position = viTri;
            }

            panelControl2.Enabled = false;
            gcPhieuNhap.Enabled = true;
            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnThoat.Enabled = btnReload.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
        }
        private void reloadBdsPN()
        {

            try
            {
                viTri = bdsPN.Position;
                this.phieuNhapTableAdapter.Fill(this.DS.PhieuNhap);
                this.cTPNTableAdapter.Fill(this.DS.CTPN);
                bdsPN.Position = viTri;
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
            }
            bdsPN.CancelEdit();
            if (btnThem.Enabled == false)
            {
                reloadBdsPN();
            }

            panelControl2.Enabled = false;
            gcPhieuNhap.Enabled = true;

            btnThem.Enabled = btnHieuChinh.Enabled = btnXoa.Enabled = btnThoat.Enabled = btnReload.Enabled = true;
            btnGhi.Enabled = btnPhucHoi.Enabled = false;
        }

        private void btnReload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            reloadBdsPN();
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
            cmbTenVT.Visible = true;

            bdsCTPN.AddNew();
            txtMaPNCT.Text = txtMaPN.Text;

            panelControl3.Enabled = true;
            txtMaPNCT.Enabled = false;

            string strLenh = "EXEC sp_LayVTPN '" + txtMaPNCT.Text + "'" + ",'" + txtMSDDH.Text + "'";
            vtpn = Program.ExecSqlDataTable(strLenh);
            BindingSource bdsVTPN = new BindingSource();
            bdsVTPN.DataSource = vtpn;
            cmbTenVT.DataSource = bdsVTPN;
            cmbTenVT.DisplayMember = "TENVT";
            cmbTenVT.ValueMember = "MAVT";
            cmbTenVT.SelectedIndex = -1;

            // xóa tất cả. 
            txtDonGia.Clear();
            txtSoLuong.Clear();
            txtMaVT.Clear();

            btnGhiCT.Enabled = btnPhucHoiCT.Enabled = true;
            btnThemCT.Enabled = btnXoaCT.Enabled = btnHieuChinhCT.Enabled = false;
            btnThem.Enabled = btnXoa.Enabled = btnHieuChinh.Enabled = btnReload.Enabled = false;
        }

        private void btnXoaCT_Click(object sender, EventArgs e)
        {
            try
            {
                string strLenh = "DECLARE @RESULT INT EXEC @RESULT = sp_XuatVT '" + txtMaVT.Text + "','" + txtSoLuong.Text.ToString() + "' " + "SELECT 'result'=@RESULT"; ;
                Console.WriteLine(strLenh);
                Program.myReader = Program.ExecSqlDataReader(strLenh);
                Program.myReader.Read();

                int result = int.Parse(Program.myReader.GetValue(0).ToString());
                Program.myReader.Close();

                if (result == 0)
                {
                    throw new Exception("Xảy ra lỗi trong quá trình thêm cập nhập số lượng tồn");
                }

                bdsCTPN.RemoveCurrent();
                this.cTPNTableAdapter.Connection.ConnectionString = Program.connstr;
                this.cTPNTableAdapter.Update(this.DS.CTPN);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa chi tiết phiếu nhập.\n" + ex.Message, "", MessageBoxButtons.OK);
                this.cTPNTableAdapter.Fill(this.DS.CTPN);
                return;
            }
        }

        private void btnHieuChinhCT_Click(object sender, EventArgs e)
        {
            if (dtpNgay.DateTime != DateTime.Now.Date)
            {
                MessageBox.Show("Không được phép thao tác", "", MessageBoxButtons.OK);
                return;
            }

            panelControl3.Enabled = true;

            this.slvtpnCu = int.Parse(txtSoLuong.Text.ToString());

            // lấy ra số lượng
            string strLenh = "EXEC sp_LaySoLuongVTCTDDH" + "'" + txtMSDDH.Text.ToString() + "'" + ",'" + txtMaVT.Text.ToString() + "'";

            Program.myReader = Program.ExecSqlDataReader(strLenh);
            Program.myReader.Read();
            this.slvtctddh = int.Parse(Program.myReader.GetValue(0).ToString());
            Program.myReader.Close();

            btnGhiCT.Enabled = btnPhucHoiCT.Enabled = true;
            btnThemCT.Enabled = btnXoaCT.Enabled = btnHieuChinhCT.Enabled = false;
            btnThem.Enabled = btnXoa.Enabled = btnHieuChinh.Enabled = btnReload.Enabled = false;
        }

        private void btnGhiCT_Click(object sender, EventArgs e)
        {
            try
            {
                if (dangThemMoi)
                {
                    dangThemMoi = false;

                    DataRowCollection rows = vtpn.Rows;
                    int slctddh = (int)rows[cmbTenVT.SelectedIndex]["SOLUONG"];
                    int slpn = int.Parse(txtSoLuong.Text.ToString());
                    if (slctddh < slpn)
                    {
                        throw new Exception("Số lượng vật tư phiếu nhập không được phép lớn hơn số lượng vật tư chi tiết đặt hàng");
                    }
                    string strLenh = "DECLARE @RESULT INT EXEC @RESULT = sp_NhapVT '" + txtMaVT.Text + "','" + slpn.ToString() + "' " + "SELECT 'result'=@RESULT"; ;

                    Program.myReader = Program.ExecSqlDataReader(strLenh);
                    Program.myReader.Read();

                    int result = int.Parse(Program.myReader.GetValue(0).ToString());
                    Program.myReader.Close();

                    if (result == 0)
                    {
                        throw new Exception("Xảy ra lỗi trong quá trình thêm cập nhập số lượng tồn");
                    }


                }
                else 
                {
                    // kiem tra so luong va chinh sua

                    int slvtpnSau = int.Parse(txtSoLuong.Text.ToString());
                    if (slvtctddh < slvtpnSau)
                    {
                        throw new Exception("Số lượng vật tư phiếu nhập không được phép lớn hơn số lượng vật tư chi tiết đặt hàng");
                    }

                    // in ra
                    Console.WriteLine(slvtpnSau.ToString() + " slvtpnSau");
                    Console.WriteLine(slvtpnCu.ToString() + " slvtpnCu");

                    if (slvtpnSau > slvtpnCu)
                    {
                        // cộng thêm vào kho.
                        int temp = slvtpnSau - slvtpnCu;

                        string strLenh = "DECLARE @RESULT INT EXEC @RESULT = sp_NhapVT '" + txtMaVT.Text + "','" + temp.ToString() + "' " + "SELECT 'result'=@RESULT"; ;
                        Console.WriteLine(strLenh);
                        Program.myReader = Program.ExecSqlDataReader(strLenh);
                        Program.myReader.Read();

                        int result = int.Parse(Program.myReader.GetValue(0).ToString());
                        Program.myReader.Close();

                        if (result == 0)
                        {
                            throw new Exception("Sảy ra lỗi trong quá trình thêm cập nhập số lượng tồn");
                        }




                    }
                    else if (slvtpnCu > slvtpnSau) // nhân viên đã giảm đi số lượng, 
                    {
                        // giảm đi số lượng 
                        int temp = slvtpnCu - slvtpnSau;

                        string strLenh = "DECLARE @RESULT INT EXEC @RESULT = sp_XuatVT '" + txtMaVT.Text + "','" + temp.ToString() + "' " + "SELECT 'result'=@RESULT"; ;
                        Console.WriteLine(strLenh);
                        Program.myReader = Program.ExecSqlDataReader(strLenh);
                        Program.myReader.Read();

                        int result = int.Parse(Program.myReader.GetValue(0).ToString());
                        Program.myReader.Close();

                        if (result == 0)
                        {
                            throw new Exception("Sảy ra lỗi trong quá trình thêm cập nhập số lượng tồn");
                        }

                    }



                }
                bdsCTPN.EndEdit();
                bdsCTPN.ResetCurrentItem();
                this.cTPNTableAdapter.Connection.ConnectionString = Program.connstr;
                this.cTPNTableAdapter.Update(this.DS.CTPN);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Ghi chi tiết phiếu nhập\n" + ex.Message, "", MessageBoxButtons.OK);
                this.cTPNTableAdapter.Fill(this.DS.CTPN);

            }

            btnGhiCT.Enabled = btnPhucHoiCT.Enabled = false;
            btnThemCT.Enabled = btnXoaCT.Enabled = btnHieuChinhCT.Enabled = true;

            btnThem.Enabled = btnXoa.Enabled = btnHieuChinh.Enabled = btnReload.Enabled = true;
            panelControl3.Enabled = false;
            cmbTenVT.Visible = false;
        }

        private void btnPhucHoiCT_Click(object sender, EventArgs e)
        {
            bdsCTPN.CancelEdit();
            reloadBdsPN();
            panelControl3.Enabled = false;
            gcCTPN.Enabled = true;

            btnThem.Enabled = btnXoa.Enabled = btnHieuChinh.Enabled = btnReload.Enabled = true;

            btnGhiCT.Enabled = btnPhucHoiCT.Enabled = false;
            btnThemCT.Enabled = btnXoaCT.Enabled = btnHieuChinhCT.Enabled = true;
        }
    }
}
