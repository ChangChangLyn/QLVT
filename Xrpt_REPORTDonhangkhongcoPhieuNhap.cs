using DevExpress.XtraReports.UI;
using QLVT_DH;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace QLVT
{
    public partial class Xrpt_REPORTDonhangkhongcoPhieuNhap : DevExpress.XtraReports.UI.XtraReport
    {
        public Xrpt_REPORTDonhangkhongcoPhieuNhap()
        {
            InitializeComponent();
            this.sqlDataSource1.Connection.ConnectionString = Program.connstr;
            this.sqlDataSource1.Fill();
        }

    }
}
