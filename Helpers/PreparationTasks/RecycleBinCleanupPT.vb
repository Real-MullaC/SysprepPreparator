Imports System.IO

Namespace Helpers.PreparationTasks

    ''' <summary>
    ''' The Recycle Bin Preparation Task (PT)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class RecycleBinCleanupPT
        Inherits PreparationTask

        ''' <summary>
        ''' Attempts to clear the current user's Recycle Bin
        ''' </summary>
        ''' <returns>Whether the process succeeded</returns>
        ''' <remarks>This will not launch when in test mode</remarks>
        Public Overrides Function RunPreparationTask() As Boolean
            ' TODO: Implement a more .NET-native version
            If IsInTestMode Then Return True
            Return RunProcess(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "system32", "WindowsPowerShell", "v1.0", "powershell.exe"),
                              "-Command Clear-RecycleBin -Force")
        End Function
    End Class

End Namespace
