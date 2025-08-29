Namespace My
    ' Los siguientes eventos están disponibles para MyApplication:
    ' 
    ' Inicio: se desencadena cuando se inicia la aplicación, antes de que se cree el formulario de inicio.
    ' Apagado: generado después de cerrar todos los formularios de la aplicación. Este evento no se genera si la aplicación termina de forma anómala.
    ' UnhandledException: generado si la aplicación detecta una excepción no controlada.
    ' StartupNextInstance: se desencadena cuando se inicia una aplicación de instancia única y la aplicación ya está activa. 
    ' NetworkAvailabilityChanged: se desencadena cuando la conexión de red está conectada o desconectada.
    Partial Friend Class MyApplication

        Private Sub CatchEmAll(sender As Object, e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            DynaLog.LogMessage("Unhandled exception occurred. Gotta catch'em all !")
            DynaLog.LogMessage("Exception information:")
            DynaLog.LogMessage("- Message: " & e.Exception.Message)
            DynaLog.LogMessage("- Code (HRESULT): " & Hex(e.Exception.HResult))

            ErrorDialog.Label1.Text = ErrorDialog.Label1.Text.Replace("ERROR DETAILS HERE", String.Format("{0} - {1}", e.Exception.GetType(), e.Exception.Message))

            ErrorDialog.ShowDialog()
            If ErrorDialog.DialogResult = DialogResult.OK Then
                DynaLog.LogMessage("DISMTools will continue execution as user decided to do so.")
                DynaLog.LogMessage("This is not the most recommended thing as it may happen again.")
                e.ExitApplication = False
            ElseIf ErrorDialog.DialogResult = DialogResult.Cancel Then
                DynaLog.LogMessage("DISMTools will stop execution as user decided to do so.")
                e.ExitApplication = True
            End If
        End Sub

    End Class


End Namespace

