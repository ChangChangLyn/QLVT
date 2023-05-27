using DevExpress.XtraReports.UI;
using QLVT_DH;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace QLVT
{
    public partial class Xrpt_ReportNhanVien : DevExpress.XtraReports.UI.XtraReport
    {
        public Xrpt_ReportNhanVien()
        {
            InitializeComponent();
            this.sqlDataSource1.Connection.ConnectionString = Program.connstr;
            this.sqlDataSource1.Fill();
        }

    }
}
