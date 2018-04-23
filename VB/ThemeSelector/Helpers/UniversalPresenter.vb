Imports Microsoft.VisualBasic
Imports System.Windows.Controls
Imports DevExpress.Xpf.Core

Namespace ThemeSelector.Helpers
#If SL Then
	Public Class UniversalPresenter
		Inherits DXContentPresenter
	End Class
#Else
	Public Class UniversalPresenter
		Inherits ContentPresenter
	End Class
#End If
End Namespace
