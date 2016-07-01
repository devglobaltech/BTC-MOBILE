Imports System.Data
Imports System.IO
Imports Microsoft.VisualBasic


Module mGeneral

    Public Const Titulo As String = "BTC - Mobile"
    Public oBase As New cDatabase
    Public SNOK As New cPlaySound

    Public Sub Main()
        Dim frm As New frmIngresoBultos, Path = AppPath(True)
        Try
            Dim strError As String = "", StrConn As String = ""

            If ReadDat(StrConn) Then

                oBase.ConnectionString = StrConn '"Server=7.141.6.99;Database=btc;Uid=fohet;Pwd=homero;" 'Servidor de BD S.C.

                If Not oBase.IniciarConexion(strError) Then
                    Throw New Exception(strError)
                Else
                    Threading.Thread.Sleep(1000)
                    frm.ShowDialog()
                    Application.Exit()
                End If

            Else

                Application.Exit()

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, Titulo)
            Application.Exit()
        Finally
            frm = Nothing
        End Try
    End Sub

    Public Function ReadDat(ByRef StringConnection As String) As Boolean
        '---------------------------------------------------
        'Falta 1. Levantar la cadena del connection string.
        '---------------------------------------------------
        Dim splitChar As String = ";", Line As String = ""
        Dim MyPos As Integer = 0, MyServer As String = "",MyName As String = ""


        Dim sfile As String = AppPath(True) & "btcConfig.dat"
        Dim Fs As StreamReader
        Try
            Fs = File.OpenText(sfile)
        Catch ex As Exception
            MsgBox("El archivo de configuracion no existe. La aplicacion se cerrara: " & Chr(13) & sfile, MsgBoxStyle.Information, "Depot Mobile")
            Application.Exit()
        End Try
        Try
            Do
                Line = Fs.ReadLine
                If Not Line Is Nothing Then
                    Dim Array() As String = Split(Line, ";")

                    StringConnection = "Server=" & Array(0) & ";Database=" & Array(1) & ";Uid=" & Array(2) & ";Pwd=" & Array(3) & ";"

                    Return True
                End If
            Loop Until Trim(Line) = ""
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Titulo)
        End Try
    End Function

    Public Sub AutoSizeGrid(ByRef Grid As DataGrid, ByVal frmName As String)
        Dim Style As New DataGridTableStyle
        Dim Ds As New DataSet, ScreenSize As Integer = 480
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim k As Integer = 0
        Dim Obj As DataTable, valueStr As String = ""
        Dim LongAct As Integer = 0, Obj2 As DataTable
        Dim LongMax As Integer = 0, MaxData As Integer = 0
        Dim CstMult As Integer = 8 'Multiplico el Length por este valor para q quede todo bien.
        Try
            ScreenSize = Screen.PrimaryScreen.Bounds.Width
            If ScreenSize = 480 Then
                CstMult = 16
            End If
            Obj = Grid.DataSource
            '==============================================
            'Seteo el mismo style del tablename
            '==============================================
            Style.MappingName = Obj.TableName
            i = 0
            For i = 0 To Obj.Columns.Count - 1
                LongAct = 0
                LongMax = 0
                For j = 0 To Obj.Rows.Count - 1
                    LongAct = Len(Trim(Obj.Rows(j)(i).ToString))
                    If LongAct > LongMax Then LongMax = LongAct
                Next
                '==============================================
                'desde aca tengo el maximo para una columna i.
                '==============================================
                Dim TextCol As New DataGridTextBoxColumn
                With TextCol
                    .MappingName = Obj.Columns(i).ColumnName
                    .HeaderText = Replace(Obj.Columns(i).ColumnName, "_", " ") 'de loco que soy le saco el guion bajo.
                    '============================================================================================
                    'Esto esta porque si el dato es mas chico que el nombre de la columna me corta la columna
                    '============================================================================================
                    If Len(Obj.Columns(i).ColumnName) > LongMax Then LongMax = Len(Obj.Columns(i).ColumnName)
                    '============================================================================================
                    .Width = LongMax * CstMult
                End With
                Style.GridColumnStyles.Add(TextCol)
                TextCol = Nothing
                '==============================================
            Next
            '==============================================
            'para terminar le pongo el style a la grilla.
            '==============================================
            Grid.TableStyles.Clear() 'Le saco el estilo para que no reviente.
            Grid.TableStyles.Add(Style)
            '==============================================
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
End Module
