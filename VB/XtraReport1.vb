Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports.UI

Namespace WindowsApplication1
	Partial Public Class XtraReport1
		Inherits XtraReport
		Public Sub New()
			InitializeComponent()
			AddHandler PrintingSystem.AfterMarginsChange, AddressOf PrintingSystem_AfterMarginsChange
			AddHandler PrintingSystem.PageSettingsChanged, AddressOf PrintingSystem_PageSettingsChanged
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
			Dim newPageWidth As Single = ps.PageBounds.Width - ps.PageMargins.Left - ps.PageMargins.Right
			Dim currentPageWidth As Single = Me.PageWidth - Me.Margins.Left - Me.Margins.Right
			Dim shift As Single = currentPageWidth - newPageWidth
			For Each _band As Band In MyBase.Bands
				For Each _control As XRControl In _band.Controls
					isLocationChanged = True
					_control.LocationF = New PointF((_control.LocationF.X - shift), _control.LocationF.Y)
				Next _control
			Next _band
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