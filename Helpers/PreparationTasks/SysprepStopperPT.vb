Imports System.IO

Namespace Helpers.PreparationTasks

    Public Class SysprepStopperPT
        Inherits PreparationTask

        Public Overrides Function RunPreparationTask() As Boolean
            ' We have to stop running copies of sysprep first before we can then run sysprep ourselves.
            ' Failing to do this will return a message stating that sysprep is already running.
            DynaLog.LogMessage("Stopping existing sysprep processes...")
            Return RunProcess(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "system32", "taskkill.exe"),
                              "/F /IM sysprep.exe /T", HideWindow:=True, Inconditional:=True) = PROC_SUCCESS
        End Function
    End Class

End Namespace
