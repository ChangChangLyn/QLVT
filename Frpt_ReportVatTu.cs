using DevExpress.XtraReports.UI;
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
    public partial class Frpt_ReportVatTu : Form
    {
        public Frpt_ReportVatTu()
        {
            InitializeComponent();
        }

        private void Frpt_ReportVatTu_Load(object sender, EventArgs e)
        {

        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            Xrpt_ReportVatTu rpt = new Xrpt_ReportVatTu();
            
            ReportPrintTool preview = new ReportPrintTool(rpt);
            preview.ShowPreviewDialog();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            try
            {
                Xrpt_ReportVatTu rpt = new Xrpt_ReportVatTu();
              
                if (File.Exists(@"C:\CSDL-PT\Report\DanhSachVatTu.pdf"))
                {
                    DialogResult dr = MessageBox.Show("File DanhSachVatTu.pdf đã tồn tại!\nBạn có muốn tạo lại?",
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        rpt.ExportToPdf(@"C:\CSDL-PT\Report\DanhSachVatTu.pdf");
                        MessageBox.Show("File DanhSachVatTu.pdf đã được lưu thành công",
                "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    rpt.ExportToPdf(@"C:\CSDL-PT\Report\DanhSachVatTu.pdf");
                    MessageBox.Show("File DanhSachVatTu.pdf đã được lưu thành công",
                "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("Vui lòng đóng file DanhSachVatTu.pdf",
                    "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
        }

        
    }
}
