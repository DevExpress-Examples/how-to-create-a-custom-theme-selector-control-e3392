Imports System.Windows.Threading
Imports System.Threading
Imports System
Imports System.Windows

Public NotInheritable Class BackgroundHelper
    Private Shared fDispatcher As Dispatcher

    Private Sub New()
    End Sub
    Public Shared Sub DoInBackground(ByVal backgroundAction As Action, ByVal mainThreadAction As Action)
        DoInBackground(backgroundAction, mainThreadAction, 30)
    End Sub
    Public Shared Sub DoInBackground(ByVal backgroundAction As Action, ByVal mainThreadAction As Action, ByVal milliseconds As Integer)
        Dim thread As New Thread(CType(Function() AnonymousMethod1(milliseconds, backgroundAction, mainThreadAction), ThreadStart))
        thread.Start()
    End Sub

    Private Shared Function AnonymousMethod1(ByVal milliseconds As Integer, ByVal backgroundAction As Action, ByVal mainThreadAction As Action) As Boolean
        Thread.Sleep(milliseconds)
        If backgroundAction IsNot Nothing Then
            backgroundAction()
        End If
        If mainThreadAction IsNot Nothing Then
            Dispatcher.BeginInvoke(mainThreadAction)
        End If
        Return True
    End Function
    Public Shared Sub DoInMainThread(ByVal action As Action)
        Dim done As New AutoResetEvent(False)
        Dispatcher.BeginInvoke(CType(Function() AnonymousMethod2(action, done), Action))
        done.WaitOne()
    End Sub

    Private Shared Function AnonymousMethod2(ByVal action As Action, ByVal done As AutoResetEvent) As Boolean
        action()
        done.Set()
        Return True
    End Function
    Public Shared Property Dispatcher() As Dispatcher
        Get
            If fDispatcher Is Nothing Then
                fDispatcher = DefaultDispatcher
            End If
            Return fDispatcher
        End Get
        Set(ByVal value As Dispatcher)
            fDispatcher = value
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
