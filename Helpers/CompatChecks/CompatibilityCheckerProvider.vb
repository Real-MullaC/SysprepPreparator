Imports SysprepPreparator.Classes

Namespace Helpers.CompatChecks

    ''' <summary>
    ''' The base class for Compatibility Checker Providers (CCPs)
    ''' </summary>
    ''' <remarks>
    ''' To integrate a Compatibility Checker Provider into this program, create a class in this namespace
    ''' and inherit this base class. More information can be found in the documentation
    ''' </remarks>
    Public MustInherit Class CompatibilityCheckerProvider

        ''' <summary>
        ''' The status event
        ''' </summary>
        ''' <remarks></remarks>
        Protected Status As New CompatibilityCheckerProviderStatus()

        ''' <summary>
        ''' Performs a compability check
        ''' </summary>
        ''' <returns>A Compatibility Checker Provider event</returns>
        ''' <remarks>This must not be called from this parent class, but from classes that inherit this</remarks>
        Public MustOverride Function PerformCompatibilityCheck() As CompatibilityCheckerProviderStatus

    End Class

End Namespace
