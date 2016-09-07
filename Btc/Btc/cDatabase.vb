Imports System.Data

Imports MySql.Data.MySqlClient

Public Class cDatabase

#Region "Declaracion de Variables, constantes y Estructuras."

    Private Const Msg_DatabaseError As String = "Se perdio la conexion con la base de datos."
    Private Const Msg_ErrorPorReintentos As String = "Se completo la cantidad de reintentos de reconexion sin exito. La aplicacion sera cerrada."
    Private Const Msg_ErrorDeConexion As String = "Se perdio la conexion a la base de datos, la aplicacion intentara reconectarse."
    Private Const Msg_clsName As String = "BTC - Database."
    Private fError As New frmError
    Private Conexion As MySqlConnection
    Private Da As MySqlDataAdapter
    Private Cmd As MySqlCommand
    Private Constr As String = ""
    Private IntentosConexion As Integer = 3

    Enum TipoComando
        StoredProcedure = 0
        TableDirect = 1
        Text = 2
    End Enum

    Enum TipoEjecucion
        ExecuteNonQuery = 0
        ExecuteReader = 1
        ExecuteScalar = 2
    End Enum

#End Region

#Region "Constructor y Destructor"

    Public Sub New()
        'Constructor.
        Conexion = New MySqlConnection
    End Sub

    Protected Overrides Sub Finalize()
        Conexion.Close()
        Conexion = Nothing
        Da = Nothing
        Cmd = Nothing
        frmError = Nothing
        MyBase.Finalize()
    End Sub

#End Region

#Region "Property's"

    Public ReadOnly Property EstadoConexion() As Boolean
        Get
            Select Case Conexion.State
                Case ConnectionState.Open
                    Return True
                Case Else
                    Return False
            End Select
        End Get
    End Property

    Public Property ConnectionString() As String
        Get
            Return Constr
        End Get
        Set(ByVal value As String)
            Constr = value
        End Set
    End Property

#End Region

