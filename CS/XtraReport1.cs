using System;
using System.Drawing;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;

namespace WindowsApplication1 {
    public partial class XtraReport1 : XtraReport {
        public XtraReport1() {
            InitializeComponent();
            PrintingSystem.AfterMarginsChange += 
                new MarginsChangeEventHandler(PrintingSystem_AfterMarginsChange);
            PrintingSystem.PageSettingsChanged += 
                new EventHandler(PrintingSystem_PageSettingsChanged);
        }

        void PrintingSystem_PageSettingsChanged(object sender, EventArgs e) {
            ChangeReportSettings(sender);
        }

        void PrintingSystem_AfterMarginsChange(object sender, MarginsChangeEventArgs e) {
            ChangeReportSettings(sender);
        }

        private void ChangeReportSettings(object sender) {
            PrintingSystemBase ps = sender as PrintingSystemBase;
            bool isLocationChanged = false;
            float newPageWidth = 
                ps.PageBounds.Width - ps.PageMargins.Left - ps.PageMargins.Right;
            float currentPageWidth = 
                this.PageWidth - this.Margins.Left - this.Margins.Right;
            float shift = currentPageWidth - newPageWidth;
            foreach (Band _band in base.Bands) {
                foreach (XRControl _control in _band.Controls) {
                    isLocationChanged = true;
                    _control.LocationF = 
                        new PointF((_control.LocationF.X - shift), _control.LocationF.Y);
                }
            }
            if (isLocationChanged == true) {
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