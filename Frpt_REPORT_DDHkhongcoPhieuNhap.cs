using DevExpress.XtraReports.UI;
using QLVT_DH;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLVT
{
    public partial class Frpt_REPORT_DDHkhongcoPhieuNhap : Form
    {
        public Frpt_REPORT_DDHkhongcoPhieuNhap()
        {
            InitializeComponent();
        }
        private void Frpt_REPORT_DDHkhongcoPhieuNhap_Load(object sender, EventArgs e)
        {
            cmbChiNhanh.DataSource = Program.bds_dspm;
            cmbChiNhanh.DisplayMember = "TENCN";
            cmbChiNhanh.ValueMember = "TENSERVER";
            cmbChiNhanh.SelectedIndex = Program.mChinhanh;

            if (Program.mGroup == "CONGTY")
            {
                cmbChiNhanh.Enabled = true;
            }
            else
            {
                cmbChiNhanh.Enabled = false;
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
                MessageBox.Show("Lỗi kết nối với chi nhánh mới", "", MessageBoxButtons.OK);
            }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            Xrpt_REPORTDonhangkhongcoPhieuNhap rpt = new Xrpt_REPORTDonhangkhongcoPhieuNhap();

            rpt.lblChiNhanh.Text = cmbChiNhanh.Text;
            ReportPrintTool preview = new ReportPrintTool(rpt);
            preview.ShowPreviewDialog();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            try
            {
                Xrpt_REPORTDonhangkhongcoPhieuNhap rpt = new Xrpt_REPORTDonhangkhongcoPhieuNhap();

                rpt.lblChiNhanh.Text = cmbChiNhanh.Text;
                if (File.Exists(@"C:\CSDL-PT\Report\DanhSachDDHkhongcoPhieuNhap.pdf"))
                {
                    DialogResult dr = MessageBox.Show("File DanhSachDDHkhongcoPhieuNhap.pdf đã tồn tại!\nBạn có muốn tạo lại?",
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        rpt.ExportToPdf(@"C:\CSDL-PT\Report\DanhSachDDHkhongcoPhieuNhap.pdf");
                        MessageBox.Show("File DanhSachDDHkhongcoPhieuNhap.pdf đã được lưu thành công",
                "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    rpt.ExportToPdf(@"C:\CSDL-PT\Report\DanhSachDDHkhongcoPhieuNhap.pdf");
                    MessageBox.Show("File DanhSachDDHkhongcoPhieuNhap.pdf đã được lưu thành công",
                "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("Vui lòng đóng file DanhSachDDHkhongcoPhieuNhap.pdf",
                    "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
        }

        
    }
}