#Region "Funciones y Metodos"

    Public Function VerificarConexion() As Boolean
        Dim Contador As Integer = 0
        Try

            Select Case Conexion.State
                Case ConnectionState.Broken, ConnectionState.Closed

                    'MsgBox(Msg_ErrorDeConexion, MsgBoxStyle.OkOnly, Titulo)
                    fError.Mensaje = Msg_ErrorDeConexion
                    fError.ShowDialog()

                    Contador = 0
                    While Contador <> Me.IntentosConexion
                        Try
                            Conexion.Close()
                            Conexion.Open()
                            If Conexion.State = ConnectionState.Open Then
                                Return True
                            End If
                        Catch Msc As MySqlException
                        Catch ex As Exception
                        Finally
                            Contador = Contador + 1
                        End Try
                    End While
                    If Contador = Me.IntentosConexion Then
                        'MsgBox(Msg_ErrorPorReintentos, MsgBoxStyle.Critical, Titulo)
                        fError.Mensaje = Msg_ErrorPorReintentos
                        fError.ShowDialog()
                        'Application.Exit()
                        Return False
                    End If
                Case Else
                    Return True
            End Select

        Catch Ms As MySqlException
            MsgBox(Ms.Message, MsgBoxStyle.Critical, Msg_clsName)
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, Msg_clsName)
            Return False
        End Try
    End Function

    Public Function AgregarParametro(ByRef vParam As MySqlParameter, Optional ByRef strError As String = "") As Boolean
        Try
            Cmd.Parameters.Add(vParam)
            Return True
        Catch Ms As MySqlException
            strError = Ms.Number & " - " & Ms.Message
            Return False
        Catch ex As Exception
            strError = ex.Message
            Return False
        End Try
    End Function

    Public Function GetValorParametroOutput(ByVal ParamName As String, ByRef ValorRetorno As VariantType, _
                                            Optional ByRef strError As String = "") As Boolean
        Try
            ValorRetorno = Cmd.Parameters(ParamName).Value
            Return True
        Catch Ms As MySqlException
            strError = Ms.Number & " - " & Ms.Message
            Return False
        Catch ex As Exception
            strError = ex.Message
            Return False
        End Try

    End Function

    Public Function LimpiarParametros(Optional ByRef strError As String = "") As Boolean
        Try
            Cmd.Parameters.Clear()
            Return True
        Catch Ms As MySqlException
            strError = Ms.Number & " - " & Ms.Message
        Catch ex As Exception
            strError = ex.Message
        End Try
    End Function

    Public Function IniciarConexion(Optional ByRef strError As String = "") As Boolean
        Try
            Conexion.ConnectionString = Me.ConnectionString
            Conexion.Open()
            If Conexion.State = ConnectionState.Open Then
                Me.Cmd = Me.Conexion.CreateCommand
                Cmd.CommandTimeout = 20
                Da = New MySqlDataAdapter(Cmd)
                Return True
            Else
                Return False
            End If
        Catch Ms As MySqlException
            strError = Ms.Number & " - " & Ms.Message
        Catch ex As Exception
            strError = ex.Message
        End Try
    End Function

    Public Function GetDataset(ByRef DS As DataSet, ByVal ComandoSQL As String, ByVal TComando As TipoComando, Optional ByRef strError As String = "") As Boolean
        Try
            Try
                Conexion.Ping()
            Catch ex As Exception
            End Try
            If VerificarConexion() Then
                Cmd.CommandText = ComandoSQL
                If TComando = TipoComando.StoredProcedure Then Cmd.CommandType = CommandType.StoredProcedure
                If TComando = TipoComando.TableDirect Then Cmd.CommandType = CommandType.TableDirect
                If TComando = TipoComando.Text Then Cmd.CommandType = CommandType.Text
                Da.Fill(DS)
                Return True
            Else strError = Msg_DatabaseError
                Return False
            End If
            Return True
        Catch Ms As MySqlException
            strError = Ms.Number & " - " & Ms.Message
            Return False
        Catch ex As Exception
            strError = ex.Message
            Return False
        End Try
    End Function

    Public Function EjecutarSQL(ByVal ComandoSQL As String, ByVal TComando As TipoComando, Optional ByRef strError As String = "") As Boolean
        Dim mCMD As MySqlCommand
        Try

            Try
                Conexion.Ping()
            Catch ex As Exception
            End Try

            If VerificarConexion() Then

                Cmd.CommandText = ComandoSQL
                If TComando = TipoComando.StoredProcedure Then Cmd.CommandType = CommandType.StoredProcedure
                If TComando = TipoComando.TableDirect Then Cmd.CommandType = CommandType.TableDirect
                If TComando = TipoComando.Text Then Cmd.CommandType = CommandType.Text

                Cmd.ExecuteNonQuery()

                Return True
            Else : strError = Msg_DatabaseError
                Return False
            End If
            Return True
        Catch Ms As MySqlException
            strError = Ms.Number & " - " & Ms.Message
            Return False
        Catch ex As Exception
            strError = ex.Message
            Return False
        Finally
            mCMD = Nothing
        End Try
    End Function

    Public Function EjecutarSQLEscalar(ByRef Resultado As Long, ByVal ComandoSQL As String, ByVal TComando As TipoComando, Optional ByRef strError As String = "") As Boolean
        Try

            Try
                Conexion.Ping()
            Catch ex As Exception
            End Try

            If VerificarConexion() Then
                Cmd.CommandText = ComandoSQL
                If TComando = TipoComando.StoredProcedure Then Cmd.CommandType = CommandType.StoredProcedure
                If TComando = TipoComando.TableDirect Then Cmd.CommandType = CommandType.TableDirect
                If TComando = TipoComando.Text Then Cmd.CommandType = CommandType.Text

                Resultado = CLng(Cmd.ExecuteScalar)
                Return True
            Else : strError = Msg_DatabaseError
                Return False
            End If
            Return True
        Catch Ms As MySqlException
            strError = Ms.Number & " - " & Ms.Message
            Return False
        Catch ex As Exception
            strError = ex.Message
            Return False
        End Try
    End Function
#End Region

End Class
