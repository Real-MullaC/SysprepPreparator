Imports Microsoft.Win32
Imports System.IO

Namespace Helpers.CompatChecks

    ''' <summary>
    ''' The Pending Servicing Operations Compatibility Checker Provider (CCP)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PendingServicingOperationsCCP
        Inherits CompatibilityCheckerProvider

        Private SystemPendingServicingFile As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "WinSxS", "pending.xml")

        ''' <summary>
        ''' Determines whether there are pending servicing operations (like updates or other tasks that rely on Windows CBS).
        ''' App installations that leave temporary files behind are also detected
        ''' </summary>
        ''' <returns>Whether there are pending operations</returns>
        ''' <remarks>
        ''' An event detailing results.
        ''' </remarks>
        Public Overrides Function PerformCompatibilityCheck() As Classes.CompatibilityCheckerProviderStatus
            ' Check pending.xml first before moving to registry stuff
            If File.Exists(SystemPendingServicingFile) Then
                Status.Compatible = False
                Status.StatusMessage = New Classes.StatusMessage("Pending servicing operations",
                                                                 "The system needs to perform some servicing operations before continuing.",
                                                                 "Please restart your computer for these operations to be done.",
                                                                 Classes.StatusMessage.StatusMessageSeverity.Warning)
                Return Status
            End If

            ' Our marker doesn't appear to be in WinSxS. May it be in the registry?
            ' Let's check this!

            ' 1. Pending File Rename Operations
            Dim NTSMRk As RegistryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Control\Session Manager", False)
            Dim PFROValue As String() = NTSMRk.GetValue("PendingFileRenameOperations", New String() {})
            NTSMRk.Close()
            If PFROValue.Count > 0 Then
                ' A reboot is required to rename files that were in use
                Status.Compatible = True
                Status.StatusMessage = New Classes.StatusMessage("Pending servicing operations",
                                                                 "Some applications need you to restart your computer to clear up temporary files. You can continue, but it is best that you restart.",
                                                                 "Please restart your computer for these operations to be done.",
                                                                 Classes.StatusMessage.StatusMessageSeverity.Info)
                Return Status
            End If

            ' 2. CBS
            Dim CBSRk As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Component Based Servicing", False)
            Dim RPValue As Integer = CBSRk.GetValue("RebootPending", 0)
            CBSRk.Close()
            If RPValue <> 0 Then
                ' Windows Updates are still pending
                Status.Compatible = False
                Status.StatusMessage = New Classes.StatusMessage("Pending servicing operations",
                                                                 "The system needs to perform some servicing operations before continuing.",
                                                                 "Please restart your computer for these operations to be done.",
                                                                 Classes.StatusMessage.StatusMessageSeverity.Warning)
                Return Status
            End If

            ' None of our checks failed. We're good to go here!
            Status.Compatible = True
            Status.StatusMessage = New Classes.StatusMessage("Pending servicing operations",
                                                             "The system does not need to perform any servicing operations.",
                                                             Classes.StatusMessage.StatusMessageSeverity.Info)

            Return Status
        End Function
    End Class

End Namespace
