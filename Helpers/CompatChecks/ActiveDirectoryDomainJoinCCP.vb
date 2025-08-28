Imports System.DirectoryServices.ActiveDirectory

Namespace Helpers.CompatChecks

    Public Class ActiveDirectoryDomainJoinCCP
        Inherits CompatibilityCheckerProvider

        Public Overrides Function PerformCompatibilityCheck() As Classes.CompatibilityCheckerProviderStatus
            DynaLog.LogMessage("Detecting if device is part of Active Directory domain...")
            Try
                ' This, in a non-joined system, should throw an exception
                Dim currentDomain As Domain = Domain.GetComputerDomain()
                If currentDomain IsNot Nothing Then
                    DynaLog.LogMessage("Current domain info is not nothing. This device is well likely part of a domain.")
                    Status.Compatible = True
                    Status.StatusMessage = New Classes.StatusMessage("Active Directory Domain Join",
                                                                     "This device is joined to a domain. While you can continue, Sysprep will remove this device from the domain.",
                                                                     "Either remove the device from the domain now, or let Sysprep do it for you. You can re-add it later if you want.",
                                                                     Classes.StatusMessage.StatusMessageSeverity.Info)
                End If
            Catch ex As Exception
                DynaLog.LogMessage("An error occurred. Message: " & ex.Message)
                DynaLog.LogMessage("This is expected to happen when the device is not part of a domain.")
                Status.Compatible = True
                Status.StatusMessage = New Classes.StatusMessage("Active Directory Domain Join",
                                                                 "This device is either not joined to a domain or domain information is not available.",
                                                                 Classes.StatusMessage.StatusMessageSeverity.Info)
            End Try
            Return Status
        End Function

    End Class

End Namespace
