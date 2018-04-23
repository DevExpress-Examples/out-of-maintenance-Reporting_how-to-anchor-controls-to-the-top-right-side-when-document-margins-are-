using System;
using System.Drawing;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;

namespace WindowsApplication1 {
    public partial class XtraReport1 : XtraReport {
        public XtraReport1() {
            InitializeComponent();
            PrintingSystem.AfterMarginsChange += new MarginsChangeEventHandler(PrintingSystem_AfterMarginsChange);
            PrintingSystem.PageSettingsChanged += new EventHandler(PrintingSystem_PageSettingsChanged);
        }

        void PrintingSystem_PageSettingsChanged(object sender, EventArgs e) {
            ChangeReportSettings(sender);
        }

        void PrintingSystem_AfterMarginsChange(object sender, MarginsChangeEventArgs e) {
            ChangeReportSettings(sender);
        }

        private void ChangeReportSettings(object sender) {
            PrintingSystem ps = sender as PrintingSystem;
            bool isLocationChanged = false;
            int newPageWidth = ps.PageBounds.Width - ps.PageMargins.Left - ps.PageMargins.Right;
            int currentPageWidth = this.PageWidth - this.Margins.Left - this.Margins.Right;
            int shift = currentPageWidth - newPageWidth;
            foreach(Band _band in base.Bands) {
                foreach(XRControl _control in _band.Controls) {
                    isLocationChanged = true;
                    _control.Location = new Point((_control.Location.X - shift), _control.Location.Y);
                }
            }
            if(isLocationChanged == true) {
                Margins.Top = ps.PageMargins.Top;
                Margins.Bottom = ps.PageMargins.Bottom;
                Margins.Left = ps.PageMargins.Left;
                Margins.Right = ps.PageMargins.Right;
                PaperKind = ps.PageSettings.PaperKind;
                PaperName = ps.PageSettings.PaperName;
                Landscape = ps.PageSettings.Landscape;
                CreateDocument();
            }
        }
    }
}