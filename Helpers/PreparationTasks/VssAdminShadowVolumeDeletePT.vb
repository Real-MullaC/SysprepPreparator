Imports System.IO

Namespace Helpers.PreparationTasks

    ''' <summary>
    ''' The Volume Shadow Copy / Restore Points Preparation Task (PT)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class VssAdminShadowVolumeDeletePT
        Inherits PreparationTask

        ''' <summary>
        ''' Launches the VSC/RP process
        ''' </summary>
        ''' <returns>Whether the process succeeded</returns>
        ''' <remarks>This will not run when in test mode</remarks>
        Public Overrides Function RunPreparationTask() As Boolean
            If IsInTestMode Then Return True
            Return RunProcess(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "system32", "vssadmin.exe"),
                              "delete shadows /all /quiet", HideWindow:=True) = PROC_SUCCESS
        End Function

    End Class

End Namespace
