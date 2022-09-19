using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace lightP03
{
    public partial class FormPrint : Form
    {
        public FormPrint()
        {
            InitializeComponent();
        }
        public void setReportSource(ReportClass report)
        {
            crystalReportViewer1.ReportSource = report;
        }
        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
