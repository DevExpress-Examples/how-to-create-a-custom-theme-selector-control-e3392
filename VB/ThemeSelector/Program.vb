Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Runtime.InteropServices
Imports DevExpress.Data.Utils
Imports DevExpress.Xpf.Utils.Native
Imports System.Globalization
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Reflection

Namespace ThemeSelector
	Public NotInheritable Class Program
		Private Class App
			Inherits Application
			Public Sub New()
				Dim mainWindow As New MainWindow()
				mainWindow.Show()
			End Sub
		End Class
		Private Sub New()
		End Sub
		<STAThread> _
		Public Shared Sub Main(ByVal args() As String)
			Dim app As New App()
			app.Run()
		End Sub
	End Class
End Namespace
