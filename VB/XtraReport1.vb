Imports System
Imports System.Drawing
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports.UI

Namespace WindowsApplication1

    Public Partial Class XtraReport1
        Inherits XtraReport

        Public Sub New()
            InitializeComponent()
            AddHandler PrintingSystem.AfterMarginsChange, New MarginsChangeEventHandler(AddressOf PrintingSystem_AfterMarginsChange)
            AddHandler PrintingSystem.PageSettingsChanged, New EventHandler(AddressOf PrintingSystem_PageSettingsChanged)
        End Sub

        Private Sub PrintingSystem_PageSettingsChanged(ByVal sender As Object, ByVal e As EventArgs)
            ChangeReportSettings(sender)
        End Sub

        Private Sub PrintingSystem_AfterMarginsChange(ByVal sender As Object, ByVal e As MarginsChangeEventArgs)
            ChangeReportSettings(sender)
        End Sub

        Private Sub ChangeReportSettings(ByVal sender As Object)
            Dim ps As PrintingSystemBase = TryCast(sender, PrintingSystemBase)
            Dim isLocationChanged As Boolean = False
            Dim newPageWidth As Integer = ps.PageBounds.Width - ps.PageMargins.Left - ps.PageMargins.Right
            Dim currentPageWidth As Integer = PageWidth - Margins.Left - Margins.Right
            Dim shift As Integer = currentPageWidth - newPageWidth
            For Each _band As Band In Bands
                For Each _control As XRControl In _band.Controls
                    isLocationChanged = True
                    _control.Location = New Point(_control.Location.X - shift, _control.Location.Y)
                Next
            Next

            If isLocationChanged = True Then
                Margins.Top = ps.PageMargins.Top
                Margins.Bottom = ps.PageMargins.Bottom
                Margins.Left = ps.PageMargins.Left
                Margins.Right = ps.PageMargins.Right
                PaperKind = ps.PageSettings.PaperKind
                PaperName = ps.PageSettings.PaperName
                Landscape = ps.PageSettings.Landscape
                CreateDocument()
            End If
        End Sub
    End Class
End Namespace
