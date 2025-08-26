Namespace Classes

    ''' <summary>
    ''' This class provides properties for status events reported by Compatibility Checker Providers (CCPs)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CompatibilityCheckerProviderStatus

        ''' <summary>
        ''' Whether the check passed
        ''' </summary>
        Public Property Compatible As Boolean
        ''' <summary>
        ''' The status message associated to the event
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property StatusMessage As StatusMessage

        ''' <summary>
        ''' Initializes a default instance of an event
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            Compatible = False
            StatusMessage = New StatusMessage()
        End Sub

        ''' <summary>
        ''' Initializes an instance of an event with associated compatibility and status message properties
        ''' </summary>
        ''' <param name="cmp">Whether the check succeeded</param>
        ''' <param name="stMsg">The status message</param>
        ''' <remarks></remarks>
        Public Sub New(cmp As Boolean, stMsg As StatusMessage)
            Compatible = cmp
            StatusMessage = stMsg
        End Sub

        Public Overrides Function ToString() As String
            Return "Compatibility Checker Provider finished. Compatible? " & Compatible & " ; Status: " & StatusMessage.ToString()
        End Function

    End Class

End Namespace
