using DevExpress.XtraBars;
using DevExpress.XtraRichEdit.Model;
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
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmMain()
        {
            InitializeComponent();
        }
        private Form CheckExists(Type ftype)
        {
            foreach (Form f in this.MdiChildren)
                if (f.GetType() == ftype)
                    return f;
            return null;
        }
        

        private void btnDDH_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmDDH));
            if (frm != null) frm.Activate();
            else
            {
                frmDDH f = new frmDDH();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnDangNhap_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmDangNhap));
            if (frm != null) frm.Activate();
            else
            {
                frmDangNhap f = new frmDangNhap();
                f.MdiParent = this;
                f.Show();
            }
        }
        public void HienThiMenu()
        {
            tssMANV.Text = "Mã NV : " + Program.username;
            tssHOTEN.Text = "Họ tên nhân viên : " + Program.mHoten;
            tssNHOM.Text = "Nhóm : " + Program.mGroup;
            rbnDanhMuc.Visible = rbnNghiepVu.Visible = rbnBaoCao.Visible = true;
            if (Program.mGroup != "USER")
            {
                btnTaoTK.Enabled = true;
            }

        }

        private void btnNhanVien_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmNhanVien));
            if (frm != null) frm.Activate();
            else
            {
                frmNhanVien f = new frmNhanVien();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnKho_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmKho));
            if (frm != null) frm.Activate();
            else
            {
                frmKho f = new frmKho();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnVatTu_ItemClick(object sender, ItemClickEventArgs e)
        {

            Form frm = this.CheckExists(typeof(frmVatTu));
            if (frm != null) frm.Activate();
            else
            {
                frmVatTu f = new frmVatTu();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnTaoTK_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmTaoTaiKhoan));
            if (frm != null) frm.Activate();
            else
            {
                frmTaoTaiKhoan f = new frmTaoTaiKhoan();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnPN_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmPhieuNhap));
            if (frm != null) frm.Activate();
            else
            {
                frmPhieuNhap f = new frmPhieuNhap();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnPX_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(frmPhieuXuat));
            if (frm != null) frm.Activate();
            else
            {
                frmPhieuXuat f = new frmPhieuXuat();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnDSNV_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(Frpt_ReportNhanVien));
            if (frm != null) frm.Activate();
            else
            {
                Frpt_ReportNhanVien f = new Frpt_ReportNhanVien();              
                f.Show();
            }
        }

        private void btnDSVT_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(Frpt_ReportVatTu));
            if (frm != null) frm.Activate();
            else
            {
                Frpt_ReportVatTu f = new Frpt_ReportVatTu();
                f.Show();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void btnDangXuat_ItemClick(object sender, ItemClickEventArgs e)
        {        

            tssMANV.Text = "Mã NV";
            tssHOTEN.Text = "Họ tên nhân viên";
            tssNHOM.Text = "Nhóm";
            btnTaoTK.Enabled = false;

            rbnDanhMuc.Visible = rbnNghiepVu.Visible = rbnBaoCao.Visible = false;

            foreach (Form f in this.MdiChildren)
            {
                f.Close();
            }        
        }

        private void btnDSDDH_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(Frpt_REPORT_DDHkhongcoPhieuNhap));
            if (frm != null) frm.Activate();
            else
            {
                Frpt_REPORT_DDHkhongcoPhieuNhap f = new Frpt_REPORT_DDHkhongcoPhieuNhap();
                f.Show();
            }
        }
    }
}