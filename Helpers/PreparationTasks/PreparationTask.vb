Imports SysprepPreparator.Helpers
Imports System.Windows.Forms
Imports System.IO

Namespace Helpers.PreparationTasks

    ''' <summary>
    ''' The base class for Preparation Tasks (PTs)
    ''' </summary>
    ''' <remarks>
    ''' To integrate a Preparation Task into this program, create a class in this namespace
    ''' and inherit this base class. More information can be found in the documentation
    ''' </remarks>
    Public MustInherit Class PreparationTask
        Implements IUserInterfaceInterop, IProcessRunner

        ''' <summary>
        ''' Runs a preparation task
        ''' </summary>
        ''' <returns>Whether the preparation task succeeded</returns>
        ''' <remarks>This must not be called from this parent class, but from classes that inherit this</remarks>
        Public MustOverride Function RunPreparationTask() As Boolean

        ''' <summary>
        ''' Constant for external processes that run successfully
        ''' </summary>
        ''' <remarks></remarks>
        Protected Friend Const PROC_SUCCESS As Integer = 0

        ''' <summary>
        ''' Determines whether the Sysprep Preparation Tool is in test mode
        ''' </summary>
        ''' <remarks>
        ''' This is useful when prototyping Preparation Tasks so you don't run tasks that are typically
        ''' run on reference computers.
        ''' </remarks>
        Protected Friend IsInTestMode As Boolean = Environment.GetCommandLineArgs().Contains("/test")

        ''' <summary>
        ''' Shows a file picker to open a file
        ''' </summary>
        ''' <returns>The path of the chosen file</returns>
        ''' <remarks></remarks>
        Public Function ShowOpenFileDialog() As String Implements IUserInterfaceInterop.ShowOpenFileDialog
            Dim ofd As New OpenFileDialog() With {
                .SupportMultiDottedExtensions = True
            }

            If ofd.ShowDialog() = DialogResult.OK Then
                Return ofd.FileName
            End If
            Return ""
        End Function

        ''' <summary>
        ''' Shows a file picker to save a file
        ''' </summary>
        ''' <returns>The path of the new file</returns>
        ''' <remarks></remarks>
        Public Function ShowSaveFileDialog() As String Implements IUserInterfaceInterop.ShowSaveFileDialog
            Dim sfd As New SaveFileDialog() With {
                .SupportMultiDottedExtensions = True
            }

            If sfd.ShowDialog() = DialogResult.OK Then
                Return sfd.FileName
            End If
            Return ""
        End Function

        ''' <summary>
        ''' Shows a folder picker
        ''' </summary>
        ''' <param name="Description">The description to show in the folder picker</param>
        ''' <returns>The selected path in the folder picker</returns>
        ''' <remarks></remarks>
        Public Function ShowFolderBrowserDialog(Description As String) As String Implements IUserInterfaceInterop.ShowFolderBrowserDialog
            Dim fbd As New FolderBrowserDialog() With {
                .RootFolder = Environment.SpecialFolder.MyComputer,
                .ShowNewFolderButton = True,
                .Description = Description
            }

            If fbd.ShowDialog() = DialogResult.OK Then
                Return fbd.SelectedPath
            End If
            Return ""
        End Function

        ''' <summary>
        ''' Shows a message box
        ''' </summary>
        ''' <param name="Message">The message to display</param>
        ''' <param name="Caption">The title of the message</param>
        ''' <remarks></remarks>
        Public Sub ShowMessage(Message As String, Optional Caption As String = "") Implements IUserInterfaceInterop.ShowMessage
            MessageBox.Show(Message, Caption, MessageBoxButtons.OK)
        End Sub

        ''' <summary>
        ''' Starts an external process
        ''' </summary>
        ''' <param name="FileName">The file to run</param>
        ''' <param name="Arguments">The arguments to pass to the file</param>
        ''' <param name="WorkingDirectory">The directory the program should run on</param>
        ''' <param name="HideWindow">Whether to hide a window, if created by the program</param>
        ''' <returns>The exit code of the process</returns>
        ''' <remarks>
        ''' If a working directory is not specified, this function will use the directory the program specified in FileName is located on
        ''' as the working directory. Please consider changing this to a different directory in your Preparation Task
        ''' if you experience path issues on the external program
        ''' </remarks>
        Public Function RunProcess(FileName As String, Optional Arguments As String = "", Optional WorkingDirectory As String = "", Optional HideWindow As Boolean = False) As Integer Implements IProcessRunner.RunProcess
            Try
                Dim Proc As New Process() With {
                    .StartInfo = New ProcessStartInfo() With {
                        .FileName = FileName,
                        .Arguments = Arguments,
                        .WorkingDirectory = If(WorkingDirectory <> "", WorkingDirectory, Path.GetDirectoryName(FileName)),
                        .CreateNoWindow = HideWindow,
                        .WindowStyle = If(HideWindow, ProcessWindowStyle.Hidden, ProcessWindowStyle.Normal)
                    }
                }

                Proc.Start()
                Proc.WaitForExit()
                Return Proc.ExitCode
            Catch ex As Exception
                Return ex.HResult
            End Try
        End Function


    End Class

End Namespace