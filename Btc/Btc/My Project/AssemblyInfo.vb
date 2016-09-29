Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

' La información general sobre un ensamblado se controla mediante el siguiente
' conjunto de atributos. Cambie estos atributos para modificar la información
' asociada con un ensamblado.

' Revisar los valores de los atributos del ensamblado

<Assembly: AssemblyTitle("B.T.C.")> 
<Assembly: AssemblyDescription("Business to Consumer")> 
<Assembly: AssemblyCompany("Globaltech S.A.")> 
<Assembly: AssemblyProduct("B.T.C.")> 
<Assembly: AssemblyCopyright("Copyright ©  2016")>
<Assembly: AssemblyTrademark("Globaltech S.A.")> 

<Assembly: CLSCompliant(True)>

<Assembly: ComVisible(False)>

'El siguiente GUID sirve como identificador de typelib si este proyecto se expone a COM
<Assembly: Guid("55f90a3e-423e-4c7c-9749-6be12172b76a")>

' La información de versión de un ensamblado consta de los cuatro valores siguientes:
'
'      Versión principal
'      Versión secundaria
'      Número de versión de compilación
'      Revisión
'
' Puede especificar todos los valores o aceptar los valores predeterminados de los números de versión de compilación y de revisión
' mediante el asterisco ('*'), como se muestra a continuación:
' <Assembly: AssemblyVersion("1.0.*")>

<Assembly: AssemblyVersion("1.0.3.0")> 

'El atributo siguiente es para suprimir la advertencia sobre FxCop "CA2232 : Microsoft.Usage : Agregue STAThreadAttribute al ensamblado"
' como aplicación para dispositivos no admite el subproceso STA.
<Assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2232:MarkWindowsFormsEntryPointsWithStaThread")>
