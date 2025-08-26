Imports SysprepPreparator.Classes
Imports SysprepPreparator.Helpers.CompatChecks

Namespace Helpers

    ''' <summary>
    ''' The Compatibility Checker Provider Helper module: it will launch all the registered CCPs
    ''' </summary>
    ''' <remarks></remarks>
    Public Module CompatibilityCheckerHelper

        ''' <summary>
        ''' The list of registered CCPs
        ''' </summary>
        ''' <remarks>To develop and register a CCP, read the documentation</remarks>
        Private CompatibilityCheckerModules As New List(Of CompatibilityCheckerProvider) From {
            New SetupStateCCP(),
            New PendingServicingOperationsCCP(),
            New DismThirdPartyDriverCCP()
        }

        ''' <summary>
        ''' Performs the checks of all the registered Compatibility Checker Providers (CCPs)
        ''' </summary>
        ''' <returns>All the status events logged</returns>
        ''' <remarks></remarks>
        Public Function RunChecks() As List(Of CompatibilityCheckerProviderStatus)
            Dim StatusList As New List(Of CompatibilityCheckerProviderStatus)

            For Each CompatibilityCheckerModule In CompatibilityCheckerModules
                Dim result As CompatibilityCheckerProviderStatus = CompatibilityCheckerModule.PerformCompatibilityCheck()
                Debug.WriteLine(String.Format("[{0}] {1}", CompatibilityCheckerModule.GetType().Name, result.ToString()))
                StatusList.Add(result)
            Next

            Return StatusList
        End Function

    End Module

End Namespace
