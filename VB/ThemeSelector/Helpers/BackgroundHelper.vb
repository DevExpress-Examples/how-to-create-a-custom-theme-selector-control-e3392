' Developer Express Code Central Example:
' How to use the Theme Selector that is used in DemoCenter
' 
' This sample illustrates how to use our theme selector that is used in our demos
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E3392

Imports System
Imports System.Threading
Imports System.Windows
Imports System.Windows.Threading

Namespace ThemeSelector.Helpers
    Public NotInheritable Class BackgroundHelper

        Private Sub New()
        End Sub

        Private Shared dispatcher_Renamed As Dispatcher

        Public Shared Sub DoInBackground(ByVal backgroundAction As Action, ByVal mainThreadAction As Action)
            DoInBackground(backgroundAction, mainThreadAction, 30)
        End Sub
        Public Shared Sub DoInBackground(ByVal backgroundAction As Action, ByVal mainThreadAction As Action, ByVal milliseconds As Integer)
            Dim thread As New Thread(Sub()
                System.Threading.Thread.Sleep(milliseconds)
                If backgroundAction IsNot Nothing Then
                    backgroundAction()
                End If
                If mainThreadAction IsNot Nothing Then
                    Dispatcher.BeginInvoke(mainThreadAction)
                End If
            End Sub)
            thread.Start()
        End Sub
        Public Shared Sub DoInMainThread(ByVal action As Action)
            Dim done As New AutoResetEvent(False)
            Dispatcher.BeginInvoke(CType(Sub()
                action()
                done.Set()
            End Sub, Action)
           )
            done.WaitOne()
        End Sub
        Public Shared Property Dispatcher() As Dispatcher
            Get
                If dispatcher_Renamed Is Nothing Then
                    dispatcher_Renamed = DefaultDispatcher
                End If
                Return dispatcher_Renamed
            End Get
            Set(ByVal value As Dispatcher)
                dispatcher_Renamed = value
            End Set
        End Property
        Private Shared ReadOnly Property DefaultDispatcher() As Dispatcher
            Get
#If SL Then
                Return Deployment.Current.Dispatcher
#Else
                Return Application.Current.Dispatcher
#End If
            End Get
        End Property
    End Class
End Namespace
