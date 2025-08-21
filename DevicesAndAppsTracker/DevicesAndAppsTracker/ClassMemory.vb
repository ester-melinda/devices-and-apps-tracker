Imports System.Runtime.InteropServices

Public Class ClassMemory
    <DllImport("KERNEL32.DLL", EntryPoint:="SetProcessWorkingSetSize", SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Friend Shared Function SetProcessWorkingSetSize(ByVal pProcess As IntPtr, ByVal dwMinimumWorkingSetSize As Integer, ByVal dwMaximumWorkingSetSize As Integer) As Boolean
    End Function

    <DllImport("KERNEL32.DLL", EntryPoint:="GetCurrentProcess", SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Friend Shared Function GetCurrentProcess() As IntPtr
    End Function

    Public Sub New()
        Dim pHandle As IntPtr = GetCurrentProcess()
        SetProcessWorkingSetSize(pHandle, -1, -1)
    End Sub
End Class
