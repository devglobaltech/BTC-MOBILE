Imports System.Threading

Public Class cPlaySound

    Dim miThread As Thread

    'Private Ok As cSnd 'New cSnd(AppPath(True) & "fileOk.wav")
    Private NOK As cSnd 'New cSnd(AppPath(True) & "fileNOK.wav")

    'Public Sub PlayOK()
    '    Try
    '        miThread = New Thread(New ThreadStart(AddressOf Ok.Play))
    '        miThread.Start()
    '        miThread = Nothing
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Public Sub PlayNOK()
        Try
            miThread = New Thread(New ThreadStart(AddressOf NOK.Play))
            miThread.Start()
            miThread = Nothing
        Catch ex As Exception
        End Try
    End Sub

    Public Function AppPath(Optional ByVal backSlash As Boolean = False) As String
        Dim path As String = ""
        path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)
        path = Replace(path, "file:\", "")
        If backSlash Then
            path &= "\"
        End If
        Return path
    End Function

    Public Sub New()
        Dim vOK As String = "", vNok As String = ""
        vOK = AppPath(True) & "fileOk.wav"
        vNok = AppPath(True) & "fileNOK.wav"

        NOK = New cSnd(vNok)
        'Ok = New cSnd(vOK)
    End Sub

    Protected Overrides Sub Finalize()
        'Ok = Nothing
        NOK = Nothing
        MyBase.Finalize()
    End Sub
End Class
