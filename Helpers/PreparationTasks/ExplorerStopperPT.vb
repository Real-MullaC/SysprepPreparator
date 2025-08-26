Imports System.IO

Namespace Helpers.PreparationTasks

    ''' <summary>
    ''' The Explorer Preparation Task (PT)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ExplorerStopperPT
        Inherits PreparationTask

        ''' <summary>
        ''' Stops the Windows Explorer
        ''' </summary>
        ''' <returns>Whether the process succeeded</returns>
        ''' <remarks>This will not launch when in test mode</remarks>
        Public Overrides Function RunPreparationTask() As Boolean
            If IsInTestMode Then Return True
            Return RunProcess(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "system32", "taskkill.exe"),
                              "/f /im explorer.exe", HideWindow:=True) = PROC_SUCCESS
        End Function
    End Class

End Namespace
